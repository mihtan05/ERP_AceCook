using System;
using System.Data;
using Npgsql;
using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;

namespace KeToanTaiChinh.Services
{
    internal sealed class ChiService
    {
        public DataTable DanhSach(string tuKhoa, string trangThai)
        {
            return Db.Query(@"
SELECT p.maPhieuChi AS MaPhieu, p.ngayLap AS NgayLap,
       k.TenDoanhNghiep AS DoiTac, p.soTien AS SoTien,
       p.hinhThucThanhToan AS HinhThuc, n.TenNV AS NguoiLap,
       p.trangThai AS TrangThai,
       MAX(CASE WHEN c.dienGiai ILIKE '[NỢ]%' THEN c.maTaiKhoan END) AS TaiKhoanNo,
       MAX(CASE WHEN c.dienGiai ILIKE '[CÓ]%' THEN c.maTaiKhoan END) AS TaiKhoanCo,
       MAX(c.maCongNo) AS MaCongNo
FROM PhieuChi p
INNER JOIN KhachHang k ON k.ID_KH = p.ID_KH
INNER JOIN NhanVien n ON n.ID_NV = p.ID_NV
LEFT JOIN ChiTietPhieuChi c ON c.maPhieuChi = p.maPhieuChi
WHERE (@TuKhoa = '' OR p.maPhieuChi ILIKE '%' || @TuKhoa || '%'
       OR k.TenDoanhNghiep ILIKE '%' || @TuKhoa || '%')
  AND (@TrangThai = '' OR p.trangThai = @TrangThai)
GROUP BY p.maPhieuChi, p.ngayLap, k.TenDoanhNghiep, p.soTien,
         p.hinhThucThanhToan, n.TenNV, p.trangThai
ORDER BY p.ngayLap DESC;",
                Db.P("@TuKhoa", tuKhoa ?? string.Empty), Db.P("@TrangThai", trangThai ?? string.Empty));
        }

        public ChungTuInput Lay(string maPhieu)
        {
            DataTable table = Db.Query(@"
SELECT p.maPhieuChi, p.ngayLap, p.ID_KH, p.soTien, p.hinhThucThanhToan, p.trangThai,
       MAX(c.maCongNo) AS maCongNo,
       MAX(CASE WHEN c.dienGiai ILIKE '[NỢ]%' THEN c.maTaiKhoan END) AS taiKhoanNo,
       MAX(CASE WHEN c.dienGiai ILIKE '[CÓ]%' THEN c.maTaiKhoan END) AS taiKhoanCo,
       MAX(CASE WHEN c.dienGiai ILIKE '[NỢ]%' THEN SUBSTRING(c.dienGiai, 6, 255) END) AS dienGiai
FROM PhieuChi p LEFT JOIN ChiTietPhieuChi c ON c.maPhieuChi=p.maPhieuChi
WHERE p.maPhieuChi=@Ma
GROUP BY p.maPhieuChi, p.ngayLap, p.ID_KH, p.soTien, p.hinhThucThanhToan, p.trangThai;",
                Db.P("@Ma", maPhieu));
            if (table.Rows.Count == 0) throw new InvalidOperationException("Không tìm thấy phiếu chi.");
            DataRow r = table.Rows[0];
            return new ChungTuInput
            {
                MaPhieu = Convert.ToString(r["maPhieuChi"]),
                NgayLap = Convert.ToDateTime(r["ngayLap"]),
                MaDoiTac = Convert.ToString(r["ID_KH"]),
                SoTien = Convert.ToDecimal(r["soTien"]),
                HinhThuc = Convert.ToString(r["hinhThucThanhToan"]),
                TrangThai = Convert.ToString(r["trangThai"]),
                MaCongNo = r["maCongNo"] == DBNull.Value ? null : Convert.ToString(r["maCongNo"]),
                TaiKhoanNo = Convert.ToString(r["taiKhoanNo"]),
                TaiKhoanCo = Convert.ToString(r["taiKhoanCo"]),
                DienGiai = Convert.ToString(r["dienGiai"])
            };
        }

        public void Luu(ChungTuInput input)
        {
            Validate(input);
            bool isNew = string.IsNullOrWhiteSpace(input.MaPhieu);
            if (isNew) input.MaPhieu = IdGenerator.NewId("PC");

            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(input.MaCongNo))
                    {
                        decimal remaining = ScalarDecimal(connection, transaction,
                            @"SELECT soDuConLai FROM KhoanCongNo 
WHERE maCongNo=@Ma AND ID_KH=@DoiTac AND loaiCongNo='Phải trả' FOR UPDATE;",
                            Db.P("@Ma", input.MaCongNo), Db.P("@DoiTac", input.MaDoiTac));
                        if (input.SoTien > remaining) throw new InvalidOperationException("Số tiền chi vượt số dư công nợ còn lại.");
                    }

                    if (isNew)
                    {
                        Execute(connection, transaction, @"
INSERT INTO PhieuChi(maPhieuChi, ngayLap, ID_KH, soTien, hinhThucThanhToan, ID_NV, trangThai)
VALUES(@Ma, @Ngay, @DoiTac, @SoTien, @HinhThuc, @NguoiLap, @TrangThai);",
                            Db.P("@Ma", input.MaPhieu), Db.P("@Ngay", input.NgayLap),
                            Db.P("@DoiTac", input.MaDoiTac), Db.P("@SoTien", input.SoTien),
                            Db.P("@HinhThuc", input.HinhThuc), Db.P("@NguoiLap", Session.MaNhanVien),
                            Db.P("@TrangThai", input.TrangThai));
                    }
                    else
                    {
                        object oldStatus = Scalar(connection, transaction,
                            "SELECT trangThai FROM PhieuChi  WHERE maPhieuChi=@Ma FOR UPDATE",
                            Db.P("@Ma", input.MaPhieu));
                        string status = Convert.ToString(oldStatus);
                        if (status != "Nháp" && status != "Chờ duyệt")
                            throw new InvalidOperationException("Không thể cập nhật phiếu chi đã phê duyệt.");

                        Execute(connection, transaction, @"
UPDATE PhieuChi SET ngayLap=@Ngay, ID_KH=@DoiTac, soTien=@SoTien,
    hinhThucThanhToan=@HinhThuc, trangThai=@TrangThai
WHERE maPhieuChi=@Ma; DELETE FROM ChiTietPhieuChi WHERE maPhieuChi=@Ma;",
                            Db.P("@Ngay", input.NgayLap), Db.P("@DoiTac", input.MaDoiTac),
                            Db.P("@SoTien", input.SoTien), Db.P("@HinhThuc", input.HinhThuc),
                            Db.P("@TrangThai", input.TrangThai), Db.P("@Ma", input.MaPhieu));
                    }

                    InsertDetail(connection, transaction, IdGenerator.NewId("CN"), input, input.TaiKhoanNo, "[NỢ] ");
                    InsertDetail(connection, transaction, IdGenerator.NewId("CC"), input, input.TaiKhoanCo, "[CÓ] ");
                    transaction.Commit();
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        public void Duyet(string maPhieu)
        {
            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    DataTable phieu = Query(connection, transaction,
                        "SELECT soTien, trangThai, ID_KH, ID_NV FROM PhieuChi WHERE maPhieuChi=@Ma FOR UPDATE",
                        Db.P("@Ma", maPhieu));
                    if (phieu.Rows.Count == 0) throw new InvalidOperationException("Không tìm thấy phiếu chi.");
                    if (Convert.ToString(phieu.Rows[0]["trangThai"]) != "Chờ duyệt")
                        throw new InvalidOperationException("Chỉ phiếu Chờ duyệt mới được duyệt.");

                    decimal amount = Convert.ToDecimal(phieu.Rows[0]["soTien"]);
                    DataTable details = Query(connection, transaction,
                        "SELECT maTaiKhoan,maCongNo,soTien,dienGiai FROM ChiTietPhieuChi WHERE maPhieuChi=@Ma",
                        Db.P("@Ma", maPhieu));
                    if (details.Rows.Count != 2) throw new InvalidOperationException("Phiếu chi chưa có đủ hai dòng hạch toán Nợ/Có.");

                    string debtId = null;
                    int soDongNo = 0;
                    int soDongCo = 0;
                    foreach (DataRow row in details.Rows)
                    {
                        if (Convert.ToDecimal(row["soTien"]) != amount)
                            throw new InvalidOperationException("Số tiền hạch toán không khớp số tiền phiếu chi.");
                        string explanation = Convert.ToString(row["dienGiai"]);
                        if (explanation.StartsWith("[CÓ]", StringComparison.OrdinalIgnoreCase))
                        {
                            soDongCo++;
                            decimal balance = ScalarDecimal(connection, transaction,
                                "SELECT soDuHienTai FROM TaiKhoanKeToan  WHERE maTaiKhoan=@Ma FOR UPDATE",
                                Db.P("@Ma", Convert.ToString(row["maTaiKhoan"])));
                            if (balance < amount) throw new InvalidOperationException("Số dư quỹ/tài khoản ngân hàng không đủ để duyệt phiếu chi.");
                            CapNhatSoDu(connection, transaction, Convert.ToString(row["maTaiKhoan"]), false, amount);
                        }
                        else if (explanation.StartsWith("[NỢ]", StringComparison.OrdinalIgnoreCase))
                        {
                            soDongNo++;
                            CapNhatSoDu(connection, transaction, Convert.ToString(row["maTaiKhoan"]), true, amount);
                        }
                        if (row["maCongNo"] != DBNull.Value)
                        {
                            string currentDebt = Convert.ToString(row["maCongNo"]);
                            if (debtId != null && debtId != currentDebt)
                                throw new InvalidOperationException("Hai dòng hạch toán đang liên kết với hai khoản công nợ khác nhau.");
                            debtId = currentDebt;
                        }
                    }
                    if (soDongNo != 1 || soDongCo != 1)
                        throw new InvalidOperationException("Phiếu chi phải có đúng một dòng Nợ và một dòng Có.");

                    if (!string.IsNullOrEmpty(debtId))
                    {
                        decimal remaining = ScalarDecimal(connection, transaction,
                            @"SELECT soDuConLai FROM KhoanCongNo 
WHERE maCongNo=@Ma AND ID_KH=@DoiTac AND loaiCongNo='Phải trả' FOR UPDATE;",
                            Db.P("@Ma", debtId), Db.P("@DoiTac", Convert.ToString(phieu.Rows[0]["ID_KH"])));
                        if (amount > remaining) throw new InvalidOperationException("Số tiền chi vượt số dư công nợ còn lại.");
                        Execute(connection, transaction, @"
UPDATE KhoanCongNo SET soTienDaThanhToan=soTienDaThanhToan+@SoTien,
    soDuConLai=soDuConLai-@SoTien,
    trangThai=CASE WHEN soDuConLai-@SoTien=0 THEN 'Đã thanh toán' ELSE 'Thanh toán một phần' END
WHERE maCongNo=@Ma;", Db.P("@SoTien", amount), Db.P("@Ma", debtId));
                    }

                    Execute(connection, transaction, @"
UPDATE PhieuChi SET trangThai='Đã duyệt',nguoiDuyet=@NguoiDuyet,ngayDuyet=CURRENT_TIMESTAMP
WHERE maPhieuChi=@Ma;", Db.P("@NguoiDuyet", Session.MaNhanVien), Db.P("@Ma", maPhieu));
                    transaction.Commit();
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        public void TuChoi(string maPhieu, string lyDo)
        {
            if (string.IsNullOrWhiteSpace(lyDo)) throw new InvalidOperationException("Vui lòng nhập lý do từ chối.");
            int changed = Db.Execute(@"
UPDATE PhieuChi SET trangThai='Từ chối',nguoiDuyet=@NguoiDuyet,ngayDuyet=CURRENT_TIMESTAMP,lyDoTuChoi=@LyDo
WHERE maPhieuChi=@Ma AND trangThai='Chờ duyệt';",
                Db.P("@NguoiDuyet", Session.MaNhanVien), Db.P("@LyDo", lyDo.Trim()), Db.P("@Ma", maPhieu));
            if (changed == 0) throw new InvalidOperationException("Chỉ phiếu Chờ duyệt mới có thể từ chối.");
        }

        private static void Validate(ChungTuInput input)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (string.IsNullOrWhiteSpace(input.MaDoiTac) || string.IsNullOrWhiteSpace(input.HinhThuc) ||
                string.IsNullOrWhiteSpace(input.TaiKhoanNo) || string.IsNullOrWhiteSpace(input.TaiKhoanCo) ||
                string.IsNullOrWhiteSpace(input.DienGiai))
                throw new InvalidOperationException("Vui lòng điền đầy đủ và chính xác thông tin.");
            if (input.SoTien <= 0) throw new InvalidOperationException("Số tiền phải lớn hơn 0.");
            if (input.TaiKhoanNo == input.TaiKhoanCo) throw new InvalidOperationException("Tài khoản Nợ và Có phải khác nhau.");
            if (input.HinhThuc != "Tiền mặt" && input.HinhThuc != "Chuyển khoản")
                throw new InvalidOperationException("Hình thức thanh toán không hợp lệ.");
            string taiKhoanTien = input.HinhThuc == "Tiền mặt" ? "1111" : "1121";
            if (input.TaiKhoanCo != taiKhoanTien)
                throw new InvalidOperationException("Tài khoản Có phải là " + taiKhoanTien + " theo hình thức thanh toán đã chọn.");
            if (input.TrangThai != "Nháp" && input.TrangThai != "Chờ duyệt")
                throw new InvalidOperationException("Trạng thái phiếu không hợp lệ.");
        }

        private static void InsertDetail(NpgsqlConnection c, NpgsqlTransaction t, string id,
            ChungTuInput input, string account, string prefix)
        {
            Execute(c, t, @"
INSERT INTO ChiTietPhieuChi(maCTPC,maPhieuChi,maCongNo,maTaiKhoan,soTien,dienGiai)
VALUES(@ChiTiet,@Phieu,@CongNo,@TaiKhoan,@SoTien,@DienGiai);",
                Db.P("@ChiTiet", id), Db.P("@Phieu", input.MaPhieu),
                Db.P("@CongNo", string.IsNullOrEmpty(input.MaCongNo) ? (object)DBNull.Value : input.MaCongNo),
                Db.P("@TaiKhoan", account), Db.P("@SoTien", input.SoTien), Db.P("@DienGiai", prefix + input.DienGiai));
        }

        private static void CapNhatSoDu(NpgsqlConnection c, NpgsqlTransaction t, string taiKhoan, bool laNo, decimal soTien)
        {
            int changed = Execute(c, t, @"
UPDATE TaiKhoanKeToan SET soDuHienTai=COALESCE(soDuHienTai,0) +
 CASE WHEN loaiTaiKhoan IN ('Tài sản','Chi phí')
      THEN CASE WHEN @LaNo THEN @SoTien ELSE -@SoTien END
      ELSE CASE WHEN @LaNo THEN -@SoTien ELSE @SoTien END END
WHERE maTaiKhoan=@TaiKhoan;", Db.P("@LaNo", laNo), Db.P("@SoTien", soTien), Db.P("@TaiKhoan", taiKhoan));
            if (changed != 1) throw new InvalidOperationException("Tài khoản hạch toán không tồn tại.");
        }

        private static DataTable Query(NpgsqlConnection c, NpgsqlTransaction t, string sql, params NpgsqlParameter[] p)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, c, t))
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            { if (p != null && p.Length > 0) command.Parameters.AddRange(p); command.CommandTimeout = 30; DataTable d = new DataTable(); adapter.Fill(d); return d; }
        }
        private static object Scalar(NpgsqlConnection c, NpgsqlTransaction t, string sql, params NpgsqlParameter[] p)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, c, t))
            { if (p != null && p.Length > 0) command.Parameters.AddRange(p); command.CommandTimeout = 30; return command.ExecuteScalar(); }
        }
        private static decimal ScalarDecimal(NpgsqlConnection c, NpgsqlTransaction t, string sql, params NpgsqlParameter[] p)
        {
            object value = Scalar(c, t, sql, p);
            if (value == null || value == DBNull.Value) throw new InvalidOperationException("Không tìm thấy dữ liệu liên quan.");
            return Convert.ToDecimal(value);
        }
        private static int Execute(NpgsqlConnection c, NpgsqlTransaction t, string sql, params NpgsqlParameter[] p)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, c, t))
            { if (p != null && p.Length > 0) command.Parameters.AddRange(p); command.CommandTimeout = 30; return command.ExecuteNonQuery(); }
        }
    }
}

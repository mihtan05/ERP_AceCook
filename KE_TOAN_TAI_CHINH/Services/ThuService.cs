using System;
using System.Data;
using Npgsql;
using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;

namespace KeToanTaiChinh.Services
{
    internal sealed class ThuService
    {
        public DataTable DanhSach(string tuKhoa, string trangThai)
        {
            return Db.Query(@"
SELECT p.maPhieuThu AS MaPhieu, p.ngayLap AS NgayLap,
       k.TenDoanhNghiep AS DoiTac, p.soTien AS SoTien,
       p.hinhThucThanhToan AS HinhThuc, n.TenNV AS NguoiLap,
       p.trangThai AS TrangThai,
       MAX(CASE WHEN c.dienGiai ILIKE '[NỢ]%' THEN c.maTaiKhoan END) AS TaiKhoanNo,
       MAX(CASE WHEN c.dienGiai ILIKE '[CÓ]%' THEN c.maTaiKhoan END) AS TaiKhoanCo,
       MAX(c.maCongNo) AS MaCongNo
FROM PhieuThu p
INNER JOIN KhachHang k ON k.ID_KH = p.ID_KH
INNER JOIN NhanVien n ON n.ID_NV = p.ID_NV
LEFT JOIN ChiTietPhieuThu c ON c.maPhieuThu = p.maPhieuThu
WHERE (@TuKhoa = '' OR p.maPhieuThu ILIKE '%' || @TuKhoa || '%'
       OR k.TenDoanhNghiep ILIKE '%' || @TuKhoa || '%')
  AND (@TrangThai = '' OR p.trangThai = @TrangThai)
GROUP BY p.maPhieuThu, p.ngayLap, k.TenDoanhNghiep, p.soTien,
         p.hinhThucThanhToan, n.TenNV, p.trangThai
ORDER BY p.ngayLap DESC;",
                Db.P("@TuKhoa", tuKhoa ?? string.Empty), Db.P("@TrangThai", trangThai ?? string.Empty));
        }

        public void Tao(ChungTuInput input)
        {
            Validate(input);
            input.MaPhieu = IdGenerator.NewId("PT");

            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(input.MaCongNo))
                    {
                        decimal conLai = ScalarDecimal(connection, transaction,
                            @"SELECT soDuConLai FROM KhoanCongNo 
WHERE maCongNo=@Ma AND ID_KH=@DoiTac AND loaiCongNo='Phải thu' FOR UPDATE;",
                            Db.P("@Ma", input.MaCongNo), Db.P("@DoiTac", input.MaDoiTac));
                        if (input.SoTien > conLai)
                            throw new InvalidOperationException("Số tiền thu vượt số dư công nợ còn lại.");
                    }

                    Execute(connection, transaction, @"
INSERT INTO PhieuThu(maPhieuThu, ngayLap, ID_KH, soTien, hinhThucThanhToan, ID_NV, trangThai)
VALUES(@Ma, @Ngay, @DoiTac, @SoTien, @HinhThuc, @NguoiLap, @TrangThai);",
                        Db.P("@Ma", input.MaPhieu), Db.P("@Ngay", input.NgayLap),
                        Db.P("@DoiTac", input.MaDoiTac), Db.P("@SoTien", input.SoTien),
                        Db.P("@HinhThuc", input.HinhThuc), Db.P("@NguoiLap", Session.MaNhanVien),
                        Db.P("@TrangThai", input.TrangThai));

                    InsertDetail(connection, transaction, IdGenerator.NewId("TN"), input, input.TaiKhoanNo, "[NỢ] ");
                    InsertDetail(connection, transaction, IdGenerator.NewId("TC"), input, input.TaiKhoanCo, "[CÓ] ");
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Duyet(string maPhieu)
        {
            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    DataTable phieu = Query(connection, transaction, @"
SELECT soTien, trangThai, ID_KH, ID_NV FROM PhieuThu WHERE maPhieuThu=@Ma FOR UPDATE;",
                        Db.P("@Ma", maPhieu));
                    if (phieu.Rows.Count == 0) throw new InvalidOperationException("Không tìm thấy phiếu thu.");
                    if (Convert.ToString(phieu.Rows[0]["trangThai"]) != "Chờ duyệt")
                        throw new InvalidOperationException("Chỉ phiếu ở trạng thái Chờ duyệt mới được duyệt.");

                    DataTable details = Query(connection, transaction, @"
SELECT maTaiKhoan, maCongNo, soTien, dienGiai
FROM ChiTietPhieuThu WHERE maPhieuThu=@Ma;", Db.P("@Ma", maPhieu));
                    if (details.Rows.Count != 2) throw new InvalidOperationException("Phiếu thu chưa có đủ hai dòng hạch toán Nợ/Có.");

                    string debtId = null;
                    decimal amount = Convert.ToDecimal(phieu.Rows[0]["soTien"]);
                    int soDongNo = 0;
                    int soDongCo = 0;
                    foreach (DataRow row in details.Rows)
                    {
                        if (Convert.ToDecimal(row["soTien"]) != amount)
                            throw new InvalidOperationException("Số tiền hạch toán không khớp số tiền phiếu thu.");
                        string explanation = Convert.ToString(row["dienGiai"]);
                        if (explanation.StartsWith("[NỢ]", StringComparison.OrdinalIgnoreCase))
                        {
                            soDongNo++;
                            CapNhatSoDu(connection, transaction, Convert.ToString(row["maTaiKhoan"]), true, amount);
                        }
                        else if (explanation.StartsWith("[CÓ]", StringComparison.OrdinalIgnoreCase))
                        {
                            soDongCo++;
                            CapNhatSoDu(connection, transaction, Convert.ToString(row["maTaiKhoan"]), false, amount);
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
                        throw new InvalidOperationException("Phiếu thu phải có đúng một dòng Nợ và một dòng Có.");

                    if (!string.IsNullOrEmpty(debtId))
                    {
                        decimal remaining = ScalarDecimal(connection, transaction,
                            @"SELECT soDuConLai FROM KhoanCongNo 
WHERE maCongNo=@Ma AND ID_KH=@DoiTac AND loaiCongNo='Phải thu' FOR UPDATE;",
                            Db.P("@Ma", debtId), Db.P("@DoiTac", Convert.ToString(phieu.Rows[0]["ID_KH"])));
                        if (amount > remaining) throw new InvalidOperationException("Số tiền thu vượt số dư công nợ còn lại.");
                        Execute(connection, transaction, @"
UPDATE KhoanCongNo
SET soTienDaThanhToan = soTienDaThanhToan + @SoTien,
    soDuConLai = soDuConLai - @SoTien,
    trangThai = CASE WHEN soDuConLai - @SoTien = 0 THEN 'Đã thanh toán' ELSE 'Thanh toán một phần' END
WHERE maCongNo=@Ma;", Db.P("@SoTien", amount), Db.P("@Ma", debtId));
                    }

                    Execute(connection, transaction, @"
UPDATE PhieuThu SET trangThai='Đã duyệt', nguoiDuyet=@NguoiDuyet, ngayDuyet=CURRENT_TIMESTAMP
WHERE maPhieuThu=@Ma;", Db.P("@NguoiDuyet", Session.MaNhanVien), Db.P("@Ma", maPhieu));
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void TuChoi(string maPhieu, string lyDo)
        {
            if (string.IsNullOrWhiteSpace(lyDo)) throw new InvalidOperationException("Vui lòng nhập lý do từ chối.");
            int changed = Db.Execute(@"
UPDATE PhieuThu SET trangThai='Từ chối', nguoiDuyet=@NguoiDuyet, ngayDuyet=CURRENT_TIMESTAMP, lyDoTuChoi=@LyDo
WHERE maPhieuThu=@Ma AND trangThai='Chờ duyệt';",
                Db.P("@NguoiDuyet", Session.MaNhanVien), Db.P("@LyDo", lyDo.Trim()), Db.P("@Ma", maPhieu));
            if (changed == 0) throw new InvalidOperationException("Chỉ phiếu Chờ duyệt mới có thể từ chối.");
        }

        public void XoaNhap(string maPhieu)
        {
            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    object status = Scalar(connection, transaction,
                        "SELECT trangThai FROM PhieuThu  WHERE maPhieuThu=@Ma FOR UPDATE", Db.P("@Ma", maPhieu));
                    if (Convert.ToString(status) != "Nháp")
                        throw new InvalidOperationException("Chỉ phiếu Nháp mới được xóa.");
                    Execute(connection, transaction, "DELETE FROM ChiTietPhieuThu WHERE maPhieuThu=@Ma", Db.P("@Ma", maPhieu));
                    Execute(connection, transaction, "DELETE FROM PhieuThu WHERE maPhieuThu=@Ma", Db.P("@Ma", maPhieu));
                    transaction.Commit();
                }
                catch { transaction.Rollback(); throw; }
            }
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
            if (input.TaiKhoanNo != taiKhoanTien)
                throw new InvalidOperationException("Tài khoản Nợ phải là " + taiKhoanTien + " theo hình thức thanh toán đã chọn.");
            if (input.TrangThai != "Nháp" && input.TrangThai != "Chờ duyệt")
                throw new InvalidOperationException("Trạng thái tạo phiếu không hợp lệ.");
        }

        private static void InsertDetail(NpgsqlConnection connection, NpgsqlTransaction transaction,
            string id, ChungTuInput input, string account, string prefix)
        {
            Execute(connection, transaction, @"
INSERT INTO ChiTietPhieuThu(maCTPT, maPhieuThu, maCongNo, maTaiKhoan, soTien, dienGiai)
VALUES(@ChiTiet, @Phieu, @CongNo, @TaiKhoan, @SoTien, @DienGiai);",
                Db.P("@ChiTiet", id), Db.P("@Phieu", input.MaPhieu),
                Db.P("@CongNo", string.IsNullOrEmpty(input.MaCongNo) ? (object)DBNull.Value : input.MaCongNo),
                Db.P("@TaiKhoan", account), Db.P("@SoTien", input.SoTien),
                Db.P("@DienGiai", prefix + input.DienGiai));
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
            {
                if (p != null && p.Length > 0) command.Parameters.AddRange(p);
                command.CommandTimeout = 30;
                DataTable table = new DataTable(); adapter.Fill(table); return table;
            }
        }

        private static object Scalar(NpgsqlConnection c, NpgsqlTransaction t, string sql, params NpgsqlParameter[] p)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, c, t))
            {
                if (p != null && p.Length > 0) command.Parameters.AddRange(p);
                command.CommandTimeout = 30;
                return command.ExecuteScalar();
            }
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
            {
                if (p != null && p.Length > 0) command.Parameters.AddRange(p);
                command.CommandTimeout = 30;
                return command.ExecuteNonQuery();
            }
        }
    }
}

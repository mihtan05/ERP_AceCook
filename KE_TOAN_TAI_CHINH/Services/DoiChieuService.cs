using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;

namespace KeToanTaiChinh.Services
{
    internal sealed class DoiChieuService
    {
        public DataTable DanhSach(string tuKhoa, string trangThai)
        {
            return Db.Query(@"
SELECT b.maBienBan AS MaBienBan, b.ngayDoiChieu AS NgayDoiChieu,
       b.kyDoiChieu AS KyDoiChieu, k.TenDoanhNghiep AS DoiTac,
       n.TenNV AS NguoiLap, b.trangThai AS TrangThai,
       COALESCE(SUM(c.soTienSoSach),0) AS SoTienSoSach,
       COALESCE(SUM(c.soTienDoiTac),0) AS SoTienDoiTac,
       COALESCE(SUM(c.chenhLech),0) AS ChenhLech
FROM BienBanDoiChieu b
INNER JOIN KhachHang k ON k.ID_KH=b.ID_KH
INNER JOIN NhanVien n ON n.ID_NV=b.ID_NV
LEFT JOIN ChiTietDoiChieu c ON c.maBienBan=b.maBienBan
WHERE (@TuKhoa='' OR b.maBienBan ILIKE '%' || @TuKhoa || '%'
       OR k.TenDoanhNghiep ILIKE '%' || @TuKhoa || '%')
  AND (@TrangThai='' OR b.trangThai=@TrangThai)
GROUP BY b.maBienBan,b.ngayDoiChieu,b.kyDoiChieu,k.TenDoanhNghiep,n.TenNV,b.trangThai
ORDER BY b.ngayDoiChieu DESC;",
                Db.P("@TuKhoa", tuKhoa ?? string.Empty), Db.P("@TrangThai", trangThai ?? string.Empty));
        }

        public DataTable CongNoDoiTac(string maDoiTac)
        {
            return Db.Query(@"
SELECT maCongNo AS MaCongNo, COALESCE(soChungTu,maCongNo) AS SoChungTu,
       loaiCongNo AS LoaiCongNo, hanThanhToan AS HanThanhToan,
       soDuConLai AS SoTienSoSach, soDuConLai AS SoTienDoiTac,
       CAST(0 AS DECIMAL(18,2)) AS ChenhLech
FROM KhoanCongNo WHERE ID_KH=@DoiTac AND soDuConLai>0
ORDER BY hanThanhToan;", Db.P("@DoiTac", maDoiTac));
        }

        public void Tao(DoiChieuInput input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input.MaDoiTac) ||
                string.IsNullOrWhiteSpace(input.KyDoiChieu))
                throw new InvalidOperationException("Đối tác và kỳ đối chiếu là bắt buộc.");
            if (input.ChiTiet == null || input.ChiTiet.Count == 0)
                throw new InvalidOperationException("Không có khoản công nợ nào để đối chiếu.");
            if (input.TrangThai != "Nháp" && input.TrangThai != "Chờ duyệt")
                throw new InvalidOperationException("Trạng thái biên bản không hợp lệ.");

            input.MaBienBan = IdGenerator.NewId("BB");
            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    Execute(connection, transaction, @"
INSERT INTO BienBanDoiChieu(maBienBan,ID_KH,ngayDoiChieu,kyDoiChieu,ID_NV,trangThai)
VALUES(@Ma,@DoiTac,@Ngay,@Ky,@NguoiLap,@TrangThai);",
                        Db.P("@Ma", input.MaBienBan), Db.P("@DoiTac", input.MaDoiTac),
                        Db.P("@Ngay", input.NgayDoiChieu), Db.P("@Ky", input.KyDoiChieu),
                        Db.P("@NguoiLap", Session.MaNhanVien), Db.P("@TrangThai", input.TrangThai));

                    int index = 0;
                    HashSet<string> congNoDaThem = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (DoiChieuDong row in input.ChiTiet)
                    {
                        if (row == null || string.IsNullOrWhiteSpace(row.MaCongNo))
                            throw new InvalidOperationException("Chi tiết đối chiếu thiếu mã công nợ.");
                        if (!congNoDaThem.Add(row.MaCongNo))
                            throw new InvalidOperationException("Một khoản công nợ không được xuất hiện hai lần trong biên bản.");
                        if (row.SoTienSoSach < 0 || row.SoTienDoiTac < 0)
                            throw new InvalidOperationException("Số tiền đối chiếu không được âm.");

                        object soDu = Scalar(connection, transaction, @"SELECT soDuConLai
FROM KhoanCongNo 
WHERE maCongNo=@MaCongNo AND ID_KH=@DoiTac FOR UPDATE;",
                            Db.P("@MaCongNo", row.MaCongNo), Db.P("@DoiTac", input.MaDoiTac));
                        if (soDu == null || soDu == DBNull.Value)
                            throw new InvalidOperationException("Khoản công nợ " + row.MaCongNo + " không thuộc đối tác đã chọn.");
                        if (Convert.ToDecimal(soDu) != row.SoTienSoSach)
                            throw new InvalidOperationException("Số dư công nợ " + row.MaCongNo + " đã thay đổi. Hãy tải lại dữ liệu.");

                        index++;
                        string detailId = IdGenerator.NewId("DC");
                        Execute(connection, transaction, @"
INSERT INTO ChiTietDoiChieu(maCTDC,maBienBan,maCongNo,soTienSoSach,soTienDoiTac,chenhLech)
VALUES(@ChiTiet,@BienBan,@CongNo,@SoSach,@DoiTac,@ChenhLech);",
                            Db.P("@ChiTiet", detailId), Db.P("@BienBan", input.MaBienBan),
                            Db.P("@CongNo", row.MaCongNo), Db.P("@SoSach", row.SoTienSoSach),
                            Db.P("@DoiTac", row.SoTienDoiTac), Db.P("@ChenhLech", row.SoTienDoiTac - row.SoTienSoSach));
                    }
                    transaction.Commit();
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        public void Duyet(string maBienBan)
        {
            int changed = Db.Execute(@"
UPDATE BienBanDoiChieu SET trangThai='Đã duyệt',nguoiDuyet=@NguoiDuyet,ngayDuyet=CURRENT_TIMESTAMP
WHERE maBienBan=@Ma AND trangThai='Chờ duyệt';",
                Db.P("@NguoiDuyet", Session.MaNhanVien), Db.P("@Ma", maBienBan));
            if (changed == 0) throw new InvalidOperationException("Chỉ biên bản Chờ duyệt mới được duyệt.");
        }

        private static int Execute(NpgsqlConnection c, NpgsqlTransaction t, string sql, params NpgsqlParameter[] p)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, c, t))
            { if (p != null && p.Length > 0) command.Parameters.AddRange(p); command.CommandTimeout = 30; return command.ExecuteNonQuery(); }
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
    }
}

using System;
using System.Data;
using Npgsql;
using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;

namespace KeToanTaiChinh.Services
{
    internal sealed class LuongService
    {
        public DataTable DanhSach(string tuKhoa, string trangThai)
        {
            return Db.Query(@"
SELECT b.maBangLuong AS MaBangLuong,b.tenBangLuong AS TenBangLuong,b.kyLuong AS KyLuong,
       b.phongBan AS PhongBan,b.donViTienTe AS DonViTienTe,b.tongTienLuong AS TongTienLuong,
       b.trangThai AS TrangThai,COUNT(c.maCTL) AS SoNhanVien
FROM BangLuong b LEFT JOIN ChiTietLuongNhanVien c ON c.maBangLuong=b.maBangLuong
WHERE (@TuKhoa='' OR b.maBangLuong ILIKE '%' || @TuKhoa || '%'
       OR b.tenBangLuong ILIKE '%' || @TuKhoa || '%' OR b.kyLuong ILIKE '%' || @TuKhoa || '%')
  AND (@TrangThai='' OR b.trangThai=@TrangThai)
GROUP BY b.maBangLuong,b.tenBangLuong,b.kyLuong,b.phongBan,b.donViTienTe,b.tongTienLuong,b.trangThai
ORDER BY b.kyLuong DESC,b.maBangLuong DESC;",
                Db.P("@TuKhoa", tuKhoa ?? string.Empty), Db.P("@TrangThai", trangThai ?? string.Empty));
        }

        public void Tao(BangLuongInput input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input.TenBangLuong) || string.IsNullOrWhiteSpace(input.KyLuong))
                throw new InvalidOperationException("Tên bảng lương và kỳ lương là bắt buộc.");
            if (!string.IsNullOrWhiteSpace(input.DonViTienTe) && input.DonViTienTe != "VND")
                throw new InvalidOperationException("Bảng lương hiện chỉ hỗ trợ VND; cần có tỷ giá và bút toán quy đổi trước khi dùng ngoại tệ.");
            string department = string.IsNullOrWhiteSpace(input.PhongBan) ? "Toàn công ty" : input.PhongBan;
            int duplicate = Convert.ToInt32(Db.Scalar(@"
SELECT COUNT(*) FROM BangLuong WHERE kyLuong=@Ky AND COALESCE(phongBan,'Toàn công ty')=@PhongBan;",
                Db.P("@Ky", input.KyLuong), Db.P("@PhongBan", department)));
            if (duplicate > 0) throw new InvalidOperationException("Bảng lương kỳ này đã được tạo, vui lòng kiểm tra lại.");

            input.MaBangLuong = IdGenerator.NewId("BL");
            Db.Execute(@"
INSERT INTO BangLuong(maBangLuong,tenBangLuong,kyLuong,tongTienLuong,trangThai,
    phongBan,donViTienTe,ghiChu,nguoiLap)
VALUES(@Ma,@Ten,@Ky,0,'Nháp',@PhongBan,@TienTe,@GhiChu,@NguoiLap);",
                Db.P("@Ma", input.MaBangLuong), Db.P("@Ten", input.TenBangLuong.Trim()),
                Db.P("@Ky", input.KyLuong), Db.P("@PhongBan", department),
                Db.P("@TienTe", string.IsNullOrWhiteSpace(input.DonViTienTe) ? "VND" : input.DonViTienTe),
                Db.P("@GhiChu", input.GhiChu), Db.P("@NguoiLap", Session.MaNhanVien));
        }

        public void TinhLuong(string maBangLuong)
        {
            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    DataTable header = Query(connection, transaction,
                        "SELECT phongBan,trangThai FROM BangLuong  WHERE maBangLuong=@Ma FOR UPDATE",
                        Db.P("@Ma", maBangLuong));
                    if (header.Rows.Count == 0) throw new InvalidOperationException("Không tìm thấy bảng lương.");
                    if (Convert.ToString(header.Rows[0]["trangThai"]) != "Nháp")
                        throw new InvalidOperationException("Chỉ bảng lương Nháp mới được tính lại.");

                    string department = Convert.ToString(header.Rows[0]["phongBan"]);
                    Execute(connection, transaction, "DELETE FROM ChiTietLuongNhanVien WHERE maBangLuong=@Ma", Db.P("@Ma", maBangLuong));
                    int inserted = Execute(connection, transaction, @"
INSERT INTO ChiTietLuongNhanVien(maCTL,maBangLuong,ID_NV,luongCoBan,phuCap,thuong,khauTruBHXH,thueTNCN,thucLinh)
SELECT LEFT('L' || REPLACE(gen_random_uuid()::text,'-',''),20),@Ma,n.ID_NV,
       COALESCE(n.LuongCoBan,0),0,0,ROUND(COALESCE(n.LuongCoBan,0)*0.08,0),0,
       COALESCE(n.LuongCoBan,0)-ROUND(COALESCE(n.LuongCoBan,0)*0.08,0)
FROM NhanVien n
LEFT JOIN PhongBan pb ON pb.maPhongBan = COALESCE(n.maPhongBan, n.phongban)
WHERE COALESCE(n.TrangThai,'Đang làm việc') NOT IN ('Nghỉ việc')
  AND (@PhongBan='Toàn công ty' 
       OR COALESCE(n.PhongBan,'Toàn công ty')=@PhongBan
       OR COALESCE(n.maPhongBan,'')=@PhongBan
       OR COALESCE(pb.tenPhongBan,'')=@PhongBan);",
                        Db.P("@Ma", maBangLuong), Db.P("@PhongBan", department));
                    if (inserted == 0)
                        throw new InvalidOperationException("Không có nhân viên đang làm việc thuộc phòng ban đã chọn.");
                    Execute(connection, transaction, @"
UPDATE BangLuong SET tongTienLuong=COALESCE((SELECT SUM(thucLinh) FROM ChiTietLuongNhanVien WHERE maBangLuong=@Ma),0)
WHERE maBangLuong=@Ma;", Db.P("@Ma", maBangLuong));
                    transaction.Commit();
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        public void DongBoLuongTuNhanSu(string maBangLuong)
        {
            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    DataTable header = Query(connection, transaction,
                        "SELECT trangThai FROM BangLuong WHERE maBangLuong=@Ma FOR UPDATE",
                        Db.P("@Ma", maBangLuong));
                    if (header.Rows.Count == 0) throw new InvalidOperationException("Không tìm thấy bảng lương.");
                    if (Convert.ToString(header.Rows[0]["trangThai"]) != "Nháp")
                        throw new InvalidOperationException("Chỉ bảng lương Nháp mới được đồng bộ lại lương từ nhân sự.");

                    Execute(connection, transaction, @"
UPDATE ChiTietLuongNhanVien AS c
SET luongCoBan = COALESCE(n.LuongCoBan, 0),
    khauTruBHXH = ROUND(COALESCE(n.LuongCoBan, 0) * 0.08, 0),
    thucLinh = CASE WHEN COALESCE(n.LuongCoBan, 0) + c.phuCap + c.thuong - ROUND(COALESCE(n.LuongCoBan, 0) * 0.08, 0) - c.thueTNCN < 0 THEN 0
                    ELSE COALESCE(n.LuongCoBan, 0) + c.phuCap + c.thuong - ROUND(COALESCE(n.LuongCoBan, 0) * 0.08, 0) - c.thueTNCN END
FROM NhanVien AS n
WHERE c.ID_NV = n.ID_NV AND c.maBangLuong = @Ma;",
                        Db.P("@Ma", maBangLuong));

                    Execute(connection, transaction, @"
UPDATE BangLuong SET tongTienLuong=COALESCE((SELECT SUM(thucLinh) FROM ChiTietLuongNhanVien WHERE maBangLuong=@Ma),0)
WHERE maBangLuong=@Ma;", Db.P("@Ma", maBangLuong));
                    transaction.Commit();
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        public DataTable ChiTiet(string maBangLuong)
        {
            return Db.Query(@"
SELECT c.maCTL AS MaChiTiet,n.ID_NV AS MaNhanVien,n.TenNV AS TenNhanVien,
       c.luongCoBan AS LuongCoBan,c.phuCap AS PhuCap,c.thuong AS Thuong,
       c.khauTruBHXH AS KhauTruBHXH,c.thueTNCN AS ThueTNCN,c.thucLinh AS ThucLinh
FROM ChiTietLuongNhanVien c INNER JOIN NhanVien n ON n.ID_NV=c.ID_NV
WHERE c.maBangLuong=@Ma ORDER BY n.TenNV;", Db.P("@Ma", maBangLuong));
        }

        public void CapNhatChiTiet(string maBangLuong, string maChiTiet, decimal phuCap, decimal thuong,
            decimal bhxh, decimal thue)
        {
            if (phuCap < 0 || thuong < 0 || bhxh < 0 || thue < 0)
                throw new InvalidOperationException("Các khoản lương không được âm.");
            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int changed = Execute(connection, transaction, @"
UPDATE ChiTietLuongNhanVien AS c SET phuCap=@PhuCap,thuong=@Thuong,khauTruBHXH=@Bhxh,thueTNCN=@Thue,
    thucLinh=CASE WHEN luongCoBan+@PhuCap+@Thuong-@Bhxh-@Thue<0 THEN 0
                  ELSE luongCoBan+@PhuCap+@Thuong-@Bhxh-@Thue END
FROM BangLuong AS b
WHERE c.maCTL=@ChiTiet AND c.maBangLuong=@BangLuong AND b.maBangLuong=c.maBangLuong AND b.trangThai='Nháp';",
                        Db.P("@PhuCap", phuCap), Db.P("@Thuong", thuong), Db.P("@Bhxh", bhxh), Db.P("@Thue", thue),
                        Db.P("@ChiTiet", maChiTiet), Db.P("@BangLuong", maBangLuong));
                    if (changed == 0)
                        throw new InvalidOperationException("Không tìm thấy chi tiết hoặc bảng lương đã gửi duyệt.");

                    Execute(connection, transaction, @"
UPDATE BangLuong SET tongTienLuong=COALESCE((SELECT SUM(thucLinh) FROM ChiTietLuongNhanVien WHERE maBangLuong=@BangLuong),0)
WHERE maBangLuong=@BangLuong AND trangThai='Nháp';", Db.P("@BangLuong", maBangLuong));
                    transaction.Commit();
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        public void GuiDuyet(string maBangLuong)
        {
            int details = Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM ChiTietLuongNhanVien WHERE maBangLuong=@Ma", Db.P("@Ma", maBangLuong)));
            if (details == 0) throw new InvalidOperationException("Hãy tính lương trước khi gửi duyệt.");
            int changed = Db.Execute("UPDATE BangLuong SET trangThai='Chờ duyệt' WHERE maBangLuong=@Ma AND trangThai='Nháp'",
                Db.P("@Ma", maBangLuong));
            if (changed == 0) throw new InvalidOperationException("Chỉ bảng lương Nháp mới được gửi duyệt.");
        }

        public void Duyet(string maBangLuong)
        {
            using (NpgsqlConnection connection = Db.OpenConnection())
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    DataTable header = Query(connection, transaction, @"
SELECT tongTienLuong,nguoiLap,trangThai FROM BangLuong WHERE maBangLuong=@Ma FOR UPDATE;", Db.P("@Ma", maBangLuong));
                    if (header.Rows.Count == 0 || Convert.ToString(header.Rows[0]["trangThai"]) != "Chờ duyệt")
                        throw new InvalidOperationException("Chỉ bảng lương Chờ duyệt mới được duyệt.");
                    decimal amount = Convert.ToDecimal(header.Rows[0]["tongTienLuong"]);
                    CapNhatSoDu(connection, transaction, "6421", true, amount);
                    CapNhatSoDu(connection, transaction, "3341", false, amount);
                    Execute(connection, transaction, @"
UPDATE BangLuong SET trangThai='Đã duyệt',nguoiDuyet=@NguoiDuyet,ngayDuyet=CURRENT_TIMESTAMP
WHERE maBangLuong=@Ma;", Db.P("@NguoiDuyet", Session.MaNhanVien), Db.P("@Ma", maBangLuong));
                    transaction.Commit();
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        private static void CapNhatSoDu(NpgsqlConnection c, NpgsqlTransaction t, string taiKhoan, bool laNo, decimal soTien)
        {
            int changed = Execute(c, t, @"
UPDATE TaiKhoanKeToan SET soDuHienTai=COALESCE(soDuHienTai,0) +
 CASE WHEN loaiTaiKhoan IN ('Tài sản','Chi phí')
      THEN CASE WHEN @LaNo THEN @SoTien ELSE -@SoTien END
      ELSE CASE WHEN @LaNo THEN -@SoTien ELSE @SoTien END END
WHERE maTaiKhoan=@TaiKhoan;", Db.P("@LaNo", laNo), Db.P("@SoTien", soTien), Db.P("@TaiKhoan", taiKhoan));
            if (changed != 1) throw new InvalidOperationException("Thiếu tài khoản hạch toán lương 6421 hoặc 3341.");
        }

        private static DataTable Query(NpgsqlConnection c, NpgsqlTransaction t, string sql, params NpgsqlParameter[] p)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, c, t))
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            { if (p != null && p.Length > 0) command.Parameters.AddRange(p); command.CommandTimeout = 30; DataTable d = new DataTable(); adapter.Fill(d); return d; }
        }
        private static int Execute(NpgsqlConnection c, NpgsqlTransaction t, string sql, params NpgsqlParameter[] p)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, c, t))
            { if (p != null && p.Length > 0) command.Parameters.AddRange(p); command.CommandTimeout = 30; return command.ExecuteNonQuery(); }
        }
    }
}

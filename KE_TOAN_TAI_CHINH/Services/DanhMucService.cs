using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;

namespace KeToanTaiChinh.Services
{
    internal sealed class DanhMucService
    {
        public List<LookupItem> KhachHang()
        {
            return ToList(Db.Query(@"
SELECT ID_KH AS Id, TenDoanhNghiep AS Name, COALESCE(hanMucTinDung, 0) AS Value
FROM KhachHang ORDER BY TenDoanhNghiep;"));
        }

        public List<LookupItem> TaiKhoan()
        {
            return ToList(Db.Query(@"
SELECT maTaiKhoan AS Id, tenTaiKhoan AS Name, COALESCE(soDuHienTai, 0) AS Value
FROM TaiKhoanKeToan ORDER BY maTaiKhoan;"));
        }

        public List<LookupItem> CongNoCuaDoiTac(string maDoiTac, string loai)
        {
            return ToList(Db.Query(@"
SELECT maCongNo AS Id,
       CONCAT(COALESCE(soChungTu, maCongNo), ' | Còn ', ROUND(soDuConLai, 0)::text, ' đ') AS Name,
       soDuConLai AS Value
FROM KhoanCongNo
WHERE ID_KH = @MaDoiTac AND loaiCongNo = @Loai AND soDuConLai > 0
ORDER BY hanThanhToan;",
                Db.P("@MaDoiTac", maDoiTac), Db.P("@Loai", loai)));
        }

        public List<LookupItem> PhongBan()
        {
            DataTable table = Db.Query(@"
SELECT maPhongBan AS Id, tenPhongBan AS Name, CAST(0 AS DECIMAL(18,2)) AS Value
FROM PhongBan
UNION ALL
SELECT DISTINCT COALESCE(NULLIF(LTRIM(RTRIM(PhongBan)), ''), 'Toàn công ty') AS Id,
       COALESCE(NULLIF(LTRIM(RTRIM(PhongBan)), ''), 'Toàn công ty') AS Name,
       CAST(0 AS DECIMAL(18,2)) AS Value
FROM NhanVien
UNION ALL SELECT 'Toàn công ty', 'Toàn công ty', 0
ORDER BY Name;");
            List<LookupItem> list = ToList(table);
            Dictionary<string, LookupItem> unique = new Dictionary<string, LookupItem>(StringComparer.OrdinalIgnoreCase);
            foreach (LookupItem item in list) if (!unique.ContainsKey(item.Id)) unique.Add(item.Id, item);
            return new List<LookupItem>(unique.Values);
        }

        public DataTable TaiKhoanTable()
        {
            return Db.Query(@"
SELECT maTaiKhoan AS ""Mã tài khoản"", tenTaiKhoan AS ""Tên tài khoản"",
       loaiTaiKhoan AS ""Loại"", soDuHienTai AS ""Số dư hiện tại""
FROM TaiKhoanKeToan ORDER BY maTaiKhoan;");
        }

        public DataTable DoiTacTable()
        {
            return Db.Query(@"
SELECT ID_KH AS ""Mã đối tác"", TenDoanhNghiep AS ""Tên doanh nghiệp"",
       NguoiDaiDien AS ""Người đại diện"", MaSoThue AS ""Mã số thuế"",
       SDT AS ""Điện thoại"", Email, hanMucTinDung AS ""Hạn mức tín dụng""
FROM KhachHang ORDER BY TenDoanhNghiep;");
        }

        public void ThemTaiKhoan(string ma, string ten, string loai, decimal soDu)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(loai))
                throw new InvalidOperationException("Mã, tên và loại tài khoản là bắt buộc.");
            if (soDu < 0) throw new InvalidOperationException("Số dư không được âm.");

            Db.Execute(@"
INSERT INTO TaiKhoanKeToan(maTaiKhoan, tenTaiKhoan, loaiTaiKhoan, soDuHienTai)
VALUES(@Ma, @Ten, @Loai, @SoDu);",
                Db.P("@Ma", ma.Trim()), Db.P("@Ten", ten.Trim()),
                Db.P("@Loai", loai.Trim()), Db.P("@SoDu", soDu));
        }

        private static List<LookupItem> ToList(DataTable table)
        {
            List<LookupItem> list = new List<LookupItem>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new LookupItem
                {
                    Id = Convert.ToString(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                    Value = row["Value"] == DBNull.Value ? 0m : Convert.ToDecimal(row["Value"])
                });
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using HR_Management.DTO;
using System.Data.SqlClient;

namespace HR_Management.DAL
{
    public class HopDongDAL
    {
        public List<HopDongDTO> GetAll()
        {
            List<HopDongDTO> list = new List<HopDongDTO>();
            string query = @"
                SELECT hd.maHopDong, hd.ID_NV, nv.TenNV, hd.loaiHopDong, 
                       hd.ngayKy, hd.ngayBatDau, hd.ngayKetThuc, hd.luongCoBan, hd.trangThai
                FROM HopDong hd
                LEFT JOIN NhanVien nv ON hd.ID_NV = nv.ID_NV
                ORDER BY hd.ngayBatDau DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToHopDong(row));
            }
            return list;
        }

        public List<HopDongDTO> GetExpiringContracts(int withinDays = 30)
        {
            List<HopDongDTO> list = new List<HopDongDTO>();
            string query = @"
                SELECT hd.maHopDong, hd.ID_NV, nv.TenNV, hd.loaiHopDong, 
                       hd.ngayKy, hd.ngayBatDau, hd.ngayKetThuc, hd.luongCoBan, hd.trangThai
                FROM HopDong hd
                LEFT JOIN NhanVien nv ON hd.ID_NV = nv.ID_NV
                WHERE hd.ngayKetThuc IS NOT NULL 
                  AND hd.ngayKetThuc >= CAST(GETDATE() AS DATE)
                  AND hd.ngayKetThuc <= DATEADD(day, @days, CAST(GETDATE() AS DATE))
                  AND hd.trangThai = N'Hiệu lực'
                ORDER BY hd.ngayKetThuc ASC";

            SqlParameter[] param = { new SqlParameter("@days", withinDays) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, param);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToHopDong(row));
            }
            return list;
        }

        public bool Insert(HopDongDTO hd)
        {
            string query = @"
                INSERT INTO HopDong (maHopDong, ID_NV, loaiHopDong, ngayKy, ngayBatDau, ngayKetThuc, luongCoBan, trangThai)
                VALUES (@maHopDong, @ID_NV, @loaiHopDong, @ngayKy, @ngayBatDau, @ngayKetThuc, @luongCoBan, @trangThai)";

            SqlParameter[] param = {
                new SqlParameter("@maHopDong", hd.MaHopDong),
                new SqlParameter("@ID_NV", hd.ID_NV),
                new SqlParameter("@loaiHopDong", hd.LoaiHopDong),
                new SqlParameter("@ngayKy", hd.NgayKy),
                new SqlParameter("@ngayBatDau", hd.NgayBatDau),
                new SqlParameter("@ngayKetThuc", (object?)hd.NgayKetThuc ?? DBNull.Value),
                new SqlParameter("@luongCoBan", hd.LuongCoBan),
                new SqlParameter("@trangThai", hd.TrangThai)
            };

            return DatabaseHelper.ExecuteNonQuery(query, param) > 0;
        }

        public bool Update(HopDongDTO hd)
        {
            string query = @"
                UPDATE HopDong
                SET ID_NV = @ID_NV,
                    loaiHopDong = @loaiHopDong,
                    ngayKy = @ngayKy,
                    ngayBatDau = @ngayBatDau,
                    ngayKetThuc = @ngayKetThuc,
                    luongCoBan = @luongCoBan,
                    trangThai = @trangThai
                WHERE maHopDong = @maHopDong";

            SqlParameter[] param = {
                new SqlParameter("@maHopDong", hd.MaHopDong),
                new SqlParameter("@ID_NV", hd.ID_NV),
                new SqlParameter("@loaiHopDong", hd.LoaiHopDong),
                new SqlParameter("@ngayKy", hd.NgayKy),
                new SqlParameter("@ngayBatDau", hd.NgayBatDau),
                new SqlParameter("@ngayKetThuc", (object?)hd.NgayKetThuc ?? DBNull.Value),
                new SqlParameter("@luongCoBan", hd.LuongCoBan),
                new SqlParameter("@trangThai", hd.TrangThai)
            };

            return DatabaseHelper.ExecuteNonQuery(query, param) > 0;
        }

        public bool Delete(string maHopDong)
        {
            string query = "DELETE FROM HopDong WHERE maHopDong = @maHopDong";
            SqlParameter[] param = { new SqlParameter("@maHopDong", maHopDong) };
            return DatabaseHelper.ExecuteNonQuery(query, param) > 0;
        }

        public string GenerateNextId()
        {
            string query = "SELECT MAX(CAST(SUBSTRING(maHopDong, 3, LEN(maHopDong) - 2) AS INT)) FROM HopDong WHERE maHopDong LIKE 'HD%' AND ISNUMERIC(SUBSTRING(maHopDong, 3, LEN(maHopDong) - 2)) = 1";
            object? result = DatabaseHelper.ExecuteScalar(query);
            int nextNumber = 1;
            if (result != null && result != DBNull.Value)
            {
                nextNumber = Convert.ToInt32(result) + 1;
            }
            return $"HD{nextNumber:D3}";
        }

        public bool CheckExists(string maHopDong)
        {
            string query = "SELECT COUNT(*) FROM HopDong WHERE maHopDong = @maHopDong";
            SqlParameter[] param = { new SqlParameter("@maHopDong", maHopDong) };
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, param));
            return count > 0;
        }

        private HopDongDTO MapRowToHopDong(DataRow row)
        {
            return new HopDongDTO
            {
                MaHopDong = row["maHopDong"]?.ToString() ?? "",
                ID_NV = row["ID_NV"]?.ToString() ?? "",
                TenNV = row.Table.Columns.Contains("TenNV") && row["TenNV"] != DBNull.Value ? row["TenNV"].ToString() : "",
                LoaiHopDong = row["loaiHopDong"]?.ToString() ?? "",
                NgayKy = DatabaseHelper.ToDateTime(row["ngayKy"]),
                NgayBatDau = DatabaseHelper.ToDateTime(row["ngayBatDau"]),
                NgayKetThuc = DatabaseHelper.ToNullableDateTime(row["ngayKetThuc"]),
                LuongCoBan = Convert.ToDecimal(row["luongCoBan"]),
                TrangThai = row["trangThai"]?.ToString() ?? "Hiệu lực"
            };
        }
    }
}

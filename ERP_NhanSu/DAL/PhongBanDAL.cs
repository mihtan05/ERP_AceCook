using System;
using System.Collections.Generic;
using System.Data;
using HR_Management.DTO;
using System.Data.SqlClient;

namespace HR_Management.DAL
{
    public class PhongBanDAL
    {
        public List<PhongBanDTO> GetAll()
        {
            List<PhongBanDTO> list = new List<PhongBanDTO>();
            string query = @"
                SELECT p.maPhongBan, p.tenPhongBan, p.truongPhong, 
                       ISNULL(COUNT(nv.ID_NV), 0) AS soLuongNhanVien, 
                       p.moTa, p.trangThai
                FROM PhongBan p
                LEFT JOIN NhanVien nv ON p.maPhongBan = nv.maPhongBan
                GROUP BY p.maPhongBan, p.tenPhongBan, p.truongPhong, p.moTa, p.trangThai
                ORDER BY p.maPhongBan";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PhongBanDTO
                {
                    MaPhongBan = row["maPhongBan"]?.ToString() ?? "",
                    TenPhongBan = row["tenPhongBan"]?.ToString() ?? "",
                    TruongPhong = row["truongPhong"] != DBNull.Value ? row["truongPhong"].ToString() : "",
                    SoLuongNhanVien = Convert.ToInt32(row["soLuongNhanVien"]),
                    MoTa = row["moTa"] != DBNull.Value ? row["moTa"].ToString() : "",
                    TrangThai = row["trangThai"]?.ToString() ?? "Hoạt động"
                });
            }
            return list;
        }

        public PhongBanDTO? GetById(string maPhongBan)
        {
            string query = "SELECT * FROM PhongBan WHERE maPhongBan = @maPhongBan";
            SqlParameter[] param = { new SqlParameter("@maPhongBan", maPhongBan) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, param);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new PhongBanDTO
                {
                    MaPhongBan = row["maPhongBan"]?.ToString() ?? "",
                    TenPhongBan = row["tenPhongBan"]?.ToString() ?? "",
                    TruongPhong = row["truongPhong"] != DBNull.Value ? row["truongPhong"].ToString() : "",
                    SoLuongNhanVien = Convert.ToInt32(row["soLuongNhanVien"]),
                    MoTa = row["moTa"] != DBNull.Value ? row["moTa"].ToString() : "",
                    TrangThai = row["trangThai"]?.ToString() ?? "Hoạt động"
                };
            }
            return null;
        }

        public bool Insert(PhongBanDTO pb)
        {
            string query = @"
                INSERT INTO PhongBan (maPhongBan, tenPhongBan, truongPhong, soLuongNhanVien, moTa, trangThai)
                VALUES (@maPhongBan, @tenPhongBan, @truongPhong, @soLuongNhanVien, @moTa, @trangThai)";

            SqlParameter[] param = {
                new SqlParameter("@maPhongBan", pb.MaPhongBan),
                new SqlParameter("@tenPhongBan", pb.TenPhongBan),
                new SqlParameter("@truongPhong", (object?)pb.TruongPhong ?? DBNull.Value),
                new SqlParameter("@soLuongNhanVien", pb.SoLuongNhanVien),
                new SqlParameter("@moTa", (object?)pb.MoTa ?? DBNull.Value),
                new SqlParameter("@trangThai", pb.TrangThai)
            };

            return DatabaseHelper.ExecuteNonQuery(query, param) > 0;
        }

        public bool Update(PhongBanDTO pb)
        {
            string query = @"
                UPDATE PhongBan
                SET tenPhongBan = @tenPhongBan,
                    truongPhong = @truongPhong,
                    moTa = @moTa,
                    trangThai = @trangThai
                WHERE maPhongBan = @maPhongBan";

            SqlParameter[] param = {
                new SqlParameter("@maPhongBan", pb.MaPhongBan),
                new SqlParameter("@tenPhongBan", pb.TenPhongBan),
                new SqlParameter("@truongPhong", (object?)pb.TruongPhong ?? DBNull.Value),
                new SqlParameter("@moTa", (object?)pb.MoTa ?? DBNull.Value),
                new SqlParameter("@trangThai", pb.TrangThai)
            };

            return DatabaseHelper.ExecuteNonQuery(query, param) > 0;
        }

        public bool Delete(string maPhongBan)
        {
            string query = "DELETE FROM PhongBan WHERE maPhongBan = @maPhongBan";
            SqlParameter[] param = { new SqlParameter("@maPhongBan", maPhongBan) };
            return DatabaseHelper.ExecuteNonQuery(query, param) > 0;
        }

        public bool CheckExists(string maPhongBan)
        {
            string query = "SELECT COUNT(*) FROM PhongBan WHERE maPhongBan = @maPhongBan";
            SqlParameter[] param = { new SqlParameter("@maPhongBan", maPhongBan) };
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, param));
            return count > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using HR_Management.DTO;
using System.Data.SqlClient;

namespace HR_Management.DAL
{
    public class NhanVienDAL
    {
        public List<NhanVienChiTietDTO> GetAll()
        {
            List<NhanVienChiTietDTO> list = new List<NhanVienChiTietDTO>();
            string query = @"
                SELECT nv.ID_NV, nv.TenNV, nv.ChucVu, nv.ngaySinh, nv.gioiTinh, 
                       nv.soDienThoai, nv.email, nv.diaChi, nv.maPhongBan, 
                       pb.tenPhongBan AS TenPhongBan, nv.LuongCoBan, nv.TrangThai,
                       tt.maThongTin, tt.soCCCD, tt.trinhDo, tt.chuyenNganh, tt.kinhNghiem, tt.thanhTich
                FROM NhanVien nv
                LEFT JOIN PhongBan pb ON nv.maPhongBan = pb.maPhongBan
                LEFT JOIN ThongTinNhanVien tt ON nv.ID_NV = tt.ID_NV
                ORDER BY nv.ID_NV";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToNhanVienChiTiet(row));
            }
            return list;
        }

        public NhanVienChiTietDTO? GetById(string idNv)
        {
            string query = @"
                SELECT nv.ID_NV, nv.TenNV, nv.ChucVu, nv.ngaySinh, nv.gioiTinh, 
                       nv.soDienThoai, nv.email, nv.diaChi, nv.maPhongBan, 
                       pb.tenPhongBan AS TenPhongBan, nv.LuongCoBan, nv.TrangThai,
                       tt.maThongTin, tt.soCCCD, tt.trinhDo, tt.chuyenNganh, tt.kinhNghiem, tt.thanhTich
                FROM NhanVien nv
                LEFT JOIN PhongBan pb ON nv.maPhongBan = pb.maPhongBan
                LEFT JOIN ThongTinNhanVien tt ON nv.ID_NV = tt.ID_NV
                WHERE nv.ID_NV = @idNv";

            SqlParameter[] param = { new SqlParameter("@idNv", idNv) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, param);
            if (dt.Rows.Count > 0)
            {
                return MapRowToNhanVienChiTiet(dt.Rows[0]);
            }
            return null;
        }

        public List<NhanVienChiTietDTO> Search(string keyword, string? maPhongBan, string? trangThai)
        {
            List<NhanVienChiTietDTO> list = new List<NhanVienChiTietDTO>();
            string query = @"
                SELECT nv.ID_NV, nv.TenNV, nv.ChucVu, nv.ngaySinh, nv.gioiTinh, 
                       nv.soDienThoai, nv.email, nv.diaChi, nv.maPhongBan, 
                       pb.tenPhongBan AS TenPhongBan, nv.LuongCoBan, nv.TrangThai,
                       tt.maThongTin, tt.soCCCD, tt.trinhDo, tt.chuyenNganh, tt.kinhNghiem, tt.thanhTich
                FROM NhanVien nv
                LEFT JOIN PhongBan pb ON nv.maPhongBan = pb.maPhongBan
                LEFT JOIN ThongTinNhanVien tt ON nv.ID_NV = tt.ID_NV
                WHERE (1 = 1)";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query += " AND (nv.ID_NV LIKE @keyword OR nv.TenNV LIKE @keyword OR nv.soDienThoai LIKE @keyword OR tt.soCCCD LIKE @keyword)";
                parameters.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }

            if (!string.IsNullOrWhiteSpace(maPhongBan) && maPhongBan != "ALL")
            {
                query += " AND nv.maPhongBan = @maPhongBan";
                parameters.Add(new SqlParameter("@maPhongBan", maPhongBan));
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "ALL")
            {
                if (trangThai == "Đang làm việc")
                {
                    query += " AND (nv.TrangThai = @trangThai OR UPPER(nv.TrangThai) LIKE '%HOAT%' OR nv.TrangThai IS NULL OR (nv.TrangThai NOT LIKE '%nghỉ%' AND nv.TrangThai NOT LIKE '%NGHI%'))";
                    parameters.Add(new SqlParameter("@trangThai", trangThai));
                }
                else if (trangThai == "Đã nghỉ việc")
                {
                    query += " AND (nv.TrangThai = @trangThai OR UPPER(nv.TrangThai) LIKE '%NGHI%')";
                    parameters.Add(new SqlParameter("@trangThai", trangThai));
                }
                else
                {
                    query += " AND nv.TrangThai = @trangThai";
                    parameters.Add(new SqlParameter("@trangThai", trangThai));
                }
            }

            query += " ORDER BY nv.ID_NV";

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapRowToNhanVienChiTiet(row));
            }
            return list;
        }

        // Thêm nhân viên và hồ sơ chi tiết đồng thời bằng Transaction
        public bool InsertWithDetails(NhanVienChiTietDTO dto, out string error)
        {
            error = string.Empty;
            using (DbConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Thêm vào bảng NhanVien
                        string sqlNV = @"
                            INSERT INTO NhanVien (ID_NV, TenNV, ChucVu, ngaySinh, gioiTinh, soDienThoai, email, diaChi, maPhongBan, LuongCoBan, TrangThai)
                            VALUES (@ID_NV, @TenNV, @ChucVu, @ngaySinh, @gioiTinh, @soDienThoai, @email, @diaChi, @maPhongBan, @LuongCoBan, @TrangThai)";

                        using (DbCommand cmdNV = DatabaseHelper.CreateCommand(sqlNV, conn, trans))
                        {
                            DatabaseHelper.AddParameter(cmdNV, "@ID_NV", dto.ID_NV);
                            DatabaseHelper.AddParameter(cmdNV, "@TenNV", dto.TenNV);
                            DatabaseHelper.AddParameter(cmdNV, "@ChucVu", (object?)dto.ChucVu ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@ngaySinh", (object?)dto.NgaySinh ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@gioiTinh", (object?)dto.GioiTinh ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@soDienThoai", (object?)dto.SoDienThoai ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@email", (object?)dto.Email ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@diaChi", (object?)dto.DiaChi ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@maPhongBan", (object?)dto.MaPhongBan ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@LuongCoBan", (object?)dto.LuongCoBan ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@TrangThai", (object?)dto.TrangThai ?? "Đang làm việc");

                            cmdNV.ExecuteNonQuery();
                        }

                        // 2. Thêm vào bảng ThongTinNhanVien nếu có CCCD
                        if (!string.IsNullOrWhiteSpace(dto.SoCCCD))
                        {
                            string maTT = !string.IsNullOrWhiteSpace(dto.MaThongTin) ? dto.MaThongTin : "TT_" + dto.ID_NV;
                            string sqlTT = @"
                                INSERT INTO ThongTinNhanVien (maThongTin, ID_NV, soCCCD, trinhDo, chuyenNganh, kinhNghiem, thanhTich)
                                VALUES (@maThongTin, @ID_NV, @soCCCD, @trinhDo, @chuyenNganh, @kinhNghiem, @thanhTich)";

                            using (DbCommand cmdTT = DatabaseHelper.CreateCommand(sqlTT, conn, trans))
                            {
                                DatabaseHelper.AddParameter(cmdTT, "@maThongTin", maTT);
                                DatabaseHelper.AddParameter(cmdTT, "@ID_NV", dto.ID_NV);
                                DatabaseHelper.AddParameter(cmdTT, "@soCCCD", dto.SoCCCD);
                                DatabaseHelper.AddParameter(cmdTT, "@trinhDo", (object?)dto.TrinhDo ?? DBNull.Value);
                                DatabaseHelper.AddParameter(cmdTT, "@chuyenNganh", (object?)dto.ChuyenNganh ?? DBNull.Value);
                                DatabaseHelper.AddParameter(cmdTT, "@kinhNghiem", (object?)dto.KinhNghiem ?? DBNull.Value);
                                DatabaseHelper.AddParameter(cmdTT, "@thanhTich", (object?)dto.ThanhTich ?? DBNull.Value);

                                cmdTT.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        error = ex.Message;
                        return false;
                    }
                }
            }
        }

        // Cập nhật nhân viên và hồ sơ chi tiết đồng thời bằng Transaction
        public bool UpdateWithDetails(NhanVienChiTietDTO dto, out string error)
        {
            error = string.Empty;
            using (DbConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Cập nhật bảng NhanVien
                        string sqlNV = @"
                            UPDATE NhanVien
                            SET TenNV = @TenNV,
                                ChucVu = @ChucVu,
                                ngaySinh = @ngaySinh,
                                gioiTinh = @gioiTinh,
                                soDienThoai = @soDienThoai,
                                email = @email,
                                diaChi = @diaChi,
                                maPhongBan = @maPhongBan,
                                LuongCoBan = @LuongCoBan,
                                TrangThai = @TrangThai
                            WHERE ID_NV = @ID_NV";

                        using (DbCommand cmdNV = DatabaseHelper.CreateCommand(sqlNV, conn, trans))
                        {
                            DatabaseHelper.AddParameter(cmdNV, "@ID_NV", dto.ID_NV);
                            DatabaseHelper.AddParameter(cmdNV, "@TenNV", dto.TenNV);
                            DatabaseHelper.AddParameter(cmdNV, "@ChucVu", (object?)dto.ChucVu ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@ngaySinh", (object?)dto.NgaySinh ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@gioiTinh", (object?)dto.GioiTinh ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@soDienThoai", (object?)dto.SoDienThoai ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@email", (object?)dto.Email ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@diaChi", (object?)dto.DiaChi ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@maPhongBan", (object?)dto.MaPhongBan ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@LuongCoBan", (object?)dto.LuongCoBan ?? DBNull.Value);
                            DatabaseHelper.AddParameter(cmdNV, "@TrangThai", (object?)dto.TrangThai ?? "Đang làm việc");

                            cmdNV.ExecuteNonQuery();
                        }

                        // 2. Cập nhật hoặc thêm mới vào bảng ThongTinNhanVien
                        if (!string.IsNullOrWhiteSpace(dto.SoCCCD))
                        {
                            string sqlCheckTT = "SELECT COUNT(*) FROM ThongTinNhanVien WHERE ID_NV = @ID_NV";
                            bool exists = false;
                            using (DbCommand cmdCheck = DatabaseHelper.CreateCommand(sqlCheckTT, conn, trans))
                            {
                                DatabaseHelper.AddParameter(cmdCheck, "@ID_NV", dto.ID_NV);
                                exists = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0;
                            }

                            if (exists)
                            {
                                string sqlUpdateTT = @"
                                    UPDATE ThongTinNhanVien
                                    SET soCCCD = @soCCCD,
                                        trinhDo = @trinhDo,
                                        chuyenNganh = @chuyenNganh,
                                        kinhNghiem = @kinhNghiem,
                                        thanhTich = @thanhTich
                                    WHERE ID_NV = @ID_NV";

                                using (DbCommand cmdTT = DatabaseHelper.CreateCommand(sqlUpdateTT, conn, trans))
                                {
                                    DatabaseHelper.AddParameter(cmdTT, "@ID_NV", dto.ID_NV);
                                    DatabaseHelper.AddParameter(cmdTT, "@soCCCD", dto.SoCCCD);
                                    DatabaseHelper.AddParameter(cmdTT, "@trinhDo", (object?)dto.TrinhDo ?? DBNull.Value);
                                    DatabaseHelper.AddParameter(cmdTT, "@chuyenNganh", (object?)dto.ChuyenNganh ?? DBNull.Value);
                                    DatabaseHelper.AddParameter(cmdTT, "@kinhNghiem", (object?)dto.KinhNghiem ?? DBNull.Value);
                                    DatabaseHelper.AddParameter(cmdTT, "@thanhTich", (object?)dto.ThanhTich ?? DBNull.Value);
                                    cmdTT.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string maTT = !string.IsNullOrWhiteSpace(dto.MaThongTin) ? dto.MaThongTin : "TT_" + dto.ID_NV;
                                string sqlInsertTT = @"
                                    INSERT INTO ThongTinNhanVien (maThongTin, ID_NV, soCCCD, trinhDo, chuyenNganh, kinhNghiem, thanhTich)
                                    VALUES (@maThongTin, @ID_NV, @soCCCD, @trinhDo, @chuyenNganh, @kinhNghiem, @thanhTich)";

                                using (DbCommand cmdTT = DatabaseHelper.CreateCommand(sqlInsertTT, conn, trans))
                                {
                                    DatabaseHelper.AddParameter(cmdTT, "@maThongTin", maTT);
                                    DatabaseHelper.AddParameter(cmdTT, "@ID_NV", dto.ID_NV);
                                    DatabaseHelper.AddParameter(cmdTT, "@soCCCD", dto.SoCCCD);
                                    DatabaseHelper.AddParameter(cmdTT, "@trinhDo", (object?)dto.TrinhDo ?? DBNull.Value);
                                    DatabaseHelper.AddParameter(cmdTT, "@chuyenNganh", (object?)dto.ChuyenNganh ?? DBNull.Value);
                                    DatabaseHelper.AddParameter(cmdTT, "@kinhNghiem", (object?)dto.KinhNghiem ?? DBNull.Value);
                                    DatabaseHelper.AddParameter(cmdTT, "@thanhTich", (object?)dto.ThanhTich ?? DBNull.Value);
                                    cmdTT.ExecuteNonQuery();
                                }
                            }
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        error = ex.Message;
                        return false;
                    }
                }
            }
        }

        public bool HasRelatedTransactions(string idNv, out string details)
        {
            details = string.Empty;
            List<string> list = new List<string>();

            try
            {
                // 1. Đơn hàng
                object? rDH = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM donhang WHERE id_nv = @idNv", new SqlParameter[] { new SqlParameter("@idNv", idNv) });
                long cDH = Convert.ToInt64(rDH ?? 0);
                if (cDH > 0) list.Add($"{cDH} đơn hàng");

                // 2. Hóa đơn
                object? rHD = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM hoadon WHERE id_nv = @idNv", new SqlParameter[] { new SqlParameter("@idNv", idNv) });
                long cHD = Convert.ToInt64(rHD ?? 0);
                if (cHD > 0) list.Add($"{cHD} hóa đơn");

                // 3. Chi tiết lương
                object? rLuong = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM chitietluongnhanvien WHERE id_nv = @idNv", new SqlParameter[] { new SqlParameter("@idNv", idNv) });
                long cLuong = Convert.ToInt64(rLuong ?? 0);
                if (cLuong > 0) list.Add($"{cLuong} kỳ bảng lương");

                // 4. Phiếu thu / chi
                object? rThu = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM phieuthu WHERE id_nv = @idNv", new SqlParameter[] { new SqlParameter("@idNv", idNv) });
                long cThu = Convert.ToInt64(rThu ?? 0);
                if (cThu > 0) list.Add($"{cThu} phiếu thu");

                object? rChi = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM phieuchi WHERE id_nv = @idNv", new SqlParameter[] { new SqlParameter("@idNv", idNv) });
                long cChi = Convert.ToInt64(rChi ?? 0);
                if (cChi > 0) list.Add($"{cChi} phiếu chi");

                if (list.Count > 0)
                {
                    details = string.Join(", ", list);
                    return true;
                }
            }
            catch { }

            return false;
        }

        public bool Deactivate(string idNv, out string error)
        {
            error = string.Empty;
            using (DbConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string qNhanVien = "UPDATE NhanVien SET TrangThai = @trangThai WHERE ID_NV = @idNv";
                        using (DbCommand cmd = DatabaseHelper.CreateCommand(qNhanVien, conn, trans))
                        {
                            DatabaseHelper.AddParameter(cmd, "@trangThai", "Đã nghỉ việc");
                            DatabaseHelper.AddParameter(cmd, "@idNv", idNv);
                            cmd.ExecuteNonQuery();
                        }

                        string qHeThong = "UPDATE HeThong SET TrangThai = @trangThai WHERE ID_NV = @idNv";
                        using (DbCommand cmd = DatabaseHelper.CreateCommand(qHeThong, conn, trans))
                        {
                            DatabaseHelper.AddParameter(cmd, "@trangThai", "Bị khóa");
                            DatabaseHelper.AddParameter(cmd, "@idNv", idNv);
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        error = ex.Message;
                        return false;
                    }
                }
            }
        }

        public bool Delete(string idNv, out string error)
        {
            error = string.Empty;
            using (DbConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Xóa các dữ liệu phụ thuộc
                        string[] queries = {
                            "DELETE FROM ThongTinNhanVien WHERE ID_NV = @idNv",
                            "DELETE FROM HopDong WHERE ID_NV = @idNv",
                            "DELETE FROM HeThong WHERE ID_NV = @idNv",
                            "DELETE FROM ThongKeBaoCao WHERE ID_NV = @idNv",
                            "DELETE FROM NhanVien WHERE ID_NV = @idNv"
                        };

                        foreach (string q in queries)
                        {
                            using (DbCommand cmd = DatabaseHelper.CreateCommand(q, conn, trans))
                            {
                                DatabaseHelper.AddParameter(cmd, "@idNv", idNv);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        if (ex.Message.Contains("23503") || ex.Message.ToLower().Contains("foreign key") || ex.Message.ToLower().Contains("fk_"))
                        {
                            error = "Không thể xóa vĩnh viễn nhân viên này vì đã có dữ liệu giao dịch (Đơn hàng, Hóa đơn hoặc Lương thưởng) liên kết trong hệ thống!\nVui lòng chuyển trạng thái nhân viên sang 'Đã nghỉ việc'.";
                        }
                        else
                        {
                            error = ex.Message;
                        }
                        return false;
                    }
                }
            }
        }

        public string GenerateNextId()
        {
            string query = "SELECT MAX(CAST(SUBSTRING(ID_NV, 3, LEN(ID_NV) - 2) AS INT)) FROM NhanVien WHERE ID_NV LIKE 'NV%' AND ISNUMERIC(SUBSTRING(ID_NV, 3, LEN(ID_NV) - 2)) = 1";
            object? result = DatabaseHelper.ExecuteScalar(query);
            int nextNumber = 1;
            if (result != null && result != DBNull.Value)
            {
                nextNumber = Convert.ToInt32(result) + 1;
            }
            return $"NV{nextNumber:D3}";
        }

        public bool CheckIdExists(string idNv)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE ID_NV = @idNv";
            var pars = new DbParameter[] { DatabaseHelper.CreateParameter("@idNv", idNv) };
            object? result = DatabaseHelper.ExecuteScalar(query, pars);
            return Convert.ToInt32(result) > 0;
        }

        public bool CheckPhoneExists(string phone, string? excludeIdNv = null)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE soDienThoai = @phone";
            var pars = new List<DbParameter> { DatabaseHelper.CreateParameter("@phone", phone) };
            if (!string.IsNullOrWhiteSpace(excludeIdNv))
            {
                query += " AND ID_NV <> @excludeIdNv";
                pars.Add(DatabaseHelper.CreateParameter("@excludeIdNv", excludeIdNv));
            }
            object? result = DatabaseHelper.ExecuteScalar(query, pars.ToArray());
            return Convert.ToInt32(result) > 0;
        }

        public bool CheckCccdExists(string cccd, string? excludeIdNv = null)
        {
            string query = "SELECT COUNT(*) FROM ThongTinNhanVien WHERE soCCCD = @cccd";
            var pars = new List<DbParameter> { DatabaseHelper.CreateParameter("@cccd", cccd) };
            if (!string.IsNullOrWhiteSpace(excludeIdNv))
            {
                query += " AND ID_NV <> @excludeIdNv";
                pars.Add(DatabaseHelper.CreateParameter("@excludeIdNv", excludeIdNv));
            }
            object? result = DatabaseHelper.ExecuteScalar(query, pars.ToArray());
            return Convert.ToInt32(result) > 0;
        }

        public bool CheckEmailExists(string email, string? excludeIdNv = null)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE LOWER(email) = LOWER(@email)";
            var pars = new List<DbParameter> { DatabaseHelper.CreateParameter("@email", email) };
            if (!string.IsNullOrWhiteSpace(excludeIdNv))
            {
                query += " AND ID_NV <> @excludeIdNv";
                pars.Add(DatabaseHelper.CreateParameter("@excludeIdNv", excludeIdNv));
            }
            object? result = DatabaseHelper.ExecuteScalar(query, pars.ToArray());
            return Convert.ToInt32(result) > 0;
        }

        private NhanVienChiTietDTO MapRowToNhanVienChiTiet(DataRow row)
        {
            return new NhanVienChiTietDTO
            {
                ID_NV = row["ID_NV"]?.ToString() ?? "",
                TenNV = row["TenNV"]?.ToString() ?? "",
                ChucVu = row["ChucVu"] != DBNull.Value ? row["ChucVu"].ToString() : "",
                NgaySinh = DatabaseHelper.ToNullableDateTime(row["ngaySinh"]),
                GioiTinh = row["gioiTinh"] != DBNull.Value ? row["gioiTinh"].ToString() : "Nam",
                SoDienThoai = row["soDienThoai"] != DBNull.Value ? row["soDienThoai"].ToString() : "",
                Email = row["email"] != DBNull.Value ? row["email"].ToString() : "",
                DiaChi = row["diaChi"] != DBNull.Value ? row["diaChi"].ToString() : "",
                MaPhongBan = row["maPhongBan"] != DBNull.Value ? row["maPhongBan"].ToString() : "",
                PhongBan = row.Table.Columns.Contains("TenPhongBan") && row["TenPhongBan"] != DBNull.Value ? row["TenPhongBan"].ToString() : "",
                LuongCoBan = row["LuongCoBan"] != DBNull.Value ? Convert.ToDecimal(row["LuongCoBan"]) : 0,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString() : "Đang làm việc",

                MaThongTin = row.Table.Columns.Contains("maThongTin") && row["maThongTin"] != DBNull.Value ? row["maThongTin"].ToString() : "",
                SoCCCD = row.Table.Columns.Contains("soCCCD") && row["soCCCD"] != DBNull.Value ? row["soCCCD"].ToString() : "",
                TrinhDo = row.Table.Columns.Contains("trinhDo") && row["trinhDo"] != DBNull.Value ? row["trinhDo"].ToString() : "",
                ChuyenNganh = row.Table.Columns.Contains("chuyenNganh") && row["chuyenNganh"] != DBNull.Value ? row["chuyenNganh"].ToString() : "",
                KinhNghiem = row.Table.Columns.Contains("kinhNghiem") && row["kinhNghiem"] != DBNull.Value ? row["kinhNghiem"].ToString() : "",
                ThanhTich = row.Table.Columns.Contains("thanhTich") && row["thanhTich"] != DBNull.Value ? row["thanhTich"].ToString() : ""
            };
        }
    }
}

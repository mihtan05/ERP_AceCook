using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using ERP.DTO;

namespace ERP.DAL
{
    public static class DatabaseConfig
    {
        public const string ConnectionString =
            @"Host=ep-bitter-heart-b3yu3xlc-pooler.c-4.ap-southeast-1.aws.neon.tech;Database=erp_banhang;Username=neondb_owner;Password=npg_fVzi2bH5uYaj;SSL Mode=Require;";


        public static string GetConnectionString()
        {
            try
            {
                ConnectionStringSettings settingConn = ConfigurationManager.ConnectionStrings["ERP_Connection"];
                if (settingConn != null && !string.IsNullOrEmpty(settingConn.ConnectionString))
                {
                    return settingConn.ConnectionString;
                }

                ConnectionStringSettings settingBH = ConfigurationManager.ConnectionStrings["ERP_BanHang"];
                if (settingBH != null && !string.IsNullOrEmpty(settingBH.ConnectionString))
                {
                    return settingBH.ConnectionString;
                }
            }
            catch
            {
                // Dự phòng nếu không đọc được ConfigurationManager
            }
            return ConnectionString;
        }
    }

    public class DonVanChuyenDAL
    {
        private readonly string connectionString;

        public DonVanChuyenDAL()
        {
            this.connectionString = DatabaseConfig.GetConnectionString();
        }

        public DonVanChuyenDAL(string connectionString)
        {
            this.connectionString = string.IsNullOrEmpty(connectionString)
                ? DatabaseConfig.GetConnectionString()
                : connectionString;
        }

        // 1. Lấy toàn bộ danh sách đơn vận chuyển (kèm Mã Đơn Hàng & Khách Hàng)
        public List<DonVanChuyen> GetAll()
        {
            List<DonVanChuyen> list = new List<DonVanChuyen>();
            const string query = @"SELECT DVC.ID_DonVC, DVC.ID_DH, DVC.BienSoXe, DVC.MaDVC, DVC.ID_SP, DVC.SoLuongGiao, COALESCE(DVC.trong_luong, 0) AS trong_luong, DVC.ThoiGianKhoiHanh, DVC.TrangThaiDon,
                                          COALESCE(HH.TenHang, SP.LoaiSP) AS TenHang,
                                          KH.TenDoanhNghiep AS TenKhachHang,
                                          COALESCE(GH.DiaChiGiaoHang, KH.DiaChi) AS DiaChiGiao,
                                          COALESCE(GH.SDTNguoiNhan, KH.SDT) AS SDTKhachHang,
                                          COALESCE(DVC_DIEM.TenDVC, DVC.MaDVC) AS TenDVC
                                   FROM DonVanChuyen DVC
                                   LEFT JOIN SanPham SP ON DVC.ID_SP = SP.ID_SP
                                   LEFT JOIN HangHoa HH ON SP.MaHang = HH.MaHang
                                   LEFT JOIN DonHang DH ON DVC.ID_DH = DH.ID_DH
                                   LEFT JOIN KhachHang KH ON DH.ID_KH = KH.ID_KH
                                   LEFT JOIN GiaoHang GH ON DH.ID_DH = GH.ID_DH
                                   LEFT JOIN DiemVanChuyen DVC_DIEM ON DVC.MaDVC = DVC_DIEM.MaDVC
                                   ORDER BY DVC.ThoiGianKhoiHanh DESC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DonVanChuyen
                        {
                            ID_DonVC = reader["ID_DonVC"] != DBNull.Value ? reader["ID_DonVC"].ToString() : string.Empty,
                            ID_DH = reader["ID_DH"] != DBNull.Value ? reader["ID_DH"].ToString() : string.Empty,
                            BienSoXe = reader["BienSoXe"] != DBNull.Value ? reader["BienSoXe"].ToString() : string.Empty,
                            MaDVC = reader["MaDVC"] != DBNull.Value ? reader["MaDVC"].ToString() : string.Empty,
                            TenDVC = reader["TenDVC"] != DBNull.Value ? reader["TenDVC"].ToString() : string.Empty,
                            ID_SP = reader["ID_SP"] != DBNull.Value ? reader["ID_SP"].ToString() : string.Empty,
                            TenHang = reader["TenHang"] != DBNull.Value ? reader["TenHang"].ToString() : string.Empty,
                            TenKhachHang = reader["TenKhachHang"] != DBNull.Value ? reader["TenKhachHang"].ToString() : string.Empty,
                            DiaChiGiao = reader["DiaChiGiao"] != DBNull.Value ? reader["DiaChiGiao"].ToString() : string.Empty,
                            SDTKhachHang = reader["SDTKhachHang"] != DBNull.Value ? reader["SDTKhachHang"].ToString() : string.Empty,
                            SoLuongGiao = reader["SoLuongGiao"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongGiao"]) : 0,
                            TrongLuong = reader["trong_luong"] != DBNull.Value ? Convert.ToDecimal(reader["trong_luong"]) : 0,
                            ThoiGianKhoiHanh = reader["ThoiGianKhoiHanh"] != DBNull.Value ? Convert.ToDateTime(reader["ThoiGianKhoiHanh"]) : DateTime.MinValue,
                            TrangThaiDon = reader["TrangThaiDon"] != DBNull.Value ? reader["TrangThaiDon"].ToString() : string.Empty
                        });
                    }
                }
            }
            return list;
        }

        public DonVanChuyen GetByID(string id)
        {
            const string query = @"SELECT DVC.ID_DonVC, DVC.ID_DH, DVC.BienSoXe, DVC.MaDVC, DVC.ID_SP, DVC.SoLuongGiao, COALESCE(DVC.trong_luong, 0) AS trong_luong, DVC.ThoiGianKhoiHanh, DVC.TrangThaiDon,
                                          COALESCE(HH.TenHang, SP.LoaiSP) AS TenHang,
                                          KH.TenDoanhNghiep AS TenKhachHang,
                                          COALESCE(GH.DiaChiGiaoHang, KH.DiaChi) AS DiaChiGiao,
                                          COALESCE(GH.SDTNguoiNhan, KH.SDT) AS SDTKhachHang,
                                          COALESCE(DVC_DIEM.TenDVC, DVC.MaDVC) AS TenDVC
                                   FROM DonVanChuyen DVC
                                   LEFT JOIN SanPham SP ON DVC.ID_SP = SP.ID_SP
                                   LEFT JOIN HangHoa HH ON SP.MaHang = HH.MaHang
                                   LEFT JOIN DonHang DH ON DVC.ID_DH = DH.ID_DH
                                   LEFT JOIN KhachHang KH ON DH.ID_KH = KH.ID_KH
                                   LEFT JOIN GiaoHang GH ON DH.ID_DH = GH.ID_DH
                                   LEFT JOIN DiemVanChuyen DVC_DIEM ON DVC.MaDVC = DVC_DIEM.MaDVC
                                   WHERE DVC.ID_DonVC = @ID_DonVC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.Add(new NpgsqlParameter("@ID_DonVC", NpgsqlDbType.Varchar) { Value = id ?? string.Empty });
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new DonVanChuyen
                        {
                            ID_DonVC = reader["ID_DonVC"] != DBNull.Value ? reader["ID_DonVC"].ToString() : string.Empty,
                            ID_DH = reader["ID_DH"] != DBNull.Value ? reader["ID_DH"].ToString() : string.Empty,
                            BienSoXe = reader["BienSoXe"] != DBNull.Value ? reader["BienSoXe"].ToString() : string.Empty,
                            MaDVC = reader["MaDVC"] != DBNull.Value ? reader["MaDVC"].ToString() : string.Empty,
                            TenDVC = reader["TenDVC"] != DBNull.Value ? reader["TenDVC"].ToString() : string.Empty,
                            ID_SP = reader["ID_SP"] != DBNull.Value ? reader["ID_SP"].ToString() : string.Empty,
                            TenHang = reader["TenHang"] != DBNull.Value ? reader["TenHang"].ToString() : string.Empty,
                            TenKhachHang = reader["TenKhachHang"] != DBNull.Value ? reader["TenKhachHang"].ToString() : string.Empty,
                            DiaChiGiao = reader["DiaChiGiao"] != DBNull.Value ? reader["DiaChiGiao"].ToString() : string.Empty,
                            SDTKhachHang = reader["SDTKhachHang"] != DBNull.Value ? reader["SDTKhachHang"].ToString() : string.Empty,
                            SoLuongGiao = reader["SoLuongGiao"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongGiao"]) : 0,
                            TrongLuong = reader["trong_luong"] != DBNull.Value ? Convert.ToDecimal(reader["trong_luong"]) : 0,
                            ThoiGianKhoiHanh = reader["ThoiGianKhoiHanh"] != DBNull.Value ? Convert.ToDateTime(reader["ThoiGianKhoiHanh"]) : DateTime.MinValue,
                            TrangThaiDon = reader["TrangThaiDon"] != DBNull.Value ? reader["TrangThaiDon"].ToString() : string.Empty
                        };
                    }
                }
            }
            return null;
        }

        // 2. Thêm mới đơn vận chuyển (Tự động đồng bộ trạng thái sang Bán hàng và Xe)
        public bool Insert(DonVanChuyen don)
        {
            const string query = @"INSERT INTO DonVanChuyen
                                    (ID_DonVC, ID_DH, BienSoXe, MaDVC, ID_SP, SoLuongGiao, trong_luong, ThoiGianKhoiHanh, TrangThaiDon)
                                   VALUES
                                    (@ID_DonVC, @ID_DH, @BienSoXe, @MaDVC, @ID_SP, @SoLuongGiao, @TrongLuong, @ThoiGianKhoiHanh, @TrangThaiDon)";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection, transaction))
                        {
                            command.Parameters.Add(new NpgsqlParameter("@ID_DonVC", NpgsqlDbType.Varchar) { Value = (object)don.ID_DonVC ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = (object)don.ID_DH ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = (object)don.BienSoXe ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@MaDVC", NpgsqlDbType.Varchar) { Value = (object)don.MaDVC ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@ID_SP", NpgsqlDbType.Varchar) { Value = (object)don.ID_SP ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@SoLuongGiao", NpgsqlDbType.Integer) { Value = don.SoLuongGiao });
                            command.Parameters.Add(new NpgsqlParameter("@TrongLuong", NpgsqlDbType.Numeric) { Value = don.TrongLuong });
                            command.Parameters.Add(new NpgsqlParameter("@ThoiGianKhoiHanh", NpgsqlDbType.Timestamp) { Value = don.ThoiGianKhoiHanh });
                            command.Parameters.Add(new NpgsqlParameter("@TrangThaiDon", NpgsqlDbType.Varchar) { Value = (object)don.TrangThaiDon ?? DBNull.Value });
                            command.ExecuteNonQuery();
                        }

                        // Đồng bộ trạng thái xe và bán hàng
                        if (don.TrangThaiDon == "Đang vận chuyển")
                        {
                            CapNhatTrangThaiXe(connection, transaction, don.BienSoXe, "Đang vận chuyển");
                            DongBoTrangThaiBanHang(connection, transaction, don.ID_DH, "Đang giao", "Đang giao");
                        }
                        else if (don.TrangThaiDon == "Hoàn thành")
                        {
                            CapNhatTrangThaiXe(connection, transaction, don.BienSoXe, "Đang rảnh");
                            DongBoTrangThaiBanHang(connection, transaction, don.ID_DH, "Đã giao", "Đã giao", true);
                        }
                        else if (don.TrangThaiDon == "Khởi tạo")
                        {
                            DongBoTrangThaiBanHang(connection, transaction, don.ID_DH, "Chờ giao", "Chờ giao");
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // 3. Cập nhật đơn vận chuyển (Tự động đồng bộ trạng thái 2 chiều)
        public bool Update(DonVanChuyen don)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string oldBienSo = null;
                        string oldTrangThai = null;
                        string oldIdDH = null;
                        const string qOld = "SELECT BienSoXe, TrangThaiDon, ID_DH FROM DonVanChuyen WHERE ID_DonVC = @ID_DonVC";
                        using (NpgsqlCommand cmdOld = new NpgsqlCommand(qOld, connection, transaction))
                        {
                            cmdOld.Parameters.Add(new NpgsqlParameter("@ID_DonVC", NpgsqlDbType.Varchar) { Value = don.ID_DonVC });
                            using (NpgsqlDataReader r = cmdOld.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    oldBienSo = r["BienSoXe"] != DBNull.Value ? r["BienSoXe"].ToString() : null;
                                    oldTrangThai = r["TrangThaiDon"] != DBNull.Value ? r["TrangThaiDon"].ToString() : null;
                                    oldIdDH = r["ID_DH"] != DBNull.Value ? r["ID_DH"].ToString() : null;
                                }
                            }
                        }

                        const string query = @"UPDATE DonVanChuyen
                                                SET ID_DH = @ID_DH,
                                                    BienSoXe = @BienSoXe,
                                                    MaDVC = @MaDVC,
                                                    ID_SP = @ID_SP,
                                                    SoLuongGiao = @SoLuongGiao,
                                                    trong_luong = @TrongLuong,
                                                    ThoiGianKhoiHanh = @ThoiGianKhoiHanh,
                                                    TrangThaiDon = @TrangThaiDon
                                                WHERE ID_DonVC = @ID_DonVC";

                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection, transaction))
                        {
                            command.Parameters.Add(new NpgsqlParameter("@ID_DonVC", NpgsqlDbType.Varchar) { Value = (object)don.ID_DonVC ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = (object)don.ID_DH ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = (object)don.BienSoXe ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@MaDVC", NpgsqlDbType.Varchar) { Value = (object)don.MaDVC ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@ID_SP", NpgsqlDbType.Varchar) { Value = (object)don.ID_SP ?? DBNull.Value });
                            command.Parameters.Add(new NpgsqlParameter("@SoLuongGiao", NpgsqlDbType.Integer) { Value = don.SoLuongGiao });
                            command.Parameters.Add(new NpgsqlParameter("@TrongLuong", NpgsqlDbType.Numeric) { Value = don.TrongLuong });
                            command.Parameters.Add(new NpgsqlParameter("@ThoiGianKhoiHanh", NpgsqlDbType.Timestamp) { Value = don.ThoiGianKhoiHanh });
                            command.Parameters.Add(new NpgsqlParameter("@TrangThaiDon", NpgsqlDbType.Varchar) { Value = (object)don.TrangThaiDon ?? DBNull.Value });
                            command.ExecuteNonQuery();
                        }

                        // Xử lý trạng thái xe và bán hàng
                        if (don.TrangThaiDon == "Đang vận chuyển")
                        {
                            CapNhatTrangThaiXe(connection, transaction, don.BienSoXe, "Đang vận chuyển");
                            DongBoTrangThaiBanHang(connection, transaction, don.ID_DH, "Đang giao", "Đang giao");
                            if (!string.IsNullOrEmpty(oldBienSo) && !string.Equals(oldBienSo, don.BienSoXe, StringComparison.OrdinalIgnoreCase))
                            {
                                GiaiPhongXeNeuRanh(connection, transaction, oldBienSo, don.ID_DonVC);
                            }
                        }
                        else if (don.TrangThaiDon == "Hoàn thành")
                        {
                            GiaiPhongXeNeuRanh(connection, transaction, don.BienSoXe, don.ID_DonVC);
                            DongBoTrangThaiBanHang(connection, transaction, don.ID_DH, "Đã giao", "Đã giao", true);
                            if (!string.IsNullOrEmpty(oldBienSo) && !string.Equals(oldBienSo, don.BienSoXe, StringComparison.OrdinalIgnoreCase))
                            {
                                GiaiPhongXeNeuRanh(connection, transaction, oldBienSo, don.ID_DonVC);
                            }
                        }
                        else if (don.TrangThaiDon == "Đã hủy")
                        {
                            GiaiPhongXeNeuRanh(connection, transaction, don.BienSoXe, don.ID_DonVC);
                            DongBoTrangThaiBanHang(connection, transaction, don.ID_DH, "Chưa giao", "Đã duyệt");
                            if (!string.IsNullOrEmpty(oldBienSo) && !string.Equals(oldBienSo, don.BienSoXe, StringComparison.OrdinalIgnoreCase))
                            {
                                GiaiPhongXeNeuRanh(connection, transaction, oldBienSo, don.ID_DonVC);
                            }
                        }
                        else
                        {
                            // Khởi tạo
                            GiaiPhongXeNeuRanh(connection, transaction, don.BienSoXe, don.ID_DonVC);
                            DongBoTrangThaiBanHang(connection, transaction, don.ID_DH, "Chờ giao", "Chờ giao");
                            if (!string.IsNullOrEmpty(oldBienSo) && !string.Equals(oldBienSo, don.BienSoXe, StringComparison.OrdinalIgnoreCase))
                            {
                                GiaiPhongXeNeuRanh(connection, transaction, oldBienSo, don.ID_DonVC);
                            }
                        }

                        // Nếu thay đổi mã đơn hàng, hoàn nguyên đơn hàng cũ
                        if (!string.IsNullOrEmpty(oldIdDH) && !string.Equals(oldIdDH, don.ID_DH, StringComparison.OrdinalIgnoreCase))
                        {
                            DongBoTrangThaiBanHang(connection, transaction, oldIdDH, "Chưa giao", "Đã duyệt");
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // 4. Xóa đơn vận chuyển theo ID_DonVC
        public bool Delete(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string oldBienSo = null;
                        string oldTrangThai = null;
                        string oldIdDH = null;
                        const string qOld = "SELECT BienSoXe, TrangThaiDon, ID_DH FROM DonVanChuyen WHERE ID_DonVC = @ID_DonVC";
                        using (NpgsqlCommand cmdOld = new NpgsqlCommand(qOld, connection, transaction))
                        {
                            cmdOld.Parameters.Add(new NpgsqlParameter("@ID_DonVC", NpgsqlDbType.Varchar) { Value = id });
                            using (NpgsqlDataReader r = cmdOld.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    oldBienSo = r["BienSoXe"] != DBNull.Value ? r["BienSoXe"].ToString() : null;
                                    oldTrangThai = r["TrangThaiDon"] != DBNull.Value ? r["TrangThaiDon"].ToString() : null;
                                    oldIdDH = r["ID_DH"] != DBNull.Value ? r["ID_DH"].ToString() : null;
                                }
                            }
                        }

                        const string query = "DELETE FROM DonVanChuyen WHERE ID_DonVC = @ID_DonVC";
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection, transaction))
                        {
                            command.Parameters.Add(new NpgsqlParameter("@ID_DonVC", NpgsqlDbType.Varchar) { Value = (object)id ?? DBNull.Value });
                            command.ExecuteNonQuery();
                        }

                        if (!string.IsNullOrEmpty(oldBienSo))
                        {
                            GiaiPhongXeNeuRanh(connection, transaction, oldBienSo, id);
                        }

                        if (!string.IsNullOrEmpty(oldIdDH))
                        {
                            DongBoTrangThaiBanHang(connection, transaction, oldIdDH, "Chưa giao", "Đã duyệt");
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // 5. Tìm kiếm theo ID_DonVC, ID_DH, BienSoXe hoặc MaDVC (dùng LIKE)
        public List<DonVanChuyen> Search(string keyword)
        {
            List<DonVanChuyen> list = new List<DonVanChuyen>();
            const string query = @"SELECT DVC.ID_DonVC, DVC.ID_DH, DVC.BienSoXe, DVC.MaDVC, DVC.ID_SP, DVC.SoLuongGiao, DVC.ThoiGianKhoiHanh, DVC.TrangThaiDon,
                                          COALESCE(HH.TenHang, SP.LoaiSP) AS TenHang,
                                          KH.TenDoanhNghiep AS TenKhachHang,
                                          COALESCE(GH.DiaChiGiaoHang, KH.DiaChi) AS DiaChiGiao,
                                          COALESCE(GH.SDTNguoiNhan, KH.SDT) AS SDTKhachHang,
                                          COALESCE(DVC_DIEM.TenDVC, DVC.MaDVC) AS TenDVC
                                   FROM DonVanChuyen DVC
                                   LEFT JOIN SanPham SP ON DVC.ID_SP = SP.ID_SP
                                   LEFT JOIN HangHoa HH ON SP.MaHang = HH.MaHang
                                   LEFT JOIN DonHang DH ON DVC.ID_DH = DH.ID_DH
                                   LEFT JOIN KhachHang KH ON DH.ID_KH = KH.ID_KH
                                   LEFT JOIN GiaoHang GH ON DH.ID_DH = GH.ID_DH
                                   LEFT JOIN DiemVanChuyen DVC_DIEM ON DVC.MaDVC = DVC_DIEM.MaDVC
                                   WHERE DVC.ID_DonVC LIKE @Keyword
                                      OR DVC.ID_DH LIKE @Keyword
                                      OR DVC.BienSoXe LIKE @Keyword
                                      OR DVC.MaDVC LIKE @Keyword
                                      OR DVC_DIEM.TenDVC LIKE @Keyword
                                      OR DVC.ID_SP LIKE @Keyword
                                      OR HH.TenHang LIKE @Keyword
                                      OR SP.LoaiSP LIKE @Keyword
                                      OR KH.TenDoanhNghiep LIKE @Keyword
                                   ORDER BY DVC.ThoiGianKhoiHanh DESC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                string searchPattern = "%" + (keyword ?? string.Empty).Trim() + "%";
                command.Parameters.Add(new NpgsqlParameter("@Keyword", NpgsqlDbType.Varchar) { Value = searchPattern });

                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DonVanChuyen
                        {
                            ID_DonVC = reader["ID_DonVC"] != DBNull.Value ? reader["ID_DonVC"].ToString() : string.Empty,
                            ID_DH = reader["ID_DH"] != DBNull.Value ? reader["ID_DH"].ToString() : string.Empty,
                            BienSoXe = reader["BienSoXe"] != DBNull.Value ? reader["BienSoXe"].ToString() : string.Empty,
                            MaDVC = reader["MaDVC"] != DBNull.Value ? reader["MaDVC"].ToString() : string.Empty,
                            TenDVC = reader["TenDVC"] != DBNull.Value ? reader["TenDVC"].ToString() : string.Empty,
                            ID_SP = reader["ID_SP"] != DBNull.Value ? reader["ID_SP"].ToString() : string.Empty,
                            TenHang = reader["TenHang"] != DBNull.Value ? reader["TenHang"].ToString() : string.Empty,
                            TenKhachHang = reader["TenKhachHang"] != DBNull.Value ? reader["TenKhachHang"].ToString() : string.Empty,
                            DiaChiGiao = reader["DiaChiGiao"] != DBNull.Value ? reader["DiaChiGiao"].ToString() : string.Empty,
                            SDTKhachHang = reader["SDTKhachHang"] != DBNull.Value ? reader["SDTKhachHang"].ToString() : string.Empty,
                            SoLuongGiao = reader["SoLuongGiao"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongGiao"]) : 0,
                            ThoiGianKhoiHanh = reader["ThoiGianKhoiHanh"] != DBNull.Value ? Convert.ToDateTime(reader["ThoiGianKhoiHanh"]) : DateTime.MinValue,
                            TrangThaiDon = reader["TrangThaiDon"] != DBNull.Value ? reader["TrangThaiDon"].ToString() : string.Empty
                        });
                    }
                }
            }
            return list;
        }

        // 6. Kiểm tra ID_DonVC đã tồn tại chưa (phục vụ kiểm tra A2)
        public bool IsExist(string id)
        {
            const string query = "SELECT COUNT(1) FROM DonVanChuyen WHERE ID_DonVC = @ID_DonVC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.Add(new NpgsqlParameter("@ID_DonVC", NpgsqlDbType.Varchar) { Value = (object)id ?? DBNull.Value });

                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        // 6.1 Kiểm tra xe có đang bận vận chuyển ở đơn khác, phiếu trả hàng khác hoặc đang bảo trì không
        public bool KiemTraXeDangBan(string bienSoXe, string excludeIdDonVC, out string lyDoBan)
        {
            lyDoBan = string.Empty;
            if (string.IsNullOrWhiteSpace(bienSoXe))
            {
                return false;
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // 1. Kiểm tra bảng DonVanChuyen: có đơn khác đang 'Đang vận chuyển' không
                const string queryDonVC = @"SELECT ID_DonVC 
                                           FROM DonVanChuyen 
                                           WHERE BienSoXe = @BienSoXe 
                                             AND TrangThaiDon = 'Đang vận chuyển'
                                             AND (@ExcludeIdDonVC IS NULL OR ID_DonVC <> @ExcludeIdDonVC)
                                           LIMIT 1";
                using (NpgsqlCommand cmd = new NpgsqlCommand(queryDonVC, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                    cmd.Parameters.Add(new NpgsqlParameter("@ExcludeIdDonVC", NpgsqlDbType.Varchar) { Value = (object)excludeIdDonVC ?? DBNull.Value });
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        lyDoBan = $"Xe đang thực hiện đơn vận chuyển [{result}] ở trạng thái 'Đang vận chuyển'";
                        return true;
                    }
                }

                // 2. Kiểm tra bảng PhieuTraHang: có phiếu trả hàng nào đang thực hiện không
                const string queryPhieuTra = @"SELECT ID_PhieuTra, TrangThai 
                                              FROM PhieuTraHang 
                                              WHERE BienSoXe = @BienSoXe 
                                                AND TrangThai IN ('Đang thu hồi', 'Đang lấy hàng', 'Đang vận chuyển')
                                              LIMIT 1";
                using (NpgsqlCommand cmd = new NpgsqlCommand(queryPhieuTra, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string idPhieu = reader["ID_PhieuTra"].ToString();
                            string tt = reader["TrangThai"].ToString();
                            lyDoBan = $"Xe đang bận thực hiện phiếu trả hàng [{idPhieu}] (Trạng thái: {tt})";
                            return true;
                        }
                    }
                }

                // 3. Kiểm tra bảng PhuongTien: Trạng thái và Kích hoạt
                const string queryPhuongTien = @"SELECT TrangThaiXe, KichHoat 
                                                FROM PhuongTien 
                                                WHERE BienSoXe = @BienSoXe
                                                LIMIT 1";
                using (NpgsqlCommand cmd = new NpgsqlCommand(queryPhuongTien, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool kichHoat = reader["KichHoat"] == DBNull.Value || Convert.ToBoolean(reader["KichHoat"]);
                            string trangThaiXe = reader["TrangThaiXe"] != DBNull.Value ? reader["TrangThaiXe"].ToString() : string.Empty;

                            if (!kichHoat)
                            {
                                lyDoBan = "Phương tiện này hiện đang bị ngưng kích hoạt trong hệ thống";
                                return true;
                            }

                            if (string.Equals(trangThaiXe, "Bảo trì", StringComparison.OrdinalIgnoreCase))
                            {
                                lyDoBan = "Phương tiện này đang ở trạng thái 'Bảo trì'";
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        // Cập nhật trạng thái phương tiện trong bảng PhuongTien
        private void CapNhatTrangThaiXe(NpgsqlConnection connection, NpgsqlTransaction transaction, string bienSoXe, string trangThaiXe)
        {
            if (string.IsNullOrWhiteSpace(bienSoXe)) return;

            const string query = "UPDATE PhuongTien SET TrangThaiXe = @TrangThaiXe WHERE TRIM(BienSoXe) = TRIM(@BienSoXe)";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection, transaction))
            {
                command.Parameters.Add(new NpgsqlParameter("@TrangThaiXe", NpgsqlDbType.Varchar) { Value = trangThaiXe });
                command.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                command.ExecuteNonQuery();
            }
        }

        // Giải phóng xe về trạng thái 'Đang rảnh' nếu không còn đơn hoặc phiếu nào đang vận chuyển
        private void GiaiPhongXeNeuRanh(NpgsqlConnection connection, NpgsqlTransaction transaction, string bienSoXe, string excludeIdDonVC)
        {
            if (string.IsNullOrWhiteSpace(bienSoXe)) return;

            const string qDonVC = @"SELECT COUNT(1) FROM DonVanChuyen
                                   WHERE TRIM(BienSoXe) = TRIM(@BienSoXe)
                                     AND TrangThaiDon = 'Đang vận chuyển'
                                     AND (@ExcludeIdDonVC IS NULL OR ID_DonVC <> @ExcludeIdDonVC)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(qDonVC, connection, transaction))
            {
                cmd.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                cmd.Parameters.Add(new NpgsqlParameter("@ExcludeIdDonVC", NpgsqlDbType.Varchar) { Value = (object)excludeIdDonVC ?? DBNull.Value });
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0) return;
            }

            const string qPhieuTra = @"SELECT COUNT(1) FROM PhieuTraHang
                                      WHERE TRIM(BienSoXe) = TRIM(@BienSoXe)
                                        AND TrangThai IN ('Đang thu hồi', 'Đang lấy hàng', 'Đang vận chuyển')";
            using (NpgsqlCommand cmd = new NpgsqlCommand(qPhieuTra, connection, transaction))
            {
                cmd.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                int countPhieu = Convert.ToInt32(cmd.ExecuteScalar());
                if (countPhieu > 0) return;
            }

            const string qCheckXe = "SELECT TrangThaiXe FROM PhuongTien WHERE TRIM(BienSoXe) = TRIM(@BienSoXe)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(qCheckXe, connection, transaction))
            {
                cmd.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                object ttObj = cmd.ExecuteScalar();
                string ttXe = ttObj != null && ttObj != DBNull.Value ? ttObj.ToString().Trim() : "";
                if (ttXe != "Bảo trì")
                {
                    CapNhatTrangThaiXe(connection, transaction, bienSoXe, "Đang rảnh");
                }
            }
        }

        // Tự động rà soát và đồng bộ trạng thái toàn bộ xe trong hệ thống
        public void DongBoTrangThaiXeToanHeThong()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // 1. Xe đang có đơn vận chuyển hoặc phiếu trả hàng đang chạy -> chuyển thành 'Đang vận chuyển' (trừ xe 'Bảo trì')
                    const string qBusy = @"
                        UPDATE PhuongTien
                        SET TrangThaiXe = 'Đang vận chuyển'
                        WHERE (TrangThaiXe IS NULL OR TrangThaiXe <> 'Bảo trì')
                          AND (TrangThaiXe <> 'Đang vận chuyển' OR TrangThaiXe IS NULL)
                          AND (
                              TRIM(BienSoXe) IN (
                                  SELECT TRIM(BienSoXe) 
                                  FROM DonVanChuyen 
                                  WHERE TrangThaiDon = 'Đang vận chuyển' 
                                    AND BienSoXe IS NOT NULL AND TRIM(BienSoXe) <> ''
                              )
                              OR TRIM(BienSoXe) IN (
                                  SELECT TRIM(BienSoXe) 
                                  FROM PhieuTraHang 
                                  WHERE TrangThai IN ('Đang thu hồi', 'Đang lấy hàng', 'Đang vận chuyển')
                                    AND BienSoXe IS NOT NULL AND TRIM(BienSoXe) <> ''
                              )
                          );";

                    using (NpgsqlCommand cmdBusy = new NpgsqlCommand(qBusy, connection))
                    {
                        cmdBusy.ExecuteNonQuery();
                    }

                    // 2. Xe không còn đơn vận chuyển hay phiếu trả hàng nào đang chạy -> chuyển thành 'Đang rảnh' (trừ xe 'Bảo trì')
                    const string qFree = @"
                        UPDATE PhuongTien
                        SET TrangThaiXe = 'Đang rảnh'
                        WHERE (TrangThaiXe IS NULL OR TrangThaiXe <> 'Bảo trì')
                          AND TrangThaiXe = 'Đang vận chuyển'
                          AND TRIM(BienSoXe) NOT IN (
                              SELECT TRIM(BienSoXe) 
                              FROM DonVanChuyen 
                              WHERE TrangThaiDon = 'Đang vận chuyển' 
                                AND BienSoXe IS NOT NULL AND TRIM(BienSoXe) <> ''
                          )
                          AND TRIM(BienSoXe) NOT IN (
                              SELECT TRIM(BienSoXe) 
                              FROM PhieuTraHang 
                              WHERE TrangThai IN ('Đang thu hồi', 'Đang lấy hàng', 'Đang vận chuyển')
                                AND BienSoXe IS NOT NULL AND TRIM(BienSoXe) <> ''
                          );";

                    using (NpgsqlCommand cmdFree = new NpgsqlCommand(qFree, connection))
                    {
                        cmdFree.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi kết nối tạm thời
            }
        }

        // 7. Lấy danh sách BienSoXe từ bảng PhuongTien có KichHoat = 1 VÀ TrangThaiXe là 'Đang rảnh' hoặc 'Sẵn sàng'
        //    VÀ không nằm trong đơn vận chuyển hay phiếu trả hàng nào đang hoạt động
        public List<string> GetDanhSachXeKhaDung()
        {
            List<string> list = new List<string>();
            const string query = @"SELECT BienSoXe
                                   FROM PhuongTien
                                   WHERE (KichHoat = true OR KichHoat IS NULL)
                                     AND (TrangThaiXe = 'Đang rảnh' OR TrangThaiXe = 'Sẵn sàng' OR TrangThaiXe IS NULL)
                                     AND BienSoXe NOT IN (
                                         SELECT BienSoXe
                                         FROM DonVanChuyen
                                         WHERE TrangThaiDon = 'Đang vận chuyển'
                                           AND BienSoXe IS NOT NULL
                                     )
                                     AND BienSoXe NOT IN (
                                         SELECT BienSoXe
                                         FROM PhieuTraHang
                                         WHERE TrangThai IN ('Đang thu hồi', 'Đang lấy hàng', 'Đang vận chuyển')
                                           AND BienSoXe IS NOT NULL
                                     )
                                   ORDER BY BienSoXe ASC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["BienSoXe"] != DBNull.Value)
                        {
                            list.Add(reader["BienSoXe"].ToString());
                        }
                    }
                }
            }
            return list;
        }

        // 7.1 Lấy danh sách xe khả dụng kèm tải trọng (dùng cho ComboBox hiển thị)
        public List<XeKhaDungItem> GetDanhSachXeKhaDungKemTaiTrong()
        {
            List<XeKhaDungItem> list = new List<XeKhaDungItem>();
            const string query = @"SELECT BienSoXe, LoaiXe, COALESCE(TaiTrong, 0) AS TaiTrong
                                   FROM PhuongTien
                                   WHERE (KichHoat = true OR KichHoat IS NULL)
                                     AND (TrangThaiXe = 'Đang rảnh' OR TrangThaiXe = 'Sẵn sàng' OR TrangThaiXe IS NULL)
                                     AND BienSoXe NOT IN (
                                         SELECT BienSoXe
                                         FROM DonVanChuyen
                                         WHERE TrangThaiDon = 'Đang vận chuyển'
                                           AND BienSoXe IS NOT NULL
                                     )
                                     AND BienSoXe NOT IN (
                                         SELECT BienSoXe
                                         FROM PhieuTraHang
                                         WHERE TrangThai IN ('Đang thu hồi', 'Đang lấy hàng', 'Đang vận chuyển')
                                           AND BienSoXe IS NOT NULL
                                     )
                                   ORDER BY BienSoXe ASC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new XeKhaDungItem
                        {
                            BienSoXe = reader["BienSoXe"] != DBNull.Value ? reader["BienSoXe"].ToString() : string.Empty,
                            LoaiXe = reader["LoaiXe"] != DBNull.Value ? reader["LoaiXe"].ToString() : string.Empty,
                            TaiTrong = reader["TaiTrong"] != DBNull.Value ? Convert.ToDecimal(reader["TaiTrong"]) : 0
                        });
                    }
                }
            }
            return list;
        }

        // 7.2 Lấy tải trọng quy chuẩn (Kg) của xe theo biển số
        public decimal LayTaiTrongXe(string bienSoXe)
        {
            if (string.IsNullOrWhiteSpace(bienSoXe)) return 0;
            const string query = "SELECT TaiTrong FROM PhuongTien WHERE BienSoXe = @BienSoXe";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && obj != DBNull.Value && decimal.TryParse(obj.ToString(), out decimal tt))
                {
                    // Nếu <= 50 quy ước là Tấn, đổi sang Kg (* 1000)
                    return tt <= 50 ? tt * 1000 : tt;
                }
            }
            return 0;
        }

        // 7.3 Lấy thông tin chi tiết một xe theo biển số
        public XeKhaDungItem GetThongTinXe(string bienSoXe)
        {
            if (string.IsNullOrWhiteSpace(bienSoXe)) return null;
            const string query = "SELECT BienSoXe, LoaiXe, COALESCE(TaiTrong, 0) AS TaiTrong FROM PhuongTien WHERE BienSoXe = @BienSoXe";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.Add(new NpgsqlParameter("@BienSoXe", NpgsqlDbType.Varchar) { Value = bienSoXe });
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new XeKhaDungItem
                        {
                            BienSoXe = reader["BienSoXe"].ToString(),
                            LoaiXe = reader["LoaiXe"] != DBNull.Value ? reader["LoaiXe"].ToString() : string.Empty,
                            TaiTrong = reader["TaiTrong"] != DBNull.Value ? Convert.ToDecimal(reader["TaiTrong"]) : 0
                        };
                    }
                }
            }
            return null;
        }

        // 8. Lấy danh sách MaDVC từ bảng DiemVanChuyen
        public List<string> GetDanhSachDVC()
        {
            List<string> list = new List<string>();
            const string query = "SELECT MaDVC FROM DiemVanChuyen ORDER BY MaDVC ASC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["MaDVC"] != DBNull.Value)
                        {
                            list.Add(reader["MaDVC"].ToString());
                        }
                    }
                }
            }
            return list;
        }

        // 8.1 Lấy danh sách điểm vận chuyển đầy đủ (dùng cho ComboBox hiển thị Tên)
        public List<DiemVanChuyenComboItem> GetDanhSachDiemVanChuyen()
        {
            List<DiemVanChuyenComboItem> list = new List<DiemVanChuyenComboItem>();
            const string query = "SELECT MaDVC, TenDVC, DiaChiDVC, SDT_DVC FROM DiemVanChuyen ORDER BY TenDVC ASC, MaDVC ASC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DiemVanChuyenComboItem
                        {
                            MaDVC = reader["MaDVC"] != DBNull.Value ? reader["MaDVC"].ToString() : string.Empty,
                            TenDVC = reader["TenDVC"] != DBNull.Value ? reader["TenDVC"].ToString() : string.Empty,
                            DiaChiDVC = reader["DiaChiDVC"] != DBNull.Value ? reader["DiaChiDVC"].ToString() : string.Empty,
                            SDT = reader["SDT_DVC"] != DBNull.Value ? reader["SDT_DVC"].ToString() : string.Empty
                        });
                    }
                }
            }
            return list;
        }

        // 8.2 Tự động tìm hoặc tạo mới điểm nhận hàng theo địa chỉ của khách hàng từ Đơn hàng Bán hàng
        public DiemVanChuyenComboItem LayHoacTaoDiemNhanHangChoKhachHang(string tenKhachHang, string diaChiGiao, string sdt)
        {
            if (string.IsNullOrWhiteSpace(tenKhachHang) && string.IsNullOrWhiteSpace(diaChiGiao))
            {
                return null;
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // 1. Kiểm tra xem đã có Điểm vận chuyển nào khớp với Địa chỉ giao hoặc Tên khách hàng chưa
                const string queryFind = @"SELECT MaDVC, TenDVC, DiaChiDVC, SDT_DVC 
                                           FROM DiemVanChuyen 
                                           WHERE (LOWER(TRIM(DiaChiDVC)) = LOWER(TRIM(@DiaChiGiao)) AND DiaChiDVC IS NOT NULL AND DiaChiDVC <> '')
                                              OR (LOWER(TRIM(TenDVC)) = LOWER(TRIM(@TenKhachHang)) AND LOWER(TRIM(DiaChiDVC)) = LOWER(TRIM(@DiaChiGiao)))
                                           LIMIT 1";
                using (NpgsqlCommand cmdFind = new NpgsqlCommand(queryFind, connection))
                {
                    cmdFind.Parameters.Add(new NpgsqlParameter("@DiaChiGiao", NpgsqlDbType.Varchar) { Value = (diaChiGiao ?? string.Empty).Trim() });
                    cmdFind.Parameters.Add(new NpgsqlParameter("@TenKhachHang", NpgsqlDbType.Varchar) { Value = (tenKhachHang ?? string.Empty).Trim() });
                    using (NpgsqlDataReader reader = cmdFind.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DiemVanChuyenComboItem
                            {
                                MaDVC = reader["MaDVC"] != DBNull.Value ? reader["MaDVC"].ToString() : string.Empty,
                                TenDVC = reader["TenDVC"] != DBNull.Value ? reader["TenDVC"].ToString() : string.Empty,
                                DiaChiDVC = reader["DiaChiDVC"] != DBNull.Value ? reader["DiaChiDVC"].ToString() : string.Empty,
                                SDT = reader["SDT_DVC"] != DBNull.Value ? reader["SDT_DVC"].ToString() : string.Empty
                            };
                        }
                    }
                }

                // 2. Nếu chưa có, tự động sinh mã MaDVC mới và thêm vào DiemVanChuyen
                const string queryMax = @"SELECT MaDVC FROM DiemVanChuyen WHERE MaDVC LIKE 'DVC%' ORDER BY LENGTH(MaDVC) DESC, MaDVC DESC LIMIT 1";
                string nextMaDVC = "DVC001";
                using (NpgsqlCommand cmdMax = new NpgsqlCommand(queryMax, connection))
                {
                    object maxObj = cmdMax.ExecuteScalar();
                    if (maxObj != null && maxObj != DBNull.Value)
                    {
                        string maxCode = maxObj.ToString();
                        string numPart = System.Text.RegularExpressions.Regex.Match(maxCode, @"\d+").Value;
                        if (int.TryParse(numPart, out int num))
                        {
                            nextMaDVC = $"DVC{(num + 1).ToString("D3")}";
                        }
                    }
                }

                // Tránh trùng mã
                int suffix = 1;
                while (true)
                {
                    const string qCheck = "SELECT COUNT(1) FROM DiemVanChuyen WHERE MaDVC = @MaDVC";
                    using (NpgsqlCommand cmdCheck = new NpgsqlCommand(qCheck, connection))
                    {
                        cmdCheck.Parameters.Add(new NpgsqlParameter("@MaDVC", NpgsqlDbType.Varchar) { Value = nextMaDVC });
                        int cnt = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        if (cnt == 0) break;
                        suffix++;
                        nextMaDVC = $"DVC{suffix.ToString("D3")}";
                    }
                }

                string tenDiem = !string.IsNullOrWhiteSpace(tenKhachHang) ? tenKhachHang.Trim() : "Điểm nhận " + nextMaDVC;
                string diaChi = !string.IsNullOrWhiteSpace(diaChiGiao) ? diaChiGiao.Trim() : "Theo địa chỉ khách hàng";
                string sdtKh = !string.IsNullOrWhiteSpace(sdt) ? sdt.Trim() : "0900000000";
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdtKh, @"^\d{10}$"))
                {
                    sdtKh = "0900000000";
                }

                const string qInsert = @"INSERT INTO DiemVanChuyen (MaDVC, TenDVC, DiaChiDVC, SDT_DVC, TenNguoiDaiDien)
                                        VALUES (@MaDVC, @TenDVC, @DiaChiDVC, @SDT_DVC, @TenNguoiDaiDien)";
                using (NpgsqlCommand cmdInsert = new NpgsqlCommand(qInsert, connection))
                {
                    cmdInsert.Parameters.Add(new NpgsqlParameter("@MaDVC", NpgsqlDbType.Varchar) { Value = nextMaDVC });
                    cmdInsert.Parameters.Add(new NpgsqlParameter("@TenDVC", NpgsqlDbType.Varchar) { Value = tenDiem });
                    cmdInsert.Parameters.Add(new NpgsqlParameter("@DiaChiDVC", NpgsqlDbType.Varchar) { Value = diaChi });
                    cmdInsert.Parameters.Add(new NpgsqlParameter("@SDT_DVC", NpgsqlDbType.Varchar) { Value = sdtKh });
                    cmdInsert.Parameters.Add(new NpgsqlParameter("@TenNguoiDaiDien", NpgsqlDbType.Varchar) { Value = tenDiem });
                    cmdInsert.ExecuteNonQuery();
                }

                return new DiemVanChuyenComboItem
                {
                    MaDVC = nextMaDVC,
                    TenDVC = tenDiem,
                    DiaChiDVC = diaChi,
                    SDT = sdtKh
                };
            }
        }

        // 9. Lấy danh sách ID_SP từ bảng SanPham
        public List<string> GetDanhSachSanPham()
        {
            List<string> list = new List<string>();
            const string query = "SELECT ID_SP FROM SanPham ORDER BY ID_SP ASC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["ID_SP"] != DBNull.Value)
                        {
                            list.Add(reader["ID_SP"].ToString());
                        }
                    }
                }
            }
            return list;
        }

        // 9.1 Lấy danh sách SanPham kèm TenHang (từ SanPham join HangHoa)
        public List<SanPhamComboItem> GetDanhSachSanPhamWithTen()
        {
            List<SanPhamComboItem> list = new List<SanPhamComboItem>();
            const string query = @"SELECT SP.ID_SP, COALESCE(HH.TenHang, SP.LoaiSP) AS TenHang
                                   FROM SanPham SP
                                   LEFT JOIN HangHoa HH ON SP.MaHang = HH.MaHang
                                   ORDER BY SP.ID_SP ASC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SanPhamComboItem
                        {
                            ID_SP = reader["ID_SP"] != DBNull.Value ? reader["ID_SP"].ToString() : string.Empty,
                            TenHang = reader["TenHang"] != DBNull.Value ? reader["TenHang"].ToString() : string.Empty
                        });
                    }
                }
            }
            return list;
        }

        // Các hàm phụ trợ tương thích ngược với BLL hiện tại
        public DataTable GetTatCaDonVanChuyen()
        {
            const string query = @"
                SELECT DVC.ID_DonVC, DVC.BienSoXe, DVC.MaDVC,
                       COALESCE(DMC.TenDVC, DVC.MaDVC) AS TenDVC,
                       DVC.ID_SP, DVC.SoLuongGiao, DVC.ThoiGianKhoiHanh,
                       DVC.TrangThaiDon
                FROM DonVanChuyen DVC
                LEFT JOIN DiemVanChuyen DMC ON DVC.MaDVC = DMC.MaDVC
                ORDER BY DVC.ThoiGianKhoiHanh DESC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
            {
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }

        public void ThemDon(DonVanChuyen don)
        {
            Insert(don);
        }

        // Tự động đồng bộ trạng thái sang phân hệ Bán hàng (GiaoHang, DonHang, HoaDon)
        private void DongBoTrangThaiBanHang(NpgsqlConnection connection, NpgsqlTransaction transaction, string idDH, string trangThaiGiao, string trangThaiDonHang, bool isHoanThanh = false)
        {
            if (string.IsNullOrWhiteSpace(idDH)) return;
            string cleanIdDH = idDH.Trim();

            // 1. Cập nhật bảng GiaoHang
            string qGH = isHoanThanh
                ? "UPDATE GiaoHang SET TrangThaiGiaoHang = @TrangThaiGiao, NgayNhan = NOW() WHERE TRIM(ID_DH) = @ID_DH"
                : "UPDATE GiaoHang SET TrangThaiGiaoHang = @TrangThaiGiao WHERE TRIM(ID_DH) = @ID_DH";

            int rowsGh = 0;
            using (NpgsqlCommand cmdGH = new NpgsqlCommand(qGH, connection, transaction))
            {
                cmdGH.Parameters.Add(new NpgsqlParameter("@TrangThaiGiao", NpgsqlDbType.Varchar) { Value = trangThaiGiao });
                cmdGH.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = cleanIdDH });
                rowsGh = cmdGH.ExecuteNonQuery();
            }

            // Nếu đơn hàng chưa có bản ghi trong bảng GiaoHang thì tạo mới
            if (rowsGh == 0)
            {
                try
                {
                    string nguoiNhan = "Khách hàng";
                    string diaChi = "Theo đơn hàng";
                    string sdt = "0900000000";

                    const string qInfo = @"
                        SELECT COALESCE(KH.NguoiDaiDien, KH.TenDoanhNghiep) AS NguoiNhan,
                               COALESCE(KH.DiaChi, 'Theo đơn hàng') AS DiaChi,
                               COALESCE(KH.SDT, '0900000000') AS SDT
                        FROM DonHang DH
                        LEFT JOIN KhachHang KH ON DH.ID_KH = KH.ID_KH
                        WHERE TRIM(DH.ID_DH) = @ID_DH
                        LIMIT 1";
                    using (NpgsqlCommand cmdInfo = new NpgsqlCommand(qInfo, connection, transaction))
                    {
                        cmdInfo.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = cleanIdDH });
                        using (NpgsqlDataReader r = cmdInfo.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                if (r["NguoiNhan"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["NguoiNhan"].ToString())) nguoiNhan = r["NguoiNhan"].ToString().Trim();
                                if (r["DiaChi"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["DiaChi"].ToString())) diaChi = r["DiaChi"].ToString().Trim();
                                if (r["SDT"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["SDT"].ToString())) sdt = r["SDT"].ToString().Trim();
                            }
                        }
                    }

                    string idGhNew = "GH_" + cleanIdDH;
                    if (idGhNew.Length > 20) idGhNew = idGhNew.Substring(0, 20);

                    const string qInsertGH = @"
                        INSERT INTO GiaoHang (ID_GH, ID_DH, NguoiNhan, DiaChiGiaoHang, SDTNguoiNhan, TrangThaiGiaoHang, NgayGiao, NgayNhan)
                        VALUES (@ID_GH, @ID_DH, @NguoiNhan, @DiaChiGiaoHang, @SDTNguoiNhan, @TrangThaiGiaoHang, NOW(), @NgayNhan)";
                    using (NpgsqlCommand cmdIns = new NpgsqlCommand(qInsertGH, connection, transaction))
                    {
                        cmdIns.Parameters.Add(new NpgsqlParameter("@ID_GH", NpgsqlDbType.Varchar) { Value = idGhNew });
                        cmdIns.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = cleanIdDH });
                        cmdIns.Parameters.Add(new NpgsqlParameter("@NguoiNhan", NpgsqlDbType.Varchar) { Value = nguoiNhan });
                        cmdIns.Parameters.Add(new NpgsqlParameter("@DiaChiGiaoHang", NpgsqlDbType.Varchar) { Value = diaChi });
                        cmdIns.Parameters.Add(new NpgsqlParameter("@SDTNguoiNhan", NpgsqlDbType.Varchar) { Value = sdt });
                        cmdIns.Parameters.Add(new NpgsqlParameter("@TrangThaiGiaoHang", NpgsqlDbType.Varchar) { Value = trangThaiGiao });
                        cmdIns.Parameters.Add(new NpgsqlParameter("@NgayNhan", NpgsqlDbType.Timestamp) { Value = isHoanThanh ? (object)DateTime.Now : DBNull.Value });
                        cmdIns.ExecuteNonQuery();
                    }
                }
                catch { }
            }

            // 2. Cập nhật bảng DonHang
            if (!string.IsNullOrWhiteSpace(trangThaiDonHang))
            {
                const string qDH = "UPDATE DonHang SET TrangThai = @TrangThai WHERE TRIM(ID_DH) = @ID_DH";
                using (NpgsqlCommand cmdDH = new NpgsqlCommand(qDH, connection, transaction))
                {
                    cmdDH.Parameters.Add(new NpgsqlParameter("@TrangThai", NpgsqlDbType.Varchar) { Value = trangThaiDonHang });
                    cmdDH.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = cleanIdDH });
                    cmdDH.ExecuteNonQuery();
                }
            }

            // 3. Nếu hoàn thành đơn hàng, cập nhật bảng HoaDon sang 'Đã thanh toán' (đồng bộ với Bán hàng)
            if (isHoanThanh)
            {
                try
                {
                    const string qHD = "UPDATE HoaDon SET TrangThai = 'Đã thanh toán' WHERE TRIM(ID_DH) = @ID_DH";
                    using (NpgsqlCommand cmdHD = new NpgsqlCommand(qHD, connection, transaction))
                    {
                        cmdHD.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = cleanIdDH });
                        cmdHD.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }

        // Tự động đồng bộ liên kết 2 chiều giữa Phân hệ Bán hàng (DonHang, GiaoHang) và Logistics (DonVanChuyen)
        public void DongBoLienKetBanHangVaLogistics()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // 1. Nếu DonHang bên Bán hàng đã là 'Hoàn thành' hoặc 'Đã giao' nhưng DonVanChuyen đang là 'Đang vận chuyển' hoặc 'Khởi tạo'
                    // -> Tự động đồng bộ DonVanChuyen sang 'Hoàn thành'
                    const string qSyncDvcDone = @"
                        UPDATE DonVanChuyen DVC
                        SET TrangThaiDon = 'Hoàn thành'
                        FROM DonHang DH
                        WHERE TRIM(DVC.ID_DH) = TRIM(DH.ID_DH)
                          AND (DH.TrangThai IN ('Hoàn thành', 'Đã giao'))
                          AND DVC.TrangThaiDon IN ('Đang vận chuyển', 'Khởi tạo');";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qSyncDvcDone, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Nếu DonVanChuyen là 'Hoàn thành', đảm bảo DonHang là 'Hoàn thành', GiaoHang là 'Đã giao', HoaDon là 'Đã thanh toán'
                    const string qSyncDhDone = @"
                        UPDATE DonHang DH
                        SET TrangThai = 'Hoàn thành'
                        FROM DonVanChuyen DVC
                        WHERE TRIM(DVC.ID_DH) = TRIM(DH.ID_DH)
                          AND DVC.TrangThaiDon = 'Hoàn thành'
                          AND DH.TrangThai NOT IN ('Hoàn thành', 'Đã giao', 'Đã hủy');";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qSyncDhDone, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    const string qSyncGhDone = @"
                        UPDATE GiaoHang GH
                        SET TrangThaiGiaoHang = 'Đã giao', NgayNhan = COALESCE(GH.NgayNhan, NOW())
                        FROM DonVanChuyen DVC
                        WHERE TRIM(DVC.ID_DH) = TRIM(GH.ID_DH)
                          AND DVC.TrangThaiDon = 'Hoàn thành'
                          AND GH.TrangThaiGiaoHang <> 'Đã giao';";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qSyncGhDone, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    const string qSyncHdDone = @"
                        UPDATE HoaDon HD
                        SET TrangThai = 'Đã thanh toán'
                        FROM DonVanChuyen DVC
                        WHERE TRIM(DVC.ID_DH) = TRIM(HD.ID_DH)
                          AND DVC.TrangThaiDon = 'Hoàn thành'
                          AND HD.TrangThai <> 'Đã thanh toán';";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qSyncHdDone, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 3. Nếu DonVanChuyen là 'Đang vận chuyển', đảm bảo GiaoHang là 'Đang giao' và DonHang là 'Đang giao'
                    const string qSyncGhShipping = @"
                        UPDATE GiaoHang GH
                        SET TrangThaiGiaoHang = 'Đang giao'
                        FROM DonVanChuyen DVC
                        WHERE TRIM(DVC.ID_DH) = TRIM(GH.ID_DH)
                          AND DVC.TrangThaiDon = 'Đang vận chuyển'
                          AND GH.TrangThaiGiaoHang <> 'Đang giao';";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qSyncGhShipping, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    const string qSyncDhShipping = @"
                        UPDATE DonHang DH
                        SET TrangThai = 'Đang giao'
                        FROM DonVanChuyen DVC
                        WHERE TRIM(DVC.ID_DH) = TRIM(DH.ID_DH)
                          AND DVC.TrangThaiDon = 'Đang vận chuyển'
                          AND DH.TrangThai <> 'Đang giao';";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qSyncDhShipping, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi kết nối tạm thời
            }
        }

        // Lấy danh sách các đơn hàng từ Bán hàng cần được giao
        public List<DonHangChoGiaoItem> GetDanhSachDonHangChoGiao()
        {
            List<DonHangChoGiaoItem> list = new List<DonHangChoGiaoItem>();
            const string query = @"
                SELECT 
                    DH.ID_DH,
                    DH.ID_KH,
                    KH.TenDoanhNghiep AS TenKhachHang,
                    COALESCE(GH.DiaChiGiaoHang, KH.DiaChi) AS DiaChiGiao,
                    COALESCE(GH.SDTNguoiNhan, KH.SDT) AS SDT,
                    CTDH.ID_SP,
                    COALESCE(HH.TenHang, SP.LoaiSP, CTDH.ID_SP) AS TenSP,
                    COALESCE(CTDH.SoLuong, 1) AS SoLuongDat,
                    COALESCE(GH.TrangThaiGiaoHang, 'Chưa giao') AS TrangThaiGiao
                FROM DonHang DH
                INNER JOIN KhachHang KH ON DH.ID_KH = KH.ID_KH
                LEFT JOIN GiaoHang GH ON DH.ID_DH = GH.ID_DH
                LEFT JOIN (
                    SELECT DISTINCT ON (ID_DH) ID_DH, ID_SP, SoLuong 
                    FROM ChiTietDonHang 
                    ORDER BY ID_DH, SoLuong DESC
                ) CTDH ON DH.ID_DH = CTDH.ID_DH
                LEFT JOIN SanPham SP ON CTDH.ID_SP = SP.ID_SP
                LEFT JOIN HangHoa HH ON SP.MaHang = HH.MaHang
                WHERE COALESCE(GH.TrangThaiGiaoHang, 'Chưa giao') NOT IN ('Đã giao', 'Hoàn thành')
                  AND (DH.TrangThai IS NULL OR DH.TrangThai NOT IN ('Đã giao', 'Đã hủy', 'Hoàn thành'))
                  AND NOT EXISTS (
                      SELECT 1 
                      FROM DonVanChuyen DVC 
                      WHERE DVC.ID_DH = DH.ID_DH 
                        AND DVC.TrangThaiDon != 'Đã hủy'
                  )
                ORDER BY DH.NgayTao DESC";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DonHangChoGiaoItem
                        {
                            ID_DH = reader["ID_DH"] != DBNull.Value ? reader["ID_DH"].ToString() : string.Empty,
                            ID_KH = reader["ID_KH"] != DBNull.Value ? reader["ID_KH"].ToString() : string.Empty,
                            TenKhachHang = reader["TenKhachHang"] != DBNull.Value ? reader["TenKhachHang"].ToString() : string.Empty,
                            DiaChiGiao = reader["DiaChiGiao"] != DBNull.Value ? reader["DiaChiGiao"].ToString() : string.Empty,
                            SDT = reader["SDT"] != DBNull.Value ? reader["SDT"].ToString() : string.Empty,
                            ID_SP = reader["ID_SP"] != DBNull.Value ? reader["ID_SP"].ToString() : string.Empty,
                            TenSP = reader["TenSP"] != DBNull.Value ? reader["TenSP"].ToString() : string.Empty,
                            SoLuongDat = reader["SoLuongDat"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongDat"]) : 0,
                            TrangThaiGiao = reader["TrangThaiGiao"] != DBNull.Value ? reader["TrangThaiGiao"].ToString() : string.Empty
                        });
                    }
                }
            }
            return list;
        }

        // Kiểm tra xem đơn hàng bán có đang nằm trong chuyến vận chuyển nào chưa hoàn tất không (Check trùng)
        public bool KiemTraDonHangDangVanChuyen(string idDH, string excludeIdDonVC, out string lyDo)
        {
            lyDo = string.Empty;
            if (string.IsNullOrWhiteSpace(idDH)) return false;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                const string query = @"SELECT ID_DonVC, TrangThaiDon 
                                       FROM DonVanChuyen 
                                       WHERE ID_DH = @ID_DH 
                                         AND TrangThaiDon != 'Đã hủy'
                                         AND (@ExcludeIdDonVC IS NULL OR ID_DonVC <> @ExcludeIdDonVC)
                                       LIMIT 1";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = idDH });
                    cmd.Parameters.Add(new NpgsqlParameter("@ExcludeIdDonVC", NpgsqlDbType.Varchar) { Value = (object)excludeIdDonVC ?? DBNull.Value });
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string idVC = reader["ID_DonVC"].ToString();
                            string tt = reader["TrangThaiDon"].ToString();
                            lyDo = $"Đơn hàng [{idDH}] đã được gắn vào Đơn vận chuyển [{idVC}] (Trạng thái: {tt}). Không thể tạo trùng!";
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Lấy số lượng đặt tối đa của đơn hàng
        public int LaySoLuongDatCuaDonHang(string idDH, string idSP)
        {
            if (string.IsNullOrWhiteSpace(idDH)) return int.MaxValue;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                const string query = @"SELECT COALESCE(SUM(SoLuong), 0) 
                                       FROM ChiTietDonHang 
                                       WHERE ID_DH = @ID_DH
                                         AND (@ID_SP IS NULL OR ID_SP = @ID_SP)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@ID_DH", NpgsqlDbType.Varchar) { Value = idDH });
                    cmd.Parameters.Add(new NpgsqlParameter("@ID_SP", NpgsqlDbType.Varchar) { Value = (object)idSP ?? DBNull.Value });
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                    {
                        return Convert.ToInt32(res);
                    }
                }
            }
            return 0;
        }
    }
}




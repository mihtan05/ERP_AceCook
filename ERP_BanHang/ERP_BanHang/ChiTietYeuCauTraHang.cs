using System;
using System.Configuration;
using System.Data;
using Npgsql; // Đã chuyển đổi sang Npgsql cho PostgreSQL
using System.Drawing;
using System.Windows.Forms;

namespace ERP_BanHang
{
    public partial class ChiTietYeuCauTraHang : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ERP_Connection"].ConnectionString;
        private string maYeuCau;

        public ChiTietYeuCauTraHang(string idYC)
        {
            InitializeComponent();
            this.maYeuCau = idYC;
        }

        private void ChiTietYeuCauTraHang_Load(object sender, EventArgs e)
        {
            KhoiTaoCotBang();
            LoadThongTinYeuCau();
            LoadDanhSachSanPhamLoi();
        }

        private void KhoiTaoCotBang()
        {
            BangChiTietLoi.Columns.Clear();
            BangChiTietLoi.AutoGenerateColumns = false;

            // 1. Mã CTYC
            DataGridViewTextBoxColumn colIDCTYC = new DataGridViewTextBoxColumn();
            colIDCTYC.Name = "colIDCTYC";
            colIDCTYC.HeaderText = "MÃ CTYC";
            colIDCTYC.DataPropertyName = "ID_CTYC";
            colIDCTYC.FillWeight = 12;
            colIDCTYC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            BangChiTietLoi.Columns.Add(colIDCTYC);

            // 2. Mã SP
            DataGridViewTextBoxColumn colIDSP = new DataGridViewTextBoxColumn();
            colIDSP.Name = "colIDSP";
            colIDSP.HeaderText = "MÃ SP";
            colIDSP.DataPropertyName = "ID_SP";
            colIDSP.FillWeight = 12;
            colIDSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colIDSP.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colIDSP.DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            BangChiTietLoi.Columns.Add(colIDSP);

            // 3. Tên Sản Phẩm
            DataGridViewTextBoxColumn colTenHang = new DataGridViewTextBoxColumn();
            colTenHang.Name = "colTenHang";
            colTenHang.HeaderText = "TÊN SẢN PHẨM";
            colTenHang.DataPropertyName = "TenHang";
            colTenHang.FillWeight = 28;
            BangChiTietLoi.Columns.Add(colTenHang);

            // 4. Số Lượng Lỗi
            DataGridViewTextBoxColumn colSoLuong = new DataGridViewTextBoxColumn();
            colSoLuong.Name = "colSoLuong";
            colSoLuong.HeaderText = "SL LỖI";
            colSoLuong.DataPropertyName = "SoLuong";
            colSoLuong.FillWeight = 10;
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuong.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            BangChiTietLoi.Columns.Add(colSoLuong);

            // 5. Đơn Vị Tính
            DataGridViewTextBoxColumn colDVT = new DataGridViewTextBoxColumn();
            colDVT.Name = "colDVT";
            colDVT.HeaderText = "ĐVT";
            colDVT.DataPropertyName = "DonViTinh";
            colDVT.FillWeight = 10;
            colDVT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            BangChiTietLoi.Columns.Add(colDVT);

            // 6. Tình Trạng Lỗi Chi Tiết
            DataGridViewTextBoxColumn colTinhTrang = new DataGridViewTextBoxColumn();
            colTinhTrang.Name = "colTinhTrang";
            colTinhTrang.HeaderText = "CHI TIẾT LỖI / TÌNH TRẠNG";
            colTinhTrang.DataPropertyName = "TinhTrang";
            colTinhTrang.FillWeight = 28;
            BangChiTietLoi.Columns.Add(colTinhTrang);

            BangChiTietLoi.AllowUserToResizeColumns = true;
            BangChiTietLoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadThongTinYeuCau()
        {
            // PostgreSQL: Thay ISNULL thành COALESCE
            string query = @"
                SELECT 
                    YC.ID_YC,
                    YC.ID_DH,
                    YC.NgayYeuCau,
                    YC.LoaiYeuCau,
                    YC.TrangThai,
                    YC.MoTa,
                    KH.TenDoanhNghiep,
                    KH.NguoiDaiDien,
                    KH.SDT,
                    COALESCE(NV.TenNV, N'Chưa phân công') AS TenNV
                FROM YeuCauSauBanHang YC
                INNER JOIN KhachHang KH ON YC.ID_KH = KH.ID_KH
                LEFT JOIN NhanVien NV ON YC.ID_NV = NV.ID_NV
                WHERE YC.ID_YC = @ID_YC";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_YC", maYeuCau);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblMaYC.Text = reader["ID_YC"].ToString();
                                lblMaDH.Text = reader["ID_DH"].ToString();
                                lblNgayTao.Text = Convert.ToDateTime(reader["NgayYeuCau"]).ToString("dd/MM/yyyy HH:mm");
                                lblLoaiYC.Text = reader["LoaiYeuCau"].ToString();
                                lblTrangThai.Text = reader["TrangThai"].ToString();
                                lblTenKH.Text = reader["TenDoanhNghiep"].ToString();
                                lblNguoiLienHe.Text = reader["NguoiDaiDien"].ToString() + " - " + reader["SDT"].ToString();
                                lblNhanVien.Text = reader["TenNV"].ToString();
                                txtMoTaYeuCau.Text = reader["MoTa"].ToString();

                                string status = lblTrangThai.Text;
                                if (status == "Đã xử lý")
                                {
                                    lblTrangThai.ForeColor = Color.ForestGreen;
                                    btnXacNhanChuyenVC.Enabled = false;
                                }
                                else if (status == "Đang xử lý")
                                {
                                    lblTrangThai.ForeColor = Color.DarkOrange;
                                }
                                else
                                {
                                    lblTrangThai.ForeColor = Color.Crimson;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải thông tin yêu cầu: " + ex.Message, "Lỗi PostgreSQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDanhSachSanPhamLoi()
        {
            // PostgreSQL: Thay ISNULL thành COALESCE
            string query = @"
                SELECT 
                    CTYC.ID_CTYC,
                    CTYC.ID_SP,
                    HH.TenHang,
                    CTYC.SoLuong,
                    COALESCE(HH.DonViTinh, N'Thùng') AS DonViTinh,
                    CTYC.TinhTrang
                FROM ChiTietYeuCau CTYC
                INNER JOIN SanPham SP ON CTYC.ID_SP = SP.ID_SP
                INNER JOIN HangHoa HH ON SP.MaHang = HH.MaHang
                WHERE CTYC.ID_YC = @ID_YC";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_YC", maYeuCau);

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        BangChiTietLoi.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách sản phẩm lỗi: " + ex.Message, "Lỗi PostgreSQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================
        // XÁC NHẬN LỖI: CẬP NHẬT TRẠNG THÁI YÊU CẦU THÀNH "ĐANG XỬ LÝ"
        // =========================================================================
        private void btnXacNhanChuyenVC_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Bạn có chắc chắn muốn XÁC NHẬN LỖI cho yêu cầu này?",
                "Xác nhận xử lý lỗi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dr != DialogResult.Yes) return;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Cập nhật trạng thái Yêu cầu thành "Đang xử lý"
                    string updateQuery = "UPDATE YeuCauSauBanHang SET TrangThai = N'Đang xử lý' WHERE ID_YC = @ID_YC";
                    using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@ID_YC", maYeuCau);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đã xác nhận lỗi và yêu cầu bên vận chuyển xử lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại dữ liệu lên giao diện
                    LoadThongTinYeuCau();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật trạng thái yêu cầu: " + ex.Message, "Lỗi PostgreSQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
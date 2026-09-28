using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace ERP_Khach
{
    public partial class TaoYeuCauTraHang : Form
    {
        private string connectionString;
        private DataTable dtSanPhamDonHang;

        public TaoYeuCauTraHang()
        {
            InitializeComponent();

            var connectionSetting = ConfigurationManager.ConnectionStrings["ERP_Connection"];
            if (connectionSetting != null)
            {
                connectionString = connectionSetting.ConnectionString;
            }
        }

        private void TaoYeuCauTraHang_Load(object sender, EventArgs e)
        {
            KhoiTaoGiaoDien();
            LoadDanhSachDonHang();
        }

        private void KhoiTaoGiaoDien()
        {
            cboLoaiYeuCau.Items.Clear();
            cboLoaiYeuCau.Items.Add("Đổi trả hàng lỗi");
            cboLoaiYeuCau.Items.Add("Bảo hành sản phẩm");
            cboLoaiYeuCau.Items.Add("Khiếu nại giao thiếu/sai hàng");
            cboLoaiYeuCau.SelectedIndex = 0;

            // Cấu hình bảng sản phẩm chọn trả
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.Columns.Clear();

            // 1. Cột Checkbox chọn
            DataGridViewCheckBoxColumn colSelect = new DataGridViewCheckBoxColumn
            {
                Name = "colSelect",
                HeaderText = "CHỌN",
                Width = 50
            };
            dgvSanPham.Columns.Add(colSelect);

            // 2. Mã SP
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colID_SP",
                HeaderText = "MÃ SP",
                DataPropertyName = "ID_SP",
                ReadOnly = true,
                Width = 90
            });

            // 3. Tên SP
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenHang",
                HeaderText = "TÊN SẢN PHẨM",
                DataPropertyName = "TenHang",
                ReadOnly = true,
                Width = 220
            });

            // 4. Cột Số Lượng Mua Chuẩn (Chỉ đọc)
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongMua",
                HeaderText = "SL ĐÃ MUA",
                DataPropertyName = "SoLuongMua",
                ReadOnly = true,
                Width = 100
            });

            // 5. Cột Nhập Số Lượng Trả (Điền sẵn bằng SL Mua)
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongTra",
                HeaderText = "SL TRẢ/LỖI",
                DataPropertyName = "SoLuongTra",
                Width = 100
            });

            // 6. Cột Nhập Mô Tả Lỗi
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTinhTrangLoi",
                HeaderText = "MÔ TẢ LỖI SẢN PHẨM",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void LoadDanhSachDonHang()
        {
            if (string.IsNullOrEmpty(connectionString)) return;

            string query = @"
                SELECT DH.ID_DH, (DH.ID_DH || ' - ' || KH.TenDoanhNghiep) AS TenHienThi, DH.ID_KH
                FROM DonHang DH
                INNER JOIN KhachHang KH ON DH.ID_KH = KH.ID_KH
                ORDER BY DH.NgayTao DESC";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cboDonHang.DataSource = dt;
                        cboDonHang.DisplayMember = "TenHienThi";
                        cboDonHang.ValueMember = "ID_DH";
                        cboDonHang.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách đơn hàng: " + ex.Message, "Lỗi PostgreSQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDonHang.SelectedValue == null || cboDonHang.SelectedIndex == -1 || string.IsNullOrEmpty(connectionString)) return;

            string idDH = cboDonHang.SelectedValue.ToString();

            string query = @"
                SELECT 
                    SP.ID_SP,
                    HH.TenHang,
                    CTHD.SoLuong AS SoLuongMua,
                    CTHD.SoLuong AS SoLuongTra
                FROM ChiTietHoaDon CTHD
                INNER JOIN HoaDon HD ON CTHD.ID_HD = HD.ID_HD
                INNER JOIN SanPham SP ON CTHD.ID_SP = SP.ID_SP
                INNER JOIN HangHoa HH ON SP.MaHang = HH.MaHang
                WHERE HD.ID_DH = @ID_DH";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_DH", idDH);

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        dtSanPhamDonHang = new DataTable();
                        da.Fill(dtSanPhamDonHang);

                        dgvSanPham.DataSource = dtSanPhamDonHang;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải sản phẩm đơn hàng: " + ex.Message, "Lỗi PostgreSQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =======================================================
        // KIỂM TRA BẮT LỖI NGAY KHI NGƯỜI DÙNG NHẬP SỐ LƯỢNG TRẢ
        // =======================================================
        private void dgvSanPham_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSanPham.Columns[e.ColumnIndex].Name == "colSoLuongTra")
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                int slMua = Convert.ToInt32(row.Cells["colSoLuongMua"].Value ?? 0);
                string inputStr = row.Cells["colSoLuongTra"].Value?.ToString();

                if (!int.TryParse(inputStr, out int slTra) || slTra <= 0)
                {
                    MessageBox.Show("Số lượng trả phải là số nguyên lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells["colSoLuongTra"].Value = slMua; // Reset về số lượng mua ban đầu
                    return;
                }

                if (slTra > slMua)
                {
                    MessageBox.Show($"Số lượng trả ({slTra}) không được vượt quá số lượng đã mua ({slMua})!", "Cảnh báo giới hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells["colSoLuongTra"].Value = slMua; // Reset về tối đa cho phép
                }
            }
        }

        private void btnLuuYeuCau_Click(object sender, EventArgs e)
        {
            if (cboDonHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần tạo yêu cầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kết thúc chỉnh sửa cell dgv để cập nhật dữ liệu mới nhất
            dgvSanPham.EndEdit();

            bool hasSelected = false;

            // Kiểm tra tính hợp lệ của tất cả dòng được chọn trước khi lưu
            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["colSelect"].Value ?? false);
                if (isSelected)
                {
                    hasSelected = true;
                    int slMua = Convert.ToInt32(row.Cells["colSoLuongMua"].Value ?? 0);
                    int slTra = Convert.ToInt32(row.Cells["colSoLuongTra"].Value ?? 0);
                    string tenSP = row.Cells["colTenHang"].Value?.ToString();

                    if (slTra <= 0 || slTra > slMua)
                    {
                        MessageBox.Show($"Sản phẩm [{tenSP}] có số lượng trả ({slTra}) không hợp lệ! (Cho phép từ 1 đến {slMua})", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (!hasSelected)
            {
                MessageBox.Show("Vui lòng tích chọn ít nhất một sản phẩm bị lỗi/trả!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idDH = cboDonHang.SelectedValue.ToString();
            string loaiYC = cboLoaiYeuCau.Text;
            string moTaChung = txtMoTaChung.Text.Trim();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Lấy ID_KH từ đơn hàng
                    string getIDKH = "SELECT ID_KH FROM DonHang WHERE ID_DH = @ID_DH";
                    string idKH = "";
                    using (NpgsqlCommand cmdKH = new NpgsqlCommand(getIDKH, conn, transaction))
                    {
                        cmdKH.Parameters.AddWithValue("@ID_DH", idDH);
                        idKH = cmdKH.ExecuteScalar()?.ToString();
                    }

                    // 2. Sinh mã tự động YC001, YC002...
                    string getNextID = "SELECT COALESCE(MAX(CAST(SUBSTRING(ID_YC FROM 3 FOR 10) AS INT)), 0) + 1 FROM YeuCauSauBanHang";
                    int nextID;
                    using (NpgsqlCommand cmdNext = new NpgsqlCommand(getNextID, conn, transaction))
                    {
                        nextID = Convert.ToInt32(cmdNext.ExecuteScalar());
                    }
                    string idYC = "YC" + nextID.ToString("D3");

                    // 3. Insert vào YeuCauSauBanHang
                    string insertYC = @"
                        INSERT INTO YeuCauSauBanHang (ID_YC, ID_DH, ID_KH, NgayYeuCau, LoaiYeuCau, TrangThai, MoTa)
                        VALUES (@ID_YC, @ID_DH, @ID_KH, CURRENT_TIMESTAMP, @LoaiYeuCau, N'Chờ xử lý', @MoTa)";

                    using (NpgsqlCommand cmdYC = new NpgsqlCommand(insertYC, conn, transaction))
                    {
                        cmdYC.Parameters.AddWithValue("@ID_YC", idYC);
                        cmdYC.Parameters.AddWithValue("@ID_DH", idDH);
                        cmdYC.Parameters.AddWithValue("@ID_KH", idKH);
                        cmdYC.Parameters.AddWithValue("@LoaiYeuCau", loaiYC);
                        cmdYC.Parameters.AddWithValue("@MoTa", moTaChung);
                        cmdYC.ExecuteNonQuery();
                    }

                    // 4. Insert từng sản phẩm chọn vào ChiTietYeuCau
                    int ctycIndex = 1;
                    foreach (DataGridViewRow row in dgvSanPham.Rows)
                    {
                        bool isSelected = Convert.ToBoolean(row.Cells["colSelect"].Value ?? false);
                        if (isSelected)
                        {
                            string idSP = row.Cells["colID_SP"].Value?.ToString();
                            int slTra = Convert.ToInt32(row.Cells["colSoLuongTra"].Value);
                            string tinhTrangLoi = row.Cells["colTinhTrangLoi"].Value?.ToString() ?? "Lỗi từ phía khách hàng phản hồi";

                            string idCTYC = "CTYC" + nextID.ToString("D3") + "_" + ctycIndex++;

                            string insertCTYC = @"
                                INSERT INTO ChiTietYeuCau (ID_CTYC, ID_YC, ID_SP, SoLuong, TinhTrang)
                                VALUES (@ID_CTYC, @ID_YC, @ID_SP, @SoLuong, @TinhTrang)";

                            using (NpgsqlCommand cmdCT = new NpgsqlCommand(insertCTYC, conn, transaction))
                            {
                                cmdCT.Parameters.AddWithValue("@ID_CTYC", idCTYC);
                                cmdCT.Parameters.AddWithValue("@ID_YC", idYC);
                                cmdCT.Parameters.AddWithValue("@ID_SP", idSP);
                                cmdCT.Parameters.AddWithValue("@SoLuong", slTra);
                                cmdCT.Parameters.AddWithValue("@TinhTrang", tinhTrangLoi);
                                cmdCT.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show($"Tạo phiếu yêu cầu thành công! Mã yêu cầu: [{idYC}]", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi khi tạo yêu cầu trả hàng: " + ex.Message, "Lỗi PostgreSQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Data;
using Npgsql;
using System.Drawing;
using System.Windows.Forms;
using ERP.DAL;

namespace ERP
{
    public partial class QuanLyDiemVanChuyen : Form
    {
        // Chuỗi kết nối CSDL lấy từ cấu hình
        private string connectionString = DatabaseConfig.GetConnectionString();
        private DataTable dtDiemVC;

        public QuanLyDiemVanChuyen()
        {
            InitializeComponent();
        }

        private FlowLayoutPanel pnlKpiContainer;
        private Label lblKpiTotalVal, lblKpiHubVal, lblKpiDeliveryVal, lblKpiContactVal;

        private void QuanLyDiemVanChuyen_Load(object sender, EventArgs e)
        {
            // this.WindowState = FormWindowState.Maximized;

            // Áp dụng chuẩn hóa giao diện UI/UX Pro Max
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplySidebar(this.pnlSidebar, this.btnQuanLyDiemVanChuyen, this);
            UIThemeHelper.ApplyModernTopHeader(this.pnlTopHeader, "ERP Logistics", "Quản lý điểm vận chuyển", this);
            UIThemeHelper.ApplyActionButton(this.btnAdd, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnEdit, ButtonRole.Secondary);
            UIThemeHelper.ApplyActionButton(this.btnDelete, ButtonRole.Danger);

            // Thiết lập Filter Bar dạng Card hiện đại chuẩn UI/UX Pro Max
            UIThemeHelper.SetupModernFilterCard(
                this.pnlActionTool,
                this.txtSearch,
                320,
                () => {
                    txtSearch.Text = "🔍 Tìm kiếm theo Mã điểm, Tên điểm, Địa chỉ...";
                    txtSearch.ForeColor = Color.Gray;
                    LocDuLieu();
                }
            );

            InitKpiPanel();

            KhoiTaoCotBang();
            LoadDataDiemVanChuyen();
        }

        private void InitKpiPanel()
        {
            if (pnlKpiContainer != null) return;

            pnlKpiContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 10),
                WrapContents = false,
                AutoScroll = true
            };

            var card1 = UIThemeHelper.CreateKpiCard("TỔNG ĐIỂM VC", "0", "Mạng lưới phân phối", Color.FromArgb(37, 99, 235));
            lblKpiTotalVal = card1.Controls[1].Controls[0] as Label;

            var card2 = UIThemeHelper.CreateKpiCard("ĐIỂM KHO VẬN TẢI", "0", "Kho tổng & Trung chuyển", Color.FromArgb(22, 163, 74));
            lblKpiHubVal = card2.Controls[1].Controls[0] as Label;

            var card3 = UIThemeHelper.CreateKpiCard("ĐIỂM GIAO NHẬN", "0", "Đại lý & Khách hàng", Color.FromArgb(14, 165, 233));
            lblKpiDeliveryVal = card3.Controls[1].Controls[0] as Label;

            var card4 = UIThemeHelper.CreateKpiCard("ĐẠI DIỆN LIÊN HỆ", "0", "Người phụ trách", Color.FromArgb(217, 119, 6));
            lblKpiContactVal = card4.Controls[1].Controls[0] as Label;

            pnlKpiContainer.Controls.Add(card1);
            pnlKpiContainer.Controls.Add(card2);
            pnlKpiContainer.Controls.Add(card3);
            pnlKpiContainer.Controls.Add(card4);

            pnlMainContent.Controls.Add(pnlKpiContainer);
            pnlKpiContainer.SendToBack();
            pnlActionTool.BringToFront();
            dgvData.BringToFront();
        }

        private void UpdateKpiMetrics()
        {
            try
            {
                int total = dtDiemVC != null ? dtDiemVC.Rows.Count : 0;
                int hub = 0;
                int contact = 0;

                if (dtDiemVC != null)
                {
                    foreach (DataRow row in dtDiemVC.Rows)
                    {
                        string ten = row["TenDVC"]?.ToString() ?? "";
                        if (ten.IndexOf("kho", StringComparison.OrdinalIgnoreCase) >= 0 || ten.IndexOf("tổng", StringComparison.OrdinalIgnoreCase) >= 0 || ten.IndexOf("hub", StringComparison.OrdinalIgnoreCase) >= 0)
                            hub++;

                        string dd = row["TenNguoiDaiDien"]?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(dd))
                            contact++;
                    }
                }

                if (lblKpiTotalVal != null) lblKpiTotalVal.Text = total.ToString();
                if (lblKpiHubVal != null) lblKpiHubVal.Text = (hub > 0 ? hub : 1).ToString();
                if (lblKpiDeliveryVal != null) lblKpiDeliveryVal.Text = (total - (hub > 0 ? hub : 1) >= 0 ? total - (hub > 0 ? hub : 1) : 0).ToString();
                if (lblKpiContactVal != null) lblKpiContactVal.Text = contact.ToString();
            }
            catch { }
        }

        private void KhoiTaoCotBang()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            // 1. Mã Điểm Vận Chuyển (MaDVC)
            DataGridViewTextBoxColumn colID = new DataGridViewTextBoxColumn();
            colID.Name = "colMaDVC";
            colID.HeaderText = "MÃ ĐIỂM VC";
            colID.DataPropertyName = "MaDVC";
            colID.FillWeight = 15;
            colID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colID.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colID.DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            dgvData.Columns.Add(colID);

            // 2. Tên Điểm Vận Chuyển (TenDVC)
            DataGridViewTextBoxColumn colTen = new DataGridViewTextBoxColumn();
            colTen.Name = "colTenDVC";
            colTen.HeaderText = "TÊN ĐIỂM VẬN CHUYỂN";
            colTen.DataPropertyName = "TenDVC";
            colTen.FillWeight = 25;
            colTen.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colTen);

            // 3. Địa Chỉ Chi Tiết (DiaChiDVC)
            DataGridViewTextBoxColumn colDiaChi = new DataGridViewTextBoxColumn();
            colDiaChi.Name = "colDiaChiDVC";
            colDiaChi.HeaderText = "ĐỊA CHỈ CHI TIẾT";
            colDiaChi.DataPropertyName = "DiaChiDVC";
            colDiaChi.FillWeight = 30;
            colDiaChi.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colDiaChi);

            // 4. Số Điện Thoại (SDT_DVC)
            DataGridViewTextBoxColumn colSDT = new DataGridViewTextBoxColumn();
            colSDT.Name = "colSDT_DVC";
            colSDT.HeaderText = "SỐ ĐIỆN THOẠI";
            colSDT.DataPropertyName = "SDT_DVC";
            colSDT.FillWeight = 15;
            colSDT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSDT.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colSDT);

            // 5. Người Đại Diện (TenNguoiDaiDien)
            DataGridViewTextBoxColumn colNguoiDD = new DataGridViewTextBoxColumn();
            colNguoiDD.Name = "colTenNguoiDaiDien";
            colNguoiDD.HeaderText = "NGƯỜI ĐẠI DIỆN";
            colNguoiDD.DataPropertyName = "TenNguoiDaiDien";
            colNguoiDD.FillWeight = 15;
            colNguoiDD.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colNguoiDD);

            dgvData.AllowUserToResizeColumns = true;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Áp dụng định dạng bảng dữ liệu Data-Dense theo chuẩn UI/UX Pro Max
            UIThemeHelper.ApplyModernGridStyle(dgvData);
        }

        private void LoadDataDiemVanChuyen()
        {
            // Truy vấn đúng tên 5 cột trong bảng DiemVanChuyen
            string query = "SELECT MaDVC, TenDVC, DiaChiDVC, SDT_DVC, TenNguoiDaiDien FROM DiemVanChuyen ORDER BY MaDVC DESC";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                    dtDiemVC = new DataTable();
                    da.Fill(dtDiemVC);

                    dgvData.DataSource = dtDiemVC;
                    UpdateKpiMetrics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu điểm vận chuyển: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==========================================
        // TÌM KIẾM VÀ LỌC DỮ LIỆU
        // ==========================================

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LocDuLieu();
        }

        private void LocDuLieu()
        {
            if (dtDiemVC == null) return;

            string keyword = txtSearch.Text.Trim().Replace("'", "''");
            if (keyword == "🔍 Tìm kiếm theo Mã điểm, Tên điểm, Địa chỉ...") keyword = "";

            DataView dv = dtDiemVC.DefaultView;
            string filter = "1=1";

            if (!string.IsNullOrEmpty(keyword))
            {
                filter += $" AND (MaDVC LIKE '%{keyword}%' OR TenDVC LIKE '%{keyword}%' OR DiaChiDVC LIKE '%{keyword}%' OR SDT_DVC LIKE '%{keyword}%' OR TenNguoiDaiDien LIKE '%{keyword}%')";
            }

            dv.RowFilter = filter;
            dgvData.DataSource = dv;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Tìm kiếm theo Mã điểm, Tên điểm, Địa chỉ...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Tìm kiếm theo Mã điểm, Tên điểm, Địa chỉ...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        // ==========================================
        // THÊM, SỬA, XÓA ĐIỂM VẬN CHUYỂN
        // ==========================================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmThemDiemVanChuyen frmThem = new FrmThemDiemVanChuyen();

            // Nếu thêm dữ liệu thành công (DialogResult.OK), hệ thống sẽ tự động load lại bảng dữ liệu
            if (frmThem.ShowDialog() == DialogResult.OK)
            {
                LoadDataDiemVanChuyen();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string maDVC = dgvData.CurrentRow.Cells["colMaDVC"].Value?.ToString();

                if (!string.IsNullOrEmpty(maDVC))
                {
                    // Gọi Form Sửa Điểm Vận Chuyển
                    FrmSuaDiemVanChuyen frmSua = new FrmSuaDiemVanChuyen(maDVC);

                    // Nếu bấm Lưu thành công, load lại danh sách
                    if (frmSua.ShowDialog() == DialogResult.OK)
                    {
                        LoadDataDiemVanChuyen();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Điểm vận chuyển cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào trên bảng chưa
            if (dgvData.CurrentRow != null)
            {
                // Lấy Mã DVC và Tên DVC từ dòng đang chọn
                string maDVC = dgvData.CurrentRow.Cells["colMaDVC"].Value?.ToString();
                string tenDVC = dgvData.CurrentRow.Cells["colTenDVC"].Value?.ToString();

                // 2. Hiển thị hộp thoại xác nhận trước khi xóa
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa điểm vận chuyển '{tenDVC} ({maDVC})' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM DiemVanChuyen WHERE MaDVC = @MaDVC";

                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@MaDVC", maDVC);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa điểm vận chuyển thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataDiemVanChuyen();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy điểm vận chuyển cần xóa trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (PostgresException ex)
                        {
                            // Lỗi mã 547: Bị vướng khóa ngoại (Foreign Key)
                            if ((ex.SqlState == "23503" || ex.SqlState == "23514"))
                            {
                                MessageBox.Show("Không thể xóa điểm vận chuyển này vì đã phát sinh dữ liệu liên quan trong hệ thống.", "Lỗi ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Điểm vận chuyển cần xóa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // ĐIỀU HƯỚNG SIDEBAR - PHÂN HỆ VẬN CHUYỂN
        // ==========================================

        private void btnQuanLyXe_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyXe>(this);
        }

        private void btnQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyNhaCungCap>(this);
        }

        private void btnQuanLyTraHang_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyTraHang>(this);
        }

        private void btnQuanLyDonVanChuyen_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyDonVanChuyen>(this);
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            LogisticsHelper.DangXuat(this);
        }
    }
}



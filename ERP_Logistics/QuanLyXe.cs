using System;
using System.Data;
using Npgsql;
using System.Drawing;
using System.Windows.Forms;
using ERP.DAL;

namespace ERP
{
    public partial class QuanLyXe : Form, IRefreshableForm
    {
        private string connectionString = DatabaseConfig.GetConnectionString();
        private DataTable dtXe;
        private readonly ERP.BLL.DonVanChuyenBLL donVanChuyenBLL = new ERP.BLL.DonVanChuyenBLL();

        public QuanLyXe()
        {
            InitializeComponent();
        }

        private FlowLayoutPanel pnlKpiContainer;
        private Label lblKpiTotalVal, lblKpiFreeVal, lblKpiBusyVal, lblKpiMaintVal;

        public void LamMoiDuLieu()
        {
            try
            {
                LoadDataXe();
                LoadTrangThaiFilter();
            }
            catch { }
        }

        private void QuanLyXe_Load(object sender, EventArgs e)
        {
            // Không cần set WindowState ở đây - đầu mục sẽ được đặt ngay khi mở form
            // this.WindowState = FormWindowState.Maximized;

            // Áp dụng chuẩn hóa giao diện UI/UX Pro Max
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplySidebar(this.pnlSidebar, this.btnQuanLyXe, this);
            UIThemeHelper.ApplyModernTopHeader(this.pnlTopHeader, "ERP Logistics", "Quản lý xe", this);
            UIThemeHelper.ApplyActionButton(this.btnAdd, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnEdit, ButtonRole.Secondary);
            UIThemeHelper.ApplyActionButton(this.btnDelete, ButtonRole.Danger);

            // Thiết lập Filter Bar dạng Card hiện đại chuẩn UI/UX Pro Max
            UIThemeHelper.SetupModernFilterCard(
                this.pnlActionTool,
                this.txtSearch,
                280,
                () => {
                    txtSearch.Text = "🔍 Tìm kiếm phương tiện, tài xế...";
                    txtSearch.ForeColor = Color.Gray;
                    if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
                    LamMoiDuLieu();
                },
                new FilterItem("Trạng thái:", this.cboStatus, 160)
            );

            // Đăng ký sự kiện khi form được hiển thị trở lại từ Hide()
            this.VisibleChanged += (s, ev) =>
            {
                if (this.Visible && !this.IsDisposed)
                {
                    LamMoiDuLieu();
                }
            };

            // Đổ dữ liệu lên các filter từ database
            LoadTrangThaiFilter();

            InitKpiPanel();

            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;

            KhoiTaoCotBang();
            LoadDataXe();
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

            var card1 = UIThemeHelper.CreateKpiCard("TỔNG PHƯƠNG TIỆN", "0", "Đội xe vận tải", Color.FromArgb(37, 99, 235));
            lblKpiTotalVal = card1.Controls[1].Controls[0] as Label;

            var card2 = UIThemeHelper.CreateKpiCard("XE ĐANG RẢNH", "0", "Sẵn sàng điều phối", Color.FromArgb(22, 163, 74));
            lblKpiFreeVal = card2.Controls[1].Controls[0] as Label;

            var card3 = UIThemeHelper.CreateKpiCard("ĐANG VẬN CHUYỂN", "0", "Đang trên lộ trình", Color.FromArgb(14, 165, 233));
            lblKpiBusyVal = card3.Controls[1].Controls[0] as Label;

            var card4 = UIThemeHelper.CreateKpiCard("ĐANG BẢO TRÌ", "0", "Bảo dưỡng định kỳ", Color.FromArgb(217, 119, 6));
            lblKpiMaintVal = card4.Controls[1].Controls[0] as Label;

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
                int total = dtXe != null ? dtXe.Rows.Count : 0;
                int free = 0;
                int busy = 0;
                int maint = 0;

                if (dtXe != null)
                {
                    foreach (DataRow row in dtXe.Rows)
                    {
                        string tt = row["TrangThaiXe"]?.ToString() ?? "";
                        if (tt.IndexOf("rảnh", StringComparison.OrdinalIgnoreCase) >= 0 || tt.IndexOf("sẵn sàng", StringComparison.OrdinalIgnoreCase) >= 0)
                            free++;
                        else if (tt.IndexOf("vận chuyển", StringComparison.OrdinalIgnoreCase) >= 0 || tt.IndexOf("đang giao", StringComparison.OrdinalIgnoreCase) >= 0)
                            busy++;
                        else if (tt.IndexOf("bảo trì", StringComparison.OrdinalIgnoreCase) >= 0 || tt.IndexOf("sửa", StringComparison.OrdinalIgnoreCase) >= 0)
                            maint++;
                    }
                }

                if (lblKpiTotalVal != null) lblKpiTotalVal.Text = total.ToString();
                if (lblKpiFreeVal != null) lblKpiFreeVal.Text = free.ToString();
                if (lblKpiBusyVal != null) lblKpiBusyVal.Text = busy.ToString();
                if (lblKpiMaintVal != null) lblKpiMaintVal.Text = maint.ToString();
            }
            catch { }
        }

        private void KhoiTaoCotBang()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            // 1. Biển Số Xe (Khóa chính BienSoXe)
            DataGridViewTextBoxColumn colBienSo = new DataGridViewTextBoxColumn();
            colBienSo.Name = "colBienSoXe";
            colBienSo.HeaderText = "BIỂN SỐ XE";
            colBienSo.DataPropertyName = "BienSoXe";
            colBienSo.FillWeight = 15;
            colBienSo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colBienSo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colBienSo.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colBienSo.DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            dgvData.Columns.Add(colBienSo);

            // 2. Loại Xe (LoaiXe)
            DataGridViewTextBoxColumn colLoaiXe = new DataGridViewTextBoxColumn();
            colLoaiXe.Name = "colLoaiXe";
            colLoaiXe.HeaderText = "LOẠI XE";
            colLoaiXe.DataPropertyName = "LoaiXe";
            colLoaiXe.FillWeight = 18;
            colLoaiXe.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colLoaiXe);

            // 3. Tải Trọng (TaiTrong)
            DataGridViewTextBoxColumn colTaiTrong = new DataGridViewTextBoxColumn();
            colTaiTrong.Name = "colTaiTrong";
            colTaiTrong.HeaderText = "TẢI TRỌNG (TẤN)";
            colTaiTrong.DataPropertyName = "TaiTrong";
            colTaiTrong.FillWeight = 15;
            colTaiTrong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTaiTrong.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTaiTrong.DefaultCellStyle.Format = "N2";
            dgvData.Columns.Add(colTaiTrong);

            // 4. Tên Tài Xế (TenTaiXe)
            DataGridViewTextBoxColumn colTaiXe = new DataGridViewTextBoxColumn();
            colTaiXe.Name = "colTenTaiXe";
            colTaiXe.HeaderText = "TÊN TÀI XẾ";
            colTaiXe.DataPropertyName = "TenTaiXe";
            colTaiXe.FillWeight = 22;
            colTaiXe.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colTaiXe);

            // 5. Trạng Thái Xe (TrangThaiXe)
            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
            colTrangThai.Name = "colTrangThaiXe";
            colTrangThai.HeaderText = "TRẠNG THÁI";
            colTrangThai.DataPropertyName = "TrangThaiXe";
            colTrangThai.FillWeight = 18;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTrangThai.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTrangThai.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvData.Columns.Add(colTrangThai);

            // 6. Kích Hoạt (KichHoat - Hiển thị dạng CheckBox hoặc Text)
            DataGridViewCheckBoxColumn colKichHoat = new DataGridViewCheckBoxColumn();
            colKichHoat.Name = "colKichHoat";
            colKichHoat.HeaderText = "KÍCH HOẠT";
            colKichHoat.DataPropertyName = "KichHoat";
            colKichHoat.FillWeight = 12;
            colKichHoat.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colKichHoat.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colKichHoat);

            dgvData.AllowUserToResizeColumns = true;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Áp dụng chuẩn bảng biểu UI/UX Pro Max
            UIThemeHelper.ApplyModernGridStyle(dgvData);
        }

        // Đổ dữ liệu trạng thái xe lên filter combobox từ database
        private void LoadTrangThaiFilter()
        {
            string query = "SELECT DISTINCT TrangThaiXe FROM PhuongTien WHERE TrangThaiXe IS NOT NULL ORDER BY TrangThaiXe ASC";

            cboStatus.Items.Clear();
            cboStatus.Items.Add("Tất cả trạng thái"); // Item mặc định

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string trangThai = reader["TrangThaiXe"].ToString();
                            cboStatus.Items.Add(trangThai);
                        }
                    }

                    if (cboStatus.Items.Count > 0)
                        cboStatus.SelectedIndex = 0; // Chọn "Tất cả" mặc định
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách trạng thái: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDataXe()
        {
            // Tự động đồng bộ trạng thái toàn bộ phương tiện theo thực tế hoạt động
            try
            {
                donVanChuyenBLL.DongBoTrangThaiXeToanHeThong();
            }
            catch { }

            // Truy vấn lấy dữ liệu chính xác từ bảng PhuongTien
            string query = "SELECT BienSoXe, LoaiXe, TaiTrong, TenTaiXe, TrangThaiXe, KichHoat FROM PhuongTien ORDER BY BienSoXe ASC";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                    dtXe = new DataTable();
                    da.Fill(dtXe);

                    dgvData.DataSource = dtXe;
                    UpdateKpiMetrics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách phương tiện: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocDuLieu();
        }

        private void LocDuLieu()
        {
            if (dtXe == null) return;

            string keyword = txtSearch.Text.Trim().Replace("'", "''");
            if (keyword == "🔍 Tìm kiếm chủ xe, biển số, SĐT..." || keyword == "🔍 Tìm kiếm phương tiện, tài xế...") keyword = "";

            DataView dv = dtXe.DefaultView;
            string filter = "1=1";

            if (!string.IsNullOrEmpty(keyword))
            {
                filter += $" AND (BienSoXe LIKE '%{keyword}%' OR LoaiXe LIKE '%{keyword}%' OR TenTaiXe LIKE '%{keyword}%')";
            }

            // Lọc theo trạng thái xe
            if (cboStatus.SelectedIndex > 0)
            {
                string selectedStatus = cboStatus.SelectedItem.ToString();
                filter += $" AND TrangThaiXe = '{selectedStatus}'";
            }

            dv.RowFilter = filter;
            dgvData.DataSource = dv;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Tìm kiếm chủ xe, biển số, SĐT..." || txtSearch.Text == "🔍 Tìm kiếm phương tiện, tài xế...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Tìm kiếm phương tiện, tài xế...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        // ==========================================
        // THÊM, SỬA, XÓA PHƯƠNG TIỆN
        // ==========================================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmThemPhuongTien frmThem = new FrmThemPhuongTien();

            // Nếu thêm dữ liệu thành công (DialogResult.OK), hệ thống sẽ tự động load lại bảng dữ liệu
            if (frmThem.ShowDialog() == DialogResult.OK)
            {
                LoadDataXe();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string bienSo = dgvData.CurrentRow.Cells["colBienSoXe"].Value?.ToString();

                if (!string.IsNullOrEmpty(bienSo))
                {
                    // Gọi Form Sửa Phương Tiện và truyền biển số xe vào
                    FrmSuaPhuongTien frmSua = new FrmSuaPhuongTien(bienSo);

                    // Nếu cập nhật thành công, load lại danh sách phương tiện
                    if (frmSua.ShowDialog() == DialogResult.OK)
                    {
                        LoadDataXe();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn xe cần chỉnh sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào trên bảng chưa
            if (dgvData.CurrentRow != null)
            {
                // Lấy Biển số xe từ dòng đang chọn
                string bienSoXe = dgvData.CurrentRow.Cells["colBienSoXe"].Value?.ToString();
                string loaiXe = dgvData.CurrentRow.Cells["colLoaiXe"].Value?.ToString();

                // 2. Hiển thị hộp thoại xác nhận trước khi xóa
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa phương tiện biển số '{bienSoXe}' ({loaiXe}) không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM PhuongTien WHERE BienSoXe = @BienSoXe";

                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@BienSoXe", bienSoXe);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa phương tiện thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataXe();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy phương tiện cần xóa trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (PostgresException ex)
                        {
                            // Lỗi mã 547: Bị vướng khóa ngoại (Foreign Key) nếu phương tiện này đã được gán vào đơn vận chuyển hoặc bảng khác
                            if ((ex.SqlState == "23503" || ex.SqlState == "23514"))
                            {
                                MessageBox.Show("Không thể xóa phương tiện này vì đã phát sinh dữ liệu liên quan (như đơn vận chuyển) trong hệ thống.", "Lỗi ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Vui lòng chọn phương tiện cần xóa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // ĐIỀU HƯỚNG SIDEBAR
        // ==========================================

        private void btnQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyNhaCungCap>(this);
        }

        private void btnQuanLyDiemVanChuyen_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyDiemVanChuyen>(this);
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



using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ERP.BLL;
using ERP.DTO;

namespace ERP
{
    public partial class QuanLyTraHang : Form, IRefreshableForm
    {
        private readonly PhieuTraHangBLL bll = new PhieuTraHangBLL();

        public QuanLyTraHang()
        {
            InitializeComponent();
        }

        private FlowLayoutPanel pnlKpiContainer;
        private Label lblKpiTotalVal, lblKpiPendingVal, lblKpiDoneVal, lblKpiCanceledVal;

        public void LamMoiDuLieu()
        {
            try
            {
                LoadDataPhieuTraHang();
            }
            catch { }
        }

        private void QuanLyTraHang_Load(object sender, EventArgs e)
        {
            // this.WindowState = FormWindowState.Maximized;

            // Áp dụng chuẩn hóa giao diện UI/UX Pro Max
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplySidebar(this.pnlSidebar, this.btnQuanLyTraHang, this);
            UIThemeHelper.ApplyModernTopHeader(this.pnlTopHeader, "ERP Logistics", "Quản lý trả hàng", this);
            UIThemeHelper.ApplyActionButton(this.btnAdd, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnEdit, ButtonRole.Secondary);
            UIThemeHelper.ApplyActionButton(this.btnDelete, ButtonRole.Danger);
            UIThemeHelper.ApplyActionButton(this.btnUpdateStatus, ButtonRole.Edit);

            // Thiết lập Filter Bar dạng Card hiện đại chuẩn UI/UX Pro Max (tránh đè chồng, nhãn rõ ràng)
            UIThemeHelper.SetupModernFilterCard(
                this.pnlActionTool,
                this.txtSearch,
                260,
                () => {
                    txtSearch.Text = "🔍 Tìm kiếm theo Mã phiếu, Biển số xe, Mã CTYC, Tên ĐVC...";
                    txtSearch.ForeColor = Color.Gray;
                    dtpTuNgay.Checked = false;
                    dtpDenNgay.Checked = false;
                    if (cmbTrangThai.Items.Count > 0) cmbTrangThai.SelectedIndex = 0;
                    LamMoiDuLieu();
                },
                new FilterItem("Từ ngày:", this.dtpTuNgay, 120),
                new FilterItem("Đến:", this.dtpDenNgay, 120),
                new FilterItem("Trạng thái:", this.cmbTrangThai, 180)
            );

            // Tự động làm mới dữ liệu khi form hiển thị lại từ Hide()
            this.VisibleChanged += (s, ev) =>
            {
                if (this.Visible && !this.IsDisposed)
                {
                    LamMoiDuLieu();
                }
            };

            InitKpiPanel();
            KhoiTaoCotBang();

            LoadComboBoxData();
            LoadDataPhieuTraHang();
            dtpTuNgay.ValueChanged += (s, ev) => TimKiem();
            dtpDenNgay.ValueChanged += (s, ev) => TimKiem();
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

            var card1 = UIThemeHelper.CreateKpiCard("TỔNG PHIẾU TRẢ HÀNG", "0", "Toàn hệ thống", Color.FromArgb(37, 99, 235));
            lblKpiTotalVal = card1.Controls[1].Controls[0] as Label;

            var card2 = UIThemeHelper.CreateKpiCard("CHỜ XỬ LÝ", "0", "Cần kiểm tra/nhập kho", Color.FromArgb(217, 119, 6));
            lblKpiPendingVal = card2.Controls[1].Controls[0] as Label;

            var card3 = UIThemeHelper.CreateKpiCard("ĐÃ XỬ LÝ / NHẬP KHO", "0", "Hoàn tất kiểm định", Color.FromArgb(22, 163, 74));
            lblKpiDoneVal = card3.Controls[1].Controls[0] as Label;

            var card4 = UIThemeHelper.CreateKpiCard("TỪ CHỐI / HỦY", "0", "Không hợp lệ", Color.FromArgb(220, 38, 38));
            lblKpiCanceledVal = card4.Controls[1].Controls[0] as Label;

            pnlKpiContainer.Controls.Add(card1);
            pnlKpiContainer.Controls.Add(card2);
            pnlKpiContainer.Controls.Add(card3);
            pnlKpiContainer.Controls.Add(card4);

            pnlMainContent.Controls.Add(pnlKpiContainer);
            pnlKpiContainer.SendToBack();
            pnlActionTool.BringToFront();
            dgvData.BringToFront();
        }

        private void UpdateKpiMetrics(List<PhieuTraHang> list)
        {
            try
            {
                int total = list != null ? list.Count : 0;
                int pending = 0;
                int done = 0;
                int canceled = 0;

                if (list != null)
                {
                    foreach (var p in list)
                    {
                        string tt = p.TrangThai ?? "";
                        if (tt.IndexOf("chờ", StringComparison.OrdinalIgnoreCase) >= 0 || tt.IndexOf("mới", StringComparison.OrdinalIgnoreCase) >= 0)
                            pending++;
                        else if (tt.IndexOf("nhập kho", StringComparison.OrdinalIgnoreCase) >= 0 || (tt.IndexOf("xử lý", StringComparison.OrdinalIgnoreCase) >= 0 && tt.IndexOf("chờ", StringComparison.OrdinalIgnoreCase) < 0) || tt.IndexOf("hoàn thành", StringComparison.OrdinalIgnoreCase) >= 0 || tt.IndexOf("duyệt", StringComparison.OrdinalIgnoreCase) >= 0)
                            done++;
                        else if (tt.IndexOf("từ chối", StringComparison.OrdinalIgnoreCase) >= 0 || tt.IndexOf("hủy", StringComparison.OrdinalIgnoreCase) >= 0)
                            canceled++;
                    }
                }

                if (lblKpiTotalVal != null) lblKpiTotalVal.Text = total.ToString();
                if (lblKpiPendingVal != null) lblKpiPendingVal.Text = pending.ToString();
                if (lblKpiDoneVal != null) lblKpiDoneVal.Text = done.ToString();
                if (lblKpiCanceledVal != null) lblKpiCanceledVal.Text = canceled.ToString();
            }
            catch { }
        }

        private void KhoiTaoCotBang()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn colIDPT = new DataGridViewTextBoxColumn();
            colIDPT.Name = "colID_PhieuTra";
            colIDPT.HeaderText = "MÃ PHIẾU TRẢ";
            colIDPT.DataPropertyName = "ID_PhieuTra";
            colIDPT.FillWeight = 16;
            colIDPT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colIDPT.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colIDPT.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colIDPT.DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            dgvData.Columns.Add(colIDPT);

            DataGridViewTextBoxColumn colNgayTra = new DataGridViewTextBoxColumn();
            colNgayTra.Name = "colNgayTra";
            colNgayTra.HeaderText = "NGÀY TRẢ";
            colNgayTra.DataPropertyName = "NgayTra";
            colNgayTra.FillWeight = 20;
            colNgayTra.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayTra.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayTra.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvData.Columns.Add(colNgayTra);

            // 3. Mã Điểm Vận Chuyển (MaDVC)
            DataGridViewTextBoxColumn colMaDVC = new DataGridViewTextBoxColumn();
            colMaDVC.Name = "colMaDVC";
            colMaDVC.HeaderText = "MÃ ĐIỂM VC";
            colMaDVC.DataPropertyName = "MaDVC";
            colMaDVC.FillWeight = 18;
            colMaDVC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaDVC.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colMaDVC);

            // 3.5. Biển Số Xe (BienSoXe)
            DataGridViewTextBoxColumn colBienSoXe = new DataGridViewTextBoxColumn();
            colBienSoXe.Name = "colBienSoXe";
            colBienSoXe.HeaderText = "BIỂN SỐ XE";
            colBienSoXe.DataPropertyName = "BienSoXe";
            colBienSoXe.FillWeight = 18;
            colBienSoXe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colBienSoXe.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colBienSoXe);

            // 4. Mã Chi Tiết Yêu Cầu (ID_CTYC)
            DataGridViewTextBoxColumn colIDCTYC = new DataGridViewTextBoxColumn();
            colIDCTYC.Name = "colID_CTYC";
            colIDCTYC.HeaderText = "MÃ CHI TIẾT YC";
            colIDCTYC.DataPropertyName = "ID_CTYC";
            colIDCTYC.FillWeight = 20;
            colIDCTYC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colIDCTYC.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colIDCTYC);

            // 5. Trạng Thái (TrangThai)
            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
            colTrangThai.Name = "colTrangThai";
            colTrangThai.HeaderText = "TRẠNG THÁI";
            colTrangThai.DataPropertyName = "TrangThai";
            colTrangThai.FillWeight = 18;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTrangThai.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTrangThai.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvData.Columns.Add(colTrangThai);

            dgvData.AllowUserToResizeColumns = true;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Áp dụng định dạng bảng dữ liệu Data-Dense theo chuẩn UI/UX Pro Max
            UIThemeHelper.ApplyModernGridStyle(dgvData);
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Đổ data vào ComboBox bộ lọc trạng thái
                string[] dsTrangThai = { "Chờ xử lý", "Đang thu hồi", "Đã xử lý", "Đã hủy" };
                cmbTrangThai.Items.Clear();
                cmbTrangThai.Items.Add("Tất cả trạng thái");
                cmbTrangThai.Items.AddRange(dsTrangThai);
                cmbTrangThai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataPhieuTraHang()
        {
            try
            {
                List<PhieuTraHang> list = bll.GetAll();
                BindData(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phiếu trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindData(List<PhieuTraHang> list)
        {
            dgvData.DataSource = null;
            dgvData.DataSource = list;

            // Cập nhật số liệu hiển thị trên thẻ KPI
            UpdateKpiMetrics(list);

            // Định dạng màu sắc trạng thái
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Cells["colTrangThai"].Value != null)
                {
                    string tt = row.Cells["colTrangThai"].Value.ToString();
                    if (tt == "Chờ xử lý")
                    {
                        row.Cells["colTrangThai"].Style.ForeColor = Color.FromArgb(255, 152, 0); // Cam
                    }
                    else if (tt == "Đang thu hồi" || tt == "Đang lấy hàng")
                    {
                        row.Cells["colTrangThai"].Style.ForeColor = Color.FromArgb(13, 110, 253); // Xanh dương
                    }
                    else if (tt == "Đã xử lý" || tt == "Đã nhập kho")
                    {
                        row.Cells["colTrangThai"].Style.ForeColor = Color.FromArgb(40, 167, 69); // Xanh lá
                    }
                    else if (tt == "Đã hủy" || tt == "Từ chối")
                    {
                        row.Cells["colTrangThai"].Style.ForeColor = Color.FromArgb(220, 53, 69); // Đỏ
                    }
                }
            }
        }

        // ==========================================
        // 2. SỰ KIỆN CLICK TÌM KIẾM
        // ==========================================
        private void TimKiem()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                if (keyword == "🔍 Tìm kiếm theo Mã phiếu, Biển số xe, Mã CTYC, Tên ĐVC..." || keyword == "🔍 Tìm kiếm theo Mã phiếu, Biển số xe, Mã CTYC, Tên ĐVC...")
                {
                    keyword = "";
                }

                DateTime tuNgay = dtpTuNgay.Checked ? dtpTuNgay.Value : DateTime.MinValue;
                DateTime denNgay = dtpDenNgay.Checked ? dtpDenNgay.Value : DateTime.MaxValue;
                string trangThai = cmbTrangThai.SelectedItem?.ToString() ?? "";

                var list = bll.TimKiem(keyword, tuNgay, denNgay, trangThai);
                if (list.Count == 0)
                {
                    // Tránh việc văng popup liên tục khi gõ, ta chỉ reset list
                    dgvData.DataSource = null;
                }
                else
                {
                    BindData(list);
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi Luồng A2
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "🔍 Tìm kiếm theo Mã phiếu, Biển số xe, Mã CTYC, Tên ĐVC..." &&
                txtSearch.Text != "🔍 Tìm kiếm theo Mã phiếu, Biển số xe, Mã CTYC, Tên ĐVC...")
            {
                TimKiem();
            }
        }

        private void cmbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Tìm kiếm theo Mã phiếu, Biển số xe, Mã CTYC, Tên ĐVC..." ||
                txtSearch.Text == "🔍 Tìm kiếm theo Mã phiếu, Biển số xe, Mã CTYC, Tên ĐVC...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Tìm kiếm theo Mã phiếu, Biển số xe, Mã CTYC, Tên ĐVC...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        // ==========================================
        // 3. SỰ KIỆN THÊM / SỬA / XÓA
        // ==========================================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmPhieuTraHang frm = new FrmPhieuTraHang(null);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataPhieuTraHang();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string id = dgvData.CurrentRow.Cells["colID_PhieuTra"].Value?.ToString();
                string status = dgvData.CurrentRow.Cells["colTrangThai"].Value?.ToString();

                if (status == "Đã xử lý" || status == "Đã nhập kho" || status == "Đã hủy" || status == "Từ chối")
                {
                    MessageBox.Show($"Phiếu trả hàng [{id}] đã '{status}' (Kho đã duyệt / kết thúc) nên thông tin đã bị khóa!\n\nHệ thống sẽ mở phiếu ở chế độ chỉ đọc để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (!string.IsNullOrEmpty(id))
                {
                    FrmPhieuTraHang frm = new FrmPhieuTraHang(id);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDataPhieuTraHang();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu trả hàng cần sửa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string id = dgvData.CurrentRow.Cells["colID_PhieuTra"].Value?.ToString();
                string statusHienTai = dgvData.CurrentRow.Cells["colTrangThai"].Value?.ToString();

                if (statusHienTai == "Đã xử lý" || statusHienTai == "Đã nhập kho")
                {
                    MessageBox.Show("Phiếu trả hàng này đã HOÀN THÀNH / ĐƯỢC KHO DUYỆT XỬ LÝ nên không thể thay đổi trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (statusHienTai == "Đã hủy" || statusHienTai == "Từ chối")
                {
                    MessageBox.Show("Phiếu trả hàng này ĐÃ HỦY / TỪ CHỐI nên không thể thay đổi trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(id))
                {
                    using (FrmCapNhatTrangThaiTraHang frm = new FrmCapNhatTrangThaiTraHang(id, statusHienTai))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadDataPhieuTraHang();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu trả hàng cần cập nhật trạng thái!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string id = dgvData.CurrentRow.Cells["colID_PhieuTra"].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu trả [{id}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            bll.XoaPhieu(id);
                            MessageBox.Show("Xóa phiếu trả hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataPhieuTraHang();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu trả hàng cần xóa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            // Nút này đã bị xóa/thay thế nhưng nếu Form designer còn bind event thì gọi TimKiem.
            TimKiem();
        }

        // ==========================================
        // ĐIỀU HƯỚNG SIDEBAR
        // ==========================================
        private void btnQuanLyXe_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyXe>(this);
        }

        private void btnQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyNhaCungCap>(this);
        }

        private void btnQuanLyDiemVanChuyen_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyDiemVanChuyen>(this);
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ERP.BLL;
using ERP.DTO;

namespace ERP
{
    public partial class QuanLyDonVanChuyen : Form, IRefreshableForm
    {
        private readonly DonVanChuyenBLL bll = new DonVanChuyenBLL();
        public Button btnExport;
        public Button btnInVanDon;

        public QuanLyDonVanChuyen()
        {
            InitializeComponent();
        }

        private FlowLayoutPanel pnlKpiContainer;
        private Label lblKpiTotalVal, lblKpiShippingVal, lblKpiDoneVal, lblKpiReadyVehiclesVal;

        public void LamMoiDuLieu()
        {
            try
            {
                LoadDataDonVanChuyen();
            }
            catch { }
        }

        private void QuanLyDonVanChuyen_Load(object sender, EventArgs e)
        {
            // Không cần set WindowState ở đây vì đã được cài sẵn trong Designer
            // this.WindowState = FormWindowState.Maximized;

            // 1. Áp dụng chuẩn hóa giao diện UI/UX Pro Max
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplySidebar(this.pnlSidebar, this.btnQuanLyDonVanChuyen, this);
            UIThemeHelper.ApplyModernTopHeader(this.pnlTopHeader, "ERP Logistics", "Quản lý đơn vận chuyển", this);
            UIThemeHelper.ApplyActionButton(this.btnAdd, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnEdit, ButtonRole.Secondary);
            UIThemeHelper.ApplyActionButton(this.btnDelete, ButtonRole.Danger);
            UIThemeHelper.ApplyActionButton(this.btnUpdateStatus, ButtonRole.Edit);

            // Thiết lập Filter Bar dạng Card hiện đại chuẩn UI/UX Pro Max
            UIThemeHelper.SetupModernFilterCard(
                this.pnlActionTool,
                this.txtSearch,
                320,
                () => {
                    txtSearch.Text = "🔍 Tìm kiếm theo Mã đơn, Mã xe, Địa điểm...";
                    txtSearch.ForeColor = Color.Gray;
                    if (cmbTrangThai.Items.Count > 0) cmbTrangThai.SelectedIndex = 0;
                    LamMoiDuLieu();
                },
                new FilterItem("Trạng thái:", this.cmbTrangThai, 200)
            );

            // Tự động làm mới dữ liệu khi form hiển thị lại từ Hide()
            this.VisibleChanged += (s, ev) =>
            {
                if (this.Visible && !this.IsDisposed)
                {
                    LamMoiDuLieu();
                }
            };

            InitExportButton();
            InitKpiPanel();
            KhoiTaoCotBang();

            // 2. Gọi BLL.LayDanhSach() → bind vào DataGridView
            LoadDataDonVanChuyen();

            // 3. Nạp trạng thái vào ComboBox lọc
            LoadTrangThaiComboBox();
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

            var card1 = UIThemeHelper.CreateKpiCard("TỔNG ĐƠN VẬN CHUYỂN", "0", "Toàn hệ thống", Color.FromArgb(37, 99, 235));
            lblKpiTotalVal = card1.Controls[1].Controls[0] as Label;

            var card2 = UIThemeHelper.CreateKpiCard("ĐANG VẬN CHUYỂN", "0", "Đang trên lộ trình", Color.FromArgb(14, 165, 233));
            lblKpiShippingVal = card2.Controls[1].Controls[0] as Label;

            var card3 = UIThemeHelper.CreateKpiCard("ĐÃ HOÀN THÀNH", "0", "Giao nhận thành công", Color.FromArgb(22, 163, 74));
            lblKpiDoneVal = card3.Controls[1].Controls[0] as Label;

            var card4 = UIThemeHelper.CreateKpiCard("XE SẴN SÀNG", "0", "Sẵn sàng nhận lệnh", Color.FromArgb(217, 119, 6));
            lblKpiReadyVehiclesVal = card4.Controls[1].Controls[0] as Label;

            pnlKpiContainer.Controls.Add(card1);
            pnlKpiContainer.Controls.Add(card2);
            pnlKpiContainer.Controls.Add(card3);
            pnlKpiContainer.Controls.Add(card4);

            pnlMainContent.Controls.Add(pnlKpiContainer);
            pnlKpiContainer.SendToBack();
            pnlActionTool.BringToFront();
            dgvData.BringToFront();
        }

        private void UpdateKpiMetrics(List<DonVanChuyen> list)
        {
            try
            {
                int total = list != null ? list.Count : 0;
                int shipping = 0;
                int done = 0;

                if (list != null)
                {
                    foreach (var d in list)
                    {
                        if (string.Equals(d.TrangThaiDon, "Đang vận chuyển", StringComparison.OrdinalIgnoreCase))
                            shipping++;
                        else if (string.Equals(d.TrangThaiDon, "Hoàn thành", StringComparison.OrdinalIgnoreCase))
                            done++;
                    }
                }

                int readyVehicles = 0;
                try
                {
                    var xeList = bll.LayDanhSachXeKhaDung();
                    readyVehicles = xeList != null ? xeList.Count : 0;
                }
                catch { }

                if (lblKpiTotalVal != null) lblKpiTotalVal.Text = total.ToString();
                if (lblKpiShippingVal != null) lblKpiShippingVal.Text = shipping.ToString();
                if (lblKpiDoneVal != null) lblKpiDoneVal.Text = done.ToString();
                if (lblKpiReadyVehiclesVal != null) lblKpiReadyVehiclesVal.Text = readyVehicles.ToString();
            }
            catch { }
        }

        private void InitExportButton()
        {
            if (btnExport == null)
            {
                // Nút Xuất File trên thanh công cụ
                btnExport = new Button();
                btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btnExport.Location = new Point(360, 10);
                btnExport.Name = "btnExport";
                btnExport.Size = new Size(130, 35);
                btnExport.TabIndex = 9;
                btnExport.Text = "📥 Xuất File";
                btnExport.Click += new EventHandler(this.btnExport_Click);
                UIThemeHelper.ApplyActionButton(btnExport, ButtonRole.Secondary);
                pnlActionTool.Controls.Add(btnExport);
            }

            if (btnInVanDon == null)
            {
                // Nút In Vận Đơn trên thanh công cụ
                btnInVanDon = new Button();
                btnInVanDon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btnInVanDon.Location = new Point(500, 10);
                btnInVanDon.Name = "btnInVanDon";
                btnInVanDon.Size = new Size(140, 35);
                btnInVanDon.TabIndex = 10;
                btnInVanDon.Text = "📄 In Vận Đơn";
                btnInVanDon.Click += new EventHandler(this.btnInVanDon_Click);
                UIThemeHelper.ApplyActionButton(btnInVanDon, ButtonRole.Report);
                pnlActionTool.Controls.Add(btnInVanDon);
            }
        }

        private void KhoiTaoCotBang()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            // 1. Mã Đơn Vận Chuyển (ID_DonVC)
            DataGridViewTextBoxColumn colID = new DataGridViewTextBoxColumn();
            colID.Name = "colID_DonVC";
            colID.HeaderText = "MÃ ĐƠN VC";
            colID.DataPropertyName = "ID_DonVC";
            colID.FillWeight = 13;
            colID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colID.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colID.DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            dgvData.Columns.Add(colID);

            // 1.1 Mã Đơn Hàng Bán Hàng (ID_DH)
            DataGridViewTextBoxColumn colDH = new DataGridViewTextBoxColumn();
            colDH.Name = "colID_DH";
            colDH.HeaderText = "ĐƠN HÀNG BH";
            colDH.DataPropertyName = "ID_DH";
            colDH.FillWeight = 12;
            colDH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDH.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDH.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colDH.DefaultCellStyle.ForeColor = Color.FromArgb(40, 167, 69);
            dgvData.Columns.Add(colDH);

            // 1.2 Khách Hàng (TenKhachHang)
            DataGridViewTextBoxColumn colKH = new DataGridViewTextBoxColumn();
            colKH.Name = "colTenKhachHang";
            colKH.HeaderText = "KHÁCH HÀNG";
            colKH.DataPropertyName = "TenKhachHang";
            colKH.FillWeight = 16;
            colKH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colKH.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colKH);

            // 2. Biển Số Xe (BienSoXe)
            DataGridViewTextBoxColumn colXe = new DataGridViewTextBoxColumn();
            colXe.Name = "colBienSoXe";
            colXe.HeaderText = "BIỂN SỐ XE";
            colXe.DataPropertyName = "BienSoXe";
            colXe.FillWeight = 14;
            colXe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colXe.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colXe);

            // 3. Điểm Vận Chuyển (TenDVC)
            DataGridViewTextBoxColumn colMaDVC = new DataGridViewTextBoxColumn();
            colMaDVC.Name = "colMaDVC";
            colMaDVC.HeaderText = "ĐIỂM GIAO NHẬN";
            colMaDVC.DataPropertyName = "TenDVC";
            colMaDVC.FillWeight = 18;
            colMaDVC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colMaDVC.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colMaDVC);

            // 4. Mã Sản Phẩm (ID_SP)
            DataGridViewTextBoxColumn colSP = new DataGridViewTextBoxColumn();
            colSP.Name = "colID_SP";
            colSP.HeaderText = "MÃ SP";
            colSP.DataPropertyName = "ID_SP";
            colSP.FillWeight = 10;
            colSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colSP);

            // 4.1 Tên Hàng Hóa (TenHang)
            DataGridViewTextBoxColumn colTenHang = new DataGridViewTextBoxColumn();
            colTenHang.Name = "colTenHang";
            colTenHang.HeaderText = "TÊN HÀNG HÓA";
            colTenHang.DataPropertyName = "TenHang";
            colTenHang.FillWeight = 20;
            colTenHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colTenHang.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colTenHang);

            // 5. Số Lượng Giao (SoLuongGiao)
            DataGridViewTextBoxColumn colSL = new DataGridViewTextBoxColumn();
            colSL.Name = "colSoLuongGiao";
            colSL.HeaderText = "SL GIAO";
            colSL.DataPropertyName = "SoLuongGiao";
            colSL.FillWeight = 12;
            colSL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSL.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colSL);

            // 6. Thời Gian Khởi Hành (ThoiGianKhoiHanh)
            DataGridViewTextBoxColumn colNgay = new DataGridViewTextBoxColumn();
            colNgay.Name = "colThoiGianKhoiHanh";
            colNgay.HeaderText = "THỜI GIAN KHỞI HÀNH";
            colNgay.DataPropertyName = "ThoiGianKhoiHanh";
            colNgay.FillWeight = 20;
            colNgay.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgay.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgay.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvData.Columns.Add(colNgay);

            // 7. Trạng Thái Đơn (TrangThaiDon)
            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
            colTrangThai.Name = "colTrangThaiDon";
            colTrangThai.HeaderText = "TRẠNG THÁI";
            colTrangThai.DataPropertyName = "TrangThaiDon";
            colTrangThai.FillWeight = 14;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTrangThai.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTrangThai.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvData.Columns.Add(colTrangThai);

            dgvData.AllowUserToResizeColumns = true;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Áp dụng định dạng bảng dữ liệu Data-Dense theo chuẩn UI/UX Pro Max
            UIThemeHelper.ApplyModernGridStyle(dgvData);
        }

        // ==========================================
        // TẢI DỮ LIỆU VÀ NẠP DANH MỤC
        // ==========================================

        private void LoadDataDonVanChuyen()
        {
            try
            {
                try
                {
                    bll.DongBoLienKetBanHangVaLogistics();
                }
                catch { }

                List<DonVanChuyen> list = bll.LayDanhSach();
                BindData(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đơn vận chuyển: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindData(List<DonVanChuyen> list)
        {
            dgvData.DataSource = null;
            dgvData.DataSource = list;

            // Cập nhật số liệu hiển thị trên thẻ KPI
            UpdateKpiMetrics(list);

            // Định dạng màu sắc trực quan theo từng trạng thái
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Cells["colTrangThaiDon"].Value != null)
                {
                    string tt = row.Cells["colTrangThaiDon"].Value.ToString();
                    if (tt == "Khởi tạo")
                    {
                        row.Cells["colTrangThaiDon"].Style.ForeColor = Color.FromArgb(13, 110, 253);
                    }
                    else if (tt == "Đang vận chuyển")
                    {
                        row.Cells["colTrangThaiDon"].Style.ForeColor = Color.FromArgb(255, 152, 0);
                    }
                    else if (tt == "Hoàn thành")
                    {
                        row.Cells["colTrangThaiDon"].Style.ForeColor = Color.FromArgb(40, 167, 69);
                    }
                    else if (tt == "Đã hủy")
                    {
                        row.Cells["colTrangThaiDon"].Style.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                }
            }
        }

        private void LoadTrangThaiComboBox()
        {
            string[] trangThais = { "Khởi tạo", "Đang vận chuyển", "Hoàn thành", "Đã hủy" };

            cmbTrangThai.Items.Clear();
            cmbTrangThai.Items.Add("Tất cả trạng thái");
            cmbTrangThai.Items.AddRange(trangThais);
            cmbTrangThai.SelectedIndex = 0;
        }

        // ==========================================
        // CÁC NÚT HÀNH ĐỘNG CRUD & IN ẤN
        // ==========================================

        private void btnInVanDon_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn vận chuyển trên danh sách để in vận đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string idDonVC = dgvData.CurrentRow.Cells["colID_DonVC"].Value?.ToString();
                if (string.IsNullOrEmpty(idDonVC))
                {
                    MessageBox.Show("Không tìm thấy mã đơn vận chuyển đã chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DonVanChuyen don = bll.LayDonTheoID(idDonVC);
                if (don == null)
                {
                    MessageBox.Show($"Không tìm thấy dữ liệu đơn vận chuyển [{idDonVC}]!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FrmInPhieuVanDon frmIn = new FrmInPhieuVanDon(don);
                frmIn.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở phiếu vận đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmThemDonVanChuyen frm = new FrmThemDonVanChuyen();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataDonVanChuyen();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string id = dgvData.CurrentRow.Cells["colID_DonVC"].Value?.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    FrmSuaDonVanChuyen frm = new FrmSuaDonVanChuyen(id);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDataDonVanChuyen();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn vận chuyển cần chỉnh sửa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string id = dgvData.CurrentRow.Cells["colID_DonVC"].Value?.ToString();
                string status = dgvData.CurrentRow.Cells["colTrangThaiDon"].Value?.ToString();

                if (!string.IsNullOrEmpty(id))
                {
                    if (status == "Hoàn thành")
                    {
                        MessageBox.Show("Không thể xóa đơn vận chuyển đã HOÀN THÀNH để đảm bảo tính toàn vẹn dữ liệu và lịch sử đối soát nghiệp vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (status == "Đang vận chuyển")
                    {
                        MessageBox.Show("Không thể xóa đơn vận chuyển ĐANG VẬN CHUYỂN! Phương tiện đang thực hiện hành trình trên đường.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa đơn vận chuyển '{id}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            bll.XoaDon(id);
                            MessageBox.Show("Xóa đơn vận chuyển thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataDonVanChuyen();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn vận chuyển cần xóa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string id = dgvData.CurrentRow.Cells["colID_DonVC"].Value?.ToString();
                string statusHienTai = dgvData.CurrentRow.Cells["colTrangThaiDon"].Value?.ToString();

                if (statusHienTai == "Hoàn thành")
                {
                    MessageBox.Show("Đơn vận chuyển này đã HOÀN THÀNH nên không thể thay đổi trạng thái!\n\nNếu khách hàng muốn đổi trả hàng, vui lòng sử dụng chức năng 'Quản lý trả hàng' để lập phiếu thu hồi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (statusHienTai == "Đã hủy")
                {
                    MessageBox.Show("Đơn vận chuyển này đã ĐÃ HỦY nên không thể thay đổi trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(id))
                {
                    using (FrmCapNhatTrangThai frm = new FrmCapNhatTrangThai(id, statusHienTai))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadDataDonVanChuyen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn vận chuyển cần cập nhật trạng thái!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // TÌM KIẾM VÀ LỌC DỮ LIỆU
        // ==========================================

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LocDuLieu();
        }

        private void cmbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocDuLieu();
        }

        private void LocDuLieu()
        {
            string keyword = txtSearch.Text.Trim();
            if (keyword == "🔍 Tìm kiếm theo Mã đơn, Mã xe, Địa điểm...") keyword = string.Empty;

            try
            {
                List<DonVanChuyen> list = string.IsNullOrEmpty(keyword)
                    ? bll.LayDanhSach()
                    : bll.TimKiem(keyword);

                if (cmbTrangThai.SelectedIndex > 0)
                {
                    string selectedStatus = cmbTrangThai.SelectedItem.ToString();
                    list = list.FindAll(d => d.TrangThaiDon == selectedStatus);
                }

                BindData(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Tìm kiếm theo Mã đơn, Mã xe, Địa điểm...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Tìm kiếm theo Mã đơn, Mã xe, Địa điểm...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        // ==========================================
        // NÚT XUẤT FILE
        // ==========================================

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files|*.csv|Excel Files|*.xlsx";
                sfd.FileName = "DonVanChuyen_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        // Tiêu đề cột
                        List<string> headers = new List<string>();
                        foreach (DataGridViewColumn col in dgvData.Columns)
                        {
                            if (col.Visible)
                            {
                                headers.Add("\"" + col.HeaderText.Replace("\"", "\"\"") + "\"");
                            }
                        }
                        sb.AppendLine(string.Join(",", headers));

                        // Dữ liệu dòng
                        foreach (DataGridViewRow row in dgvData.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                List<string> cells = new List<string>();
                                foreach (DataGridViewColumn col in dgvData.Columns)
                                {
                                    if (col.Visible)
                                    {
                                        object val = row.Cells[col.Index].Value;
                                        string strVal = val != null ? val.ToString() : "";
                                        cells.Add("\"" + strVal.Replace("\"", "\"\"") + "\"");
                                    }
                                }
                                sb.AppendLine(string.Join(",", cells));
                            }
                        }

                        // Ghi file kèm BOM UTF-8 để mở tiếng Việt không lỗi font
                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xuất file thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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

        private void btnQuanLyTraHang_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyTraHang>(this);
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            LogisticsHelper.DangXuat(this);
        }
    }
}
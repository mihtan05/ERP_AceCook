using System;
using System.Data;
using Npgsql;
using System.Drawing;
using System.Windows.Forms;
using ERP.DAL;

namespace ERP
{
    public partial class QuanLyNhaCungCap : Form
    {
        // Chuỗi kết nối CSDL lấy từ cấu hình
        private string connectionString = DatabaseConfig.GetConnectionString();
        private DataTable dtNCC;

        public QuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        private FlowLayoutPanel pnlKpiContainer;
        private Label lblKpiTotalVal, lblKpiActiveVal, lblKpiTransVal, lblKpiCertVal;

        private void QuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            // this.WindowState = FormWindowState.Maximized;

            // Áp dụng chuẩn hóa giao diện UI/UX Pro Max
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplySidebar(this.pnlSidebar, this.btnQuanLyNhaCungCap, this);
            UIThemeHelper.ApplyModernTopHeader(this.pnlTopHeader, "ERP Logistics", "Quản lý nhà cung cấp", this);
            UIThemeHelper.ApplyActionButton(this.btnAdd, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnEdit, ButtonRole.Secondary);
            UIThemeHelper.ApplyActionButton(this.btnDelete, ButtonRole.Danger);

            // Thiết lập Filter Bar dạng Card hiện đại chuẩn UI/UX Pro Max
            UIThemeHelper.SetupModernFilterCard(
                this.pnlActionTool,
                this.txtSearch,
                320,
                () => {
                    txtSearch.Text = "🔍 Tìm kiếm theo Mã, Tên nhà cung cấp, SĐT...";
                    txtSearch.ForeColor = Color.Gray;
                    LocDuLieu();
                }
            );

            InitKpiPanel();

            KhoiTaoCotBang();
            LoadDataNhaCungCap();
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

            var card1 = UIThemeHelper.CreateKpiCard("TỔNG NHÀ CUNG CẤP", "0", "Hệ sinh thái đối tác", Color.FromArgb(37, 99, 235));
            lblKpiTotalVal = card1.Controls[1].Controls[0] as Label;

            var card2 = UIThemeHelper.CreateKpiCard("ĐANG HỢP TÁC", "0", "Hoạt động tích cực", Color.FromArgb(22, 163, 74));
            lblKpiActiveVal = card2.Controls[1].Controls[0] as Label;

            var card3 = UIThemeHelper.CreateKpiCard("ĐỐI TÁC VẬN TẢI", "0", "Đội xe liên kết", Color.FromArgb(14, 165, 233));
            lblKpiTransVal = card3.Controls[1].Controls[0] as Label;

            var card4 = UIThemeHelper.CreateKpiCard("CÓ CHỨNG NHẬN", "0", "Chuẩn ISO/HACCP", Color.FromArgb(217, 119, 6));
            lblKpiCertVal = card4.Controls[1].Controls[0] as Label;

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
                int total = dtNCC != null ? dtNCC.Rows.Count : 0;
                int cert = 0;
                int trans = 0;

                if (dtNCC != null)
                {
                    foreach (DataRow row in dtNCC.Rows)
                    {
                        string cn = row["TenChungNhan"]?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(cn) && !cn.Equals("Không có", StringComparison.OrdinalIgnoreCase))
                            cert++;

                        string ten = row["TenNCC"]?.ToString() ?? "";
                        if (ten.IndexOf("vận", StringComparison.OrdinalIgnoreCase) >= 0 || ten.IndexOf("tải", StringComparison.OrdinalIgnoreCase) >= 0 || ten.IndexOf("logistics", StringComparison.OrdinalIgnoreCase) >= 0)
                            trans++;
                    }
                }

                if (lblKpiTotalVal != null) lblKpiTotalVal.Text = total.ToString();
                if (lblKpiActiveVal != null) lblKpiActiveVal.Text = total.ToString();
                if (lblKpiTransVal != null) lblKpiTransVal.Text = (trans > 0 ? trans : (total > 0 ? 1 : 0)).ToString();
                if (lblKpiCertVal != null) lblKpiCertVal.Text = cert.ToString();
            }
            catch { }
        }

        private void KhoiTaoCotBang()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            // 1. Mã Nhà Cung Cấp
            DataGridViewTextBoxColumn colID = new DataGridViewTextBoxColumn();
            colID.Name = "colID_NCC";
            colID.HeaderText = "MÃ NCC";
            colID.DataPropertyName = "ID_NCC";
            colID.FillWeight = 12;
            colID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colID.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colID.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colID.DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            dgvData.Columns.Add(colID);

            // 2. Tên Nhà Cung Cấp
            DataGridViewTextBoxColumn colTen = new DataGridViewTextBoxColumn();
            colTen.Name = "colTenNCC";
            colTen.HeaderText = "TÊN NHÀ CUNG CẤP";
            colTen.DataPropertyName = "TenNCC";
            colTen.FillWeight = 28;
            colTen.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colTen);

            // 3. Số Điện Thoại
            DataGridViewTextBoxColumn colSDT = new DataGridViewTextBoxColumn();
            colSDT.Name = "colSDT";
            colSDT.HeaderText = "SỐ ĐIỆN THOẠI";
            colSDT.DataPropertyName = "SDT";
            colSDT.FillWeight = 15;
            colSDT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSDT.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.Columns.Add(colSDT);

            // 4. Địa Chỉ
            DataGridViewTextBoxColumn colDiaChi = new DataGridViewTextBoxColumn();
            colDiaChi.Name = "colDiaChi";
            colDiaChi.HeaderText = "ĐỊA CHỈ";
            colDiaChi.DataPropertyName = "DiaChi";
            colDiaChi.FillWeight = 30;
            colDiaChi.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colDiaChi);

            // 5. Tên Chứng Nhận (Nếu có)
            DataGridViewTextBoxColumn colChungNhan = new DataGridViewTextBoxColumn();
            colChungNhan.Name = "colTenChungNhan";
            colChungNhan.HeaderText = "CHỨNG NHẬN";
            colChungNhan.DataPropertyName = "TenChungNhan";
            colChungNhan.FillWeight = 15;
            colChungNhan.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns.Add(colChungNhan);

            dgvData.AllowUserToResizeColumns = true;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Áp dụng định dạng bảng dữ liệu Data-Dense theo chuẩn UI/UX Pro Max
            UIThemeHelper.ApplyModernGridStyle(dgvData);
        }

        private void LoadDataNhaCungCap()
        {
            // Lấy đúng các cột có trong bảng NhaCungCap
            string query = @"SELECT ID_NCC, TenNCC, DiaChi, SDT, 
                            COALESCE(TenChungNhan, N'Không có') AS TenChungNhan, 
                            COALESCE(SoHieuCN, N'') AS SoHieuCN, 
                            NgayCap, NgayHetHan 
                     FROM NhaCungCap 
                     ORDER BY ID_NCC DESC";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                    dtNCC = new DataTable();
                    da.Fill(dtNCC);

                    dgvData.DataSource = dtNCC;
                    UpdateKpiMetrics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu nhà cung cấp: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dtNCC == null) return;

            // Lấy từ khóa tìm kiếm và chống lỗi cú pháp SQL injection cho DataView RowFilter
            string keyword = txtSearch.Text.Trim().Replace("'", "''");
            if (keyword == "🔍 Tìm kiếm theo Mã, Tên nhà cung cấp, SĐT..." || string.IsNullOrEmpty(keyword))
            {
                keyword = "";
            }

            DataView dv = dtNCC.DefaultView;

            if (!string.IsNullOrEmpty(keyword))
            {
                // Cho phép tìm kiếm linh hoạt trên Mã, Tên, SĐT, Địa chỉ hoặc Tên chứng nhận
                dv.RowFilter = $"ID_NCC LIKE '%{keyword}%' OR TenNCC LIKE '%{keyword}%' OR SDT LIKE '%{keyword}%' OR DiaChi LIKE '%{keyword}%' OR TenChungNhan LIKE '%{keyword}%'";
            }
            else
            {
                dv.RowFilter = ""; // Hiển thị toàn bộ nếu không nhập từ khóa
            }

            dgvData.DataSource = dv;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Tìm kiếm theo Mã, Tên nhà cung cấp, SĐT...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Tìm kiếm theo Mã, Tên nhà cung cấp, SĐT...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        // ==========================================
        // THÊM, SỬA, XÓA NHÀ CUNG CẤP
        // ==========================================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmThemNhaCungCap frmThem = new FrmThemNhaCungCap();

            // Nếu thêm dữ liệu thành công (DialogResult.OK), hệ thống sẽ tự động load lại bảng dữ liệu
            if (frmThem.ShowDialog() == DialogResult.OK)
            {
                LoadDataNhaCungCap();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null)
            {
                string idNCC = dgvData.CurrentRow.Cells["colID_NCC"].Value?.ToString();

                // Kiểm tra xem mã lấy ra có rỗng không trước khi mở form
                if (!string.IsNullOrEmpty(idNCC))
                {
                    // Gọi Form Sửa NCC và truyền vào idNCC
                    FrmSuaNhaCungCap frmSua = new FrmSuaNhaCungCap(idNCC);

                    // Nếu người dùng bấm lưu thành công (DialogResult.OK), load lại bảng dữ liệu
                    if (frmSua.ShowDialog() == DialogResult.OK)
                    {
                        LoadDataNhaCungCap();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp cần chỉnh sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào trên bảng chưa
            if (dgvData.CurrentRow != null)
            {
                // Lấy mã NCC từ dòng đang chọn
                string idNCC = dgvData.CurrentRow.Cells["colID_NCC"].Value?.ToString();
                string tenNCC = dgvData.CurrentRow.Cells["colTenNCC"].Value?.ToString();

                // 2. Hiện hộp thoại xác nhận trước khi xóa
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhà cung cấp '{tenNCC} ({idNCC})' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM NhaCungCap WHERE ID_NCC = @ID_NCC";

                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@ID_NCC", idNCC);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataNhaCungCap();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy nhà cung cấp cần xóa trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (PostgresException ex)
                        {
                            // Lỗi mã 547 thường do vướng khóa ngoại (Foreign Key)
                            if ((ex.SqlState == "23503" || ex.SqlState == "23514"))
                            {
                                MessageBox.Show("Không thể xóa nhà cung cấp này vì đã phát sinh dữ liệu liên quan (như phiếu nhập hàng, sản phẩm,...).", "Lỗi ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Vui lòng chọn Nhà cung cấp cần xóa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // ĐIỀU HƯỚNG SIDEBAR - PHÂN HỆ VẬN CHUYỂN
        // ==========================================

        private void btnQuanLyXe_Click(object sender, EventArgs e)
        {
            LogisticsHelper.NavigateToForm<QuanLyXe>(this);
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



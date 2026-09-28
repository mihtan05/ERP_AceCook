using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrQLLohangHSD : Form
    {
        private DataTable dtLoHang;

        public FrQLLohangHSD()
        {
            InitializeComponent();
        }

        private void FrQLLohangHSD_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhoFilter();
            LoadDataFromDatabase();

            cboFilterHSD.SelectedIndex = 0;
        }

        private bool IsPermittedUser()
        {
            string chucVu = UserSession.ChucVu ?? "";
            return chucVu.Equals("Nhân viên kho", StringComparison.OrdinalIgnoreCase) ||
                   chucVu.Equals("Quản lý kho", StringComparison.OrdinalIgnoreCase) ||
                   chucVu.Equals("Quản lý", StringComparison.OrdinalIgnoreCase) ||
                   chucVu.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                   chucVu.Equals("Quản trị viên", StringComparison.OrdinalIgnoreCase);
        }

        private void LoadDanhSachKhoFilter()
        {
            try
            {
                cboFilterKho.Items.Clear();
                cboFilterKho.Items.Add("-- Tất cả Kho --");

                string sql = "SELECT tenkho FROM kho ORDER BY makho";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cboFilterKho.Items.Add(row["tenkho"].ToString());
                    }
                }
                cboFilterKho.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadDataFromDatabase()
        {
            try
            {
                // Tính toán trạng thái HSD TỰ ĐỘNG theo thời gian thực chuẩn PostgreSQL (CURRENT_DATE)
                string sql = @"
            SELECT DISTINCT
                lh.malo AS ""MaLoHang"",
                lh.mahang AS ""MaVatTu"",
                hh.tenhang AS ""TenVatTu"",
                COALESCE(k.tenkho, 'Chờ phân bổ') AS ""TenKho"",
                lh.ngaysanxuat AS ""NgaySanXuat"",
                lh.hansudung AS ""HanSuDung"",
                lh.soluongcon AS ""SoLuongTonLo"",
                COALESCE(hh.donvitinh, 'Kg') AS ""DonViTinh"",
                
                -- TỰ ĐỘNG TÍNH TRẠNG THÁI THEO SÁT NGÀY HSD REAL-TIME:
                CASE 
                    WHEN lh.trangthai = 'Phong tỏa' OR lh.trangthai = 'Đã khóa' THEN 'Phong tỏa'
                    WHEN lh.hansudung < CURRENT_DATE THEN 'Đã quá hạn'
                    WHEN (lh.hansudung::date - CURRENT_DATE) <= 30 THEN 'Sắp hết hạn (< 30 ngày)'
                    ELSE 'Còn hạn an toàn'
                END AS ""TrangThaiHSD""

            FROM lohang lh
            INNER JOIN hanghoa hh ON lh.mahang = hh.mahang
            LEFT JOIN tonkho tk ON lh.malo = tk.malo
            LEFT JOIN vitriluutru vt ON tk.mavitri = vt.mavitri
            LEFT JOIN kho k ON vt.makho = k.makho
            ORDER BY lh.hansudung ASC"; // Chuẩn FEFO: Lô sắp hết hạn xếp lên đầu

                dtLoHang = DatabaseHelper.ExecuteQuery(sql);
                dgvLoHang.DataSource = dtLoHang;
                DinhDangLuoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lô hàng từ CSDL: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangLuoi()
        {
            if (dgvLoHang.Columns.Contains("MaLoHang")) dgvLoHang.Columns["MaLoHang"].HeaderText = "Mã Lô Hàng";
            if (dgvLoHang.Columns.Contains("MaVatTu")) dgvLoHang.Columns["MaVatTu"].HeaderText = "Mã Vật Tư";
            if (dgvLoHang.Columns.Contains("TenVatTu")) dgvLoHang.Columns["TenVatTu"].HeaderText = "Tên Sản Phẩm / Vật Tư";
            if (dgvLoHang.Columns.Contains("TenKho")) dgvLoHang.Columns["TenKho"].HeaderText = "Nhà Kho";
            if (dgvLoHang.Columns.Contains("NgaySanXuat")) dgvLoHang.Columns["NgaySanXuat"].HeaderText = "Ngày Sản Xuất";
            if (dgvLoHang.Columns.Contains("HanSuDung")) dgvLoHang.Columns["HanSuDung"].HeaderText = "Hạn Sử Dụng (HSD)";
            if (dgvLoHang.Columns.Contains("SoLuongTonLo")) dgvLoHang.Columns["SoLuongTonLo"].HeaderText = "Tồn Theo Lô";
            if (dgvLoHang.Columns.Contains("DonViTinh")) dgvLoHang.Columns["DonViTinh"].HeaderText = "ĐVT";
            if (dgvLoHang.Columns.Contains("TrangThaiHSD")) dgvLoHang.Columns["TrangThaiHSD"].HeaderText = "Tình Trạng HSD";

            if (dgvLoHang.Columns.Contains("NgaySanXuat"))
                dgvLoHang.Columns["NgaySanXuat"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (dgvLoHang.Columns.Contains("HanSuDung"))
                dgvLoHang.Columns["HanSuDung"].DefaultCellStyle.Format = "dd/MM/yyyy";

            if (dgvLoHang.Columns.Contains("SoLuongTonLo"))
            {
                dgvLoHang.Columns["SoLuongTonLo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLoHang.Columns["SoLuongTonLo"].DefaultCellStyle.Format = "#,##0.##";
            }
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            if (dtLoHang == null) return;
            string filter = "1=1";

            if (cboFilterKho.SelectedIndex > 0)
            {
                string khoSelect = cboFilterKho.SelectedItem.ToString().Replace("'", "''");
                filter += $" AND TenKho = '{khoSelect}'";
            }

            if (cboFilterHSD.SelectedIndex > 0)
            {
                string hsdSelect = cboFilterHSD.SelectedItem.ToString().Replace("'", "''");
                filter += $" AND TrangThaiHSD LIKE '%{hsdSelect}%'";
            }

            if (!string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != "Tìm kiếm theo mã lô, mã vật tư, tên sản phẩm...")
            {
                string keyword = txtSearch.Text.Trim().Replace("'", "''");
                filter += $" AND (MaLoHang LIKE '%{keyword}%' OR MaVatTu LIKE '%{keyword}%' OR TenVatTu LIKE '%{keyword}%')";
            }

            (dgvLoHang.DataSource as DataTable).DefaultView.RowFilter = filter;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => Filter_Changed(sender, e);

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm theo mã lô, mã vật tư, tên sản phẩm...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm theo mã lô, mã vật tư, tên sản phẩm...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void dgvLoHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLoHang.Columns[e.ColumnIndex].Name == "TrangThaiHSD" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Còn hạn an toàn" || status == "Khả dụng")
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
                else if (status.Contains("Sắp hết hạn") || status.Contains("FEFO"))
                {
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.Font = new Font(dgvLoHang.Font, FontStyle.Bold);
                }
                else if (status.Contains("quá hạn") || status.Contains("khóa") || status.Contains("Phong tỏa"))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvLoHang.Font, FontStyle.Bold);
                }
            }
        }

        private void btnThemLoHang_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo lô hàng mới!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrTaoLoHang frm = new FrTaoLoHang();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromDatabase();
            }
        }

        private void btnSuaLoHang_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền sửa thông tin lô hàng!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvLoHang.SelectedRows.Count > 0)
            {
                string maLo = dgvLoHang.SelectedRows[0].Cells["MaLoHang"].Value.ToString();
                string tenVT = dgvLoHang.SelectedRows[0].Cells["TenVatTu"].Value.ToString();
                FrCapNhatLoHang frm = new FrCapNhatLoHang(maLo, tenVT);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDataFromDatabase();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lô hàng cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCanhBaoHSD_Click(object sender, EventArgs e)
        {
            cboFilterHSD.SelectedIndex = 2; // Chọn lọc "Sắp hết hạn"
            Filter_Changed(sender, e);
        }

        private void btnKhoaLoHang_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền phong tỏa/khóa lô hàng!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvLoHang.SelectedRows.Count > 0)
            {
                string maLo = dgvLoHang.SelectedRows[0].Cells["MaLoHang"].Value.ToString();
                FrKhoaLoHang frm = new FrKhoaLoHang(maLo);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDataFromDatabase();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn lô hàng cần khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvLoHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSuaLoHang_Click(sender, e);
            }
        }

        private void pnlMainContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
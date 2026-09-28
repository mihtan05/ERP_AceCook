using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrQLLuutruvavitri : Form
    {
        public static DataTable dtViTri;
        public static DataTable dtLoHangAtViTri;
        private readonly string placeholderSearch = "Tìm mã vị trí, khu vực, kệ, ô chứa...";

        public FrQLLuutruvavitri()
        {
            InitializeComponent();
        }

        private void FrQLLuutruvavitri_Load(object sender, EventArgs e)
        {
            if (cboFilterKho.Items.Count > 0) cboFilterKho.SelectedIndex = 0;
            if (cboFilterTrangThai.Items.Count > 0) cboFilterTrangThai.SelectedIndex = 0;

            LoadDataFromDatabase();

            dgvViTri.DataSource = dtViTri;
            dgvChiTietLoHang.DataSource = dtLoHangAtViTri;

            DinhDangLuoi();
            Filter_Changed(null, null);
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

        public static void LoadDataFromDatabase()
        {
            try
            {
                // 1. TỰ ĐỘNG CẬP NHẬT TRẠNG THÁI Ô CHỨA TRONG CSDL POSTGRESQL (Nếu Đã dùng >= Sức chứa tối đa -> 'Đầy')
                string sqlSyncStatus = @"
                    UPDATE vitriluutru vt
                    SET trangthai = CASE 
                        WHEN (SELECT COALESCE(SUM(soluongton), 0) FROM tonkho tk WHERE tk.mavitri = vt.mavitri) >= vt.succhuatoida THEN 'Đầy'
                        WHEN vt.trangthai = 'Đang bảo trì' THEN 'Đang bảo trì'
                        ELSE 'Khả dụng'
                    END;";

                DatabaseHelper.ExecuteNonQuery(sqlSyncStatus);

                // 2. Truy vấn danh mục vị trí kho thực tế từ CSDL PostgreSQL
                string queryViTri = @"
                    SELECT 
                        vt.mavitri AS ""MaViTri"",
                        COALESCE(k.tenkho, 'Kho Nguyên Liệu A1') AS ""TenKho"",
                        COALESCE(vt.khuvuc, 'Zone A') AS ""KhuVuc"",
                        COALESCE(vt.ke, 'Dãy 1') AS ""Day"",
                        COALESCE(vt.ke, 'Kệ 01') AS ""Ke"",
                        COALESCE(vt.ochua, 'Ô 01') AS ""OChua"",
                        COALESCE(vt.succhuatoida, 1000) AS ""SucChuaToida"",
                        COALESCE(SUM(tk.soluongton), 0) AS ""SucChuaDaDung"",
                        vt.trangthai AS ""TrangThai""
                    FROM vitriluutru vt
                    LEFT JOIN kho k ON vt.makho = k.makho
                    LEFT JOIN tonkho tk ON vt.mavitri = tk.mavitri
                    GROUP BY vt.mavitri, k.tenkho, vt.khuvuc, vt.ke, vt.ochua, vt.succhuatoida, vt.trangthai";

                dtViTri = DatabaseHelper.ExecuteQuery(queryViTri);

                if (!dtViTri.Columns.Contains("PhanTramLapDay"))
                {
                    dtViTri.Columns.Add("PhanTramLapDay", typeof(double), "IIF(SucChuaToida=0, 0, (SucChuaDaDung/SucChuaToida)*100)");
                }

                // 3. Truy vấn danh sách chi tiết lô hàng lưu trữ tại ô chứa
                string queryLoHang = @"
                    SELECT 
                        tk.mavitri AS ""MaViTri"",
                        lh.malo AS ""MaLo"",
                        hh.mahang AS ""MaHang"",
                        hh.tenhang AS ""TenHang"",
                        tk.soluongton AS ""SoLuong"",
                        COALESCE(hh.donvitinh, 'Kg') AS ""DonViTinh"",
                        lh.ngaysanxuat AS ""NgayCatHang""
                    FROM tonkho tk
                    INNER JOIN lohang lh ON tk.malo = lh.malo
                    INNER JOIN hanghoa hh ON lh.mahang = hh.mahang";

                dtLoHangAtViTri = DatabaseHelper.ExecuteQuery(queryLoHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangLuoi()
        {
            if (dgvViTri.Columns.Contains("MaViTri")) dgvViTri.Columns["MaViTri"].HeaderText = "Mã Vị Trí";
            if (dgvViTri.Columns.Contains("TenKho")) dgvViTri.Columns["TenKho"].HeaderText = "Kho";
            if (dgvViTri.Columns.Contains("KhuVuc")) dgvViTri.Columns["KhuVuc"].HeaderText = "Khu Vực";
            if (dgvViTri.Columns.Contains("Day")) dgvViTri.Columns["Day"].HeaderText = "Dãy";
            if (dgvViTri.Columns.Contains("Ke")) dgvViTri.Columns["Ke"].HeaderText = "Kệ";
            if (dgvViTri.Columns.Contains("OChua")) dgvViTri.Columns["OChua"].HeaderText = "Ô Chứa";
            if (dgvViTri.Columns.Contains("SucChuaToida")) dgvViTri.Columns["SucChuaToida"].HeaderText = "Sức Chứa Tối Đa";
            if (dgvViTri.Columns.Contains("SucChuaDaDung")) dgvViTri.Columns["SucChuaDaDung"].HeaderText = "Đã Dùng";
            if (dgvViTri.Columns.Contains("PhanTramLapDay")) dgvViTri.Columns["PhanTramLapDay"].HeaderText = "% Lấp Đầy";
            if (dgvViTri.Columns.Contains("TrangThai")) dgvViTri.Columns["TrangThai"].HeaderText = "Trạng Thái";

            if (dgvViTri.Columns.Contains("SucChuaToida")) dgvViTri.Columns["SucChuaToida"].DefaultCellStyle.Format = "#,##0";
            if (dgvViTri.Columns.Contains("SucChuaDaDung")) dgvViTri.Columns["SucChuaDaDung"].DefaultCellStyle.Format = "#,##0";
            if (dgvViTri.Columns.Contains("PhanTramLapDay")) dgvViTri.Columns["PhanTramLapDay"].DefaultCellStyle.Format = "0.0'%'";

            if (dgvChiTietLoHang.Columns.Contains("MaViTri")) dgvChiTietLoHang.Columns["MaViTri"].Visible = false;
            if (dgvChiTietLoHang.Columns.Contains("MaLo")) dgvChiTietLoHang.Columns["MaLo"].HeaderText = "Mã Lô";
            if (dgvChiTietLoHang.Columns.Contains("MaHang")) dgvChiTietLoHang.Columns["MaHang"].HeaderText = "Mã Hàng";
            if (dgvChiTietLoHang.Columns.Contains("TenHang")) dgvChiTietLoHang.Columns["TenHang"].HeaderText = "Tên Mặt Hàng";
            if (dgvChiTietLoHang.Columns.Contains("SoLuong")) dgvChiTietLoHang.Columns["SoLuong"].HeaderText = "Số Lượng Lưu Trữ";
            if (dgvChiTietLoHang.Columns.Contains("DonViTinh")) dgvChiTietLoHang.Columns["DonViTinh"].HeaderText = "ĐVT";
            if (dgvChiTietLoHang.Columns.Contains("NgayCatHang")) dgvChiTietLoHang.Columns["NgayCatHang"].HeaderText = "Ngày Cất Hàng";

            if (dgvChiTietLoHang.Columns.Contains("SoLuong")) dgvChiTietLoHang.Columns["SoLuong"].DefaultCellStyle.Format = "#,##0";
            if (dgvChiTietLoHang.Columns.Contains("NgayCatHang")) dgvChiTietLoHang.Columns["NgayCatHang"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void dgvViTri_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvViTri.CurrentRow == null || dtLoHangAtViTri == null) return;
            DataRowView drv = dgvViTri.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            string maViTri = drv["MaViTri"]?.ToString();
            lblLoHangTitle.Text = $"📦 Các lô hàng hiện đang lưu trữ tại ô [{maViTri}]:";
            dtLoHangAtViTri.DefaultView.RowFilter = $"MaViTri = '{maViTri}'";
        }

        private void dgvViTri_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvViTri.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Khả dụng") e.CellStyle.ForeColor = Color.ForestGreen;
                else if (status == "Đầy") e.CellStyle.ForeColor = Color.Crimson;
                else e.CellStyle.ForeColor = Color.DarkOrange;
                e.CellStyle.Font = new Font(dgvViTri.Font, FontStyle.Bold);
            }

            if (dgvViTri.Columns[e.ColumnIndex].Name == "PhanTramLapDay" && e.Value != DBNull.Value && e.Value != null)
            {
                double pct = Convert.ToDouble(e.Value);
                if (pct >= 100) e.CellStyle.BackColor = Color.MistyRose;
                else if (pct >= 80) e.CellStyle.BackColor = Color.LightYellow;
            }
        }

        private void btnTaoViTri_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo vị trí lưu trữ mới!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrTaoViTri frm = new FrTaoViTri();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromDatabase();
                dgvViTri.DataSource = dtViTri;
                dgvChiTietLoHang.DataSource = dtLoHangAtViTri;
                MessageBox.Show("Thêm mới ô chứa vào kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCapNhatViTri_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền cập nhật vị trí lưu trữ!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvViTri.CurrentRow == null) return;
            DataRowView drv = dgvViTri.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            DataRow selectedRow = drv.Row;
            FrCapNhatViTri frm = new FrCapNhatViTri(selectedRow);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromDatabase();
                dgvViTri.DataSource = dtViTri;
                dgvChiTietLoHang.DataSource = dtLoHangAtViTri;
                MessageBox.Show("Đã cập nhật vị trí thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnKiemTraSucChua_Click(object sender, EventArgs e)
        {
            int totalBins = dtViTri.Rows.Count;
            int fullBins = 0;
            double totalCap = 0;
            double totalUsed = 0;

            foreach (DataRow row in dtViTri.Rows)
            {
                double cap = Convert.ToDouble(row["SucChuaToida"]);
                double used = Convert.ToDouble(row["SucChuaDaDung"]);
                totalCap += cap;
                totalUsed += used;
                if (used >= cap || row["TrangThai"].ToString() == "Đầy") fullBins++;
            }

            double avgPct = totalCap > 0 ? (totalUsed / totalCap) * 100 : 0;
            string report = $"📊 --- BÁO CÁO TỔNG QUAN SỨC CHỨA KHO ---\n\n" +
                            $"• Tổng số ô chứa toàn hệ thống: {totalBins} ô\n" +
                            $"• Số ô đã lấp đầy (100%): {fullBins} ô\n" +
                            $"• Tổng công suất chứa tối đa: {totalCap:N0}\n" +
                            $"• Tổng dung lượng đang sử dụng: {totalUsed:N0}\n" +
                            $"• Tỷ lệ lấp đầy toàn kho: {avgPct:F1}%\n\n" +
                            $"=> Đánh giá: Kho hoạt động ổn định.";
            MessageBox.Show(report, "Cảnh Báo Sức Chứa Realtime", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPhanBoViTri_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền phân bổ vị trí lưu trữ!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrPhanBoViTri frm = new FrPhanBoViTri();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromDatabase();
                dgvViTri.DataSource = dtViTri;
                dgvChiTietLoHang.DataSource = dtLoHangAtViTri;
                dgvViTri_SelectionChanged(null, null);
            }
        }

        private void btnChuyenViTri_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền chuyển vị trí lưu trữ!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvChiTietLoHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 lô hàng cụ thể để chuyển vị trí!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = dgvChiTietLoHang.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            FrChuyenViTri frm = new FrChuyenViTri(drv.Row);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromDatabase();
                dgvViTri.DataSource = dtViTri;
                dgvChiTietLoHang.DataSource = dtLoHangAtViTri;
                dgvViTri_SelectionChanged(null, null);
            }
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            if (dtViTri == null) return;
            string filter = "1=1";

            if (cboFilterKho.SelectedIndex > 0)
                filter += $" AND TenKho = '{cboFilterKho.SelectedItem}'";

            if (cboFilterTrangThai.SelectedIndex > 0)
                filter += $" AND TrangThai = '{cboFilterTrangThai.SelectedItem}'";

            if (!string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != placeholderSearch)
            {
                string kw = txtSearch.Text.Trim().Replace("'", "''");
                filter += $" AND (MaViTri LIKE '%{kw}%' OR KhuVuc LIKE '%{kw}%' OR Ke LIKE '%{kw}%' OR OChua LIKE '%{kw}%')";
            }

            dtViTri.DefaultView.RowFilter = filter;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => Filter_Changed(sender, e);

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == placeholderSearch) { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = placeholderSearch; txtSearch.ForeColor = Color.Gray; }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Text = placeholderSearch;
            txtSearch.ForeColor = Color.Gray;
            cboFilterKho.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndex = 0;
            dtViTri.DefaultView.RowFilter = "";
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrQLNhapKho : Form
    {
        private const string PLACEHOLDER = "Tìm mã phiếu, NCC, người lập...";

        public FrQLNhapKho()
        {
            InitializeComponent();
        }

        private void FrQLNhapKho_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            // Kéo giãn tất cả các cột trải đều full toàn bộ chiều rộng bảng DataGridView
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadComboboxFilters();
            LoadDataDataGrid();
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

        private bool KiemTraQuyenQuanLyHoacAdmin(string tenChucNang)
        {
            string chucVu = UserSession.ChucVu ?? "";
            bool isToanQuyen = chucVu.Equals("Quản lý kho", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản lý", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản trị viên", StringComparison.OrdinalIgnoreCase);

            if (!isToanQuyen)
            {
                MessageBox.Show($"Tài khoản vai trò ({chucVu}) không có quyền thực hiện [{tenChucNang}]!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void LoadComboboxFilters()
        {
            cboLoaiHang.Items.Clear();
            cboLoaiHang.Items.AddRange(new object[] {
                "-- Tất cả loại hàng --",
                "Thành phẩm",
                "Nguyên liệu",
                "Bao bì",
                "Vật tư"
            });
            cboLoaiHang.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] {
                "-- Tất cả trạng thái --",
                "Chờ duyệt",
                "Đã duyệt",
                "Đã nhập kho",
                "Đã hủy"
            });
            cboTrangThai.SelectedIndex = 0;
        }

        public void LoadDataDataGrid()
        {
            try
            {
                string query = @"
                    SELECT DISTINCT
                        pn.maphieunhap AS ""Mã Phiếu"",
                        pn.loainhap AS ""Loại Hàng"",
                        pn.loainhap AS ""Nguồn Nhập (NCC/SX)"",
                        COALESCE(k.tenkho, 'Chờ phân bổ kho') AS ""Kho Nhập"",
                        pn.ngaylap AS ""Ngày Lập"",
                        COALESCE(nd.hoten, pn.nguoilap) AS ""Người Lập"",
                        hh.mahang AS ""Mã Hàng"",
                        hh.tenhang AS ""Tên Hàng"",
                        COALESCE(tk.soluongton, ctpn.soluong) AS ""Số Lượng"",
                        COALESCE(hh.donvitinh, 'Kg') AS ""ĐVT"",
                        lh.malo AS ""Mã Lô"",
                        lh.ngaysanxuat AS ""Ngày SX"",
                        lh.hansudung AS ""Hạn SD"",
                        COALESCE(vt.mavitri, 'Chờ phân bổ vị trí') AS ""Vị Trí Lưu Trữ"",
                        pn.trangthai AS ""Trạng Thái""
                    FROM phieunhap pn
                    INNER JOIN chitietphieunhap ctpn ON pn.maphieunhap = ctpn.maphieunhap
                    INNER JOIN lohang lh ON ctpn.malo = lh.malo
                    INNER JOIN hanghoa hh ON lh.mahang = hh.mahang
                    LEFT JOIN nguoidung nd ON pn.nguoilap = nd.manguoidung
                    LEFT JOIN tonkho tk ON lh.malo = tk.malo
                    LEFT JOIN vitriluutru vt ON tk.mavitri = vt.mavitri
                    LEFT JOIN kho k ON vt.makho = k.makho
                    WHERE pn.ngaylap >= @TuNgay AND pn.ngaylap <= @DenNgay";

                if (cboLoaiHang.SelectedIndex > 0)
                {
                    string loai = cboLoaiHang.SelectedItem.ToString();
                    if (loai == "Thành phẩm")
                    {
                        query += @" AND (
                            pn.loainhap = 'Thành phẩm' 
                            OR hh.mahang IN (SELECT mahang FROM sanpham)
                            OR hh.mahang LIKE 'SP%' OR hh.mahang LIKE 'HH%'
                            OR hh.tenhang LIKE '%Mì%' OR hh.tenhang LIKE '%Miến%'
                        )";
                    }
                    else if (loai == "Nguyên liệu")
                    {
                        query += @" AND (
                            pn.loainhap = 'Nguyên liệu' 
                            OR (hh.mahang LIKE 'NL%' AND hh.mahang NOT IN (SELECT mahang FROM sanpham))
                            OR hh.tenhang LIKE '%Bột%' OR hh.tenhang LIKE '%Gia vị%' OR hh.tenhang LIKE '%Dầu%'
                        )";
                    }
                    else if (loai == "Bao bì")
                    {
                        query += @" AND (
                            pn.loainhap = 'Bao bì' 
                            OR hh.mahang LIKE 'BB%'
                            OR hh.tenhang LIKE '%Bao%' OR hh.tenhang LIKE '%Thùng%' OR hh.tenhang LIKE '%Màng%'
                        )";
                    }
                    else if (loai == "Vật tư")
                    {
                        query += @" AND (
                            pn.loainhap = 'Vật tư' 
                            OR hh.mahang LIKE 'VT%'
                            OR hh.tenhang LIKE '%Vật tư%' OR hh.tenhang LIKE '%Băng keo%'
                        )";
                    }
                }

                if (cboTrangThai.SelectedIndex > 0)
                {
                    query += " AND pn.trangthai = @TrangThai";
                }

                string searchKeyword = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(searchKeyword) && searchKeyword != PLACEHOLDER)
                {
                    query += " AND (pn.maphieunhap LIKE @Search OR nd.hoten LIKE @Search OR hh.tenhang LIKE @Search)";
                }

                query += " ORDER BY pn.ngaylap DESC";

                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? ""),
                    new SqlParameter("@Search", "%" + searchKeyword + "%")
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                dgvDanhSach.DataSource = dt;

                // Tự động căn dãn full trang cho bảng
                dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvDanhSach.Columns["Số Lượng"] != null)
                    dgvDanhSach.Columns["Số Lượng"].DefaultCellStyle.Format = "#,##0";
                if (dgvDanhSach.Columns["Ngày Lập"] != null)
                    dgvDanhSach.Columns["Ngày Lập"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                if (dgvDanhSach.Columns["Ngày SX"] != null)
                    dgvDanhSach.Columns["Ngày SX"].DefaultCellStyle.Format = "dd/MM/yyyy";
                if (dgvDanhSach.Columns["Hạn SD"] != null)
                    dgvDanhSach.Columns["Hạn SD"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phiếu nhập kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e) => LoadDataDataGrid();
        private void dtpDenNgay_ValueChanged(object sender, EventArgs e) => LoadDataDataGrid();
        private void cboLoaiHang_SelectedIndexChanged(object sender, EventArgs e) => LoadDataDataGrid();
        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e) => LoadDataDataGrid();
        private void txtTimKiem_TextChanged(object sender, EventArgs e) => LoadDataDataGrid();

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = PLACEHOLDER;
            txtTimKiem.ForeColor = Color.Gray;
            cboLoaiHang.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
            LoadDataDataGrid();
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == PLACEHOLDER)
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = PLACEHOLDER;
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDanhSach.Columns[e.ColumnIndex].Name == "Trạng Thái" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Chờ duyệt")
                {
                    e.CellStyle.Font = new Font(dgvDanhSach.Font, FontStyle.Bold);
                    e.CellStyle.ForeColor = Color.DarkOrange;
                }
                else if (status == "Đã duyệt")
                {
                    e.CellStyle.Font = new Font(dgvDanhSach.Font, FontStyle.Bold);
                    e.CellStyle.ForeColor = Color.RoyalBlue;
                }
                else if (status == "Đã nhập kho")
                {
                    e.CellStyle.Font = new Font(dgvDanhSach.Font, FontStyle.Bold);
                    e.CellStyle.ForeColor = Color.ForestGreen;
                }
                else if (status == "Đã hủy")
                {
                    e.CellStyle.Font = new Font(dgvDanhSach.Font, FontStyle.Regular);
                    e.CellStyle.ForeColor = Color.Crimson;
                }
            }
        }

        private string GetSelectedMaPhieu()
        {
            if (dgvDanhSach.SelectedRows.Count > 0)
            {
                return dgvDanhSach.SelectedRows[0].Cells["Mã Phiếu"].Value?.ToString();
            }
            return null;
        }

        private void btnTaoPhieu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo phiếu nhập kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrLapPhieuNhap fr = new FrLapPhieuNhap(this);
            if (fr.ShowDialog() == DialogResult.OK)
            {
                LoadDataDataGrid();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền cập nhật phiếu nhập kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = GetSelectedMaPhieu();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrCapNhatPhieuNhap fr = new FrCapNhatPhieuNhap(maPhieu, this);
            if (fr.ShowDialog() == DialogResult.OK)
            {
                LoadDataDataGrid();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Hủy phiếu nhập")) return;

            string maPhieu = GetSelectedMaPhieu();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn HỦY phiếu nhập [{maPhieu}]?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "UPDATE phieunhap SET trangthai = 'Đã hủy' WHERE maphieunhap = @MaPhieu AND trangthai = 'Chờ duyệt'";
                int rows = DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });

                if (rows > 0)
                {
                    MessageBox.Show("Hủy phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataDataGrid();
                }
                else
                {
                    MessageBox.Show("Chỉ có thể hủy phiếu nhập đang ở trạng thái 'Chờ duyệt'!", "Không thể hủy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDuyetPhieu_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Duyệt phiếu nhập")) return;

            string maPhieu = GetSelectedMaPhieu();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "UPDATE phieunhap SET trangthai = 'Đã duyệt' WHERE maphieunhap = @MaPhieu AND trangthai = 'Chờ duyệt'";
            int rows = DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });

            if (rows > 0)
            {
                MessageBox.Show($"Duyệt phiếu nhập [{maPhieu}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataDataGrid();
            }
            else
            {
                MessageBox.Show("Phiếu này đã được duyệt hoặc không nằm ở trạng thái 'Chờ duyệt'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXacNhanNhap_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Xác nhận nhập kho")) return;

            string maPhieu = GetSelectedMaPhieu();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần xác nhận nhập kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sqlCheckMultiLocation = @"
                    SELECT 
                        ctpn.malo, 
                        ctpn.soluong AS SLPhieu, 
                        COALESCE(SUM(tk.soluongton), 0) AS SLDaXepKho
                    FROM chitietphieunhap ctpn
                    LEFT JOIN tonkho tk ON ctpn.malo = tk.malo
                    WHERE ctpn.maphieunhap = @MaPhieu
                    GROUP BY ctpn.malo, ctpn.soluong";

                DataTable dtCheck = DatabaseHelper.ExecuteQuery(sqlCheckMultiLocation, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });

                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    foreach (DataRow r in dtCheck.Rows)
                    {
                        string lo = r["malo"].ToString();
                        decimal slPhieu = Convert.ToDecimal(r["SLPhieu"]);
                        decimal slDaXep = Convert.ToDecimal(r["SLDaXepKho"]);

                        if (slDaXep != slPhieu)
                        {
                            MessageBox.Show($"Không thể Xác nhận nhập kho!\n\n" +
                                            $"- Lô hàng [{lo}] có tổng số lượng nhập là: {slPhieu:#,##0} Kg\n" +
                                            $"- Tổng số lượng đã phân bổ vào các vị trí kho là: {slDaXep:#,##0} Kg\n\n" +
                                            $"Số lượng phân bổ ở các vị trí phải khớp hoàn toàn với phiếu nhập trước khi xác nhận!",
                                            "⚠️ Kiểm Soát Nghiệp Vụ Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                string sqlUpdate = @"
                    UPDATE phieunhap SET trangthai = 'Đã nhập kho' WHERE maphieunhap = @MaPhieu AND trangthai = 'Đã duyệt';
                    
                    UPDATE hanghoa 
                    SET tonkho = tonkho + CAST(sub.soluong AS INT)
                    FROM (
                        SELECT ctpn.soluong, lh.mahang 
                        FROM chitietphieunhap ctpn
                        INNER JOIN lohang lh ON ctpn.malo = lh.malo
                        WHERE ctpn.maphieunhap = @MaPhieu
                    ) AS sub
                    WHERE hanghoa.mahang = sub.mahang;";

                int rows = DatabaseHelper.ExecuteNonQuery(sqlUpdate, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });

                if (rows > 0)
                {
                    MessageBox.Show($"Xác nhận nhập kho thành công cho phiếu [{maPhieu}]!",
                                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataDataGrid();
                }
                else
                {
                    MessageBox.Show("Chỉ phiếu nhập ở trạng thái 'Đã duyệt' mới có thể Xác nhận nhập kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xác nhận nhập kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlTopHeader_Paint(object sender, PaintEventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền xuất dữ liệu Excel!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvDanhSach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất tập tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV File (*.csv)|*.csv",
                FileName = $"DanhSachPhieuNhapKho_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < dgvDanhSach.Columns.Count; i++)
                    {
                        sb.Append(dgvDanhSach.Columns[i].HeaderText + (i == dgvDanhSach.Columns.Count - 1 ? "" : ","));
                    }
                    sb.AppendLine();

                    foreach (DataGridViewRow row in dgvDanhSach.Rows)
                    {
                        for (int j = 0; j < dgvDanhSach.Columns.Count; j++)
                        {
                            string val = row.Cells[j].Value?.ToString().Replace(",", " ") ?? "";
                            sb.Append(val + (j == dgvDanhSach.Columns.Count - 1 ? "" : ","));
                        }
                        sb.AppendLine();
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Xuất danh sách phiếu nhập kho ra tập tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
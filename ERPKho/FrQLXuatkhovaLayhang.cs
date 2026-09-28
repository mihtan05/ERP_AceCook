using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrQLXuatkhovaLayhang : Form
    {
        private const string PLACEHOLDER_SEARCH = "Tìm theo mã phiếu, bộ phận yêu cầu...";

        public FrQLXuatkhovaLayhang()
        {
            InitializeComponent();
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

        private void FrQLXuatkhovaLayhang_Load(object sender, EventArgs e)
        {
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            if (cboFilterLoaiXuat.Items.Count > 0) cboFilterLoaiXuat.SelectedIndex = 0;
            if (cboFilterTrangThai.Items.Count > 0) cboFilterTrangThai.SelectedIndex = 0;

            txtSearch.Text = PLACEHOLDER_SEARCH;
            txtSearch.ForeColor = Color.Gray;

            LoadDataXuatKho();
        }

        public void LoadDataXuatKho()
        {
            try
            {
                string sql = "SELECT " +
                             "px.maphieuxuat AS MaPhieuXuat, " +
                             "px.ngaylap AS NgayLap, " +
                             "px.bophanyeucau AS BoPhanYeuCau, " +
                             "COALESCE(nd1.hoten, px.nguoilap) AS NguoiLap, " +
                             "COALESCE(nd2.hoten, px.nguoiduyet) AS NguoiDuyet, " +
                             "COALESCE(SUM(ctx.soluongyeucau), 0) AS TongSoLuong, " +
                             "px.ngayxacnhan AS NgayXacNhan, " +
                             "COALESCE(px.trangthai, 'Chờ duyệt') AS TrangThai " +
                             "FROM phieuxuat px " +
                             "LEFT JOIN chitietphieuxuat ctx ON px.maphieuxuat = ctx.maphieuxuat " +
                             "LEFT JOIN nguoidung nd1 ON px.nguoilap = nd1.manguoidung " +
                             "LEFT JOIN nguoidung nd2 ON px.nguoiduyet = nd2.manguoidung " +
                             "WHERE 1=1";

                if (cboFilterTrangThai.SelectedIndex > 0 && cboFilterTrangThai.SelectedItem != null)
                {
                    string selectedStatus = cboFilterTrangThai.SelectedItem.ToString();
                    if (selectedStatus != "Tất cả trạng thái")
                    {
                        sql += " AND px.trangthai = @TrangThai";
                    }
                }

                string searchKw = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(searchKw) && searchKw != PLACEHOLDER_SEARCH)
                {
                    sql += " AND (px.maphieuxuat ILIKE @Search OR px.bophanyeucau ILIKE @Search)";
                }

                sql += " GROUP BY px.maphieuxuat, px.ngaylap, px.bophanyeucau, px.nguoilap, px.nguoiduyet, px.ngayxacnhan, px.trangthai, nd1.hoten, nd2.hoten";
                sql += " ORDER BY px.ngaylap DESC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TrangThai", cboFilterTrangThai.SelectedItem?.ToString() ?? ""),
                    new SqlParameter("@Search", "%" + searchKw + "%")
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                dgvXuatKho.DataSource = dt;
                DinhDangLuoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phiếu xuất kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangLuoi()
        {
            if (dgvXuatKho.Columns.Contains("MaPhieuXuat")) dgvXuatKho.Columns["MaPhieuXuat"].HeaderText = "Mã Phiếu Xuất";
            if (dgvXuatKho.Columns.Contains("NgayLap"))
            {
                dgvXuatKho.Columns["NgayLap"].HeaderText = "Ngày Lập";
                dgvXuatKho.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvXuatKho.Columns.Contains("BoPhanYeuCau")) dgvXuatKho.Columns["BoPhanYeuCau"].HeaderText = "Bộ Phận Yêu Cầu";
            if (dgvXuatKho.Columns.Contains("NguoiLap")) dgvXuatKho.Columns["NguoiLap"].HeaderText = "Người Lập";
            if (dgvXuatKho.Columns.Contains("NguoiDuyet")) dgvXuatKho.Columns["NguoiDuyet"].HeaderText = "Người Duyệt";
            if (dgvXuatKho.Columns.Contains("TongSoLuong"))
            {
                dgvXuatKho.Columns["TongSoLuong"].HeaderText = "Tổng SL Xuất";
                dgvXuatKho.Columns["TongSoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvXuatKho.Columns["TongSoLuong"].DefaultCellStyle.Format = "#,##0.##";
            }
            if (dgvXuatKho.Columns.Contains("NgayXacNhan"))
            {
                dgvXuatKho.Columns["NgayXacNhan"].HeaderText = "Ngày Xác Nhận";
                dgvXuatKho.Columns["NgayXacNhan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvXuatKho.Columns.Contains("TrangThai")) dgvXuatKho.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void Filter_Changed(object sender, EventArgs e) => LoadDataXuatKho();

        private void txtSearch_TextChanged(object sender, EventArgs e) => Filter_Changed(sender, e);

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == PLACEHOLDER_SEARCH)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = PLACEHOLDER_SEARCH;
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void dgvXuatKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvXuatKho.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Hoàn tất xuất" || status == "Đã xuất kho") e.CellStyle.ForeColor = Color.DarkGreen;
                else if (status == "Đang lấy hàng" || status == "Picking") e.CellStyle.ForeColor = Color.DarkOrange;
                else if (status == "Chờ duyệt") e.CellStyle.ForeColor = Color.DodgerBlue;
                else if (status == "Đã duyệt") e.CellStyle.ForeColor = Color.Purple;
                else if (status == "Đã hủy") e.CellStyle.ForeColor = Color.Red;
                e.CellStyle.Font = new Font(dgvXuatKho.Font, FontStyle.Bold);
            }
        }

        private DataRow GetSelectedRow()
        {
            if (dgvXuatKho.CurrentRow != null && dgvXuatKho.CurrentRow.DataBoundItem is DataRowView drv)
            {
                return drv.Row;
            }
            return null;
        }

        private void btnTaoPhieuXuat_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo phiếu xuất kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrTaoPhieuXuat frm = new FrTaoPhieuXuat();
            if (frm.ShowDialog() == DialogResult.OK) LoadDataXuatKho();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền cập nhật phiếu xuất kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = GetSelectedRow();
            if (row == null) { MessageBox.Show("Vui lòng chọn phiếu xuất cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string maPX = row["MaPhieuXuat"]?.ToString();
            FrCapNhatPhieuXuat frm = new FrCapNhatPhieuXuat(maPX);
            if (frm.ShowDialog() == DialogResult.OK) LoadDataXuatKho();
        }

        private void btnDuyetPhieu_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Duyệt phiếu xuất kho")) return;

            DataRow row = GetSelectedRow();
            if (row == null) { MessageBox.Show("Vui lòng chọn phiếu xuất cần duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string maPX = row.Table.Columns.Contains("MaPhieuXuat") ? row["MaPhieuXuat"]?.ToString()?.Trim() :
                          row.Table.Columns.Contains("maphieuxuat") ? row["maphieuxuat"]?.ToString()?.Trim() : "";

            if (string.IsNullOrEmpty(maPX)) return;

            string nguoiDuyet = UserSession.MaNguoiDung ?? "";

            try
            {
                // Sử dụng Subquery để đảm bảo nguoiduyet luôn hợp lệ với bảng nguoidung (tránh lỗi FK 23503)
                string sql = @"
                    UPDATE phieuxuat 
                    SET trangthai = 'Đã duyệt', 
                        nguoiduyet = COALESCE(
                            (SELECT manguoidung FROM nguoidung WHERE TRIM(manguoidung) = TRIM(@NguoiDuyet) LIMIT 1),
                            (SELECT manguoidung FROM nguoidung WHERE TRIM(manguoidung) = TRIM(phieuxuat.nguoilap) LIMIT 1),
                            (SELECT manguoidung FROM nguoidung LIMIT 1)
                        ) 
                    WHERE TRIM(maphieuxuat) = TRIM(@Ma)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@NguoiDuyet", nguoiDuyet),
                    new SqlParameter("@Ma", maPX)
                };

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                MessageBox.Show($"Duyệt lệnh xuất kho [{maPX}] thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataXuatKho();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi duyệt phiếu xuất: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyPhieu_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Hủy phiếu xuất kho")) return;

            DataRow row = GetSelectedRow();
            if (row == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất cần hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPX = row["MaPhieuXuat"].ToString();
            string trangThai = row["TrangThai"]?.ToString() ?? "Chờ duyệt";

            if (!trangThai.Equals("Chờ duyệt", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Không thể hủy phiếu xuất [{maPX}]!\n\nQuy định hệ thống: Chỉ được phép hủy các đơn hàng đang ở trạng thái [Chờ duyệt]. Phiếu này hiện tại đang ở trạng thái [{trangThai}].",
                                "Từ chối hủy đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn HỦY phiếu xuất kho [{maPX}]?", "Xác nhận hủy đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "UPDATE phieuxuat SET trangthai = 'Đã hủy' WHERE TRIM(maphieuxuat) = TRIM(@Ma) AND trangthai = 'Chờ duyệt'";
                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@Ma", maPX) });

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Hủy phiếu xuất kho [{maPX}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataXuatKho();
                    }
                    else
                    {
                        MessageBox.Show("Hủy phiếu thất bại! Đơn hàng có thể đã được thay đổi trạng thái trước đó.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadDataXuatKho();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi SQL khi hủy phiếu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLapDSLayHang_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền lập danh sách lấy hàng!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = GetSelectedRow();
            if (row == null) { MessageBox.Show("Vui lòng chọn phiếu xuất để tạo danh sách lấy hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            FrLapDanhSachLayHangFEFO frm = new FrLapDanhSachLayHangFEFO(row);
            if (frm.ShowDialog() == DialogResult.OK) LoadDataXuatKho();
        }

        private void btnXacNhanXuat_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Xác nhận xuất kho")) return;

            DataRow row = GetSelectedRow();
            if (row == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất cần xác nhận hoàn tất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrXacNhanXuatKho frm = new FrXacNhanXuatKho(row);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataXuatKho();
            }
        }

        private void dgvXuatKho_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnCapNhat_Click(sender, e);
        }

        private void pnlMainContent_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
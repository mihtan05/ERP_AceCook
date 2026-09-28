using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrCapNhatPhieuXuat : Form
    {
        private readonly string maPhieuXuat;
        private DataTable dtChiTiet;

        public FrCapNhatPhieuXuat(string maPX)
        {
            InitializeComponent();
            this.maPhieuXuat = maPX;
            txtMaPX.Text = maPX;
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

        private void FrCapNhatPhieuXuat_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền cập nhật phiếu xuất kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadThongTinPhieu();
            LoadChiTietHangHoa();
        }

        private void LoadThongTinPhieu()
        {
            try
            {
                string sql = "SELECT boPhanYeuCau, ngayLap, trangThai FROM PhieuXuat WHERE maPhieuXuat = @MaPX";
                SqlParameter[] p = { new SqlParameter("@MaPX", maPhieuXuat) };
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, p);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtBoPhan.Text = r["boPhanYeuCau"]?.ToString();
                    if (r["ngayLap"] != DBNull.Value)
                        dtpNgayLap.Value = Convert.ToDateTime(r["ngayLap"]);

                    string status = r["trangThai"]?.ToString();
                    lblTrangThaiVal.Text = status;

                    // Nếu đã qua các bước xử lý hoặc đã hủy, khóa form không cho sửa
                    if (status != "Chờ duyệt")
                    {
                        btnLuu.Enabled = false;
                        btnLuu.BackColor = Color.Gray;
                        txtBoPhan.ReadOnly = true;
                        dtpNgayLap.Enabled = false;
                        dgvChiTiet.ReadOnly = true;

                        if (status == "Hoàn tất xuất" || status == "Đã hủy")
                        {
                            btnHuyPhieu.Enabled = false;
                            btnHuyPhieu.BackColor = Color.Gray;
                        }

                        if (status != "Chờ duyệt")
                        {
                            MessageBox.Show($"Phiếu xuất đã ở trạng thái [{status}], chỉ các phiếu ở trạng thái [Chờ duyệt] mới được phép chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phiếu xuất: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietHangHoa()
        {
            try
            {
                string sql = @"
                    SELECT 
                        ct.maCTPX,
                        ct.MaHang,
                        hh.TenHang,
                        hh.DonViTinh,
                        ct.soLuongYeuCau
                    FROM ChiTietPhieuXuat ct
                    INNER JOIN HangHoa hh ON ct.MaHang = hh.MaHang
                    WHERE ct.maPhieuXuat = @MaPX";

                SqlParameter[] p = { new SqlParameter("@MaPX", maPhieuXuat) };
                dtChiTiet = DatabaseHelper.ExecuteQuery(sql, p);

                dgvChiTiet.DataSource = dtChiTiet;

                if (dgvChiTiet.Columns["maCTPX"] != null) dgvChiTiet.Columns["maCTPX"].Visible = false;

                if (dgvChiTiet.Columns["MaHang"] != null)
                {
                    dgvChiTiet.Columns["MaHang"].HeaderText = "Mã Hàng";
                    dgvChiTiet.Columns["MaHang"].ReadOnly = true;
                }
                if (dgvChiTiet.Columns["TenHang"] != null)
                {
                    dgvChiTiet.Columns["TenHang"].HeaderText = "Tên Vật Tư / Sản Phẩm";
                    dgvChiTiet.Columns["TenHang"].ReadOnly = true;
                }
                if (dgvChiTiet.Columns["DonViTinh"] != null)
                {
                    dgvChiTiet.Columns["DonViTinh"].HeaderText = "ĐVT";
                    dgvChiTiet.Columns["DonViTinh"].ReadOnly = true;
                }
                if (dgvChiTiet.Columns["soLuongYeuCau"] != null)
                {
                    dgvChiTiet.Columns["soLuongYeuCau"].HeaderText = "Số Lượng Yêu Cầu (Nhập để sửa)";
                    dgvChiTiet.Columns["soLuongYeuCau"].ReadOnly = false;
                    dgvChiTiet.Columns["soLuongYeuCau"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chi tiết hàng hóa: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBoPhan.Text))
            {
                MessageBox.Show("Vui lòng nhập bộ phận yêu cầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string sqlUpdatePX = @"
                        UPDATE PhieuXuat 
                        SET boPhanYeuCau = @BoPhan, ngayLap = @NgayLap 
                        WHERE maPhieuXuat = @MaPX AND trangThai = 'Chờ duyệt'";

                    SqlCommand cmdPX = new SqlCommand(sqlUpdatePX, conn, transaction);
                    cmdPX.Parameters.AddWithValue("@BoPhan", txtBoPhan.Text.Trim());
                    cmdPX.Parameters.AddWithValue("@NgayLap", dtpNgayLap.Value);
                    cmdPX.Parameters.AddWithValue("@MaPX", maPhieuXuat);
                    int rows = cmdPX.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Không thể lưu! Phiếu xuất đã được duyệt hoặc không còn ở trạng thái 'Chờ duyệt'.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dgvChiTiet.EndEdit();
                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        if (row.RowState == DataRowState.Deleted) continue;

                        string maCTPX = row["maCTPX"].ToString();
                        decimal soLuongMoi = Convert.ToDecimal(row["soLuongYeuCau"]);

                        string sqlUpdateCT = @"
                            UPDATE ChiTietPhieuXuat 
                            SET soLuongYeuCau = @SL 
                            WHERE maCTPX = @MaCTPX";

                        SqlCommand cmdCT = new SqlCommand(sqlUpdateCT, conn, transaction);
                        cmdCT.Parameters.AddWithValue("@SL", soLuongMoi);
                        cmdCT.Parameters.AddWithValue("@MaCTPX", maCTPX);
                        cmdCT.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Cập nhật thông tin và số lượng xuất kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi khi lưu thông tin: " + ex.Message, "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuyPhieu_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Hủy phiếu xuất kho")) return;

            // KIỂM TRA ĐIỀU KIỆN CHẶT CHẼ TRƯỚC KHI HỦY: Chỉ được phép hủy khi trạng thái là 'Chờ duyệt'
            string currentStatus = lblTrangThaiVal.Text.Trim();
            if (!currentStatus.Equals("Chờ duyệt", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Không thể hủy phiếu xuất [{maPhieuXuat}]!\n\nQuy định nhà máy: Chỉ được phép hủy đơn khi phiếu đang ở trạng thái [Chờ duyệt]. Phiếu này hiện tại đang ở trạng thái [{currentStatus}].",
                                "Từ chối hủy đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn HỦY PHIẾU XUẤT [{maPhieuXuat}] không?", "Xác nhận hủy phiếu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Thêm điều kiện AND trangThai = 'Chờ duyệt' vào câu lệnh SQL để bảo vệ tuyệt đối dữ liệu
                    string sqlHuyPX = "UPDATE PhieuXuat SET trangThai = N'Đã hủy' WHERE maPhieuXuat = @MaPX AND trangThai = 'Chờ duyệt'";
                    SqlCommand cmdPX = new SqlCommand(sqlHuyPX, conn, transaction);
                    cmdPX.Parameters.AddWithValue("@MaPX", maPhieuXuat);
                    int rows = cmdPX.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Hủy phiếu thất bại! Đơn hàng đã được duyệt hoặc thay đổi trạng thái trước đó.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sqlHuyDS = "UPDATE DanhSachLayHang SET trangThai = N'Đã hủy' WHERE maPhieuXuat = @MaPX";
                    SqlCommand cmdDS = new SqlCommand(sqlHuyDS, conn, transaction);
                    cmdDS.Parameters.AddWithValue("@MaPX", maPhieuXuat);
                    cmdDS.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show($"Đã HỦY phiếu xuất kho [{maPhieuXuat}] thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi khi hủy phiếu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
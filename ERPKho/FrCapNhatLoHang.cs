using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrCapNhatLoHang : Form
    {
        private readonly string maLo;
        private readonly string tenVT;

        public FrCapNhatLoHang(string _maLo, string _tenVT)
        {
            InitializeComponent();
            maLo = _maLo;
            tenVT = _tenVT;
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

        private void FrCapNhatLoHang_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền sửa thông tin lô hàng!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            txtMaLo.Text = maLo;
            txtTenVatTu.Text = tenVT;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] {
                "Khả dụng",
                "Còn hạn an toàn",
                "Sắp hết hạn",
                "Cần ưu tiên xuất (FEFO)",
                "Đã khóa",
                "Phong tỏa"
            });

            LoadTrangThaiHienTai();
        }

        private void LoadTrangThaiHienTai()
        {
            try
            {
                string sql = "SELECT trangthai FROM lohang WHERE malo = @MaLo";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaLo", maLo) });

                if (dt != null && dt.Rows.Count > 0)
                {
                    string tt = dt.Rows[0]["trangthai"]?.ToString();
                    if (!string.IsNullOrEmpty(tt) && cboTrangThai.Items.Contains(tt))
                    {
                        cboTrangThai.SelectedItem = tt;
                    }
                    else
                    {
                        cboTrangThai.SelectedIndex = 0;
                    }
                }
            }
            catch { cboTrangThai.SelectedIndex = 0; }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string trangThaiMoi = cboTrangThai.SelectedItem?.ToString() ?? "Khả dụng";

            try
            {
                string sqlUpdate = "UPDATE lohang SET trangthai = @TrangThai WHERE malo = @MaLo";
                int rows = DatabaseHelper.ExecuteNonQuery(sqlUpdate, new SqlParameter[] {
                    new SqlParameter("@TrangThai", trangThaiMoi),
                    new SqlParameter("@MaLo", maLo)
                });

                if (rows > 0)
                {
                    MessageBox.Show($"Cập nhật thông tin lô hàng [{maLo}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật lô hàng: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e) => this.Close();
    }
}
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrKhoaLoHang : Form
    {
        private readonly string maLo;

        public FrKhoaLoHang(string _maLo)
        {
            InitializeComponent();
            maLo = _maLo;
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

        private void FrKhoaLoHang_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền phong tỏa/khóa lô hàng!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            txtMaLo.Text = maLo;
            txtLyDo.Text = "Lô hàng bị lỗi chất lượng / Quá hạn sử dụng cần phong tỏa";
        }

        private void btnXacNhanKhoa_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn phong tỏa/khóa lô hàng [{maLo}]?", "Xác nhận khóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string sqlUpdate = "UPDATE lohang SET trangthai = 'Phong tỏa' WHERE malo = @MaLo";
                    int rows = DatabaseHelper.ExecuteNonQuery(sqlUpdate, new SqlParameter[] { new SqlParameter("@MaLo", maLo) });

                    if (rows > 0)
                    {
                        MessageBox.Show($"Đã phong tỏa thành công lô hàng [{maLo}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi khóa lô hàng: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e) => this.Close();
    }
}
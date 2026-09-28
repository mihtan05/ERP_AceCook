using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrDangNhap : Form
    {
        public FrDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Cập nhật truy vấn chuẩn PostgreSQL: dùng COALESCE thay ISNULL và chữ thường tên bảng/cột
                string sql = @"SELECT h.mataikhoan AS ""maNguoiDung"", 
                                      COALESCE(n.tennv, h.tendangnhap) AS ""tenNguoiDung"", 
                                      COALESCE(n.chucvu, h.vaitro) AS ""chucVu"" 
                               FROM hethong h 
                               LEFT JOIN nhanvien n ON h.id_nv = n.id_nv 
                               WHERE h.tendangnhap = @User AND h.matkhau = @Pass";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@User", username),
                    new SqlParameter("@Pass", password)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Lưu thông tin người dùng vào Session
                    UserSession.MaNguoiDung = dt.Rows[0]["maNguoiDung"].ToString();
                    UserSession.TenNguoiDung = dt.Rows[0]["tenNguoiDung"].ToString();
                    UserSession.ChucVu = dt.Rows[0]["chucVu"] != DBNull.Value ? dt.Rows[0]["chucVu"].ToString() : "Admin";

                    MessageBox.Show($"Đăng nhập thành công! Xin chào {UserSession.TenNguoiDung}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void FrDangNhap_Load(object sender, EventArgs e)
        {
            // Có thể để trống hoặc thêm code khởi tạo khi form hiển thị
        }
    }
}
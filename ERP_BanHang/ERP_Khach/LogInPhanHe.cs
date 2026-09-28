using System;
using System.Windows.Forms;

namespace ERP_Khach
{
    public partial class LogInPhanHe : Form
    {
        public LogInPhanHe()
        {
            InitializeComponent();
        }

        private void LogInPhanHe_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            lblThoiGian.Text = DateTime.Now.ToString("dddd, dd 'tháng' MM 'năm' yyyy · HH:mm");
        }

        private void pnlSanXuat_Click(object sender, EventArgs e)
        {
            MoFormDangNhap("Sản xuất");
        }

        private void pnlBanHang_Click(object sender, EventArgs e)
        {
            MoFormDangNhap("Bán hàng");
        }

        private void pnlLogistics_Click(object sender, EventArgs e)
        {
            MoFormDangNhap("Logistics");
        }

        private void pnlKho_Click(object sender, EventArgs e)
        {
            MoFormDangNhap("Kho");
        }

        private void pnlNhanSu_Click(object sender, EventArgs e)
        {
            MoFormDangNhap("Nhân sự");
        }

        private void pnlTaiChinh_Click(object sender, EventArgs e)
        {
            MoFormDangNhap("Tài chính");
        }

        // Hàm dùng chung để mở Form Đăng Nhập theo phân hệ
        private void MoFormDangNhap(string phanHe = "Bán hàng")
        {
            // Giữ nguyên màn hình LogInPhanHe hiển thị phía sau, không ẩn
            using (FormDangNhap login = new FormDangNhap(phanHe))
            {
                login.StartPosition = FormStartPosition.CenterParent;
                login.ShowDialog(this);
            }
        }
    }
}
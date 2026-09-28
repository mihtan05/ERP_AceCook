using System;
using System.Windows.Forms;

namespace ERPKho
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Hiện Form Đăng nhập đầu tiên
            FrDangNhap frmLogin = new FrDangNhap();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                // Đăng nhập thành công mới mở Form Main
                Application.Run(new FrMain());
            }
        }
    }
}
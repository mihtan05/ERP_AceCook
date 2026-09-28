using System;
using System.Windows.Forms;
using KeToanTaiChinh.Forms;
using KeToanTaiChinh.Services;

namespace KeToanTaiChinh
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => {
                try { System.IO.File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash.log"), e.ExceptionObject.ToString()); } catch { }
            };
            Application.ThreadException += (s, e) => {
                try { System.IO.File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash.log"), e.Exception.ToString()); } catch { }
            };
            try
            {
                DatabaseCompatibilityService.KiemTra();
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                try { System.IO.File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash.log"), ex.ToString()); } catch { }
                MessageBox.Show(
                    "Không thể khởi động hệ thống.\n\n" + Infrastructure.Db.MoTaLoi(ex) +
                    "\n\nHãy kiểm tra ERP_PG_PASSWORD, role erp_app và migration Neon. Dùng Database/Neon_PostgreSQL.sql nếu database chưa có bảng.",
                    "Lỗi kết nối/cấu trúc cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

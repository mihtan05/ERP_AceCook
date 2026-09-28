using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ERP
{
    public interface IRefreshableForm
    {
        void LamMoiDuLieu();
    }

    public static class LogisticsHelper
    {
        public static void DangXuat(Form currentForm)
        {
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn ĐĂNG XUẤT và quay lại màn hình chọn phân hệ?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                // Tìm form LogInPhanHe đang mở trong cùng tiến trình (ERP_Khach)
                Form loginForm = null;
                foreach (Form f in Application.OpenForms)
                {
                    if (f.GetType().Name == "LogInPhanHe")
                    {
                        loginForm = f;
                        break;
                    }
                }

                if (loginForm != null)
                {
                    // Hiển thị lại màn hình LogInPhanHe
                    loginForm.Show();
                    loginForm.BringToFront();
                }
                else
                {
                    // Nếu chạy độc lập, tự động tìm và khởi chạy ứng dụng tổng ERP_Khach.exe
                    string baseDir = Application.StartupPath;
                    string[] possiblePaths = new string[]
                    {
                        Path.Combine(baseDir, "ERP_Khach.exe"),
                        Path.Combine(baseDir, "..", "ERP_Khach.exe"),
                        Path.Combine(baseDir, "..", "ERP_Khach", "ERP_Khach.exe"),
                        Path.GetFullPath(Path.Combine(baseDir, @"..\ERP_BanHang\ERP_Khach\bin\Debug\ERP_Khach.exe")),
                        Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\ERP_BanHang\ERP_Khach\bin\Debug\ERP_Khach.exe")),
                        @"E:\ERP(dev)\ERP (Tổng)\ERP_BanHang\ERP_Khach\bin\Debug\ERP_Khach.exe"
                    };

                    string foundPath = null;
                    foreach (string p in possiblePaths)
                    {
                        if (File.Exists(p))
                        {
                            foundPath = p;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(foundPath))
                    {
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = foundPath,
                            WorkingDirectory = Path.GetDirectoryName(foundPath),
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                }

                // Đóng tất cả các form Logistics đang mở (dùng Show() thay vì ShowDialog())
                List<Form> formsToClose = new List<Form>();
                foreach (Form f in Application.OpenForms)
                {
                    if (f != loginForm)
                        formsToClose.Add(f);
                }
                foreach (Form f in formsToClose)
                {
                    try { f.Close(); } catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void NavigateToForm<T>(Form currentForm) where T : Form, new()
        {
            string formName = typeof(T).Name;

            // Nếu đang ở chính form này thì đưa lên trước và focus
            if (currentForm != null && currentForm.GetType().Name == formName)
            {
                currentForm.BringToFront();
                currentForm.Focus();
                return;
            }

            Form targetForm = Application.OpenForms[formName];

            if (targetForm == null)
            {
                targetForm = new T();

                // Đảm bảo khi người dùng chủ động tắt form này thì đóng ứng dụng / hiện lại login
                targetForm.FormClosed += (s, e) =>
                {
                    bool hasVisibleLogistics = false;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Visible && f.GetType().Name.StartsWith("QuanLy"))
                        {
                            hasVisibleLogistics = true;
                            break;
                        }
                    }

                    if (!hasVisibleLogistics)
                    {
                        Form login = null;
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.GetType().Name == "LogInPhanHe" || f.GetType().Name == "FormDangNhap")
                            {
                                login = f;
                                break;
                            }
                        }

                        if (login != null && !login.Visible)
                        {
                            login.Show();
                            login.BringToFront();
                        }
                    }
                };
            }

            // Đồng bộ kích thước và vị trí để chuyển màn hình liền mạch (Seamless Single-Window)
            if (currentForm != null && !currentForm.IsDisposed)
            {
                targetForm.StartPosition = FormStartPosition.Manual;
                targetForm.WindowState = currentForm.WindowState;
                if (currentForm.WindowState == FormWindowState.Normal)
                {
                    targetForm.Location = currentForm.Location;
                    targetForm.Size = currentForm.Size;
                }
            }

            targetForm.Show();
            targetForm.BringToFront();
            targetForm.Focus();

            if (targetForm is IRefreshableForm refreshable)
            {
                try
                {
                    refreshable.LamMoiDuLieu();
                }
                catch { }
            }

            // Ẩn form cũ để người dùng chỉ thấy 1 CỬA SỔ DUY NHẤT (tránh mở nhiều tab / cửa sổ chồng chéo)
            if (currentForm != null && currentForm != targetForm && !currentForm.IsDisposed)
            {
                currentForm.Hide();
            }
        }
    }
}

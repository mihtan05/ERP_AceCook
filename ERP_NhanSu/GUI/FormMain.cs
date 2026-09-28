using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HR_Management.BLL;
using HR_Management.GUI.UserControls;

namespace HR_Management.GUI
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class FormMain : Form
    {
        private Panel pnlSidebar = null!;
        private Panel pnlTopBar = null!;
        private Panel pnlContent = null!;
        private Label lblCurrentPageTitle = null!;
        private Label lblUserInfo = null!;

        private ucDashboard ucDash = null!;
        private ucPhongBan ucPB = null!;
        private ucNhanVien ucNV = null!;
        private ucHopDong ucHD = null!;
        private ucHeThong ucHT = null!;
        private ucBaoCao ucBC = null!;

        private Button? btnActive = null;
        private Button btnDash = null!;
        private Button btnPB = null!;
        private Button btnNV = null!;
        private Button btnHD = null!;
        private Button btnHT = null!;
        private Button btnBC = null!;

        public FormMain()
        {
            InitializeComponent();
            LoadUserControls();
            SwitchView(ucDash, "Tổng quan", btnDash);
        }

        private void InitializeComponent()
        {
            this.Text = "HỆ THỐNG QUẢN LÝ NHÂN SỰ - ERP HR";
            this.Size = new Size(1280, 780);
            this.MinimumSize = new Size(1024, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ThemeColor.BodyBg;
            this.Font = ThemeColor.BodyFont;
            this.WindowState = FormWindowState.Maximized;

            // ============ 1. SIDEBAR ============
            pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 225,
                BackColor = Color.FromArgb(220, 38, 38) // Acecook Red
            };

            // --- Logo Header ---
            Panel pnlLogoHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.Transparent,
                Padding = new Padding(12, 10, 12, 8)
            };

            Panel pnlLogoCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(8)
            };
            pnlLogoCard.Paint += (s, e) =>
            {
                using (Pen p = new Pen(Color.FromArgb(240, 240, 240), 1))
                {
                    e.Graphics.DrawRectangle(p, 0, 0, pnlLogoCard.Width - 1, pnlLogoCard.Height - 1);
                }
            };

            PictureBox picLogo = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo.jpg");
            if (File.Exists(logoPath))
            {
                picLogo.Image = Image.FromFile(logoPath);
            }
            pnlLogoCard.Controls.Add(picLogo);
            pnlLogoHeader.Controls.Add(pnlLogoCard);

            // --- Menu Buttons ---
            Panel pnlMenuItems = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10, 8, 10, 8)
            };

            int btnY = 8;
            int btnH = 44;
            int gap = 8;

            btnDash = CreateMenuButton("📊  Tổng quan", btnY); btnY += btnH + gap;
            btnPB = CreateMenuButton("🏢  Phòng ban", btnY); btnY += btnH + gap;
            btnNV = CreateMenuButton("👥  Hồ sơ nhân viên", btnY); btnY += btnH + gap;
            btnHD = CreateMenuButton("📝  Hợp đồng", btnY); btnY += btnH + gap;
            btnHT = CreateMenuButton("🔐  Tài khoản", btnY); btnY += btnH + gap;
            btnBC = CreateMenuButton("📈  Báo cáo thống kê", btnY);

            btnDash.Click += (s, e) => { SwitchView(ucDash, "Tổng quan hệ thống", btnDash); ucDash.LoadData(); };
            btnPB.Click += (s, e) => { SwitchView(ucPB, "Quản lý cơ cấu phòng ban", btnPB); ucPB.LoadData(); };
            btnNV.Click += (s, e) => { SwitchView(ucNV, "Quản lý hồ sơ nhân viên", btnNV); ucNV.LoadData(); };
            btnHD.Click += (s, e) => { SwitchView(ucHD, "Quản lý hợp đồng lao động", btnHD); ucHD.LoadData(); };
            btnHT.Click += (s, e) => { SwitchView(ucHT, "Quản trị tài khoản & phân quyền", btnHT); ucHT.LoadData(); };
            btnBC.Click += (s, e) => { SwitchView(ucBC, "Trung tâm báo cáo & thống kê", btnBC); ucBC.LoadAllStats(); };

            pnlMenuItems.Controls.AddRange(new Control[] { btnDash, btnPB, btnNV, btnHD, btnHT, btnBC });

            // --- Footer: Logout + User Info ---
            Panel pnlSidebarFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(12, 10, 12, 14),
                BackColor = Color.Transparent
            };

            pnlSidebarFooter.Paint += (s, e) =>
            {
                using (Pen p = new Pen(Color.FromArgb(60, 255, 255, 255), 1))
                {
                    e.Graphics.DrawLine(p, 16, 0, pnlSidebarFooter.Width - 16, 0);
                }
            };

            string userName = HeThongBLL.CurrentUser?.TenNV ?? "Admin";
            string role = HeThongBLL.CurrentUser?.VaiTro ?? "Quản trị viên";

            Label lblUserName = new Label
            {
                Text = "👤 " + userName,
                Font = ThemeColor.BodyFontBold,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 0, 0, 4)
            };

            Label lblRole = new Label
            {
                Text = "Vai trò: " + role,
                Font = ThemeColor.SmallFont,
                ForeColor = Color.FromArgb(254, 226, 226),
                Dock = DockStyle.Top,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Panel pnlSpacer = new Panel { Dock = DockStyle.Top, Height = 16 };

            Button btnLogout = new Button
            {
                Text = "🚪 Đăng xuất",
                Font = ThemeColor.BodyFontBold,
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(220, 38, 38),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 16, 0, 0)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Restart();
                }
            };

            pnlSidebarFooter.Controls.Add(lblUserName);
            pnlSidebarFooter.Controls.Add(lblRole);
            pnlSidebarFooter.Controls.Add(pnlSpacer);
            pnlSidebarFooter.Controls.Add(btnLogout);

            pnlSidebarFooter.Controls.SetChildIndex(lblUserName, 0);
            pnlSidebarFooter.Controls.SetChildIndex(lblRole, 1);
            pnlSidebarFooter.Controls.SetChildIndex(pnlSpacer, 2);
            pnlSidebarFooter.Controls.SetChildIndex(btnLogout, 3);

            pnlSidebar.Controls.Add(pnlMenuItems);
            pnlSidebar.Controls.Add(pnlSidebarFooter);
            pnlSidebar.Controls.Add(pnlLogoHeader);

            pnlLogoHeader.SendToBack();
            pnlSidebarFooter.SendToBack();
            pnlMenuItems.BringToFront();

            // ============ 2. TOPBAR ============
            pnlTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(24, 0, 24, 0)
            };
            pnlTopBar.Paint += (s, e) =>
            {
                using (Pen p = new Pen(ThemeColor.CardBorder, 1))
                {
                    e.Graphics.DrawLine(p, 0, pnlTopBar.Height - 1, pnlTopBar.Width, pnlTopBar.Height - 1);
                }
            };

            // Left: Breadcrumb & Title
            FlowLayoutPanel flpTitle = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 18, 0, 0)
            };

            Label lblBreadcrumb = new Label
            {
                Text = "ERP HR  ›  ",
                Font = ThemeColor.SmallFontBold,
                ForeColor = ThemeColor.TextMuted,
                AutoSize = true,
                Margin = new Padding(0, 2, 4, 0)
            };

            lblCurrentPageTitle = new Label
            {
                Text = "Tổng quan",
                Font = ThemeColor.SubHeaderFont,
                ForeColor = ThemeColor.TextPrimary,
                AutoSize = true
            };

            flpTitle.Controls.Add(lblBreadcrumb);
            flpTitle.Controls.Add(lblCurrentPageTitle);

            // Right: Clock + User chip
            FlowLayoutPanel flpRight = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 14, 0, 0)
            };

            Label lblClock = new Label
            {
                Text = $"🕒 {DateTime.Now:dd/MM/yyyy  |  HH:mm:ss}",
                Font = ThemeColor.SmallFont,
                ForeColor = ThemeColor.TextSecondary,
                AutoSize = true,
                Margin = new Padding(0, 7, 20, 0)
            };

            System.Windows.Forms.Timer timerClock = new System.Windows.Forms.Timer { Interval = 1000 };
            timerClock.Tick += (s, e) => lblClock.Text = $"🕒 {DateTime.Now:dd/MM/yyyy  |  HH:mm:ss}";
            timerClock.Start();
            this.FormClosed += (s, e) => { timerClock.Stop(); timerClock.Dispose(); };

            lblUserInfo = new Label
            {
                Text = $"👤 {userName} [{role}]",
                Font = ThemeColor.SmallFontBold,
                ForeColor = ThemeColor.Primary,
                BackColor = ThemeColor.PrimaryLight,
                Padding = new Padding(12, 6, 12, 6),
                AutoSize = true
            };

            flpRight.Controls.Add(lblClock);
            flpRight.Controls.Add(lblUserInfo);

            pnlTopBar.Controls.Add(flpTitle);
            pnlTopBar.Controls.Add(flpRight);

            // ============ 3. MAIN CONTENT ============
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeColor.BodyBg,
                Padding = new Padding(0)
            };

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlTopBar);
            this.Controls.Add(pnlSidebar);
        }

        private Button CreateMenuButton(string text, int top)
        {
            Button btn = new Button
            {
                Text = "  " + text,
                Location = new Point(10, top),
                Size = new Size(205, 44),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = ThemeColor.BodyFontBold,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 28, 28);
            return btn;
        }

        private void LoadUserControls()
        {
            ucDash = new ucDashboard();
            ucPB = new ucPhongBan();
            ucNV = new ucNhanVien();
            ucHD = new ucHopDong();
            ucHT = new ucHeThong();
            ucBC = new ucBaoCao();
        }

        private void SwitchView(UserControl uc, string title, Button? clickedButton)
        {
            lblCurrentPageTitle.Text = title;

            if (btnActive != null)
            {
                btnActive.BackColor = Color.Transparent;
                btnActive.ForeColor = Color.White;
            }
            if (clickedButton != null)
            {
                btnActive = clickedButton;
                btnActive.BackColor = Color.FromArgb(153, 27, 27); // Darker red for active state
                btnActive.ForeColor = Color.White;
            }

            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);

            // Tự động làm mới dữ liệu khi chuyển trang
            if (uc is ucDashboard dash) dash.LoadData();
            else if (uc is ucNhanVien nv) nv.LoadData();
            else if (uc is ucPhongBan pb) pb.LoadData();
            else if (uc is ucHopDong hd) hd.LoadData();
            else if (uc is ucHeThong ht) ht.LoadData();
        }
    }
}

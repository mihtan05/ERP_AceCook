using KeToanTaiChinh.Infrastructure;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    public partial class MainForm : Form
    {

        private Button activeButton;

        public MainForm()
        {
            InitializeComponent();

            // Không truy vấn CSDL khi Visual Studio đang mở Form Designer.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            lblUser.Text = Session.HoTen;
            SetActive(btnTongQuan);
            ShowPage(new DashboardPage(), "Tổng quan");
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            SetActive(btnTongQuan);
            ShowPage(new DashboardPage(), "Tổng quan");
        }

        private void btnQuanLyThu_Click(object sender, EventArgs e)
        {
            SetActive(btnQuanLyThu);
            ShowPage(new ThuPage(), "Quản lý thu");
        }

        private void btnQuanLyChi_Click(object sender, EventArgs e)
        {
            SetActive(btnQuanLyChi);
            ShowPage(new ChiPage(), "Quản lý chi");
        }

        private void btnCongNo_Click(object sender, EventArgs e)
        {
            SetActive(btnCongNo);
            ShowPage(new CongNoPage(), "Quản lý công nợ");
        }

        private void btnDoiChieu_Click(object sender, EventArgs e)
        {
            SetActive(btnDoiChieu);
            ShowPage(new DoiChieuPage(), "Đối chiếu công nợ");
        }

        private void btnBangLuong_Click(object sender, EventArgs e)
        {
            SetActive(btnBangLuong);
            ShowPage(new LuongPage(), "Quản lý bảng lương");
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            SetActive(btnDanhMuc);
            ShowPage(new DanhMucPage(), "Danh mục kế toán");
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            SetActive(btnBaoCao);
            ShowPage(new BaoCaoPage(), "Báo cáo tài chính");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetActive(Button button)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = Ui.Sidebar;
                activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            }

            activeButton = button;
            activeButton.BackColor = Ui.Primary;
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void ShowPage(UserControl page, string title)
        {
            pnlContent.Controls.Clear();
            page.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(page);
            lblPageTitle.Text = title;
        }
    }
}

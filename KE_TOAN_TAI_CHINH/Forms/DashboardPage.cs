using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class DashboardPage : UserControl
    {

        private readonly BaoCaoService service = new BaoCaoService();
        private readonly FlowLayoutPanel cards = new FlowLayoutPanel();
        private readonly DataGridView grid = Ui.Grid();

        public DashboardPage()
        {
            BackColor = Ui.Canvas;
            Build();
            LoadData();
        }

        private void Build()
        {
            Button refresh = Ui.Button("Làm mới", delegate { LoadData(); }, Ui.Primary);
            Panel top = Ui.PageHeader("Tổng quan tài chính", refresh);

            cards.Dock = DockStyle.Top;
            cards.Height = 155;
            cards.WrapContents = false;
            cards.AutoScroll = true;
            cards.Padding = new Padding(0, 8, 0, 8);
            cards.BackColor = Ui.Canvas;

            GroupBox report = new GroupBox
            {
                Text = "Dòng tiền theo tháng",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(10)
            };
            report.Controls.Add(grid);
            Controls.Add(report);
            Controls.Add(cards);
            Controls.Add(top);
        }

        private void LoadData()
        {
            try
            {
                DataRow row = service.TongQuan().Rows[0];
                cards.Controls.Clear();
                cards.Controls.Add(Card("Thu tháng này", Convert.ToDecimal(row["ThuThang"]).ToString("N0") + " đ", Ui.Success));
                cards.Controls.Add(Card("Chi tháng này", Convert.ToDecimal(row["ChiThang"]).ToString("N0") + " đ", Ui.Danger));
                cards.Controls.Add(Card("Công nợ phải thu", Convert.ToDecimal(row["PhaiThu"]).ToString("N0") + " đ", Ui.Primary));
                cards.Controls.Add(Card("Công nợ phải trả", Convert.ToDecimal(row["PhaiTra"]).ToString("N0") + " đ", Color.FromArgb(111, 66, 193)));
                cards.Controls.Add(Card("Chờ duyệt", Convert.ToString(row["ChoDuyet"]) + " chứng từ", Ui.Warning));
                cards.Controls.Add(Card("Nợ quá hạn", Convert.ToString(row["NoQuaHan"]) + " khoản", Color.DarkOrange));
                grid.DataSource = service.DongTienTheoThang();
                Ui.MoneyColumns(grid);
            }
            catch (Exception ex) { Ui.Error(ex); }
        }

        private static Panel Card(string caption, string value, Color color)
        {
            Panel panel = new Panel { Width = 190, Height = 122, BackColor = Color.White, Margin = new Padding(0, 0, 14, 0) };
            Panel stripe = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = color };
            Label c = new Label { Text = caption, AutoSize = true, Location = new Point(18, 25), ForeColor = Color.DimGray };
            Label v = new Label { Text = value, AutoSize = true, Location = new Point(18, 59), Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = color };
            panel.Controls.Add(v); panel.Controls.Add(c); panel.Controls.Add(stripe);
            return panel;
        }
    }
}

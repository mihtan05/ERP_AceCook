using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class DanhMucPage : UserControl
    {

        private readonly DanhMucService service = new DanhMucService(); private readonly DataGridView accounts = Ui.Grid(); private readonly DataGridView partners = Ui.Grid();
        public DanhMucPage() { BackColor = Ui.Canvas; Build(); LoadData(); }
        private void Build()
        {
            Button add = Ui.Button("+ Thêm tài khoản", AddAccount, Ui.Primary);
            add.Visible = Session.LaQuanLy;
            Panel top = Ui.PageHeader("Danh mục dùng chung", add);
            TabControl tabs = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI",10F) };
            TabPage t1 = new TabPage("Tài khoản kế toán") { Padding = new Padding(8) }; TabPage t2 = new TabPage("Khách hàng/Đối tác") { Padding = new Padding(8) };
            t1.Controls.Add(accounts); t2.Controls.Add(partners); tabs.TabPages.Add(t1); tabs.TabPages.Add(t2); Controls.Add(tabs); Controls.Add(top);
        }
        private void LoadData()
        {
            try { accounts.DataSource=service.TaiKhoanTable(); partners.DataSource=service.DoiTacTable(); Ui.MoneyColumns(accounts); Ui.MoneyColumns(partners); }
            catch(Exception ex){Ui.Error(ex);}
        }
        private void AddAccount(object sender,EventArgs e)
        {
            try
            {
                string code=Microsoft.VisualBasic.Interaction.InputBox("Mã tài khoản:","Thêm tài khoản",""); if(string.IsNullOrWhiteSpace(code))return;
                string name=Microsoft.VisualBasic.Interaction.InputBox("Tên tài khoản:","Thêm tài khoản",""); if(string.IsNullOrWhiteSpace(name))return;
                string type=Microsoft.VisualBasic.Interaction.InputBox("Loại tài khoản:","Thêm tài khoản","Tài sản"); if(string.IsNullOrWhiteSpace(type))return;
                string text=Microsoft.VisualBasic.Interaction.InputBox("Số dư đầu kỳ:","Thêm tài khoản","0"); decimal balance; if(!decimal.TryParse(text,out balance))throw new InvalidOperationException("Số dư không hợp lệ.");
                service.ThemTaiKhoan(code,name,type,balance); Ui.Info("Đã thêm tài khoản."); LoadData();
            }
            catch(Exception ex){Ui.Error(ex);}
        }
    }
}

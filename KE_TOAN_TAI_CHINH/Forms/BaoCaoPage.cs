using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class BaoCaoPage : UserControl
    {

        private readonly BaoCaoService service=new BaoCaoService(); private readonly DataGridView cash=Ui.Grid(); private readonly DataGridView debt=Ui.Grid();
        public BaoCaoPage(){BackColor=Ui.Canvas;Build();LoadData();}
        private void Build()
        {
            Button refresh=Ui.Button("Làm mới",delegate{LoadData();},Ui.Primary);
            Panel top=Ui.PageHeader("Báo cáo quản trị",refresh);
            TabControl tabs=new TabControl{Dock=DockStyle.Fill,Font=new Font("Segoe UI",10F)};
            TabPage t1=new TabPage("Dòng tiền theo tháng"){Padding=new Padding(8)}; TabPage t2=new TabPage("Công nợ theo đối tác"){Padding=new Padding(8)};
            Panel p1=new Panel{Dock=DockStyle.Fill};Panel p2=new Panel{Dock=DockStyle.Fill};
            Button e1=Ui.Button("Xuất CSV",delegate{Ui.ExportCsv(cash.DataSource as DataTable,FindForm(),"BaoCaoDongTien.csv");},Color.FromArgb(32,201,151));e1.Dock=DockStyle.Bottom;
            Button e2=Ui.Button("Xuất CSV",delegate{Ui.ExportCsv(debt.DataSource as DataTable,FindForm(),"BaoCaoCongNoDoiTac.csv");},Color.FromArgb(32,201,151));e2.Dock=DockStyle.Bottom;
            p1.Controls.Add(cash);p1.Controls.Add(e1);p2.Controls.Add(debt);p2.Controls.Add(e2);t1.Controls.Add(p1);t2.Controls.Add(p2);tabs.TabPages.Add(t1);tabs.TabPages.Add(t2);Controls.Add(tabs);Controls.Add(top);
        }
        private void LoadData(){try{cash.DataSource=service.DongTienTheoThang();debt.DataSource=service.CongNoTheoDoiTac();Ui.MoneyColumns(cash);Ui.MoneyColumns(debt);}catch(Exception ex){Ui.Error(ex);}}
    }
}

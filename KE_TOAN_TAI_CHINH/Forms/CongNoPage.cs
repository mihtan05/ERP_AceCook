using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class CongNoPage : UserControl
    {

        private readonly CongNoService service = new CongNoService();
        private readonly DataGridView grid = Ui.Grid();
        private readonly TextBox search = Ui.SearchBox("Tìm mã công nợ, chứng từ hoặc đối tác...");
        private readonly ComboBox type = new ComboBox();
        private readonly CheckBox overdue = new CheckBox();
        private DataTable data;

        public CongNoPage()
        {
            BackColor = Ui.Canvas; Build(); LoadData();
        }
        private void Build()
        {
            Button create = Ui.Button("+ Tạo công nợ", Create, Ui.Primary);
            Panel title = Ui.PageHeader("Quản lý công nợ", create);
            search.TextChanged += delegate { if (search.ForeColor != Color.Gray) LoadData(); };
            Ui.StyleFilter(type, 200); type.Items.AddRange(new object[] { "Tất cả loại", "Phải thu", "Phải trả" }); type.SelectedIndex = 0;
            type.SelectedIndexChanged += delegate { LoadData(); };
            Panel filters = Ui.SearchFilterBar(search, type);
            overdue.Text = "Chỉ nợ quá hạn"; overdue.AutoSize = true; overdue.CheckedChanged += delegate { LoadData(); };
            FlowLayoutPanel actions = Ui.ActionBar();
            actions.Controls.Add(Ui.Button("Xuất CSV", delegate { Ui.ExportCsv(data, FindForm(), "BaoCaoCongNo.csv"); }, Color.FromArgb(32, 201, 151)));
            actions.Controls.Add(Ui.Button("Làm mới", delegate { LoadData(); }, Color.FromArgb(108, 117, 125)));
            actions.Controls.Add(overdue);
            Controls.Add(grid); Controls.Add(actions); Controls.Add(filters); Controls.Add(title);
        }
        private void LoadData()
        {
            try
            {
                string selected = type.SelectedIndex <= 0 ? string.Empty : Convert.ToString(type.SelectedItem);
                data = service.DanhSach(Ui.SearchValue(search), selected, overdue.Checked); grid.DataSource = data;
                Set("MaCongNo", "Mã công nợ"); Set("SoChungTu", "Số chứng từ"); Set("DoiTac", "Đối tác"); Set("LoaiCongNo", "Loại");
                Set("NgayPhatSinh", "Ngày phát sinh"); Set("HanThanhToan", "Hạn thanh toán"); Set("SoTienPhatSinh", "Phát sinh");
                Set("DaThanhToan", "Đã thanh toán"); Set("SoDuConLai", "Còn lại"); Set("TrangThai", "Trạng thái"); Set("DienGiai", "Diễn giải"); Ui.MoneyColumns(grid);
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private void Set(string n, string t) { if (grid.Columns.Contains(n)) grid.Columns[n].HeaderText = t; }
        private void Create(object sender, EventArgs e) { using (CongNoEditForm f = new CongNoEditForm()) if (f.ShowDialog(FindForm()) == DialogResult.OK) LoadData(); }
    }
}

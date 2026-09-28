using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class DoiChieuPage : UserControl
    {

        private readonly DoiChieuService service = new DoiChieuService(); private readonly DataGridView grid = Ui.Grid();
        private readonly TextBox search = Ui.SearchBox("Tìm mã biên bản hoặc đối tác..."); private readonly ComboBox status = new ComboBox(); private DataTable data;
        public DoiChieuPage() { BackColor = Ui.Canvas; Build(); LoadData(); }
        private void Build()
        {
            Button create = Ui.Button("+ Lập biên bản", Create, Ui.Primary);
            Panel title = Ui.PageHeader("Đối chiếu công nợ", create);
            search.TextChanged += delegate { if (search.ForeColor != Color.Gray) LoadData(); };
            Ui.StyleFilter(status, 200); status.Items.AddRange(new object[] { "Tất cả trạng thái", "Nháp", "Chờ duyệt", "Đã duyệt" }); status.SelectedIndex = 0;
            status.SelectedIndexChanged += delegate { LoadData(); };
            Panel filters = Ui.SearchFilterBar(search, status);
            FlowLayoutPanel bottom = Ui.ActionBar();
            bottom.Controls.Add(Ui.Button("Xuất CSV", delegate { Ui.ExportCsv(data, FindForm(), "BienBanDoiChieu.csv"); }, Color.FromArgb(32, 201, 151)));
            bottom.Controls.Add(Ui.Button("Làm mới", delegate { LoadData(); }, Color.FromArgb(108, 117, 125)));
            if (Session.LaQuanLy)
            {
                bottom.Controls.Add(Ui.Button("Duyệt biên bản", Approve, Ui.Success));
            }
            Controls.Add(grid); Controls.Add(bottom); Controls.Add(filters); Controls.Add(title);
        }
        private void LoadData()
        {
            try
            {
                string selected = status.SelectedIndex <= 0 ? "" : Convert.ToString(status.SelectedItem); data = service.DanhSach(Ui.SearchValue(search), selected); grid.DataSource = data;
                Set("MaBienBan", "Mã biên bản"); Set("NgayDoiChieu", "Ngày đối chiếu"); Set("KyDoiChieu", "Kỳ"); Set("DoiTac", "Đối tác"); Set("NguoiLap", "Người lập");
                Set("TrangThai", "Trạng thái"); Set("SoTienSoSach", "Theo sổ sách"); Set("SoTienDoiTac", "Theo đối tác"); Set("ChenhLech", "Chênh lệch"); Ui.MoneyColumns(grid);
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private void Set(string n, string t) { if (grid.Columns.Contains(n)) grid.Columns[n].HeaderText = t; }
        private void Create(object sender, EventArgs e) { using (DoiChieuEditForm f = new DoiChieuEditForm()) if (f.ShowDialog(FindForm()) == DialogResult.OK) LoadData(); }
        private void Approve(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(grid, "MaBienBan"); if (id == null) { Ui.Info("Hãy chọn biên bản."); return; }
            if (!Ui.Confirm("Duyệt biên bản " + id + "?", "Xác nhận duyệt")) return;
            try { service.Duyet(id); Ui.Info("Đã duyệt biên bản."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
    }
}

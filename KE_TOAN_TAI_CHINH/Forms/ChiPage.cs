using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class ChiPage : UserControl
    {

        private readonly ChiService service = new ChiService();
        private readonly DataGridView grid = Ui.Grid();
        private readonly TextBox search = Ui.SearchBox("Tìm mã phiếu hoặc đối tác...");
        private readonly ComboBox status = new ComboBox();
        private DataTable data;

        public ChiPage()
        {
            BackColor = Ui.Canvas; Build(); LoadData();
        }

        private void Build()
        {
            Button create = Ui.Button("+ Tạo phiếu chi", Create, Ui.Primary);
            Panel title = Ui.PageHeader("Quản lý chi", create);
            search.TextChanged += delegate { if (search.ForeColor != Color.Gray) LoadData(); };
            Ui.StyleFilter(status, 200);
            status.Items.AddRange(new object[] { "Tất cả trạng thái", "Nháp", "Chờ duyệt", "Đã duyệt", "Từ chối" }); status.SelectedIndex = 0;
            status.SelectedIndexChanged += delegate { LoadData(); };
            Panel filters = Ui.SearchFilterBar(search, status);

            FlowLayoutPanel actions = Ui.ActionBar();
            actions.Controls.Add(Ui.Button("Xuất CSV", delegate { Ui.ExportCsv(data, FindForm(), "DanhSachPhieuChi.csv"); }, Color.FromArgb(32, 201, 151)));
            actions.Controls.Add(Ui.Button("Làm mới", delegate { LoadData(); }, Color.FromArgb(108, 117, 125)));
            actions.Controls.Add(Ui.Button("Cập nhật", Edit, Color.FromArgb(111, 66, 193)));
            if (Session.LaQuanLy)
            {
                actions.Controls.Add(Ui.Button("Từ chối", Reject, Color.FromArgb(108, 117, 125)));
                actions.Controls.Add(Ui.Button("Duyệt phiếu", Approve, Ui.Success));
            }
            grid.CellDoubleClick += delegate { Edit(null, EventArgs.Empty); };
            Controls.Add(grid); Controls.Add(actions); Controls.Add(filters); Controls.Add(title);
        }

        private void LoadData()
        {
            try
            {
                string selected = status.SelectedIndex <= 0 ? string.Empty : Convert.ToString(status.SelectedItem);
                data = service.DanhSach(Ui.SearchValue(search), selected); grid.DataSource = data;
                Set("MaPhieu", "Mã phiếu"); Set("NgayLap", "Ngày lập"); Set("DoiTac", "Người nhận/Đối tác"); Set("SoTien", "Số tiền");
                Set("HinhThuc", "Hình thức"); Set("NguoiLap", "Người lập"); Set("TrangThai", "Trạng thái");
                Set("TaiKhoanNo", "TK Nợ"); Set("TaiKhoanCo", "TK Có"); Set("MaCongNo", "Công nợ"); Ui.MoneyColumns(grid);
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private void Set(string n, string t) { if (grid.Columns.Contains(n)) grid.Columns[n].HeaderText = t; }
        private void Create(object sender, EventArgs e) { using (ChiEditForm f = new ChiEditForm(null)) if (f.ShowDialog(FindForm()) == DialogResult.OK) LoadData(); }
        private void Edit(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(grid, "MaPhieu"); if (id == null) { Ui.Info("Hãy chọn một phiếu chi."); return; }
            try { using (ChiEditForm f = new ChiEditForm(id)) if (f.ShowDialog(FindForm()) == DialogResult.OK) LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void Approve(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(grid, "MaPhieu"); if (id == null) { Ui.Info("Hãy chọn một phiếu chi."); return; }
            if (!Ui.Confirm("Duyệt phiếu chi " + id + "? Số dư quỹ và công nợ sẽ được cập nhật.", "Xác nhận duyệt")) return;
            try { service.Duyet(id); Ui.Info("Duyệt phiếu chi thành công."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void Reject(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(grid, "MaPhieu"); if (id == null) { Ui.Info("Hãy chọn một phiếu chi."); return; }
            string reason = Microsoft.VisualBasic.Interaction.InputBox("Nhập lý do từ chối:", "Từ chối phiếu", "Thông tin chưa hợp lệ"); if (string.IsNullOrWhiteSpace(reason)) return;
            try { service.TuChoi(id, reason); Ui.Info("Đã từ chối phiếu chi."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
    }
}

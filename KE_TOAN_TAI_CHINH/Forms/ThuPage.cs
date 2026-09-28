using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class ThuPage : UserControl
    {

        private readonly ThuService service = new ThuService();
        private readonly DataGridView grid = Ui.Grid();
        private readonly TextBox search = Ui.SearchBox("Tìm mã phiếu hoặc khách hàng...");
        private readonly ComboBox status = new ComboBox();
        private DataTable data;

        public ThuPage()
        {
            BackColor = Ui.Canvas;
            Build();
            LoadData();
        }

        private void Build()
        {
            Button create = Ui.Button("+ Tạo phiếu thu", Create, Ui.Primary);
            Panel title = Ui.PageHeader("Quản lý thu", create);

            search.TextChanged += delegate { if (search.ForeColor != Color.Gray) LoadData(); };
            Ui.StyleFilter(status, 200);
            status.Items.AddRange(new object[] { "Tất cả trạng thái", "Nháp", "Chờ duyệt", "Đã duyệt", "Từ chối" });
            status.SelectedIndex = 0; status.SelectedIndexChanged += delegate { LoadData(); };
            Panel filters = Ui.SearchFilterBar(search, status);

            FlowLayoutPanel actions = Ui.ActionBar();
            actions.Controls.Add(Ui.Button("Xuất CSV", Export, Color.FromArgb(32, 201, 151)));
            actions.Controls.Add(Ui.Button("Làm mới", delegate { LoadData(); }, Color.FromArgb(108, 117, 125)));
            actions.Controls.Add(Ui.Button("Xóa bản nháp", DeleteDraft, Ui.Danger));
            if (Session.LaQuanLy)
            {
                actions.Controls.Add(Ui.Button("Từ chối", Reject, Color.FromArgb(108, 117, 125)));
                actions.Controls.Add(Ui.Button("Duyệt phiếu", Approve, Ui.Success));
            }

            Controls.Add(grid); Controls.Add(actions); Controls.Add(filters); Controls.Add(title);
        }

        private void LoadData()
        {
            if (!IsHandleCreated && Parent == null) { }
            try
            {
                string selected = status.SelectedIndex <= 0 ? string.Empty : Convert.ToString(status.SelectedItem);
                data = service.DanhSach(Ui.SearchValue(search), selected);
                grid.DataSource = data;
                Rename(); Ui.MoneyColumns(grid);
            }
            catch (Exception ex) { Ui.Error(ex); }
        }

        private void Rename()
        {
            Set("MaPhieu", "Mã phiếu"); Set("NgayLap", "Ngày lập"); Set("DoiTac", "Khách hàng/Đối tác");
            Set("SoTien", "Số tiền"); Set("HinhThuc", "Hình thức"); Set("NguoiLap", "Người lập");
            Set("TrangThai", "Trạng thái"); Set("TaiKhoanNo", "TK Nợ"); Set("TaiKhoanCo", "TK Có"); Set("MaCongNo", "Công nợ");
        }
        private void Set(string name, string text) { if (grid.Columns.Contains(name)) grid.Columns[name].HeaderText = text; }

        private void Create(object sender, EventArgs e)
        {
            using (ThuEditForm form = new ThuEditForm()) if (form.ShowDialog(FindForm()) == DialogResult.OK) LoadData();
        }
        private void Approve(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(grid, "MaPhieu"); if (id == null) { Ui.Info("Hãy chọn một phiếu thu."); return; }
            if (!Ui.Confirm("Duyệt phiếu thu " + id + "? Số dư tài khoản và công nợ sẽ được cập nhật.", "Xác nhận duyệt")) return;
            try { service.Duyet(id); Ui.Info("Duyệt phiếu thu thành công."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void Reject(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(grid, "MaPhieu"); if (id == null) { Ui.Info("Hãy chọn một phiếu thu."); return; }
            string reason = Microsoft.VisualBasic.Interaction.InputBox("Nhập lý do từ chối:", "Từ chối phiếu", "Thông tin chưa hợp lệ");
            if (string.IsNullOrWhiteSpace(reason)) return;
            try { service.TuChoi(id, reason); Ui.Info("Đã từ chối phiếu thu."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void DeleteDraft(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(grid, "MaPhieu"); if (id == null) { Ui.Info("Hãy chọn một phiếu thu."); return; }
            if (!Ui.Confirm("Xóa vĩnh viễn bản nháp " + id + "?", "Xác nhận xóa")) return;
            try { service.XoaNhap(id); Ui.Info("Đã xóa bản nháp."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void Export(object sender, EventArgs e) { Ui.ExportCsv(data, FindForm(), "DanhSachPhieuThu.csv"); }
    }
}

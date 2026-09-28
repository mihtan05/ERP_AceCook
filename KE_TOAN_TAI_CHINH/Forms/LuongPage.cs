using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class LuongPage : UserControl
    {

        private readonly LuongService service = new LuongService(); private readonly DataGridView headers = Ui.Grid(); private readonly DataGridView details = Ui.Grid();
        private readonly TextBox search = Ui.SearchBox("Tìm mã, tên hoặc kỳ lương..."); private readonly ComboBox status = new ComboBox(); private DataTable data;
        public LuongPage() { BackColor = Ui.Canvas; Build(); LoadData(); }
        private void Build()
        {
            Button create = Ui.Button("+ Tạo bảng lương", Create, Ui.Primary);
            Panel title = Ui.PageHeader("Quản lý bảng lương", create);
            search.TextChanged += delegate { if (search.ForeColor != Color.Gray) LoadData(); }; Ui.StyleFilter(status, 200);
            status.Items.AddRange(new object[] { "Tất cả trạng thái", "Nháp", "Chờ duyệt", "Đã duyệt" }); status.SelectedIndex = 0; status.SelectedIndexChanged += delegate { LoadData(); };
            Panel filters = Ui.SearchFilterBar(search, status);

            SplitContainer split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 255 };
            GroupBox g1 = new GroupBox { Text = "Danh sách bảng lương", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            GroupBox g2 = new GroupBox { Text = "Chi tiết nhân viên", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            g1.Controls.Add(headers); g2.Controls.Add(details); split.Panel1.Controls.Add(g1); split.Panel2.Controls.Add(g2);
            headers.SelectionChanged += delegate { LoadDetails(); };

            FlowLayoutPanel actions = Ui.ActionBar();
            actions.Controls.Add(Ui.Button("Làm mới", delegate { LoadData(); }, Color.FromArgb(108, 117, 125)));
            actions.Controls.Add(Ui.Button("Xuất chi tiết", ExportDetails, Color.FromArgb(32, 201, 151)));
            if (Session.LaQuanLy) actions.Controls.Add(Ui.Button("Duyệt bảng lương", Approve, Ui.Success));
            actions.Controls.Add(Ui.Button("Gửi duyệt", Submit, Ui.Primary));
            actions.Controls.Add(Ui.Button("Sửa khoản lương", EditDetail, Color.FromArgb(111, 66, 193)));
            actions.Controls.Add(Ui.Button("Tính lương", Calculate, Color.FromArgb(253, 126, 20)));
            actions.Controls.Add(Ui.Button("Đồng bộ từ Nhân sự", SyncFromHr, Color.FromArgb(13, 110, 253)));
            Controls.Add(split); Controls.Add(actions); Controls.Add(filters); Controls.Add(title);
        }
        private void LoadData()
        {
            try
            {
                string selected = status.SelectedIndex <= 0 ? "" : Convert.ToString(status.SelectedItem); data = service.DanhSach(Ui.SearchValue(search), selected); headers.DataSource = data;
                Set(headers,"MaBangLuong","Mã bảng lương"); Set(headers,"TenBangLuong","Tên bảng lương"); Set(headers,"KyLuong","Kỳ lương"); Set(headers,"PhongBan","Phòng ban");
                Set(headers,"DonViTienTe","Tiền tệ"); Set(headers,"TongTienLuong","Tổng thực lĩnh"); Set(headers,"TrangThai","Trạng thái"); Set(headers,"SoNhanVien","Số nhân viên"); Ui.MoneyColumns(headers); LoadDetails();
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private void LoadDetails()
        {
            string id = Ui.SelectedId(headers, "MaBangLuong"); if (id == null) { details.DataSource = null; return; }
            try
            {
                details.DataSource = service.ChiTiet(id); Set(details,"MaChiTiet","Mã chi tiết"); Set(details,"MaNhanVien","Mã NV"); Set(details,"TenNhanVien","Nhân viên");
                Set(details,"LuongCoBan","Lương cơ bản"); Set(details,"PhuCap","Phụ cấp"); Set(details,"Thuong","Thưởng"); Set(details,"KhauTruBHXH","BHXH");
                Set(details,"ThueTNCN","Thuế TNCN"); Set(details,"ThucLinh","Thực lĩnh"); Ui.MoneyColumns(details);
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private static void Set(DataGridView g, string n, string t) { if (g.Columns.Contains(n)) g.Columns[n].HeaderText = t; }
        private void Create(object sender, EventArgs e) { using (LuongEditForm f = new LuongEditForm()) if (f.ShowDialog(FindForm()) == DialogResult.OK) LoadData(); }
        private void SyncFromHr(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(headers,"MaBangLuong"); if (id == null) { Ui.Info("Hãy chọn bảng lương."); return; }
            if (!Ui.Confirm("Cập nhật lại Lương cơ bản và BHXH của các nhân viên theo Hồ sơ nhân viên mới nhất cho bảng lương Nháp này?", "Đồng bộ lương")) return;
            try { service.DongBoLuongTuNhanSu(id); Ui.Info("Đồng bộ lương cơ bản từ hồ sơ nhân sự thành công."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void Calculate(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(headers,"MaBangLuong"); if (id == null) { Ui.Info("Hãy chọn bảng lương."); return; }
            if (!Ui.Confirm("Tính lại sẽ xóa chi tiết cũ của bảng lương Nháp. Tiếp tục?", "Tính lương")) return;
            try { service.TinhLuong(id); Ui.Info("Tính lương thành công."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void EditDetail(object sender, EventArgs e)
        {
            string payroll = Ui.SelectedId(headers,"MaBangLuong"); string detail = Ui.SelectedId(details,"MaChiTiet"); if (payroll == null || detail == null) { Ui.Info("Hãy chọn một nhân viên trong chi tiết lương."); return; }
            try
            {
                DataGridViewRow r = details.CurrentRow; decimal allowance = Ask("Phụ cấp", Convert.ToDecimal(r.Cells["PhuCap"].Value));
                decimal bonus = Ask("Thưởng", Convert.ToDecimal(r.Cells["Thuong"].Value)); decimal insurance = Ask("Khấu trừ BHXH", Convert.ToDecimal(r.Cells["KhauTruBHXH"].Value));
                decimal tax = Ask("Thuế TNCN", Convert.ToDecimal(r.Cells["ThueTNCN"].Value)); service.CapNhatChiTiet(payroll, detail, allowance, bonus, insurance, tax); LoadData();
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private static decimal Ask(string title, decimal current)
        {
            string text = Microsoft.VisualBasic.Interaction.InputBox("Nhập " + title + ":", title, current.ToString("0")); decimal value;
            if (!decimal.TryParse(text, out value) || value < 0) throw new InvalidOperationException(title + " không hợp lệ."); return value;
        }
        private void Submit(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(headers,"MaBangLuong"); if (id == null) { Ui.Info("Hãy chọn bảng lương."); return; }
            try { service.GuiDuyet(id); Ui.Info("Đã gửi bảng lương chờ duyệt."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void Approve(object sender, EventArgs e)
        {
            string id = Ui.SelectedId(headers,"MaBangLuong"); if (id == null) { Ui.Info("Hãy chọn bảng lương."); return; }
            if (!Ui.Confirm("Duyệt bảng lương " + id + "?", "Xác nhận duyệt")) return;
            try { service.Duyet(id); Ui.Info("Đã duyệt bảng lương."); LoadData(); } catch (Exception ex) { Ui.Error(ex); }
        }
        private void ExportDetails(object sender, EventArgs e) { Ui.ExportCsv(details.DataSource as DataTable, FindForm(), "ChiTietBangLuong.csv"); }
    }
}

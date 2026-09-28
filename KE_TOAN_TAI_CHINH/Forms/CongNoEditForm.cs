using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class CongNoEditForm : Form
    {

        private readonly DanhMucService catalog = new DanhMucService(); private readonly CongNoService service = new CongNoService();
        private readonly ComboBox type = new ComboBox(); private readonly ComboBox customer = new ComboBox();
        private readonly TextBox voucher = new TextBox(); private readonly TextBox reason = new TextBox();
        private readonly DateTimePicker issue = new DateTimePicker(); private readonly DateTimePicker due = new DateTimePicker();
        private readonly NumericUpDown amount = new NumericUpDown(); private readonly ErrorProvider errors = new ErrorProvider(); private bool dirty;

        public CongNoEditForm()
        {
            Text = "Tạo khoản công nợ"; StartPosition = FormStartPosition.CenterParent; ClientSize = new Size(720, 550); BackColor = Color.White;
            Build(); customer.DataSource = catalog.KhachHang(); customer.DisplayMember = "Display"; customer.ValueMember = "Id"; customer.SelectedIndex = -1;
            foreach (Control c in new Control[] { type, customer, voucher, reason, issue, due, amount }) c.TextChanged += delegate { dirty = true; };
        }
        private void Build()
        {
            Label title = Ui.Title("Tạo khoản công nợ"); title.Location = new Point(30, 22); Controls.Add(title);
            Controls.Add(new Label { Text = "Mã công nợ: Tự sinh khi lưu", AutoSize = true, ForeColor = Color.DimGray, Location = new Point(32, 56) });
            TableLayoutPanel p = new TableLayoutPanel { ColumnCount = 2, RowCount = 7, Location = new Point(30, 75), Size = new Size(650, 370), Padding = new Padding(15), BackColor = Color.FromArgb(248, 249, 250) };
            p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180)); p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 6; i++) p.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            p.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
            type.DropDownStyle = ComboBoxStyle.DropDownList; type.Items.AddRange(new object[] { "Phải thu", "Phải trả" }); type.Width = 360;
            customer.DropDownStyle = ComboBoxStyle.DropDownList; customer.Width = 360; voucher.Width = 360;
            issue.Format = due.Format = DateTimePickerFormat.Custom; issue.CustomFormat = due.CustomFormat = "dd/MM/yyyy"; due.Value = DateTime.Today.AddDays(30);
            amount.Maximum = 1000000000000m; amount.ThousandsSeparator = true; amount.Width = 360;
            reason.Multiline = true; reason.Height = 55; reason.Width = 360;
            Add(p, 0, "Loại công nợ *", type); Add(p, 1, "Đối tác *", customer); Add(p, 2, "Số chứng từ/Hóa đơn *", voucher);
            Add(p, 3, "Ngày phát sinh *", issue); Add(p, 4, "Hạn thanh toán *", due); Add(p, 5, "Số tiền *", amount); Add(p, 6, "Lý do phát sinh *", reason);
            Controls.Add(p);
            Button cancel = Ui.Button("Hủy", Cancel, Color.FromArgb(108, 117, 125)); cancel.Location = new Point(410, 475);
            Button save = Ui.Button("Lưu công nợ", Save, Ui.Primary); save.Location = new Point(555, 475); Controls.Add(cancel); Controls.Add(save);
        }
        private static void Add(TableLayoutPanel p, int row, string text, Control c)
        {
            p.Controls.Add(new Label { Text = text, AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }, 0, row);
            c.Anchor = AnchorStyles.Left | AnchorStyles.Right; p.Controls.Add(c, 1, row);
        }
        private void Save(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            CongNoInput input = new CongNoInput { MaDoiTac = Convert.ToString(customer.SelectedValue), LoaiCongNo = Convert.ToString(type.SelectedItem),
                SoChungTu = voucher.Text.Trim(), NgayPhatSinh = issue.Value.Date, HanThanhToan = due.Value.Date, SoTien = amount.Value, DienGiai = reason.Text.Trim() };
            try
            {
                decimal total, limit;
                if (service.VuotHanMuc(input, out total, out limit) && !Ui.Confirm(
                    "Tổng nợ sau khi tạo là " + total.ToString("N0") + " đ, vượt hạn mức " + limit.ToString("N0") + " đ. Vẫn tiếp tục?", "Cảnh báo hạn mức")) return;
                service.Tao(input); dirty = false; Ui.Info("Tạo khoản công nợ thành công."); DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private bool ValidateForm()
        {
            errors.Clear(); bool ok = true; ok &= Req(type, type.SelectedIndex >= 0, "Chọn loại."); ok &= Req(customer, customer.SelectedIndex >= 0, "Chọn đối tác.");
            ok &= Req(voucher, !string.IsNullOrWhiteSpace(voucher.Text), "Nhập chứng từ."); ok &= Req(amount, amount.Value > 0, "Số tiền phải > 0.");
            ok &= Req(reason, !string.IsNullOrWhiteSpace(reason.Text), "Nhập lý do.");
            if (due.Value.Date < issue.Value.Date) { errors.SetError(due, "Hạn thanh toán không được trước ngày phát sinh."); ok = false; }
            if (!ok) Ui.Info("Vui lòng điền đầy đủ và chính xác thông tin."); return ok;
        }
        private bool Req(Control c, bool condition, string text) { c.BackColor = condition ? Color.White : Color.MistyRose; if (!condition) errors.SetError(c, text); return condition; }
        private void Cancel(object sender, EventArgs e) { if (dirty && !Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?", "Xác nhận hủy")) return; dirty = false; Close(); }
        protected override void OnFormClosing(FormClosingEventArgs e) { if (e.CloseReason == CloseReason.UserClosing && dirty && !Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?", "Xác nhận hủy")) e.Cancel = true; base.OnFormClosing(e); }
    }
}

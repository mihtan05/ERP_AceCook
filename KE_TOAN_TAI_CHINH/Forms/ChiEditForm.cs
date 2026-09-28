using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;
using KeToanTaiChinh.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class ChiEditForm : Form
    {

        private readonly string id;
        private readonly DanhMucService catalog = new DanhMucService();
        private readonly ChiService service = new ChiService();
        private readonly ErrorProvider errors = new ErrorProvider();
        private readonly DateTimePicker date = new DateTimePicker();
        private readonly ComboBox customer = Combo(); private readonly ComboBox payment = Combo(); private readonly ComboBox debt = Combo();
        private readonly ComboBox debit = Combo(); private readonly ComboBox credit = Combo();
        private readonly NumericUpDown amount = new NumericUpDown(); private readonly TextBox reason = new TextBox();
        private bool dirty;

        public ChiEditForm(string maPhieu)
        {
            id = maPhieu; Text = id == null ? "Tạo phiếu chi" : "Cập nhật phiếu chi";
            StartPosition = FormStartPosition.CenterParent; ClientSize = new Size(850, 590); BackColor = Color.White;
            Build(); LoadCatalogs(); if (id != null) LoadExisting(); Hook();
        }
        private static ComboBox Combo() { return new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 300 }; }
        private void Build()
        {
            Label title = Ui.Title(id == null ? "Tạo phiếu chi" : "Cập nhật phiếu chi"); title.Location = new Point(30, 22); Controls.Add(title);
            Controls.Add(new Label { Text = "Mã phiếu: " + (id ?? "Tự sinh khi lưu"), AutoSize = true, ForeColor = Color.DimGray, Location = new Point(32, 56) });
            TableLayoutPanel form = new TableLayoutPanel { ColumnCount = 4, RowCount = 5, Location = new Point(30, 78), Size = new Size(790, 365), Padding = new Padding(12), BackColor = Color.FromArgb(248, 249, 250) };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140)); form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140)); form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            date.Format = DateTimePickerFormat.Custom; date.CustomFormat = "dd/MM/yyyy HH:mm"; date.Width = 300;
            amount.Maximum = 1000000000000m; amount.ThousandsSeparator = true; amount.Width = 300;
            payment.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản" });
            reason.Multiline = true; reason.ScrollBars = ScrollBars.Vertical; reason.Width = 300; reason.Height = 90;
            Add(form, 0, 0, "Ngày lập *", date); Add(form, 2, 0, "Người nhận/Đối tác *", customer);
            Add(form, 0, 1, "Số tiền *", amount); Add(form, 2, 1, "Hình thức *", payment);
            Add(form, 0, 2, "Khoản phải trả", debt); Add(form, 2, 2, "Tài khoản Nợ *", debit);
            Add(form, 0, 3, "Tài khoản Có *", credit); Add(form, 2, 3, "Lý do chi *", reason);
            Controls.Add(form);
            Button cancel = Ui.Button("Hủy", Cancel, Color.FromArgb(108, 117, 125)); cancel.Location = new Point(420, 475);
            Button draft = Ui.Button("Lưu", delegate { Save(id == null ? "Nháp" : CurrentStatus()); }, Color.FromArgb(13, 202, 240)); draft.Location = new Point(555, 475);
            Button submit = Ui.Button("Lưu & Gửi duyệt", delegate { Save("Chờ duyệt"); }, Ui.Primary); submit.SetBounds(690, 475, 130, 36);
            Controls.Add(cancel); Controls.Add(draft); Controls.Add(submit);
        }
        private static void Add(TableLayoutPanel p, int c, int r, string text, Control control)
        {
            p.Controls.Add(new Label { Text = text, AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }, c, r);
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right; p.Controls.Add(control, c + 1, r);
        }
        private void LoadCatalogs()
        {
            customer.DataSource = catalog.KhachHang(); customer.DisplayMember = "Display"; customer.ValueMember = "Id"; customer.SelectedIndex = -1;
            List<LookupItem> a = catalog.TaiKhoan(); debit.DataSource = new List<LookupItem>(a); debit.DisplayMember = "Display"; debit.ValueMember = "Id"; debit.SelectedIndex = -1;
            credit.DataSource = new List<LookupItem>(a); credit.DisplayMember = "Display"; credit.ValueMember = "Id"; credit.SelectedIndex = -1;
            LoadDebts(null);
        }
        private ChungTuInput existing;
        private void LoadExisting()
        {
            existing = service.Lay(id);
            if (existing.TrangThai != "Nháp" && existing.TrangThai != "Chờ duyệt")
                throw new InvalidOperationException("Không thể cập nhật phiếu chi đã phê duyệt.");
            date.Value = existing.NgayLap; customer.SelectedValue = existing.MaDoiTac; LoadDebts(existing.MaDoiTac);
            debt.SelectedValue = existing.MaCongNo ?? string.Empty; amount.Value = existing.SoTien; payment.SelectedItem = existing.HinhThuc;
            debit.SelectedValue = existing.TaiKhoanNo; credit.SelectedValue = existing.TaiKhoanCo; reason.Text = existing.DienGiai;
        }
        private string CurrentStatus() { return existing == null ? "Nháp" : existing.TrangThai; }
        private void Hook()
        {
            customer.SelectedIndexChanged += delegate { LoadDebts(Convert.ToString(customer.SelectedValue)); dirty = true; };
            payment.SelectedIndexChanged += delegate
            {
                credit.SelectedValue = Convert.ToString(payment.SelectedItem) == "Tiền mặt" ? "1111" : "1121"; debit.SelectedValue = "3311"; dirty = true;
            };
            foreach (Control c in new Control[] { customer, payment, debt, debit, credit, amount, reason, date }) c.TextChanged += delegate { dirty = true; };
        }
        private void LoadDebts(string customerId)
        {
            List<LookupItem> items = new List<LookupItem> { new LookupItem { Id = "", Name = "Không cấn trừ công nợ" } };
            if (!string.IsNullOrEmpty(customerId)) items.AddRange(catalog.CongNoCuaDoiTac(customerId, "Phải trả"));
            debt.DataSource = items; debt.DisplayMember = "Display"; debt.ValueMember = "Id";
        }
        private void Save(string state)
        {
            if (!ValidateForm()) return;
            try
            {
                service.Luu(new ChungTuInput { MaPhieu = id, NgayLap = date.Value, MaDoiTac = Convert.ToString(customer.SelectedValue),
                    SoTien = amount.Value, HinhThuc = Convert.ToString(payment.SelectedItem), MaCongNo = Convert.ToString(debt.SelectedValue),
                    TaiKhoanNo = Convert.ToString(debit.SelectedValue), TaiKhoanCo = Convert.ToString(credit.SelectedValue),
                    DienGiai = reason.Text.Trim(), TrangThai = state });
                dirty = false; Ui.Info(id == null ? "Tạo phiếu chi thành công." : "Cập nhật phiếu chi thành công."); DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private bool ValidateForm()
        {
            errors.Clear(); bool ok = true; ok &= Req(customer, customer.SelectedIndex >= 0, "Chọn đối tác.");
            ok &= Req(payment, payment.SelectedIndex >= 0, "Chọn hình thức."); ok &= Req(amount, amount.Value > 0, "Số tiền phải lớn hơn 0.");
            ok &= Req(debit, debit.SelectedIndex >= 0, "Chọn tài khoản Nợ."); ok &= Req(credit, credit.SelectedIndex >= 0, "Chọn tài khoản Có.");
            ok &= Req(reason, !string.IsNullOrWhiteSpace(reason.Text), "Nhập lý do chi.");
            if (ok && Convert.ToString(debit.SelectedValue) == Convert.ToString(credit.SelectedValue)) { errors.SetError(credit, "Tài khoản phải khác nhau."); ok = false; }
            if (!ok) MessageBox.Show("Vui lòng điền đầy đủ và chính xác thông tin.", "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return ok;
        }
        private bool Req(Control c, bool condition, string text) { c.BackColor = condition ? Color.White : Color.MistyRose; if (!condition) errors.SetError(c, text); return condition; }
        private void Cancel(object sender, EventArgs e) { if (dirty && !Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?", "Xác nhận hủy")) return; dirty = false; DialogResult = DialogResult.Cancel; Close(); }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && dirty && DialogResult != DialogResult.OK && !Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?", "Xác nhận hủy")) e.Cancel = true;
            base.OnFormClosing(e);
        }
    }
}

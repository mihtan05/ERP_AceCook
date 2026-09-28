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
    internal sealed class ThuEditForm : Form
    {

        private readonly DanhMucService catalog = new DanhMucService();
        private readonly ThuService service = new ThuService();
        private readonly ErrorProvider errors = new ErrorProvider();
        private readonly DateTimePicker date = new DateTimePicker();
        private readonly ComboBox customer = Combo();
        private readonly ComboBox payment = Combo();
        private readonly ComboBox debt = Combo();
        private readonly ComboBox debit = Combo();
        private readonly ComboBox credit = Combo();
        private readonly NumericUpDown amount = new NumericUpDown();
        private readonly TextBox reason = new TextBox();
        private bool dirty;

        public ThuEditForm()
        {
            Text = "Tạo phiếu thu";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(850, 590);
            MinimumSize = new Size(850, 590);
            BackColor = Color.White;
            Build(); LoadCatalogs(); HookDirty();
        }

        private static ComboBox Combo() { return new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 300 }; }

        private void Build()
        {
            Label title = Ui.Title("Tạo phiếu thu"); title.Location = new Point(30, 22); Controls.Add(title);
            Controls.Add(new Label { Text = "Mã phiếu: Tự sinh khi lưu", AutoSize = true, ForeColor = Color.DimGray, Location = new Point(32, 56) });
            TableLayoutPanel form = new TableLayoutPanel
            {
                ColumnCount = 4, RowCount = 5, Location = new Point(30, 78), Size = new Size(790, 365),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None, Padding = new Padding(12), BackColor = Color.FromArgb(248, 249, 250)
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140)); form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140)); form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            form.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            date.Format = DateTimePickerFormat.Custom; date.CustomFormat = "dd/MM/yyyy HH:mm"; date.Width = 300;
            amount.Maximum = 1000000000000m; amount.DecimalPlaces = 0; amount.ThousandsSeparator = true; amount.Width = 300;
            payment.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản" });
            reason.Multiline = true; reason.ScrollBars = ScrollBars.Vertical; reason.Width = 300; reason.Height = 90;

            Add(form, 0, 0, "Ngày lập *", date); Add(form, 2, 0, "Khách hàng/Đại lý *", customer);
            Add(form, 0, 1, "Số tiền *", amount); Add(form, 2, 1, "Hình thức *", payment);
            Add(form, 0, 2, "Khoản công nợ", debt); Add(form, 2, 2, "Tài khoản Nợ *", debit);
            Add(form, 0, 3, "Tài khoản Có *", credit); Add(form, 2, 3, "Lý do thu *", reason);
            Label state = new Label { Text = "Trạng thái do nút lưu quyết định: Nháp hoặc Chờ duyệt", AutoSize = true, ForeColor = Color.DimGray };
            form.Controls.Add(state, 1, 4); form.SetColumnSpan(state, 3);
            Controls.Add(form);

            Button cancel = Ui.Button("Hủy", Cancel, Color.FromArgb(108, 117, 125)); cancel.Location = new Point(420, 475);
            Button draft = Ui.Button("Lưu nháp", delegate { Save("Nháp"); }, Color.FromArgb(13, 202, 240)); draft.Location = new Point(555, 475);
            Button submit = Ui.Button("Lưu & Gửi duyệt", delegate { Save("Chờ duyệt"); }, Ui.Primary); submit.SetBounds(690, 475, 130, 36);
            Controls.Add(cancel); Controls.Add(draft); Controls.Add(submit);
        }

        private static void Add(TableLayoutPanel panel, int col, int row, string label, Control control)
        {
            Label l = new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel.Controls.Add(l, col, row); panel.Controls.Add(control, col + 1, row);
        }

        private void LoadCatalogs()
        {
            customer.DataSource = catalog.KhachHang(); customer.DisplayMember = "Display"; customer.ValueMember = "Id"; customer.SelectedIndex = -1;
            List<LookupItem> accounts = catalog.TaiKhoan();
            debit.DataSource = new List<LookupItem>(accounts); debit.DisplayMember = "Display"; debit.ValueMember = "Id"; debit.SelectedIndex = -1;
            credit.DataSource = new List<LookupItem>(accounts); credit.DisplayMember = "Display"; credit.ValueMember = "Id"; credit.SelectedIndex = -1;
            debt.DataSource = new List<LookupItem> { new LookupItem { Id = "", Name = "Không cấn trừ công nợ" } };
            debt.DisplayMember = "Display"; debt.ValueMember = "Id";
        }

        private void HookDirty()
        {
            customer.SelectedIndexChanged += CustomerChanged;
            payment.SelectedIndexChanged += PaymentChanged;
            foreach (Control c in new Control[] { customer, payment, debt, debit, credit, amount, reason, date })
            {
                c.TextChanged += delegate { dirty = true; };
            }
        }

        private void CustomerChanged(object sender, EventArgs e)
        {
            string id = customer.SelectedValue as string;
            List<LookupItem> items = new List<LookupItem> { new LookupItem { Id = "", Name = "Không cấn trừ công nợ" } };
            if (!string.IsNullOrEmpty(id)) items.AddRange(catalog.CongNoCuaDoiTac(id, "Phải thu"));
            debt.DataSource = items; debt.DisplayMember = "Display"; debt.ValueMember = "Id";
        }

        private void PaymentChanged(object sender, EventArgs e)
        {
            string desired = Convert.ToString(payment.SelectedItem) == "Tiền mặt" ? "1111" : "1121";
            debit.SelectedValue = desired; credit.SelectedValue = "1311";
        }

        private void Save(string state)
        {
            if (!ValidateForm()) return;
            try
            {
                service.Tao(new ChungTuInput
                {
                    NgayLap = date.Value, MaDoiTac = Convert.ToString(customer.SelectedValue), SoTien = amount.Value,
                    HinhThuc = Convert.ToString(payment.SelectedItem), MaCongNo = Convert.ToString(debt.SelectedValue),
                    TaiKhoanNo = Convert.ToString(debit.SelectedValue), TaiKhoanCo = Convert.ToString(credit.SelectedValue),
                    DienGiai = reason.Text.Trim(), TrangThai = state
                });
                dirty = false; Ui.Info("Tạo phiếu thu thành công."); DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { Ui.Error(ex); }
        }

        private bool ValidateForm()
        {
            errors.Clear(); bool ok = true;
            ok &= Required(customer, customer.SelectedIndex >= 0, "Hãy chọn khách hàng.");
            ok &= Required(payment, payment.SelectedIndex >= 0, "Hãy chọn hình thức thanh toán.");
            ok &= Required(amount, amount.Value > 0, "Số tiền phải lớn hơn 0.");
            ok &= Required(debit, debit.SelectedIndex >= 0, "Hãy chọn tài khoản Nợ.");
            ok &= Required(credit, credit.SelectedIndex >= 0, "Hãy chọn tài khoản Có.");
            ok &= Required(reason, !string.IsNullOrWhiteSpace(reason.Text), "Hãy nhập lý do thu.");
            if (ok && Convert.ToString(debit.SelectedValue) == Convert.ToString(credit.SelectedValue))
            { errors.SetError(credit, "Tài khoản Có phải khác tài khoản Nợ."); ok = false; }
            if (!ok) MessageBox.Show("Vui lòng điền đầy đủ và chính xác thông tin.", "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ok;
        }

        private bool Required(Control control, bool condition, string message)
        {
            control.BackColor = condition ? Color.White : Color.MistyRose;
            if (!condition) errors.SetError(control, message);
            return condition;
        }

        private void Cancel(object sender, EventArgs e)
        {
            if (dirty && !Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?", "Xác nhận hủy")) return;
            dirty = false;
            DialogResult = DialogResult.Cancel; Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && dirty && DialogResult != DialogResult.OK &&
                !Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?", "Xác nhận hủy")) e.Cancel = true;
            base.OnFormClosing(e);
        }
    }
}

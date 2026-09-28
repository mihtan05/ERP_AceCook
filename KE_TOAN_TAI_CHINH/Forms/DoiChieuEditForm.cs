using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;
using KeToanTaiChinh.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class DoiChieuEditForm : Form
    {

        private readonly DanhMucService catalog = new DanhMucService(); private readonly DoiChieuService service = new DoiChieuService();
        private readonly ComboBox customer = new ComboBox(); private readonly TextBox period = new TextBox(); private readonly DateTimePicker date = new DateTimePicker();
        private readonly DataGridView grid = Ui.Grid(); private DataTable debts; private bool dirty;
        public DoiChieuEditForm()
        {
            Text = "Lập biên bản đối chiếu"; StartPosition = FormStartPosition.CenterParent; ClientSize = new Size(980, 650); BackColor = Color.White;
            Build(); customer.DataSource = catalog.KhachHang(); customer.DisplayMember = "Display"; customer.ValueMember = "Id"; customer.SelectedIndex = -1;
            customer.SelectedIndexChanged += delegate { LoadDebts(); dirty = true; }; period.TextChanged += delegate { dirty = true; };
        }
        private void Build()
        {
            Label title = Ui.Title("Lập biên bản đối chiếu công nợ"); title.Location = new Point(25, 18); Controls.Add(title);
            Label l1 = new Label { Text = "Đối tác *", AutoSize = true, Location = new Point(28, 78) }; customer.SetBounds(105, 72, 340, 30); customer.DropDownStyle = ComboBoxStyle.DropDownList;
            Label l2 = new Label { Text = "Kỳ *", AutoSize = true, Location = new Point(475, 78) }; period.SetBounds(520, 72, 180, 30); period.Text = DateTime.Today.ToString("MM/yyyy");
            Label l3 = new Label { Text = "Ngày", AutoSize = true, Location = new Point(725, 78) }; date.SetBounds(775, 72, 165, 30); date.Format = DateTimePickerFormat.Custom; date.CustomFormat = "dd/MM/yyyy";
            grid.SetBounds(25, 120, 915, 440); grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid.ReadOnly = false; grid.CellValueChanged += CellChanged; grid.CurrentCellDirtyStateChanged += delegate { if (grid.IsCurrentCellDirty) grid.CommitEdit(DataGridViewDataErrorContexts.Commit); };
            Button cancel = Ui.Button("Hủy", Cancel, Color.FromArgb(108, 117, 125)); cancel.SetBounds(535, 585, 125, 36);
            Button draft = Ui.Button("Lưu nháp", delegate { Save("Nháp"); }, Color.FromArgb(13, 202, 240)); draft.SetBounds(675, 585, 125, 36);
            Button submit = Ui.Button("Lưu & Gửi duyệt", delegate { Save("Chờ duyệt"); }, Ui.Primary); submit.SetBounds(815, 585, 125, 36);
            Controls.AddRange(new Control[] { l1, customer, l2, period, l3, date, grid, cancel, draft, submit });
        }
        private void LoadDebts()
        {
            string id = Convert.ToString(customer.SelectedValue); if (string.IsNullOrEmpty(id)) return;
            try
            {
                debts = service.CongNoDoiTac(id); grid.DataSource = debts;
                foreach (DataGridViewColumn c in grid.Columns) c.ReadOnly = c.Name != "SoTienDoiTac";
                Set("MaCongNo", "Mã công nợ"); Set("SoChungTu", "Chứng từ"); Set("LoaiCongNo", "Loại"); Set("HanThanhToan", "Hạn thanh toán");
                Set("SoTienSoSach", "Số tiền sổ sách"); Set("SoTienDoiTac", "Số tiền đối tác"); Set("ChenhLech", "Chênh lệch"); Ui.MoneyColumns(grid);
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private void Set(string n, string t) { if (grid.Columns.Contains(n)) grid.Columns[n].HeaderText = t; }
        private void CellChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (debts == null || e.RowIndex < 0 || e.ColumnIndex < 0 || grid.Columns[e.ColumnIndex].Name != "SoTienDoiTac") return;
            DataRow row = debts.Rows[e.RowIndex]; decimal book = row["SoTienSoSach"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SoTienSoSach"]);
            decimal partner; if (!decimal.TryParse(Convert.ToString(row["SoTienDoiTac"]), out partner)) partner = 0;
            row["ChenhLech"] = partner - book; dirty = true;
        }
        private void Save(string state)
        {
            if (customer.SelectedIndex < 0 || string.IsNullOrWhiteSpace(period.Text)) { Ui.Info("Vui lòng chọn đối tác và nhập kỳ đối chiếu."); return; }
            if (debts == null || debts.Rows.Count == 0) { Ui.Info("Đối tác không có công nợ còn lại để đối chiếu."); return; }
            List<DoiChieuDong> list = new List<DoiChieuDong>();
            foreach (DataRow r in debts.Rows) list.Add(new DoiChieuDong { MaCongNo = Convert.ToString(r["MaCongNo"]), SoTienSoSach = Convert.ToDecimal(r["SoTienSoSach"]), SoTienDoiTac = Convert.ToDecimal(r["SoTienDoiTac"]) });
            try
            {
                service.Tao(new DoiChieuInput { MaDoiTac = Convert.ToString(customer.SelectedValue), NgayDoiChieu = date.Value, KyDoiChieu = period.Text.Trim(), TrangThai = state, ChiTiet = list });
                dirty = false; Ui.Info("Lập biên bản đối chiếu thành công."); DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { Ui.Error(ex); }
        }
        private void Cancel(object sender, EventArgs e) { if (dirty && !Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?", "Xác nhận hủy")) return; dirty = false; Close(); }
        protected override void OnFormClosing(FormClosingEventArgs e) { if (e.CloseReason == CloseReason.UserClosing && dirty && !Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?", "Xác nhận hủy")) e.Cancel = true; base.OnFormClosing(e); }
    }
}

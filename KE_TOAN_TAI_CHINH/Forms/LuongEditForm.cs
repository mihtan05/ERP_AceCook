using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;
using KeToanTaiChinh.Services;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace KeToanTaiChinh.Forms
{
    internal sealed class LuongEditForm : Form
    {

        private readonly LuongService service = new LuongService(); private readonly DanhMucService catalog = new DanhMucService();
        private readonly TextBox name = new TextBox(); private readonly DateTimePicker period = new DateTimePicker(); private readonly ComboBox department = new ComboBox();
        private readonly ComboBox currency = new ComboBox(); private readonly TextBox note = new TextBox(); private bool dirty;
        public LuongEditForm()
        {
            Text = "Tạo bảng lương"; StartPosition = FormStartPosition.CenterParent; ClientSize = new Size(650, 470); BackColor = Color.White; Build();
            department.DataSource = catalog.PhongBan(); department.DisplayMember = "Display"; department.ValueMember = "Id"; currency.SelectedIndex = 0;
            foreach (Control c in new Control[] { name, period, department, currency, note }) c.TextChanged += delegate { dirty = true; };
        }
        private void Build()
        {
            Label title = Ui.Title("Tạo bảng lương"); title.Location = new Point(30, 22); Controls.Add(title);
            TableLayoutPanel p = new TableLayoutPanel { ColumnCount = 2, RowCount = 5, Location = new Point(30, 75), Size = new Size(590, 300), Padding = new Padding(15), BackColor = Color.FromArgb(248,249,250) };
            p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,160)); p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
            for(int i=0;i<4;i++) p.RowStyles.Add(new RowStyle(SizeType.Absolute,48));
            p.RowStyles.Add(new RowStyle(SizeType.Absolute,70));
            name.Width=350; period.Format=DateTimePickerFormat.Custom; period.CustomFormat="MM/yyyy"; period.ShowUpDown=true; period.Width=350;
            department.DropDownStyle=ComboBoxStyle.DropDownList; department.Width=350; currency.DropDownStyle=ComboBoxStyle.DropDownList; currency.Items.Add("VND"); currency.Width=350;
            note.Multiline=true; note.Height=60; note.Width=350;
            Add(p,0,"Tên bảng lương *",name); Add(p,1,"Kỳ lương *",period); Add(p,2,"Bộ phận/Nhà máy",department); Add(p,3,"Đơn vị tiền tệ",currency); Add(p,4,"Ghi chú",note); Controls.Add(p);
            Button cancel=Ui.Button("Hủy",Cancel,Color.FromArgb(108,117,125)); cancel.Location=new Point(340,400); Button save=Ui.Button("Khởi tạo",Save,Ui.Primary); save.Location=new Point(485,400); Controls.Add(cancel); Controls.Add(save);
        }
        private static void Add(TableLayoutPanel p,int row,string text,Control c) { p.Controls.Add(new Label{Text=text,AutoSize=true,Anchor=AnchorStyles.Left,Font=new Font("Segoe UI",9F,FontStyle.Bold)},0,row); c.Anchor=AnchorStyles.Left|AnchorStyles.Right; p.Controls.Add(c,1,row); }
        private void Save(object sender,EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(name.Text)){Ui.Info("Tên bảng lương là bắt buộc.");return;}
            try
            {
                service.Tao(new BangLuongInput{TenBangLuong=name.Text.Trim(),KyLuong=period.Value.ToString("MM/yyyy"),PhongBan=Convert.ToString(department.SelectedValue),DonViTienTe=Convert.ToString(currency.SelectedItem),GhiChu=note.Text.Trim()});
                dirty=false; Ui.Info("Tạo bảng lương thành công ở trạng thái Nháp."); DialogResult=DialogResult.OK; Close();
            }
            catch(Exception ex){Ui.Error(ex);}
        }
        private void Cancel(object sender,EventArgs e){if(dirty&&!Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?","Xác nhận hủy"))return;dirty=false;Close();}
        protected override void OnFormClosing(FormClosingEventArgs e){if(e.CloseReason==CloseReason.UserClosing&&dirty&&!Ui.Confirm("Dữ liệu chưa được lưu, bạn có muốn thoát?","Xác nhận hủy"))e.Cancel=true;base.OnFormClosing(e);}
    }
}

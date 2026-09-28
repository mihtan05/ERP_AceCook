namespace ERPKho
{
    partial class FrLapPhieuNhap
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.lblLoaiNhap = new System.Windows.Forms.Label();
            this.cboLoaiNhap = new System.Windows.Forms.ComboBox();
            this.lblHangHoa = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblMaLo = new System.Windows.Forms.Label();
            this.txtMaLo = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.lblNSX = new System.Windows.Forms.Label();
            this.dtpNSX = new System.Windows.Forms.DateTimePicker();
            this.lblHSD = new System.Windows.Forms.Label();
            this.dtpHSD = new System.Windows.Forms.DateTimePicker();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(480, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(197, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LẬP PHIẾU NHẬP KHO MỚI";
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaPhieu.Location = new System.Drawing.Point(30, 75);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(61, 15);
            this.lblMaPhieu.TabIndex = 1;
            this.lblMaPhieu.Text = "Mã Phiếu:";
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaPhieu.Location = new System.Drawing.Point(140, 72);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(300, 23);
            this.txtMaPhieu.TabIndex = 2;
            // 
            // lblLoaiNhap
            // 
            this.lblLoaiNhap.AutoSize = true;
            this.lblLoaiNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoaiNhap.Location = new System.Drawing.Point(30, 115);
            this.lblLoaiNhap.Name = "lblLoaiNhap";
            this.lblLoaiNhap.Size = new System.Drawing.Size(64, 15);
            this.lblLoaiNhap.TabIndex = 3;
            this.lblLoaiNhap.Text = "Loại Nhập:";
            // 
            // cboLoaiNhap
            // 
            this.cboLoaiNhap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoaiNhap.FormattingEnabled = true;
            this.cboLoaiNhap.Location = new System.Drawing.Point(140, 112);
            this.cboLoaiNhap.Name = "cboLoaiNhap";
            this.cboLoaiNhap.Size = new System.Drawing.Size(300, 23);
            this.cboLoaiNhap.TabIndex = 4;
            // 
            // lblHangHoa
            // 
            this.lblHangHoa.AutoSize = true;
            this.lblHangHoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHangHoa.Location = new System.Drawing.Point(30, 155);
            this.lblHangHoa.Name = "lblHangHoa";
            this.lblHangHoa.Size = new System.Drawing.Size(65, 15);
            this.lblHangHoa.TabIndex = 5;
            this.lblHangHoa.Text = "Hàng Hóa:";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboHangHoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(140, 152);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(300, 23);
            this.cboHangHoa.TabIndex = 6;
            // 
            // lblMaLo
            // 
            this.lblMaLo.AutoSize = true;
            this.lblMaLo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaLo.Location = new System.Drawing.Point(30, 195);
            this.lblMaLo.Name = "lblMaLo";
            this.lblMaLo.Size = new System.Drawing.Size(44, 15);
            this.lblMaLo.TabIndex = 7;
            this.lblMaLo.Text = "Mã Lô:";
            // 
            // txtMaLo
            // 
            this.txtMaLo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaLo.Location = new System.Drawing.Point(140, 192);
            this.txtMaLo.Name = "txtMaLo";
            this.txtMaLo.Size = new System.Drawing.Size(300, 23);
            this.txtMaLo.TabIndex = 8;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoLuong.Location = new System.Drawing.Point(30, 235);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(63, 15);
            this.lblSoLuong.TabIndex = 9;
            this.lblSoLuong.Text = "Số Lượng:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoLuong.Location = new System.Drawing.Point(140, 232);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(130, 23);
            this.txtSoLuong.TabIndex = 10;
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDonGia.Location = new System.Drawing.Point(280, 235);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(54, 15);
            this.lblDonGia.TabIndex = 11;
            this.lblDonGia.Text = "Đơn Giá:";
            // 
            // txtDonGia
            // 
            this.txtDonGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDonGia.Location = new System.Drawing.Point(335, 232);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(105, 23);
            this.txtDonGia.TabIndex = 12;
            // 
            // lblNSX
            // 
            this.lblNSX.AutoSize = true;
            this.lblNSX.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNSX.Location = new System.Drawing.Point(30, 275);
            this.lblNSX.Name = "lblNSX";
            this.lblNSX.Size = new System.Drawing.Size(89, 15);
            this.lblNSX.TabIndex = 13;
            this.lblNSX.Text = "Ngày Sản Xuất:";
            // 
            // dtpNSX
            // 
            this.dtpNSX.CustomFormat = "dd/MM/yyyy";
            this.dtpNSX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNSX.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNSX.Location = new System.Drawing.Point(140, 272);
            this.dtpNSX.Name = "dtpNSX";
            this.dtpNSX.Size = new System.Drawing.Size(130, 23);
            this.dtpNSX.TabIndex = 14;
            // 
            // lblHSD
            // 
            this.lblHSD.AutoSize = true;
            this.lblHSD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHSD.Location = new System.Drawing.Point(280, 275);
            this.lblHSD.Name = "lblHSD";
            this.lblHSD.Size = new System.Drawing.Size(35, 15);
            this.lblHSD.TabIndex = 15;
            this.lblHSD.Text = "HSD:";
            // 
            // dtpHSD
            // 
            this.dtpHSD.CustomFormat = "dd/MM/yyyy";
            this.dtpHSD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpHSD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHSD.Location = new System.Drawing.Point(320, 272);
            this.dtpHSD.Name = "dtpHSD";
            this.dtpHSD.Size = new System.Drawing.Size(120, 23);
            this.dtpHSD.TabIndex = 16;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(220, 325);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(110, 35);
            this.btnLuu.TabIndex = 17;
            this.btnLuu.Text = "Lưu Phiếu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.Location = new System.Drawing.Point(340, 325);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 35);
            this.btnHuy.TabIndex = 18;
            this.btnHuy.Text = "Đóng";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrLapPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 380);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dtpHSD);
            this.Controls.Add(this.lblHSD);
            this.Controls.Add(this.dtpNSX);
            this.Controls.Add(this.lblNSX);
            this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.lblDonGia);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.txtMaLo);
            this.Controls.Add(this.lblMaLo);
            this.Controls.Add(this.cboHangHoa);
            this.Controls.Add(this.lblHangHoa);
            this.Controls.Add(this.cboLoaiNhap);
            this.Controls.Add(this.lblLoaiNhap);
            this.Controls.Add(this.txtMaPhieu);
            this.Controls.Add(this.lblMaPhieu);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrLapPhieuNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lập Phiếu Nhập Kho";
            this.Load += new System.EventHandler(this.FrLapPhieuNhap_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblLoaiNhap;
        private System.Windows.Forms.ComboBox cboLoaiNhap;
        private System.Windows.Forms.Label lblHangHoa;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblMaLo;
        private System.Windows.Forms.TextBox txtMaLo;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label lblNSX;
        private System.Windows.Forms.DateTimePicker dtpNSX;
        private System.Windows.Forms.Label lblHSD;
        private System.Windows.Forms.DateTimePicker dtpHSD;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
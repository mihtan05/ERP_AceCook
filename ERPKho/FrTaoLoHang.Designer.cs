namespace ERPKho
{
    partial class FrTaoLoHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaLo = new System.Windows.Forms.Label();
            this.txtMaLo = new System.Windows.Forms.TextBox();
            this.lblLoaiHang = new System.Windows.Forms.Label();
            this.cboLoaiHang = new System.Windows.Forms.ComboBox();
            this.lblTenVatTu = new System.Windows.Forms.Label();
            this.txtTenVatTu = new System.Windows.Forms.TextBox();
            this.lblSoLuongCon = new System.Windows.Forms.Label();
            this.txtSoLuongCon = new System.Windows.Forms.TextBox();
            this.lblNSX = new System.Windows.Forms.Label();
            this.dtpNSX = new System.Windows.Forms.DateTimePicker();
            this.lblHSD = new System.Windows.Forms.Label();
            this.dtpHSD = new System.Windows.Forms.DateTimePicker();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(162, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tạo Lô Hàng Mới";
            // 
            // lblMaLo
            // 
            this.lblMaLo.AutoSize = true;
            this.lblMaLo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaLo.Location = new System.Drawing.Point(25, 65);
            this.lblMaLo.Name = "lblMaLo";
            this.lblMaLo.Size = new System.Drawing.Size(75, 15);
            this.lblMaLo.TabIndex = 1;
            this.lblMaLo.Text = "Mã Lô Hàng:";
            // 
            // txtMaLo
            // 
            this.txtMaLo.Location = new System.Drawing.Point(140, 62);
            this.txtMaLo.Name = "txtMaLo";
            this.txtMaLo.ReadOnly = true;
            this.txtMaLo.Size = new System.Drawing.Size(260, 23);
            this.txtMaLo.TabIndex = 2;
            // 
            // lblLoaiHang
            // 
            this.lblLoaiHang.AutoSize = true;
            this.lblLoaiHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoaiHang.Location = new System.Drawing.Point(25, 105);
            this.lblLoaiHang.Name = "lblLoaiHang";
            this.lblLoaiHang.Size = new System.Drawing.Size(65, 15);
            this.lblLoaiHang.TabIndex = 3;
            this.lblLoaiHang.Text = "Loại Hàng:";
            // 
            // cboLoaiHang
            // 
            this.cboLoaiHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiHang.FormattingEnabled = true;
            this.cboLoaiHang.Location = new System.Drawing.Point(140, 102);
            this.cboLoaiHang.Name = "cboLoaiHang";
            this.cboLoaiHang.Size = new System.Drawing.Size(260, 23);
            this.cboLoaiHang.TabIndex = 4;
            // 
            // lblTenVatTu
            // 
            this.lblTenVatTu.AutoSize = true;
            this.lblTenVatTu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenVatTu.Location = new System.Drawing.Point(25, 145);
            this.lblTenVatTu.Name = "lblTenVatTu";
            this.lblTenVatTu.Size = new System.Drawing.Size(62, 15);
            this.lblTenVatTu.TabIndex = 5;
            this.lblTenVatTu.Text = "Tên Hàng:";
            // 
            // txtTenVatTu
            // 
            this.txtTenVatTu.Location = new System.Drawing.Point(140, 142);
            this.txtTenVatTu.Name = "txtTenVatTu";
            this.txtTenVatTu.Size = new System.Drawing.Size(260, 23);
            this.txtTenVatTu.TabIndex = 6;
            // 
            // lblSoLuongCon
            // 
            this.lblSoLuongCon.AutoSize = true;
            this.lblSoLuongCon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoLuongCon.Location = new System.Drawing.Point(25, 185);
            this.lblSoLuongCon.Name = "lblSoLuongCon";
            this.lblSoLuongCon.Size = new System.Drawing.Size(113, 15);
            this.lblSoLuongCon.TabIndex = 7;
            this.lblSoLuongCon.Text = "Số Lượng Ban Đầu:";
            // 
            // txtSoLuongCon
            // 
            this.txtSoLuongCon.Location = new System.Drawing.Point(140, 182);
            this.txtSoLuongCon.Name = "txtSoLuongCon";
            this.txtSoLuongCon.Size = new System.Drawing.Size(260, 23);
            this.txtSoLuongCon.TabIndex = 8;
            // 
            // lblNSX
            // 
            this.lblNSX.AutoSize = true;
            this.lblNSX.Location = new System.Drawing.Point(25, 225);
            this.lblNSX.Name = "lblNSX";
            this.lblNSX.Size = new System.Drawing.Size(86, 15);
            this.lblNSX.TabIndex = 9;
            this.lblNSX.Text = "Ngày Sản Xuất:";
            // 
            // dtpNSX
            // 
            this.dtpNSX.CustomFormat = "dd/MM/yyyy";
            this.dtpNSX.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNSX.Location = new System.Drawing.Point(140, 222);
            this.dtpNSX.Name = "dtpNSX";
            this.dtpNSX.Size = new System.Drawing.Size(260, 23);
            this.dtpNSX.TabIndex = 10;
            // 
            // lblHSD
            // 
            this.lblHSD.AutoSize = true;
            this.lblHSD.Location = new System.Drawing.Point(25, 265);
            this.lblHSD.Name = "lblHSD";
            this.lblHSD.Size = new System.Drawing.Size(81, 15);
            this.lblHSD.TabIndex = 11;
            this.lblHSD.Text = "Hạn Sử Dụng:";
            // 
            // dtpHSD
            // 
            this.dtpHSD.CustomFormat = "dd/MM/yyyy";
            this.dtpHSD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHSD.Location = new System.Drawing.Point(140, 262);
            this.dtpHSD.Name = "dtpHSD";
            this.dtpHSD.Size = new System.Drawing.Size(260, 23);
            this.dtpHSD.TabIndex = 12;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(215, 305);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(88, 32);
            this.btnLuu.TabIndex = 13;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(312, 305);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 32);
            this.btnHuy.TabIndex = 14;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrTaoLoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 355);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dtpHSD);
            this.Controls.Add(this.lblHSD);
            this.Controls.Add(this.dtpNSX);
            this.Controls.Add(this.lblNSX);
            this.Controls.Add(this.txtSoLuongCon);
            this.Controls.Add(this.lblSoLuongCon);
            this.Controls.Add(this.txtTenVatTu);
            this.Controls.Add(this.lblTenVatTu);
            this.Controls.Add(this.cboLoaiHang);
            this.Controls.Add(this.lblLoaiHang);
            this.Controls.Add(this.txtMaLo);
            this.Controls.Add(this.lblMaLo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrTaoLoHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo Lô Hàng Mới";
            this.Load += new System.EventHandler(this.FrTaoLoHang_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaLo;
        private System.Windows.Forms.TextBox txtMaLo;
        private System.Windows.Forms.Label lblLoaiHang;
        private System.Windows.Forms.ComboBox cboLoaiHang;
        private System.Windows.Forms.Label lblTenVatTu;
        private System.Windows.Forms.TextBox txtTenVatTu;
        private System.Windows.Forms.Label lblSoLuongCon;
        private System.Windows.Forms.TextBox txtSoLuongCon;
        private System.Windows.Forms.Label lblNSX;
        private System.Windows.Forms.DateTimePicker dtpNSX;
        private System.Windows.Forms.Label lblHSD;
        private System.Windows.Forms.DateTimePicker dtpHSD;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
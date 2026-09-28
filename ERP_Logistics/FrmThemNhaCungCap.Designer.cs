namespace ERP
{
    partial class FrmThemNhaCungCap
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

        private void InitializeComponent()
        {
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblTenCN = new System.Windows.Forms.Label();
            this.txtTenCN = new System.Windows.Forms.TextBox();
            this.lblSoHieuCN = new System.Windows.Forms.Label();
            this.txtSoHieuCN = new System.Windows.Forms.TextBox();
            this.lblNgayCap = new System.Windows.Forms.Label();
            this.dtpNgayCap = new System.Windows.Forms.DateTimePicker();
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.dtpNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(30, 30);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(50, 15);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "Mã NCC:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(140, 27);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(300, 23);
            this.txtID.TabIndex = 1;
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(30, 70);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(54, 15);
            this.lblTen.TabIndex = 2;
            this.lblTen.Text = "Tên NCC:";
            // 
            // txtTenNCC
            // 
            this.txtTenNCC.Location = new System.Drawing.Point(140, 67);
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Size = new System.Drawing.Size(300, 23);
            this.txtTenNCC.TabIndex = 3;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(30, 110);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(46, 15);
            this.lblDiaChi.TabIndex = 4;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(140, 107);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(300, 23);
            this.txtDiaChi.TabIndex = 5;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(30, 150);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(30, 15);
            this.lblSDT.TabIndex = 6;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(140, 147);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(300, 23);
            this.txtSDT.TabIndex = 7;
            // 
            // lblTenCN
            // 
            this.lblTenCN.AutoSize = true;
            this.lblTenCN.Location = new System.Drawing.Point(30, 190);
            this.lblTenCN.Name = "lblTenCN";
            this.lblTenCN.Size = new System.Drawing.Size(89, 15);
            this.lblTenCN.TabIndex = 8;
            this.lblTenCN.Text = "Tên Chứng Nhận:";
            // 
            // txtTenCN
            // 
            this.txtTenCN.Location = new System.Drawing.Point(140, 187);
            this.txtTenCN.Name = "txtTenCN";
            this.txtTenCN.Size = new System.Drawing.Size(300, 23);
            this.txtTenCN.TabIndex = 9;
            // 
            // lblSoHieuCN
            // 
            this.lblSoHieuCN.AutoSize = true;
            this.lblSoHieuCN.Location = new System.Drawing.Point(30, 230);
            this.lblSoHieuCN.Name = "lblSoHieuCN";
            this.lblSoHieuCN.Size = new System.Drawing.Size(69, 15);
            this.lblSoHieuCN.TabIndex = 10;
            this.lblSoHieuCN.Text = "Số Hiệu CN:";
            // 
            // txtSoHieuCN
            // 
            this.txtSoHieuCN.Location = new System.Drawing.Point(140, 227);
            this.txtSoHieuCN.Name = "txtSoHieuCN";
            this.txtSoHieuCN.Size = new System.Drawing.Size(300, 23);
            this.txtSoHieuCN.TabIndex = 11;
            // 
            // lblNgayCap
            // 
            this.lblNgayCap.AutoSize = true;
            this.lblNgayCap.Location = new System.Drawing.Point(30, 270);
            this.lblNgayCap.Name = "lblNgayCap";
            this.lblNgayCap.Size = new System.Drawing.Size(61, 15);
            this.lblNgayCap.TabIndex = 12;
            this.lblNgayCap.Text = "Ngày Cấp:";
            // 
            // dtpNgayCap
            // 
            this.dtpNgayCap.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayCap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayCap.Location = new System.Drawing.Point(140, 267);
            this.dtpNgayCap.Name = "dtpNgayCap";
            this.dtpNgayCap.ShowCheckBox = true;
            this.dtpNgayCap.Size = new System.Drawing.Size(300, 23);
            this.dtpNgayCap.TabIndex = 13;
            // 
            // lblNgayHetHan
            // 
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Location = new System.Drawing.Point(30, 310);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new System.Drawing.Size(84, 15);
            this.lblNgayHetHan.TabIndex = 14;
            this.lblNgayHetHan.Text = "Ngày Hết Hạn:";
            // 
            // dtpNgayHetHan
            // 
            this.dtpNgayHetHan.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayHetHan.Location = new System.Drawing.Point(140, 307);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.ShowCheckBox = true;
            this.dtpNgayHetHan.Size = new System.Drawing.Size(300, 23);
            this.dtpNgayHetHan.TabIndex = 15;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(234, 360);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 35);
            this.btnLuu.TabIndex = 16;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(340, 360);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 35);
            this.btnHuy.TabIndex = 17;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrmThemNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 420);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dtpNgayHetHan);
            this.Controls.Add(this.lblNgayHetHan);
            this.Controls.Add(this.dtpNgayCap);
            this.Controls.Add(this.lblNgayCap);
            this.Controls.Add(this.txtSoHieuCN);
            this.Controls.Add(this.lblSoHieuCN);
            this.Controls.Add(this.txtTenCN);
            this.Controls.Add(this.lblTenCN);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.lblSDT);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtTenNCC);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmThemNhaCungCap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Nhà Cung Cấp Mới";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTenNCC;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblTenCN;
        private System.Windows.Forms.TextBox txtTenCN;
        private System.Windows.Forms.Label lblSoHieuCN;
        private System.Windows.Forms.TextBox txtSoHieuCN;
        private System.Windows.Forms.Label lblNgayCap;
        private System.Windows.Forms.DateTimePicker dtpNgayCap;
        private System.Windows.Forms.Label lblNgayHetHan;
        private System.Windows.Forms.DateTimePicker dtpNgayHetHan;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
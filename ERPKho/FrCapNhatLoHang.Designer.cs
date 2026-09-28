namespace ERPKho
{
    partial class FrCapNhatLoHang
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
            this.lblTenVatTu = new System.Windows.Forms.Label();
            this.txtTenVatTu = new System.Windows.Forms.TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
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
            this.lblTitle.Size = new System.Drawing.Size(223, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cập Nhật Thông Tin Lô";
            // 
            // lblMaLo
            // 
            this.lblMaLo.AutoSize = true;
            this.lblMaLo.Location = new System.Drawing.Point(25, 75);
            this.lblMaLo.Name = "lblMaLo";
            this.lblMaLo.Size = new System.Drawing.Size(71, 15);
            this.lblMaLo.TabIndex = 1;
            this.lblMaLo.Text = "Mã Lô Hàng";
            // 
            // txtMaLo
            // 
            this.txtMaLo.Location = new System.Drawing.Point(140, 72);
            this.txtMaLo.Name = "txtMaLo";
            this.txtMaLo.ReadOnly = true;
            this.txtMaLo.Size = new System.Drawing.Size(260, 23);
            this.txtMaLo.TabIndex = 1;
            // 
            // lblTenVatTu
            // 
            this.lblTenVatTu.AutoSize = true;
            this.lblTenVatTu.Location = new System.Drawing.Point(25, 115);
            this.lblTenVatTu.Name = "lblTenVatTu";
            this.lblTenVatTu.Size = new System.Drawing.Size(58, 15);
            this.lblTenVatTu.TabIndex = 2;
            this.lblTenVatTu.Text = "Tên Hàng";
            // 
            // txtTenVatTu
            // 
            this.txtTenVatTu.Location = new System.Drawing.Point(140, 112);
            this.txtTenVatTu.Name = "txtTenVatTu";
            this.txtTenVatTu.ReadOnly = true;
            this.txtTenVatTu.Size = new System.Drawing.Size(260, 23);
            this.txtTenVatTu.TabIndex = 2;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(25, 155);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(62, 15);
            this.lblTrangThai.TabIndex = 3;
            this.lblTrangThai.Text = "Trạng Thái";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(140, 152);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(260, 23);
            this.cboTrangThai.TabIndex = 3;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(215, 210);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(88, 32);
            this.btnLuu.TabIndex = 4;
            this.btnLuu.Text = "Cập Nhật";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(312, 210);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 32);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrCapNhatLoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 265);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.txtTenVatTu);
            this.Controls.Add(this.lblTenVatTu);
            this.Controls.Add(this.txtMaLo);
            this.Controls.Add(this.lblMaLo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrCapNhatLoHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập Nhật Thông Tin Lô";
            this.Load += new System.EventHandler(this.FrCapNhatLoHang_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaLo;
        private System.Windows.Forms.TextBox txtMaLo;
        private System.Windows.Forms.Label lblTenVatTu;
        private System.Windows.Forms.TextBox txtTenVatTu;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
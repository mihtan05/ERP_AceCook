namespace ERPKho
{
    partial class FrKhoaLoHang
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
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.btnXacNhanKhoa = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Crimson;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(185, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Khóa / Phong Tỏa Lô";
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
            this.txtMaLo.Location = new System.Drawing.Point(120, 72);
            this.txtMaLo.Name = "txtMaLo";
            this.txtMaLo.ReadOnly = true;
            this.txtMaLo.Size = new System.Drawing.Size(280, 23);
            this.txtMaLo.TabIndex = 1;
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(25, 115);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(37, 15);
            this.lblLyDo.TabIndex = 2;
            this.lblLyDo.Text = "Lý Do";
            // 
            // txtLyDo
            // 
            this.txtLyDo.Location = new System.Drawing.Point(120, 112);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(280, 80);
            this.txtLyDo.TabIndex = 2;
            // 
            // btnXacNhanKhoa
            // 
            this.btnXacNhanKhoa.BackColor = System.Drawing.Color.Crimson;
            this.btnXacNhanKhoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhanKhoa.ForeColor = System.Drawing.Color.White;
            this.btnXacNhanKhoa.Location = new System.Drawing.Point(195, 215);
            this.btnXacNhanKhoa.Name = "btnXacNhanKhoa";
            this.btnXacNhanKhoa.Size = new System.Drawing.Size(105, 32);
            this.btnXacNhanKhoa.TabIndex = 3;
            this.btnXacNhanKhoa.Text = "Xác Nhận Khóa";
            this.btnXacNhanKhoa.UseVisualStyleBackColor = false;
            this.btnXacNhanKhoa.Click += new System.EventHandler(this.btnXacNhanKhoa_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(310, 215);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 32);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrKhoaLoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 270);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhanKhoa);
            this.Controls.Add(this.txtLyDo);
            this.Controls.Add(this.lblLyDo);
            this.Controls.Add(this.txtMaLo);
            this.Controls.Add(this.lblMaLo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrKhoaLoHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Khóa Lô Hàng";
            this.Load += new System.EventHandler(this.FrKhoaLoHang_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaLo;
        private System.Windows.Forms.TextBox txtMaLo;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Button btnXacNhanKhoa;
        private System.Windows.Forms.Button btnHuy;
    }
}
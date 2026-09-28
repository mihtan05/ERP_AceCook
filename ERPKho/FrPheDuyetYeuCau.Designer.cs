namespace ERPKho
{
    partial class FrPheDuyetYeuCau
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtThongTin = new System.Windows.Forms.TextBox();
            this.lblHuongXuLy = new System.Windows.Forms.Label();
            this.cboHuongXuLy = new System.Windows.Forms.ComboBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(273, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÊ DUYỆT HƯỚNG XỬ LÝ";
            // 
            // txtThongTin
            // 
            this.txtThongTin.BackColor = System.Drawing.Color.White;
            this.txtThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtThongTin.Location = new System.Drawing.Point(25, 48);
            this.txtThongTin.Multiline = true;
            this.txtThongTin.Name = "txtThongTin";
            this.txtThongTin.ReadOnly = true;
            this.txtThongTin.Size = new System.Drawing.Size(380, 120);
            this.txtThongTin.TabIndex = 1;
            // 
            // lblHuongXuLy
            // 
            this.lblHuongXuLy.AutoSize = true;
            this.lblHuongXuLy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHuongXuLy.Location = new System.Drawing.Point(22, 180);
            this.lblHuongXuLy.Name = "lblHuongXuLy";
            this.lblHuongXuLy.Size = new System.Drawing.Size(181, 17);
            this.lblHuongXuLy.TabIndex = 2;
            this.lblHuongXuLy.Text = "Chọn hướng xử lý kho hàng:";
            // 
            // cboHuongXuLy
            // 
            this.cboHuongXuLy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHuongXuLy.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboHuongXuLy.FormattingEnabled = true;
            this.cboHuongXuLy.Location = new System.Drawing.Point(25, 202);
            this.cboHuongXuLy.Name = "cboHuongXuLy";
            this.cboHuongXuLy.Size = new System.Drawing.Size(380, 25);
            this.cboHuongXuLy.TabIndex = 3;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGhiChu.Location = new System.Drawing.Point(22, 238);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(130, 17);
            this.lblGhiChu.TabIndex = 4;
            this.lblGhiChu.Text = "Ghi chú thêm (Nếu có):";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGhiChu.Location = new System.Drawing.Point(25, 260);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(380, 55);
            this.txtGhiChu.TabIndex = 5;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.ForestGreen;
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(180, 330);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(135, 35);
            this.btnXacNhan.TabIndex = 6;
            this.btnXacNhan.Text = "✓ Phê Duyệt";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(325, 330);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 35);
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "Thoát";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrPheDuyetYeuCau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 385);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.cboHuongXuLy);
            this.Controls.Add(this.lblHuongXuLy);
            this.Controls.Add(this.txtThongTin);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrPheDuyetYeuCau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Phê Duyệt Xử Lý";
            this.Load += new System.EventHandler(this.FrPheDuyetYeuCau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtThongTin;
        private System.Windows.Forms.Label lblHuongXuLy;
        private System.Windows.Forms.ComboBox cboHuongXuLy;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
    }
}
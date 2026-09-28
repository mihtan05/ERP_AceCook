namespace ERPKho
{
    partial class FrXacNhanXuatKho
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPhieu = new System.Windows.Forms.Label();
            this.btnXacNhanHoanThanh = new System.Windows.Forms.Button();
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
            this.lblTitle.Size = new System.Drawing.Size(221, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "XÁC NHẬN XUẤT KHO";
            // 
            // lblPhieu
            // 
            this.lblPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPhieu.Location = new System.Drawing.Point(22, 50);
            this.lblPhieu.Name = "lblPhieu";
            this.lblPhieu.Size = new System.Drawing.Size(360, 70);
            this.lblPhieu.TabIndex = 1;
            this.lblPhieu.Text = "Mã phiếu xuất: ---\nTrạng thái hiện tại: ---";
            // 
            // btnXacNhanHoanThanh
            // 
            this.btnXacNhanHoanThanh.BackColor = System.Drawing.Color.ForestGreen;
            this.btnXacNhanHoanThanh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhanHoanThanh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXacNhanHoanThanh.ForeColor = System.Drawing.Color.White;
            this.btnXacNhanHoanThanh.Location = new System.Drawing.Point(90, 130);
            this.btnXacNhanHoanThanh.Name = "btnXacNhanHoanThanh";
            this.btnXacNhanHoanThanh.Size = new System.Drawing.Size(160, 35);
            this.btnXacNhanHoanThanh.TabIndex = 2;
            this.btnXacNhanHoanThanh.Text = "Xác Nhận Hoàn Tất";
            this.btnXacNhanHoanThanh.UseVisualStyleBackColor = false;
            this.btnXacNhanHoanThanh.Click += new System.EventHandler(this.btnXacNhanHoanThanh_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(260, 130);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(110, 35);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy Bỏ";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrXacNhanXuatKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 185);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhanHoanThanh);
            this.Controls.Add(this.lblPhieu);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrXacNhanXuatKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác nhận xuất kho";
            this.Load += new System.EventHandler(this.FrXacNhanXuatKho_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPhieu;
        private System.Windows.Forms.Button btnXacNhanHoanThanh;
        private System.Windows.Forms.Button btnHuy;
    }
}
namespace ERPKho
{
    partial class FrTuChoiYeuCau
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
            this.lblLyDoTuChoi = new System.Windows.Forms.Label();
            this.txtLyDoTuChoi = new System.Windows.Forms.TextBox();
            this.btnXacNhanTuChoi = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Crimson;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(242, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TỪ CHỐI ĐƠN YÊU CẦU";
            // 
            // txtThongTin
            // 
            this.txtThongTin.BackColor = System.Drawing.Color.White;
            this.txtThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtThongTin.Location = new System.Drawing.Point(25, 48);
            this.txtThongTin.Multiline = true;
            this.txtThongTin.Name = "txtThongTin";
            this.txtThongTin.ReadOnly = true;
            this.txtThongTin.Size = new System.Drawing.Size(380, 110);
            this.txtThongTin.TabIndex = 1;
            // 
            // lblLyDoTuChoi
            // 
            this.lblLyDoTuChoi.AutoSize = true;
            this.lblLyDoTuChoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLyDoTuChoi.ForeColor = System.Drawing.Color.DarkRed;
            this.lblLyDoTuChoi.Location = new System.Drawing.Point(22, 172);
            this.lblLyDoTuChoi.Name = "lblLyDoTuChoi";
            this.lblLyDoTuChoi.Size = new System.Drawing.Size(212, 17);
            this.lblLyDoTuChoi.TabIndex = 2;
            this.lblLyDoTuChoi.Text = "Lý do từ chối (*Bắt buộc nhập):";
            // 
            // txtLyDoTuChoi
            // 
            this.txtLyDoTuChoi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLyDoTuChoi.Location = new System.Drawing.Point(25, 195);
            this.txtLyDoTuChoi.Multiline = true;
            this.txtLyDoTuChoi.Name = "txtLyDoTuChoi";
            this.txtLyDoTuChoi.Size = new System.Drawing.Size(380, 80);
            this.txtLyDoTuChoi.TabIndex = 3;
            // 
            // btnXacNhanTuChoi
            // 
            this.btnXacNhanTuChoi.BackColor = System.Drawing.Color.Crimson;
            this.btnXacNhanTuChoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhanTuChoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXacNhanTuChoi.ForeColor = System.Drawing.Color.White;
            this.btnXacNhanTuChoi.Location = new System.Drawing.Point(180, 290);
            this.btnXacNhanTuChoi.Name = "btnXacNhanTuChoi";
            this.btnXacNhanTuChoi.Size = new System.Drawing.Size(135, 35);
            this.btnXacNhanTuChoi.TabIndex = 4;
            this.btnXacNhanTuChoi.Text = "✕ Xác Nhận Từ Chối";
            this.btnXacNhanTuChoi.UseVisualStyleBackColor = false;
            this.btnXacNhanTuChoi.Click += new System.EventHandler(this.btnXacNhanTuChoi_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(325, 290);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 35);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Thoát";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrTuChoiYeuCau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 345);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhanTuChoi);
            this.Controls.Add(this.txtLyDoTuChoi);
            this.Controls.Add(this.lblLyDoTuChoi);
            this.Controls.Add(this.txtThongTin);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrTuChoiYeuCau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Từ Chối Yêu Cầu";
            this.Load += new System.EventHandler(this.FrTuChoiYeuCau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtThongTin;
        private System.Windows.Forms.Label lblLyDoTuChoi;
        private System.Windows.Forms.TextBox txtLyDoTuChoi;
        private System.Windows.Forms.Button btnXacNhanTuChoi;
        private System.Windows.Forms.Button btnHuy;
    }
}
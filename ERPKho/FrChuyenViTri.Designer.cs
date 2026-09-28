namespace ERPKho
{
    partial class FrChuyenViTri
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblNewViTri = new System.Windows.Forms.Label();
            this.cboNewViTri = new System.Windows.Forms.ComboBox();
            this.btnChuyen = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(424, 45);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(282, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHUYỂN VỊ TRÍ LÔ HÀNG KHO (FR-02)";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfo.Location = new System.Drawing.Point(20, 58);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 15);
            this.lblInfo.TabIndex = 1;
            // 
            // lblNewViTri
            // 
            this.lblNewViTri.AutoSize = true;
            this.lblNewViTri.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNewViTri.Location = new System.Drawing.Point(20, 120);
            this.lblNewViTri.Name = "lblNewViTri";
            this.lblNewViTri.Size = new System.Drawing.Size(128, 15);
            this.lblNewViTri.TabIndex = 2;
            this.lblNewViTri.Text = "Chuyển sang vị trí mới:";
            // 
            // cboNewViTri
            // 
            this.cboNewViTri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNewViTri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNewViTri.FormattingEnabled = true;
            this.cboNewViTri.Location = new System.Drawing.Point(155, 117);
            this.cboNewViTri.Name = "cboNewViTri";
            this.cboNewViTri.Size = new System.Drawing.Size(250, 23);
            this.cboNewViTri.TabIndex = 3;
            // 
            // btnChuyen
            // 
            this.btnChuyen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnChuyen.FlatAppearance.BorderSize = 0;
            this.btnChuyen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuyen.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnChuyen.ForeColor = System.Drawing.Color.White;
            this.btnChuyen.Location = new System.Drawing.Point(175, 160);
            this.btnChuyen.Name = "btnChuyen";
            this.btnChuyen.Size = new System.Drawing.Size(145, 32);
            this.btnChuyen.TabIndex = 4;
            this.btnChuyen.Text = "🔄 Xác Nhận Chuyển";
            this.btnChuyen.UseVisualStyleBackColor = false;
            this.btnChuyen.Click += new System.EventHandler(this.btnChuyen_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.Location = new System.Drawing.Point(330, 160);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 32);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrChuyenViTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(424, 210);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnChuyen);
            this.Controls.Add(this.cboNewViTri);
            this.Controls.Add(this.lblNewViTri);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrChuyenViTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chuyển Vị Trí Lô Hàng";
            this.Load += new System.EventHandler(this.FrChuyenViTri_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblNewViTri;
        private System.Windows.Forms.ComboBox cboNewViTri;
        private System.Windows.Forms.Button btnChuyen;
        private System.Windows.Forms.Button btnHuy;
    }
}
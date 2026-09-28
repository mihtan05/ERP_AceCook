namespace ERPKho
{
    partial class FrPhanBoViTri
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
            this.lblMaLo = new System.Windows.Forms.Label();
            this.cboMaLo = new System.Windows.Forms.ComboBox();
            this.lblTenHang = new System.Windows.Forms.Label();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtDonVi = new System.Windows.Forms.TextBox();
            this.lblViTriTarget = new System.Windows.Forms.Label();
            this.cboViTriTarget = new System.Windows.Forms.ComboBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(434, 45);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(304, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÂN BỔ LÔ HÀNG VÀO Ô CHỨA (FR-02)";
            // 
            // lblMaLo
            // 
            this.lblMaLo.AutoSize = true;
            this.lblMaLo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaLo.Location = new System.Drawing.Point(20, 65);
            this.lblMaLo.Name = "lblMaLo";
            this.lblMaLo.Size = new System.Drawing.Size(75, 15);
            this.lblMaLo.TabIndex = 1;
            this.lblMaLo.Text = "Mã Lô Hàng:";
            // 
            // cboMaLo
            // 
            this.cboMaLo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaLo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboMaLo.FormattingEnabled = true;
            this.cboMaLo.Location = new System.Drawing.Point(135, 62);
            this.cboMaLo.Name = "cboMaLo";
            this.cboMaLo.Size = new System.Drawing.Size(270, 23);
            this.cboMaLo.TabIndex = 2;
            this.cboMaLo.SelectedIndexChanged += new System.EventHandler(this.cboMaLo_SelectedIndexChanged);
            // 
            // lblTenHang
            // 
            this.lblTenHang.AutoSize = true;
            this.lblTenHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenHang.Location = new System.Drawing.Point(20, 100);
            this.lblTenHang.Name = "lblTenHang";
            this.lblTenHang.Size = new System.Drawing.Size(85, 15);
            this.lblTenHang.TabIndex = 3;
            this.lblTenHang.Text = "Tên Mặt Hàng:";
            // 
            // txtTenHang
            // 
            this.txtTenHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenHang.Location = new System.Drawing.Point(135, 97);
            this.txtTenHang.Name = "txtTenHang";
            this.txtTenHang.ReadOnly = true;
            this.txtTenHang.Size = new System.Drawing.Size(270, 23);
            this.txtTenHang.TabIndex = 4;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoLuong.Location = new System.Drawing.Point(20, 135);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(103, 15);
            this.lblSoLuong.TabIndex = 5;
            this.lblSoLuong.Text = "Số Lượng Lưu Trữ:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoLuong.Location = new System.Drawing.Point(135, 132);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(170, 23);
            this.txtSoLuong.TabIndex = 6;
            // 
            // txtDonVi
            // 
            this.txtDonVi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDonVi.Location = new System.Drawing.Point(315, 132);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.ReadOnly = true;
            this.txtDonVi.Size = new System.Drawing.Size(90, 23);
            this.txtDonVi.TabIndex = 7;
            // 
            // lblViTriTarget
            // 
            this.lblViTriTarget.AutoSize = true;
            this.lblViTriTarget.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblViTriTarget.Location = new System.Drawing.Point(20, 175);
            this.lblViTriTarget.Name = "lblViTriTarget";
            this.lblViTriTarget.Size = new System.Drawing.Size(106, 15);
            this.lblViTriTarget.TabIndex = 8;
            this.lblViTriTarget.Text = "Chọn Ô Chứa Kho:";
            // 
            // cboViTriTarget
            // 
            this.cboViTriTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboViTriTarget.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboViTriTarget.FormattingEnabled = true;
            this.cboViTriTarget.Location = new System.Drawing.Point(135, 172);
            this.cboViTriTarget.Name = "cboViTriTarget";
            this.cboViTriTarget.Size = new System.Drawing.Size(270, 23);
            this.cboViTriTarget.TabIndex = 9;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXacNhan.FlatAppearance.BorderSize = 0;
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(175, 220);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(140, 32);
            this.btnXacNhan.TabIndex = 10;
            this.btnXacNhan.Text = "Xác Nhận Phân Bổ";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.Location = new System.Drawing.Point(325, 220);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 32);
            this.btnHuy.TabIndex = 11;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrPhanBoViTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(434, 270);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.cboViTriTarget);
            this.Controls.Add(this.lblViTriTarget);
            this.Controls.Add(this.txtDonVi);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.txtTenHang);
            this.Controls.Add(this.lblTenHang);
            this.Controls.Add(this.cboMaLo);
            this.Controls.Add(this.lblMaLo);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrPhanBoViTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Phân Bổ Vị Trí Cho Lô Hàng";
            this.Load += new System.EventHandler(this.FrPhanBoViTri_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaLo;
        private System.Windows.Forms.ComboBox cboMaLo;
        private System.Windows.Forms.Label lblTenHang;
        private System.Windows.Forms.TextBox txtTenHang;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtDonVi;
        private System.Windows.Forms.Label lblViTriTarget;
        private System.Windows.Forms.ComboBox cboViTriTarget;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
    }
}
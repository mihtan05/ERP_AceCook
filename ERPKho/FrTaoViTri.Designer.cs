namespace ERPKho
{
    partial class FrTaoViTri
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
            this.lblKho = new System.Windows.Forms.Label();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.cboKhuVuc = new System.Windows.Forms.ComboBox();
            this.lblDay = new System.Windows.Forms.Label();
            this.txtDay = new System.Windows.Forms.TextBox();
            this.lblKe = new System.Windows.Forms.Label();
            this.txtKe = new System.Windows.Forms.TextBox();
            this.lblOChua = new System.Windows.Forms.Label();
            this.txtOChua = new System.Windows.Forms.TextBox();
            this.lblSucChua = new System.Windows.Forms.Label();
            this.numSucChua = new System.Windows.Forms.NumericUpDown();
            this.lblMaViTri = new System.Windows.Forms.Label();
            this.txtMaViTri = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSucChua)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
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
            this.lblTitle.Size = new System.Drawing.Size(250, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TẠO VỊ TRÍ LƯU TRỮ MỚI (FR-02)";
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKho.Location = new System.Drawing.Point(20, 65);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(63, 15);
            this.lblKho.TabIndex = 1;
            this.lblKho.Text = "Chọn Kho:";
            // 
            // cboKho
            // 
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(135, 62);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(270, 23);
            this.cboKho.TabIndex = 2;
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKhuVuc.Location = new System.Drawing.Point(20, 100);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(54, 15);
            this.lblKhuVuc.TabIndex = 3;
            this.lblKhuVuc.Text = "Khu Vực:";
            // 
            // cboKhuVuc
            // 
            this.cboKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhuVuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboKhuVuc.FormattingEnabled = true;
            this.cboKhuVuc.Items.AddRange(new object[] {
            "Zone A",
            "Zone B",
            "Zone C"});
            this.cboKhuVuc.Location = new System.Drawing.Point(135, 97);
            this.cboKhuVuc.Name = "cboKhuVuc";
            this.cboKhuVuc.Size = new System.Drawing.Size(270, 23);
            this.cboKhuVuc.TabIndex = 4;
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDay.Location = new System.Drawing.Point(20, 135);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(74, 15);
            this.lblDay.TabIndex = 5;
            this.lblDay.Text = "Dãy / Kệ / Ô:";
            // 
            // txtDay
            // 
            this.txtDay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDay.Location = new System.Drawing.Point(135, 132);
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(75, 23);
            this.txtDay.TabIndex = 6;
            // 
            // lblKe
            // 
            this.lblKe.AutoSize = true;
            this.lblKe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKe.Location = new System.Drawing.Point(220, 135);
            this.lblKe.Name = "lblKe";
            this.lblKe.Size = new System.Drawing.Size(0, 15);
            this.lblKe.TabIndex = 7;
            // 
            // txtKe
            // 
            this.txtKe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKe.Location = new System.Drawing.Point(225, 132);
            this.txtKe.Name = "txtKe";
            this.txtKe.Size = new System.Drawing.Size(85, 23);
            this.txtKe.TabIndex = 8;
            // 
            // lblOChua
            // 
            this.lblOChua.AutoSize = true;
            this.lblOChua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOChua.Location = new System.Drawing.Point(318, 135);
            this.lblOChua.Name = "lblOChua";
            this.lblOChua.Size = new System.Drawing.Size(0, 15);
            this.lblOChua.TabIndex = 9;
            // 
            // txtOChua
            // 
            this.txtOChua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOChua.Location = new System.Drawing.Point(320, 132);
            this.txtOChua.Name = "txtOChua";
            this.txtOChua.Size = new System.Drawing.Size(85, 23);
            this.txtOChua.TabIndex = 10;
            // 
            // lblSucChua
            // 
            this.lblSucChua.AutoSize = true;
            this.lblSucChua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSucChua.Location = new System.Drawing.Point(20, 170);
            this.lblSucChua.Name = "lblSucChua";
            this.lblSucChua.Size = new System.Drawing.Size(97, 15);
            this.lblSucChua.TabIndex = 11;
            this.lblSucChua.Text = "Sức Chứa Tối Đa:";
            // 
            // numSucChua
            // 
            this.numSucChua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numSucChua.Location = new System.Drawing.Point(135, 168);
            this.numSucChua.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSucChua.Name = "numSucChua";
            this.numSucChua.Size = new System.Drawing.Size(270, 23);
            this.numSucChua.TabIndex = 12;
            // 
            // lblMaViTri
            // 
            this.lblMaViTri.AutoSize = true;
            this.lblMaViTri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaViTri.Location = new System.Drawing.Point(20, 205);
            this.lblMaViTri.Name = "lblMaViTri";
            this.lblMaViTri.Size = new System.Drawing.Size(56, 15);
            this.lblMaViTri.TabIndex = 13;
            this.lblMaViTri.Text = "Mã Vị Trí:";
            // 
            // txtMaViTri
            // 
            this.txtMaViTri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaViTri.Location = new System.Drawing.Point(135, 202);
            this.txtMaViTri.Name = "txtMaViTri";
            this.txtMaViTri.Size = new System.Drawing.Size(270, 23);
            this.txtMaViTri.TabIndex = 14;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(200, 245);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(110, 32);
            this.btnLuu.TabIndex = 15;
            this.btnLuu.Text = "Lưu Vị Trí";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.Location = new System.Drawing.Point(320, 245);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(85, 32);
            this.btnHuy.TabIndex = 16;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrTaoViTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(434, 295);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtMaViTri);
            this.Controls.Add(this.lblMaViTri);
            this.Controls.Add(this.numSucChua);
            this.Controls.Add(this.lblSucChua);
            this.Controls.Add(this.txtOChua);
            this.Controls.Add(this.lblOChua);
            this.Controls.Add(this.txtKe);
            this.Controls.Add(this.lblKe);
            this.Controls.Add(this.txtDay);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.cboKhuVuc);
            this.Controls.Add(this.lblKhuVuc);
            this.Controls.Add(this.cboKho);
            this.Controls.Add(this.lblKho);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrTaoViTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo Vị Trí Lưu Trữ Mới";
            this.Load += new System.EventHandler(this.FrTaoViTri_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSucChua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.ComboBox cboKhuVuc;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.TextBox txtDay;
        private System.Windows.Forms.Label lblKe;
        private System.Windows.Forms.TextBox txtKe;
        private System.Windows.Forms.Label lblOChua;
        private System.Windows.Forms.TextBox txtOChua;
        private System.Windows.Forms.Label lblSucChua;
        private System.Windows.Forms.NumericUpDown numSucChua;
        private System.Windows.Forms.Label lblMaViTri;
        private System.Windows.Forms.TextBox txtMaViTri;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
namespace ERPKho
{
    partial class FrMain
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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.pnlUserBottom = new System.Windows.Forms.Panel();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnQLTraHang = new System.Windows.Forms.Button();
            this.btnQLXuatKho = new System.Windows.Forms.Button();
            this.btnQLLoHang = new System.Windows.Forms.Button();
            this.btnQLTonKho = new System.Windows.Forms.Button();
            this.btnQLLuutru = new System.Windows.Forms.Button();
            this.btnQLNhapKho = new System.Windows.Forms.Button();
            this.picBrandLogo = new System.Windows.Forms.PictureBox();
            this.pnlTopHeader = new System.Windows.Forms.Panel();
            this.lblTopUser = new System.Windows.Forms.Label();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.pnlSidebar.SuspendLayout();
            this.pnlUserBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBrandLogo)).BeginInit();
            this.pnlTopHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.Crimson;
            this.pnlSidebar.Controls.Add(this.btnDangXuat);
            this.pnlSidebar.Controls.Add(this.pnlUserBottom);
            this.pnlSidebar.Controls.Add(this.btnQLTraHang);
            this.pnlSidebar.Controls.Add(this.btnQLXuatKho);
            this.pnlSidebar.Controls.Add(this.btnQLLoHang);
            this.pnlSidebar.Controls.Add(this.btnQLTonKho);
            this.pnlSidebar.Controls.Add(this.btnQLLuutru);
            this.pnlSidebar.Controls.Add(this.btnQLNhapKho);
            this.pnlSidebar.Controls.Add(this.picBrandLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.ForeColor = System.Drawing.Color.White;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(300, 1000);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDangXuat.FlatAppearance.BorderSize = 0;
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnDangXuat.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDangXuat.Location = new System.Drawing.Point(0, 876);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(300, 49);
            this.btnDangXuat.TabIndex = 7;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // pnlUserBottom
            // 
            this.pnlUserBottom.Controls.Add(this.lblRole);
            this.pnlUserBottom.Controls.Add(this.lblUserName);
            this.pnlUserBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUserBottom.Location = new System.Drawing.Point(0, 925);
            this.pnlUserBottom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlUserBottom.Name = "pnlUserBottom";
            this.pnlUserBottom.Size = new System.Drawing.Size(300, 75);
            this.pnlUserBottom.TabIndex = 8;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRole.ForeColor = System.Drawing.Color.White;
            this.lblRole.Location = new System.Drawing.Point(18, 37);
            this.lblRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(98, 21);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "Quản Lý Kho";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(18, 12);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 25);
            this.lblUserName.TabIndex = 0;
            // 
            // btnQLTraHang
            // 
            this.btnQLTraHang.FlatAppearance.BorderSize = 0;
            this.btnQLTraHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLTraHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQLTraHang.ForeColor = System.Drawing.Color.White;
            this.btnQLTraHang.Location = new System.Drawing.Point(15, 500);
            this.btnQLTraHang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQLTraHang.Name = "btnQLTraHang";
            this.btnQLTraHang.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnQLTraHang.Size = new System.Drawing.Size(270, 54);
            this.btnQLTraHang.TabIndex = 6;
            this.btnQLTraHang.Text = "6. Quản lý trả hàng";
            this.btnQLTraHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLTraHang.UseVisualStyleBackColor = true;
            this.btnQLTraHang.Click += new System.EventHandler(this.NavButton_Click);
            // 
            // btnQLXuatKho
            // 
            this.btnQLXuatKho.FlatAppearance.BorderSize = 0;
            this.btnQLXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLXuatKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQLXuatKho.ForeColor = System.Drawing.Color.White;
            this.btnQLXuatKho.Location = new System.Drawing.Point(15, 437);
            this.btnQLXuatKho.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQLXuatKho.Name = "btnQLXuatKho";
            this.btnQLXuatKho.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnQLXuatKho.Size = new System.Drawing.Size(270, 54);
            this.btnQLXuatKho.TabIndex = 5;
            this.btnQLXuatKho.Text = "5. Quản lý xuất kho & lấy hàng";
            this.btnQLXuatKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLXuatKho.UseVisualStyleBackColor = true;
            this.btnQLXuatKho.Click += new System.EventHandler(this.NavButton_Click);
            // 
            // btnQLLoHang
            // 
            this.btnQLLoHang.FlatAppearance.BorderSize = 0;
            this.btnQLLoHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLLoHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQLLoHang.ForeColor = System.Drawing.Color.White;
            this.btnQLLoHang.Location = new System.Drawing.Point(15, 374);
            this.btnQLLoHang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQLLoHang.Name = "btnQLLoHang";
            this.btnQLLoHang.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnQLLoHang.Size = new System.Drawing.Size(270, 54);
            this.btnQLLoHang.TabIndex = 4;
            this.btnQLLoHang.Text = "4. Quản lý lô hàng & HSD";
            this.btnQLLoHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLLoHang.UseVisualStyleBackColor = true;
            this.btnQLLoHang.Click += new System.EventHandler(this.NavButton_Click);
            // 
            // btnQLTonKho
            // 
            this.btnQLTonKho.FlatAppearance.BorderSize = 0;
            this.btnQLTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLTonKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQLTonKho.ForeColor = System.Drawing.Color.White;
            this.btnQLTonKho.Location = new System.Drawing.Point(15, 311);
            this.btnQLTonKho.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQLTonKho.Name = "btnQLTonKho";
            this.btnQLTonKho.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnQLTonKho.Size = new System.Drawing.Size(270, 54);
            this.btnQLTonKho.TabIndex = 3;
            this.btnQLTonKho.Text = "3. Quản lý tồn kho";
            this.btnQLTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLTonKho.UseVisualStyleBackColor = true;
            this.btnQLTonKho.Click += new System.EventHandler(this.NavButton_Click);
            // 
            // btnQLLuutru
            // 
            this.btnQLLuutru.FlatAppearance.BorderSize = 0;
            this.btnQLLuutru.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLLuutru.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQLLuutru.ForeColor = System.Drawing.Color.White;
            this.btnQLLuutru.Location = new System.Drawing.Point(15, 248);
            this.btnQLLuutru.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQLLuutru.Name = "btnQLLuutru";
            this.btnQLLuutru.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnQLLuutru.Size = new System.Drawing.Size(270, 54);
            this.btnQLLuutru.TabIndex = 2;
            this.btnQLLuutru.Text = "2. Quản lý lưu trữ & vị trí";
            this.btnQLLuutru.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLLuutru.UseVisualStyleBackColor = true;
            this.btnQLLuutru.Click += new System.EventHandler(this.NavButton_Click);
            // 
            // btnQLNhapKho
            // 
            this.btnQLNhapKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnQLNhapKho.FlatAppearance.BorderSize = 0;
            this.btnQLNhapKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLNhapKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnQLNhapKho.ForeColor = System.Drawing.Color.White;
            this.btnQLNhapKho.Location = new System.Drawing.Point(15, 185);
            this.btnQLNhapKho.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQLNhapKho.Name = "btnQLNhapKho";
            this.btnQLNhapKho.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnQLNhapKho.Size = new System.Drawing.Size(270, 54);
            this.btnQLNhapKho.TabIndex = 1;
            this.btnQLNhapKho.Text = "1. Quản lý nhập kho";
            this.btnQLNhapKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQLNhapKho.UseVisualStyleBackColor = false;
            this.btnQLNhapKho.Click += new System.EventHandler(this.NavButton_Click);
            // 
            // picBrandLogo
            // 
            this.picBrandLogo.ErrorImage = global::ERPKho.Properties.Resources._2e4d8137e16961373878;
            this.picBrandLogo.Image = global::ERPKho.Properties.Resources._2e4d8137e16961373878;
            this.picBrandLogo.InitialImage = global::ERPKho.Properties.Resources._2e4d8137e16961373878;
            this.picBrandLogo.Location = new System.Drawing.Point(18, 15);
            this.picBrandLogo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picBrandLogo.Name = "picBrandLogo";
            this.picBrandLogo.Size = new System.Drawing.Size(262, 137);
            this.picBrandLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBrandLogo.TabIndex = 0;
            this.picBrandLogo.TabStop = false;
            // 
            // pnlTopHeader
            // 
            this.pnlTopHeader.BackColor = System.Drawing.Color.White;
            this.pnlTopHeader.Controls.Add(this.lblTopUser);
            this.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHeader.Location = new System.Drawing.Point(300, 0);
            this.pnlTopHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTopHeader.Name = "pnlTopHeader";
            this.pnlTopHeader.Size = new System.Drawing.Size(1200, 62);
            this.pnlTopHeader.TabIndex = 1;
            // 
            // lblTopUser
            // 
            this.lblTopUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTopUser.AutoSize = true;
            this.lblTopUser.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblTopUser.Location = new System.Drawing.Point(430, 15);
            this.lblTopUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTopUser.Name = "lblTopUser";
            this.lblTopUser.Size = new System.Drawing.Size(289, 28);
            this.lblTopUser.TabIndex = 0;
            this.lblTopUser.Text = "Hệ Thống ERP Kho ACECOOK";
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(300, 62);
            this.pnlMainContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1200, 938);
            this.pnlMainContent.TabIndex = 2;
            // 
            // FrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1500, 1000);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlTopHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Kho và Lô hàng - Acecook Việt Nam";
            this.Load += new System.EventHandler(this.FrMain_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlUserBottom.ResumeLayout(false);
            this.pnlUserBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBrandLogo)).EndInit();
            this.pnlTopHeader.ResumeLayout(false);
            this.pnlTopHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.PictureBox picBrandLogo;
        private System.Windows.Forms.Button btnQLNhapKho;
        private System.Windows.Forms.Button btnQLLuutru;
        private System.Windows.Forms.Button btnQLTonKho;
        private System.Windows.Forms.Button btnQLLoHang;
        private System.Windows.Forms.Button btnQLXuatKho;
        private System.Windows.Forms.Button btnQLTraHang;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Panel pnlUserBottom;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel pnlTopHeader;
        private System.Windows.Forms.Label lblTopUser;
        private System.Windows.Forms.Panel pnlMainContent;
    }
}
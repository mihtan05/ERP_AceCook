namespace ERP_Khach
{
    partial class LogInPhanHe
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
            this.lblThoiGian = new System.Windows.Forms.Label();

            // Bảng lưới chứa 6 ô vuông (2 hàng x 3 cột)
            this.tblGridContainer = new System.Windows.Forms.TableLayoutPanel();

            // 1. Sản xuất
            this.pnlSanXuat = new System.Windows.Forms.Panel();
            this.lblIconSX = new System.Windows.Forms.Label();
            this.lblTenSX = new System.Windows.Forms.Label();
            this.lblSubSX = new System.Windows.Forms.Label();

            // 2. Bán hàng
            this.pnlBanHang = new System.Windows.Forms.Panel();
            this.lblIconBH = new System.Windows.Forms.Label();
            this.lblTenBH = new System.Windows.Forms.Label();
            this.lblSubBH = new System.Windows.Forms.Label();

            // 3. Logistics
            this.pnlLogistics = new System.Windows.Forms.Panel();
            this.lblIconLG = new System.Windows.Forms.Label();
            this.lblTenLG = new System.Windows.Forms.Label();
            this.lblSubLG = new System.Windows.Forms.Label();

            // 4. Kho
            this.pnlKho = new System.Windows.Forms.Panel();
            this.lblIconKho = new System.Windows.Forms.Label();
            this.lblTenKho = new System.Windows.Forms.Label();
            this.lblSubKho = new System.Windows.Forms.Label();

            // 5. Nhân sự
            this.pnlNhanSu = new System.Windows.Forms.Panel();
            this.lblIconNS = new System.Windows.Forms.Label();
            this.lblTenNS = new System.Windows.Forms.Label();
            this.lblSubNS = new System.Windows.Forms.Label();

            // 6. Tài chính
            this.pnlTaiChinh = new System.Windows.Forms.Panel();
            this.lblIconTC = new System.Windows.Forms.Label();
            this.lblTenTC = new System.Windows.Forms.Label();
            this.lblSubTC = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.tblGridContainer.SuspendLayout();
            this.pnlSanXuat.SuspendLayout();
            this.pnlBanHang.SuspendLayout();
            this.pnlLogistics.SuspendLayout();
            this.pnlKho.SuspendLayout();
            this.pnlNhanSu.SuspendLayout();
            this.pnlTaiChinh.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblThoiGian);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 100);

            // lblTitle
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1200, 50);
            this.lblTitle.Text = "Chọn phân hệ để bắt đầu";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblThoiGian
            this.lblThoiGian.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblThoiGian.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblThoiGian.ForeColor = System.Drawing.Color.DarkGray;
            this.lblThoiGian.Location = new System.Drawing.Point(0, 70);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(1200, 25);
            this.lblThoiGian.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // tblGridContainer (Lưới 2 hàng x 3 cột)
            // 
            this.tblGridContainer.ColumnCount = 3;
            this.tblGridContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblGridContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblGridContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblGridContainer.RowCount = 2;
            this.tblGridContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGridContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));

            // Add các ô vào bảng
            this.tblGridContainer.Controls.Add(this.pnlSanXuat, 0, 0);
            this.tblGridContainer.Controls.Add(this.pnlBanHang, 1, 0);
            this.tblGridContainer.Controls.Add(this.pnlLogistics, 2, 0);
            this.tblGridContainer.Controls.Add(this.pnlKho, 0, 1);
            this.tblGridContainer.Controls.Add(this.pnlNhanSu, 1, 1);
            this.tblGridContainer.Controls.Add(this.pnlTaiChinh, 2, 1);

            this.tblGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGridContainer.Location = new System.Drawing.Point(0, 100);
            this.tblGridContainer.Name = "tblGridContainer";
            this.tblGridContainer.Padding = new System.Windows.Forms.Padding(40, 20, 40, 40);
            this.tblGridContainer.Size = new System.Drawing.Size(1200, 650);

            // ==========================================
            // 1. SẢN XUẤT (Cột 0, Hàng 0)
            // ==========================================
            this.pnlSanXuat.BackColor = System.Drawing.Color.FromArgb(255, 245, 246);
            this.pnlSanXuat.Controls.Add(this.lblIconSX);
            this.pnlSanXuat.Controls.Add(this.lblTenSX);
            this.pnlSanXuat.Controls.Add(this.lblSubSX);
            this.pnlSanXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlSanXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSanXuat.Margin = new System.Windows.Forms.Padding(15);
            this.pnlSanXuat.Name = "pnlSanXuat";
            this.pnlSanXuat.Click += new System.EventHandler(this.pnlSanXuat_Click);
            this.lblIconSX.Click += new System.EventHandler(this.pnlSanXuat_Click);
            this.lblTenSX.Click += new System.EventHandler(this.pnlSanXuat_Click);
            this.lblSubSX.Click += new System.EventHandler(this.pnlSanXuat_Click);

            this.lblIconSX.Font = new System.Drawing.Font("Segoe UI Emoji", 32F);
            this.lblIconSX.Location = new System.Drawing.Point(20, 30);
            this.lblIconSX.Size = new System.Drawing.Size(80, 70);
            this.lblIconSX.Text = "🏭";

            this.lblTenSX.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenSX.ForeColor = System.Drawing.Color.FromArgb(130, 30, 45);
            this.lblTenSX.Location = new System.Drawing.Point(20, 110);
            this.lblTenSX.Size = new System.Drawing.Size(250, 38);
            this.lblTenSX.Text = "Sản xuất";

            this.lblSubSX.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubSX.ForeColor = System.Drawing.Color.Gray;
            this.lblSubSX.Location = new System.Drawing.Point(22, 150);
            this.lblSubSX.Size = new System.Drawing.Size(250, 30);
            this.lblSubSX.Text = "Manufacturing";

            // ==========================================
            // 2. BÁN HÀNG (Cột 1, Hàng 0)
            // ==========================================
            this.pnlBanHang.BackColor = System.Drawing.Color.FromArgb(255, 252, 240);
            this.pnlBanHang.Controls.Add(this.lblIconBH);
            this.pnlBanHang.Controls.Add(this.lblTenBH);
            this.pnlBanHang.Controls.Add(this.lblSubBH);
            this.pnlBanHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlBanHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBanHang.Margin = new System.Windows.Forms.Padding(15);
            this.pnlBanHang.Name = "pnlBanHang";
            this.pnlBanHang.Click += new System.EventHandler(this.pnlBanHang_Click);
            this.lblIconBH.Click += new System.EventHandler(this.pnlBanHang_Click);
            this.lblTenBH.Click += new System.EventHandler(this.pnlBanHang_Click);
            this.lblSubBH.Click += new System.EventHandler(this.pnlBanHang_Click);

            this.lblIconBH.Font = new System.Drawing.Font("Segoe UI Emoji", 32F);
            this.lblIconBH.Location = new System.Drawing.Point(20, 30);
            this.lblIconBH.Size = new System.Drawing.Size(80, 70);
            this.lblIconBH.Text = "📦";

            this.lblTenBH.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenBH.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblTenBH.Location = new System.Drawing.Point(20, 110);
            this.lblTenBH.Size = new System.Drawing.Size(250, 38);
            this.lblTenBH.Text = "Bán hàng";

            this.lblSubBH.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubBH.ForeColor = System.Drawing.Color.Gray;
            this.lblSubBH.Location = new System.Drawing.Point(22, 150);
            this.lblSubBH.Size = new System.Drawing.Size(250, 30);
            this.lblSubBH.Text = "Sales";

            // ==========================================
            // 3. LOGISTICS (Cột 2, Hàng 0)
            // ==========================================
            this.pnlLogistics.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.pnlLogistics.Controls.Add(this.lblIconLG);
            this.pnlLogistics.Controls.Add(this.lblTenLG);
            this.pnlLogistics.Controls.Add(this.lblSubLG);
            this.pnlLogistics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlLogistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogistics.Margin = new System.Windows.Forms.Padding(15);
            this.pnlLogistics.Name = "pnlLogistics";
            this.pnlLogistics.Click += new System.EventHandler(this.pnlLogistics_Click);
            this.lblIconLG.Click += new System.EventHandler(this.pnlLogistics_Click);
            this.lblTenLG.Click += new System.EventHandler(this.pnlLogistics_Click);
            this.lblSubLG.Click += new System.EventHandler(this.pnlLogistics_Click);

            this.lblIconLG.Font = new System.Drawing.Font("Segoe UI Emoji", 32F);
            this.lblIconLG.Location = new System.Drawing.Point(20, 30);
            this.lblIconLG.Size = new System.Drawing.Size(80, 70);
            this.lblIconLG.Text = "🚚";

            this.lblTenLG.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenLG.ForeColor = System.Drawing.Color.FromArgb(20, 60, 110);
            this.lblTenLG.Location = new System.Drawing.Point(20, 110);
            this.lblTenLG.Size = new System.Drawing.Size(250, 38);
            this.lblTenLG.Text = "Logistics";

            this.lblSubLG.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubLG.ForeColor = System.Drawing.Color.Gray;
            this.lblSubLG.Location = new System.Drawing.Point(22, 150);
            this.lblSubLG.Size = new System.Drawing.Size(250, 30);
            this.lblSubLG.Text = "Logistics";

            // ==========================================
            // 4. KHO (Cột 0, Hàng 1)
            // ==========================================
            this.pnlKho.BackColor = System.Drawing.Color.FromArgb(242, 252, 248);
            this.pnlKho.Controls.Add(this.lblIconKho);
            this.pnlKho.Controls.Add(this.lblTenKho);
            this.pnlKho.Controls.Add(this.lblSubKho);
            this.pnlKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKho.Margin = new System.Windows.Forms.Padding(15);
            this.pnlKho.Name = "pnlKho";
            this.pnlKho.Click += new System.EventHandler(this.pnlKho_Click);
            this.lblIconKho.Click += new System.EventHandler(this.pnlKho_Click);
            this.lblTenKho.Click += new System.EventHandler(this.pnlKho_Click);
            this.lblSubKho.Click += new System.EventHandler(this.pnlKho_Click);

            this.lblIconKho.Font = new System.Drawing.Font("Segoe UI Emoji", 32F);
            this.lblIconKho.Location = new System.Drawing.Point(20, 30);
            this.lblIconKho.Size = new System.Drawing.Size(80, 70);
            this.lblIconKho.Text = "🏬";

            this.lblTenKho.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenKho.ForeColor = System.Drawing.Color.FromArgb(20, 80, 50);
            this.lblTenKho.Location = new System.Drawing.Point(20, 110);
            this.lblTenKho.Size = new System.Drawing.Size(250, 38);
            this.lblTenKho.Text = "Kho";

            this.lblSubKho.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubKho.ForeColor = System.Drawing.Color.Gray;
            this.lblSubKho.Location = new System.Drawing.Point(22, 150);
            this.lblSubKho.Size = new System.Drawing.Size(250, 30);
            this.lblSubKho.Text = "Warehouse";

            // ==========================================
            // 5. NHÂN SỰ (Cột 1, Hàng 1)
            // ==========================================
            this.pnlNhanSu.BackColor = System.Drawing.Color.FromArgb(250, 245, 255);
            this.pnlNhanSu.Controls.Add(this.lblIconNS);
            this.pnlNhanSu.Controls.Add(this.lblTenNS);
            this.pnlNhanSu.Controls.Add(this.lblSubNS);
            this.pnlNhanSu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlNhanSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNhanSu.Margin = new System.Windows.Forms.Padding(15);
            this.pnlNhanSu.Name = "pnlNhanSu";
            this.pnlNhanSu.Click += new System.EventHandler(this.pnlNhanSu_Click);
            this.lblIconNS.Click += new System.EventHandler(this.pnlNhanSu_Click);
            this.lblTenNS.Click += new System.EventHandler(this.pnlNhanSu_Click);
            this.lblSubNS.Click += new System.EventHandler(this.pnlNhanSu_Click);

            this.lblIconNS.Font = new System.Drawing.Font("Segoe UI Emoji", 32F);
            this.lblIconNS.Location = new System.Drawing.Point(20, 30);
            this.lblIconNS.Size = new System.Drawing.Size(80, 70);
            this.lblIconNS.Text = "👥";

            this.lblTenNS.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenNS.ForeColor = System.Drawing.Color.FromArgb(60, 30, 100);
            this.lblTenNS.Location = new System.Drawing.Point(20, 110);
            this.lblTenNS.Size = new System.Drawing.Size(250, 38);
            this.lblTenNS.Text = "Nhân sự";

            this.lblSubNS.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubNS.ForeColor = System.Drawing.Color.Gray;
            this.lblSubNS.Location = new System.Drawing.Point(22, 150);
            this.lblSubNS.Size = new System.Drawing.Size(250, 30);
            this.lblSubNS.Text = "Human Resources";

            // ==========================================
            // 6. TÀI CHÍNH (Cột 2, Hàng 1)
            // ==========================================
            this.pnlTaiChinh.BackColor = System.Drawing.Color.FromArgb(255, 253, 245);
            this.pnlTaiChinh.Controls.Add(this.lblIconTC);
            this.pnlTaiChinh.Controls.Add(this.lblTenTC);
            this.pnlTaiChinh.Controls.Add(this.lblSubTC);
            this.pnlTaiChinh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlTaiChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTaiChinh.Margin = new System.Windows.Forms.Padding(15);
            this.pnlTaiChinh.Name = "pnlTaiChinh";
            this.pnlTaiChinh.Click += new System.EventHandler(this.pnlTaiChinh_Click);
            this.lblIconTC.Click += new System.EventHandler(this.pnlTaiChinh_Click);
            this.lblTenTC.Click += new System.EventHandler(this.pnlTaiChinh_Click);
            this.lblSubTC.Click += new System.EventHandler(this.pnlTaiChinh_Click);

            this.lblIconTC.Font = new System.Drawing.Font("Segoe UI Emoji", 32F);
            this.lblIconTC.Location = new System.Drawing.Point(20, 30);
            this.lblIconTC.Size = new System.Drawing.Size(80, 70);
            this.lblIconTC.Text = "💰";

            this.lblTenTC.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenTC.ForeColor = System.Drawing.Color.FromArgb(80, 50, 20);
            this.lblTenTC.Location = new System.Drawing.Point(20, 110);
            this.lblTenTC.Size = new System.Drawing.Size(250, 38);
            this.lblTenTC.Text = "Tài chính";

            this.lblSubTC.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubTC.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTC.Location = new System.Drawing.Point(22, 150);
            this.lblSubTC.Size = new System.Drawing.Size(250, 30);
            this.lblSubTC.Text = "Finance";

            // 
            // LogInPhanHe Form Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.tblGridContainer);
            this.Controls.Add(this.pnlHeader);
            this.Name = "LogInPhanHe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP Acecook - Chọn phân hệ";
            this.Load += new System.EventHandler(this.LogInPhanHe_Load);
            this.pnlHeader.ResumeLayout(false);
            this.tblGridContainer.ResumeLayout(false);
            this.pnlSanXuat.ResumeLayout(false);
            this.pnlBanHang.ResumeLayout(false);
            this.pnlLogistics.ResumeLayout(false);
            this.pnlKho.ResumeLayout(false);
            this.pnlNhanSu.ResumeLayout(false);
            this.pnlTaiChinh.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.TableLayoutPanel tblGridContainer;

        private System.Windows.Forms.Panel pnlSanXuat;
        private System.Windows.Forms.Label lblIconSX;
        private System.Windows.Forms.Label lblTenSX;
        private System.Windows.Forms.Label lblSubSX;

        private System.Windows.Forms.Panel pnlBanHang;
        private System.Windows.Forms.Label lblIconBH;
        private System.Windows.Forms.Label lblTenBH;
        private System.Windows.Forms.Label lblSubBH;

        private System.Windows.Forms.Panel pnlLogistics;
        private System.Windows.Forms.Label lblIconLG;
        private System.Windows.Forms.Label lblTenLG;
        private System.Windows.Forms.Label lblSubLG;

        private System.Windows.Forms.Panel pnlKho;
        private System.Windows.Forms.Label lblIconKho;
        private System.Windows.Forms.Label lblTenKho;
        private System.Windows.Forms.Label lblSubKho;

        private System.Windows.Forms.Panel pnlNhanSu;
        private System.Windows.Forms.Label lblIconNS;
        private System.Windows.Forms.Label lblTenNS;
        private System.Windows.Forms.Label lblSubNS;

        private System.Windows.Forms.Panel pnlTaiChinh;
        private System.Windows.Forms.Label lblIconTC;
        private System.Windows.Forms.Label lblTenTC;
        private System.Windows.Forms.Label lblSubTC;
    }
}
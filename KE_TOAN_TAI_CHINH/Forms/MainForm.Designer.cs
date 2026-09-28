using System.Configuration;

namespace KeToanTaiChinh.Forms
{
    partial class MainForm
    {

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.TableLayoutPanel sidebarLayout;
        private System.Windows.Forms.Panel pnlLogoArea;
        private System.Windows.Forms.Panel pnlLogoBox;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.FlowLayoutPanel flpNavigation;
        private System.Windows.Forms.Button btnTongQuan;
        private System.Windows.Forms.Button btnQuanLyThu;
        private System.Windows.Forms.Button btnQuanLyChi;
        private System.Windows.Forms.Button btnCongNo;
        private System.Windows.Forms.Button btnDoiChieu;
        private System.Windows.Forms.Button btnBangLuong;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblContentPlaceholder;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.sidebarLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLogoArea = new System.Windows.Forms.Panel();
            this.pnlLogoBox = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.flpNavigation = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTongQuan = new System.Windows.Forms.Button();
            this.btnQuanLyThu = new System.Windows.Forms.Button();
            this.btnQuanLyChi = new System.Windows.Forms.Button();
            this.btnCongNo = new System.Windows.Forms.Button();
            this.btnDoiChieu = new System.Windows.Forms.Button();
            this.btnBangLuong = new System.Windows.Forms.Button();
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblContentPlaceholder = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.sidebarLayout.SuspendLayout();
            this.pnlLogoArea.SuspendLayout();
            this.pnlLogoBox.SuspendLayout();
            this.flpNavigation.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlSidebar
            //
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.pnlSidebar.Controls.Add(this.sidebarLayout);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(230, 720);
            this.pnlSidebar.TabIndex = 0;
            //
            // sidebarLayout
            //
            this.sidebarLayout.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.sidebarLayout.ColumnCount = 1;
            this.sidebarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sidebarLayout.Controls.Add(this.pnlLogoArea, 0, 0);
            this.sidebarLayout.Controls.Add(this.flpNavigation, 0, 1);
            this.sidebarLayout.Controls.Add(this.btnThoat, 0, 2);
            this.sidebarLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebarLayout.Location = new System.Drawing.Point(0, 0);
            this.sidebarLayout.Margin = new System.Windows.Forms.Padding(0);
            this.sidebarLayout.Name = "sidebarLayout";
            this.sidebarLayout.RowCount = 3;
            this.sidebarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.sidebarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sidebarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.sidebarLayout.Size = new System.Drawing.Size(230, 720);
            this.sidebarLayout.TabIndex = 0;
            //
            // pnlLogoArea
            //
            this.pnlLogoArea.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.pnlLogoArea.Controls.Add(this.pnlLogoBox);
            this.pnlLogoArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogoArea.Location = new System.Drawing.Point(0, 0);
            this.pnlLogoArea.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogoArea.Name = "pnlLogoArea";
            this.pnlLogoArea.Size = new System.Drawing.Size(230, 112);
            this.pnlLogoArea.TabIndex = 0;
            //
            // pnlLogoBox
            //
            this.pnlLogoBox.BackColor = System.Drawing.Color.White;
            this.pnlLogoBox.Controls.Add(this.lblLogo);
            this.pnlLogoBox.Location = new System.Drawing.Point(55, 20);
            this.pnlLogoBox.Name = "pnlLogoBox";
            this.pnlLogoBox.Size = new System.Drawing.Size(120, 64);
            this.pnlLogoBox.TabIndex = 0;
            //
            // lblLogo
            //
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(194, 0, 37);
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(120, 64);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "ERP";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // flpNavigation
            //
            this.flpNavigation.AutoScroll = true;
            this.flpNavigation.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.flpNavigation.Controls.Add(this.btnTongQuan);
            this.flpNavigation.Controls.Add(this.btnQuanLyThu);
            this.flpNavigation.Controls.Add(this.btnQuanLyChi);
            this.flpNavigation.Controls.Add(this.btnCongNo);
            this.flpNavigation.Controls.Add(this.btnDoiChieu);
            this.flpNavigation.Controls.Add(this.btnBangLuong);
            this.flpNavigation.Controls.Add(this.btnDanhMuc);
            this.flpNavigation.Controls.Add(this.btnBaoCao);
            this.flpNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpNavigation.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpNavigation.Location = new System.Drawing.Point(0, 112);
            this.flpNavigation.Margin = new System.Windows.Forms.Padding(0);
            this.flpNavigation.Name = "flpNavigation";
            this.flpNavigation.Size = new System.Drawing.Size(230, 560);
            this.flpNavigation.TabIndex = 1;
            this.flpNavigation.WrapContents = false;
            //
            // btnTongQuan
            //
            this.btnTongQuan.BackColor = System.Drawing.Color.FromArgb(22, 119, 238);
            this.btnTongQuan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTongQuan.FlatAppearance.BorderSize = 0;
            this.btnTongQuan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(205, 0, 43);
            this.btnTongQuan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTongQuan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTongQuan.ForeColor = System.Drawing.Color.White;
            this.btnTongQuan.Location = new System.Drawing.Point(0, 0);
            this.btnTongQuan.Margin = new System.Windows.Forms.Padding(0);
            this.btnTongQuan.Name = "btnTongQuan";
            this.btnTongQuan.Size = new System.Drawing.Size(230, 46);
            this.btnTongQuan.TabIndex = 0;
            this.btnTongQuan.Text = "  Tổng quan";
            this.btnTongQuan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTongQuan.UseVisualStyleBackColor = false;
            this.btnTongQuan.Click += new System.EventHandler(this.btnTongQuan_Click);
            //
            // btnQuanLyThu
            //
            this.btnQuanLyThu.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.btnQuanLyThu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuanLyThu.FlatAppearance.BorderSize = 0;
            this.btnQuanLyThu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(205, 0, 43);
            this.btnQuanLyThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyThu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnQuanLyThu.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyThu.Location = new System.Drawing.Point(0, 46);
            this.btnQuanLyThu.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuanLyThu.Name = "btnQuanLyThu";
            this.btnQuanLyThu.Size = new System.Drawing.Size(230, 46);
            this.btnQuanLyThu.TabIndex = 1;
            this.btnQuanLyThu.Text = "  Quản lý thu";
            this.btnQuanLyThu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuanLyThu.UseVisualStyleBackColor = false;
            this.btnQuanLyThu.Click += new System.EventHandler(this.btnQuanLyThu_Click);
            //
            // btnQuanLyChi
            //
            this.btnQuanLyChi.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.btnQuanLyChi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuanLyChi.FlatAppearance.BorderSize = 0;
            this.btnQuanLyChi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(205, 0, 43);
            this.btnQuanLyChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnQuanLyChi.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyChi.Location = new System.Drawing.Point(0, 92);
            this.btnQuanLyChi.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuanLyChi.Name = "btnQuanLyChi";
            this.btnQuanLyChi.Size = new System.Drawing.Size(230, 46);
            this.btnQuanLyChi.TabIndex = 2;
            this.btnQuanLyChi.Text = "  Quản lý chi";
            this.btnQuanLyChi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuanLyChi.UseVisualStyleBackColor = false;
            this.btnQuanLyChi.Click += new System.EventHandler(this.btnQuanLyChi_Click);
            //
            // btnCongNo
            //
            this.btnCongNo.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.btnCongNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCongNo.FlatAppearance.BorderSize = 0;
            this.btnCongNo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(205, 0, 43);
            this.btnCongNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCongNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCongNo.ForeColor = System.Drawing.Color.White;
            this.btnCongNo.Location = new System.Drawing.Point(0, 138);
            this.btnCongNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnCongNo.Name = "btnCongNo";
            this.btnCongNo.Size = new System.Drawing.Size(230, 46);
            this.btnCongNo.TabIndex = 3;
            this.btnCongNo.Text = "  Quản lý công nợ";
            this.btnCongNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCongNo.UseVisualStyleBackColor = false;
            this.btnCongNo.Click += new System.EventHandler(this.btnCongNo_Click);
            //
            // btnDoiChieu
            //
            this.btnDoiChieu.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.btnDoiChieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoiChieu.FlatAppearance.BorderSize = 0;
            this.btnDoiChieu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(205, 0, 43);
            this.btnDoiChieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiChieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDoiChieu.ForeColor = System.Drawing.Color.White;
            this.btnDoiChieu.Location = new System.Drawing.Point(0, 184);
            this.btnDoiChieu.Margin = new System.Windows.Forms.Padding(0);
            this.btnDoiChieu.Name = "btnDoiChieu";
            this.btnDoiChieu.Size = new System.Drawing.Size(230, 46);
            this.btnDoiChieu.TabIndex = 4;
            this.btnDoiChieu.Text = "  Đối chiếu công nợ";
            this.btnDoiChieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDoiChieu.UseVisualStyleBackColor = false;
            this.btnDoiChieu.Click += new System.EventHandler(this.btnDoiChieu_Click);
            //
            // btnBangLuong
            //
            this.btnBangLuong.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.btnBangLuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBangLuong.FlatAppearance.BorderSize = 0;
            this.btnBangLuong.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(205, 0, 43);
            this.btnBangLuong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBangLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBangLuong.ForeColor = System.Drawing.Color.White;
            this.btnBangLuong.Location = new System.Drawing.Point(0, 230);
            this.btnBangLuong.Margin = new System.Windows.Forms.Padding(0);
            this.btnBangLuong.Name = "btnBangLuong";
            this.btnBangLuong.Size = new System.Drawing.Size(230, 46);
            this.btnBangLuong.TabIndex = 5;
            this.btnBangLuong.Text = "  Bảng lương";
            this.btnBangLuong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBangLuong.UseVisualStyleBackColor = false;
            this.btnBangLuong.Click += new System.EventHandler(this.btnBangLuong_Click);
            //
            // btnDanhMuc
            //
            this.btnDanhMuc.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.btnDanhMuc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDanhMuc.FlatAppearance.BorderSize = 0;
            this.btnDanhMuc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(205, 0, 43);
            this.btnDanhMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDanhMuc.ForeColor = System.Drawing.Color.White;
            this.btnDanhMuc.Location = new System.Drawing.Point(0, 276);
            this.btnDanhMuc.Margin = new System.Windows.Forms.Padding(0);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(230, 46);
            this.btnDanhMuc.TabIndex = 6;
            this.btnDanhMuc.Text = "  Danh mục";
            this.btnDanhMuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDanhMuc.UseVisualStyleBackColor = false;
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);
            //
            // btnBaoCao
            //
            this.btnBaoCao.BackColor = System.Drawing.Color.FromArgb(235, 4, 55);
            this.btnBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBaoCao.FlatAppearance.BorderSize = 0;
            this.btnBaoCao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(205, 0, 43);
            this.btnBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnBaoCao.Location = new System.Drawing.Point(0, 322);
            this.btnBaoCao.Margin = new System.Windows.Forms.Padding(0);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(230, 46);
            this.btnBaoCao.TabIndex = 7;
            this.btnBaoCao.Text = "  Báo cáo";
            this.btnBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCao.UseVisualStyleBackColor = false;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            //
            // btnThoat
            //
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.btnThoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(0, 672);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(0);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(230, 48);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "Thoát ứng dụng";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblPageTitle);
            this.pnlHeader.Controls.Add(this.lblUser);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(230, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(970, 68);
            this.pnlHeader.TabIndex = 1;
            //
            // lblPageTitle
            //
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.Location = new System.Drawing.Point(25, 20);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(103, 25);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Tổng quan";
            //
            // lblUser
            //
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.lblUser.Location = new System.Drawing.Point(630, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Padding = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.lblUser.Size = new System.Drawing.Size(340, 68);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "Người dùng";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            this.pnlContent.Controls.Add(this.lblContentPlaceholder);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(230, 68);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(22, 14, 22, 18);
            this.pnlContent.Size = new System.Drawing.Size(970, 652);
            this.pnlContent.TabIndex = 2;
            //
            // lblContentPlaceholder
            //
            this.lblContentPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContentPlaceholder.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Italic);
            this.lblContentPlaceholder.ForeColor = System.Drawing.Color.Gray;
            this.lblContentPlaceholder.Location = new System.Drawing.Point(22, 14);
            this.lblContentPlaceholder.Name = "lblContentPlaceholder";
            this.lblContentPlaceholder.Size = new System.Drawing.Size(926, 620);
            this.lblContentPlaceholder.TabIndex = 0;
            this.lblContentPlaceholder.Text = "Khu vực nội dung - các màn hình chức năng sẽ được nạp tại đây";
            this.lblContentPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1216, 759);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP Bán Hàng - Kế toán tài chính";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlSidebar.ResumeLayout(false);
            this.sidebarLayout.ResumeLayout(false);
            this.pnlLogoArea.ResumeLayout(false);
            this.pnlLogoBox.ResumeLayout(false);
            this.flpNavigation.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

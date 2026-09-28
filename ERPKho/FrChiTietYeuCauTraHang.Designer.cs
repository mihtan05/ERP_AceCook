namespace ERPKho
{
    partial class FrChiTietYeuCauTraHang
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
            this.grpThongTinChung = new System.Windows.Forms.GroupBox();
            this.lblTrangThaiVal = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblNgayGuiVal = new System.Windows.Forms.Label();
            this.lblNgayGui = new System.Windows.Forms.Label();
            this.lblNguoiGuiVal = new System.Windows.Forms.Label();
            this.lblNguoiGui = new System.Windows.Forms.Label();
            this.lblBoPhanVal = new System.Windows.Forms.Label();
            this.lblBoPhan = new System.Windows.Forms.Label();
            this.lblMaYCVal = new System.Windows.Forms.Label();
            this.lblMaYC = new System.Windows.Forms.Label();
            this.grpChiTietMatHang = new System.Windows.Forms.GroupBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblMoTaChiTiet = new System.Windows.Forms.Label();
            this.txtMoTaGhiChu = new System.Windows.Forms.TextBox();
            this.btnDong = new System.Windows.Forms.Button();
            this.grpThongTinChung.SuspendLayout();
            this.grpChiTietMatHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(315, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHI TIẾT ĐƠN YÊU CẦU TRẢ HÀNG";
            // 
            // grpThongTinChung
            // 
            this.grpThongTinChung.Controls.Add(this.lblTrangThaiVal);
            this.grpThongTinChung.Controls.Add(this.lblTrangThai);
            this.grpThongTinChung.Controls.Add(this.lblNgayGuiVal);
            this.grpThongTinChung.Controls.Add(this.lblNgayGui);
            this.grpThongTinChung.Controls.Add(this.lblNguoiGuiVal);
            this.grpThongTinChung.Controls.Add(this.lblNguoiGui);
            this.grpThongTinChung.Controls.Add(this.lblBoPhanVal);
            this.grpThongTinChung.Controls.Add(this.lblBoPhan);
            this.grpThongTinChung.Controls.Add(this.lblMaYCVal);
            this.grpThongTinChung.Controls.Add(this.lblMaYC);
            this.grpThongTinChung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpThongTinChung.Location = new System.Drawing.Point(18, 45);
            this.grpThongTinChung.Name = "grpThongTinChung";
            this.grpThongTinChung.Size = new System.Drawing.Size(548, 95);
            this.grpThongTinChung.TabIndex = 1;
            this.grpThongTinChung.TabStop = false;
            this.grpThongTinChung.Text = "Thông Tin Chung";
            // 
            // lblTrangThaiVal
            // 
            this.lblTrangThaiVal.AutoSize = true;
            this.lblTrangThaiVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiVal.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTrangThaiVal.Location = new System.Drawing.Point(365, 45);
            this.lblTrangThaiVal.Name = "lblTrangThaiVal";
            this.lblTrangThaiVal.Size = new System.Drawing.Size(27, 15);
            this.lblTrangThaiVal.TabIndex = 9;
            this.lblTrangThaiVal.Text = "N/A";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblTrangThai.ForeColor = System.Drawing.Color.Gray;
            this.lblTrangThai.Location = new System.Drawing.Point(295, 45);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(62, 15);
            this.lblTrangThai.TabIndex = 8;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // lblNgayGuiVal
            // 
            this.lblNgayGuiVal.AutoSize = true;
            this.lblNgayGuiVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblNgayGuiVal.Location = new System.Drawing.Point(365, 22);
            this.lblNgayGuiVal.Name = "lblNgayGuiVal";
            this.lblNgayGuiVal.Size = new System.Drawing.Size(27, 15);
            this.lblNgayGuiVal.TabIndex = 7;
            this.lblNgayGuiVal.Text = "N/A";
            // 
            // lblNgayGui
            // 
            this.lblNgayGui.AutoSize = true;
            this.lblNgayGui.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblNgayGui.ForeColor = System.Drawing.Color.Gray;
            this.lblNgayGui.Location = new System.Drawing.Point(295, 22);
            this.lblNgayGui.Name = "lblNgayGui";
            this.lblNgayGui.Size = new System.Drawing.Size(58, 15);
            this.lblNgayGui.TabIndex = 6;
            this.lblNgayGui.Text = "Ngày gửi:";
            // 
            // lblNguoiGuiVal
            // 
            this.lblNguoiGuiVal.AutoSize = true;
            this.lblNguoiGuiVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblNguoiGuiVal.Location = new System.Drawing.Point(95, 68);
            this.lblNguoiGuiVal.Name = "lblNguoiGuiVal";
            this.lblNguoiGuiVal.Size = new System.Drawing.Size(27, 15);
            this.lblNguoiGuiVal.TabIndex = 5;
            this.lblNguoiGuiVal.Text = "N/A";
            // 
            // lblNguoiGui
            // 
            this.lblNguoiGui.AutoSize = true;
            this.lblNguoiGui.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblNguoiGui.ForeColor = System.Drawing.Color.Gray;
            this.lblNguoiGui.Location = new System.Drawing.Point(15, 68);
            this.lblNguoiGui.Name = "lblNguoiGui";
            this.lblNguoiGui.Size = new System.Drawing.Size(63, 15);
            this.lblNguoiGui.TabIndex = 4;
            this.lblNguoiGui.Text = "Người gửi:";
            // 
            // lblBoPhanVal
            // 
            this.lblBoPhanVal.AutoSize = true;
            this.lblBoPhanVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblBoPhanVal.Location = new System.Drawing.Point(95, 45);
            this.lblBoPhanVal.Name = "lblBoPhanVal";
            this.lblBoPhanVal.Size = new System.Drawing.Size(27, 15);
            this.lblBoPhanVal.TabIndex = 3;
            this.lblBoPhanVal.Text = "N/A";
            // 
            // lblBoPhan
            // 
            this.lblBoPhan.AutoSize = true;
            this.lblBoPhan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblBoPhan.ForeColor = System.Drawing.Color.Gray;
            this.lblBoPhan.Location = new System.Drawing.Point(15, 45);
            this.lblBoPhan.Name = "lblBoPhan";
            this.lblBoPhan.Size = new System.Drawing.Size(54, 15);
            this.lblBoPhan.TabIndex = 2;
            this.lblBoPhan.Text = "Bộ phận:";
            // 
            // lblMaYCVal
            // 
            this.lblMaYCVal.AutoSize = true;
            this.lblMaYCVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaYCVal.Location = new System.Drawing.Point(95, 22);
            this.lblMaYCVal.Name = "lblMaYCVal";
            this.lblMaYCVal.Size = new System.Drawing.Size(27, 15);
            this.lblMaYCVal.TabIndex = 1;
            this.lblMaYCVal.Text = "N/A";
            // 
            // lblMaYC
            // 
            this.lblMaYC.AutoSize = true;
            this.lblMaYC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblMaYC.ForeColor = System.Drawing.Color.Gray;
            this.lblMaYC.Location = new System.Drawing.Point(15, 22);
            this.lblMaYC.Name = "lblMaYC";
            this.lblMaYC.Size = new System.Drawing.Size(71, 15);
            this.lblMaYC.TabIndex = 0;
            this.lblMaYC.Text = "Mã yêu cầu:";
            // 
            // grpChiTietMatHang
            // 
            this.grpChiTietMatHang.Controls.Add(this.dgvChiTiet);
            this.grpChiTietMatHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpChiTietMatHang.Location = new System.Drawing.Point(18, 148);
            this.grpChiTietMatHang.Name = "grpChiTietMatHang";
            this.grpChiTietMatHang.Padding = new System.Windows.Forms.Padding(8);
            this.grpChiTietMatHang.Size = new System.Drawing.Size(548, 175);
            this.grpChiTietMatHang.TabIndex = 2;
            this.grpChiTietMatHang.TabStop = false;
            this.grpChiTietMatHang.Text = "Danh Sách Mặt Hàng Trả / Tiêu Hủy";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dgvChiTiet.Location = new System.Drawing.Point(8, 24);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(532, 143);
            this.dgvChiTiet.TabIndex = 0;
            // 
            // lblMoTaChiTiet
            // 
            this.lblMoTaChiTiet.AutoSize = true;
            this.lblMoTaChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMoTaChiTiet.Location = new System.Drawing.Point(18, 332);
            this.lblMoTaChiTiet.Name = "lblMoTaChiTiet";
            this.lblMoTaChiTiet.Size = new System.Drawing.Size(126, 15);
            this.lblMoTaChiTiet.TabIndex = 3;
            this.lblMoTaChiTiet.Text = "Mô tả / Ghi chú chi tiết:";
            // 
            // txtMoTaGhiChu
            // 
            this.txtMoTaGhiChu.BackColor = System.Drawing.Color.White;
            this.txtMoTaGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMoTaGhiChu.Location = new System.Drawing.Point(18, 350);
            this.txtMoTaGhiChu.Multiline = true;
            this.txtMoTaGhiChu.Name = "txtMoTaGhiChu";
            this.txtMoTaGhiChu.ReadOnly = true;
            this.txtMoTaGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTaGhiChu.Size = new System.Drawing.Size(548, 65);
            this.txtMoTaGhiChu.TabIndex = 4;
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(466, 425);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 32);
            this.btnDong.TabIndex = 5;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrChiTietYeuCauTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 468);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.txtMoTaGhiChu);
            this.Controls.Add(this.lblMoTaChiTiet);
            this.Controls.Add(this.grpChiTietMatHang);
            this.Controls.Add(this.grpThongTinChung);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrChiTietYeuCauTraHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi Tiết Yêu Cầu Trả Hàng";
            this.Load += new System.EventHandler(this.FrChiTietYeuCauTraHang_Load);
            this.grpThongTinChung.ResumeLayout(false);
            this.grpThongTinChung.PerformLayout();
            this.grpChiTietMatHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpThongTinChung;
        private System.Windows.Forms.Label lblTrangThaiVal;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblNgayGuiVal;
        private System.Windows.Forms.Label lblNgayGui;
        private System.Windows.Forms.Label lblNguoiGuiVal;
        private System.Windows.Forms.Label lblNguoiGui;
        private System.Windows.Forms.Label lblBoPhanVal;
        private System.Windows.Forms.Label lblBoPhan;
        private System.Windows.Forms.Label lblMaYCVal;
        private System.Windows.Forms.Label lblMaYC;
        private System.Windows.Forms.GroupBox grpChiTietMatHang;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label lblMoTaChiTiet;
        private System.Windows.Forms.TextBox txtMoTaGhiChu;
        private System.Windows.Forms.Button btnDong;
    }
}
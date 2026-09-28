namespace ERPKho
{
    partial class FrQLXuatkhovaLayhang
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
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.dgvXuatKho = new System.Windows.Forms.DataGridView();
            this.pnlActionTool = new System.Windows.Forms.Panel();
            this.btnXacNhanXuat = new System.Windows.Forms.Button();
            this.btnLapDSLayHang = new System.Windows.Forms.Button();
            this.btnDuyetPhieu = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.cboFilterTrangThai = new System.Windows.Forms.ComboBox();
            this.cboFilterLoaiXuat = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnTaoPhieuXuat = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMainContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXuatKho)).BeginInit();
            this.pnlActionTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Controls.Add(this.dgvXuatKho);
            this.pnlMainContent.Controls.Add(this.pnlActionTool);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMainContent.Size = new System.Drawing.Size(980, 600);
            this.pnlMainContent.TabIndex = 0;
            this.pnlMainContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMainContent_Paint);
            // 
            // dgvXuatKho
            // 
            this.dgvXuatKho.AllowUserToAddRows = false;
            this.dgvXuatKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvXuatKho.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvXuatKho.ColumnHeadersHeight = 38;
            this.dgvXuatKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvXuatKho.Location = new System.Drawing.Point(15, 125);
            this.dgvXuatKho.Name = "dgvXuatKho";
            this.dgvXuatKho.ReadOnly = true;
            this.dgvXuatKho.RowHeadersVisible = false;
            this.dgvXuatKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvXuatKho.Size = new System.Drawing.Size(950, 460);
            this.dgvXuatKho.TabIndex = 1;
            this.dgvXuatKho.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvXuatKho_CellDoubleClick);
            this.dgvXuatKho.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvXuatKho_CellFormatting);
            // 
            // pnlActionTool
            // 
            this.pnlActionTool.Controls.Add(this.btnXacNhanXuat);
            this.pnlActionTool.Controls.Add(this.btnLapDSLayHang);
            this.pnlActionTool.Controls.Add(this.btnDuyetPhieu);
            this.pnlActionTool.Controls.Add(this.btnCapNhat);
            this.pnlActionTool.Controls.Add(this.cboFilterTrangThai);
            this.pnlActionTool.Controls.Add(this.cboFilterLoaiXuat);
            this.pnlActionTool.Controls.Add(this.txtSearch);
            this.pnlActionTool.Controls.Add(this.btnTaoPhieuXuat);
            this.pnlActionTool.Controls.Add(this.lblTitle);
            this.pnlActionTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActionTool.Location = new System.Drawing.Point(15, 15);
            this.pnlActionTool.Name = "pnlActionTool";
            this.pnlActionTool.Size = new System.Drawing.Size(950, 110);
            this.pnlActionTool.TabIndex = 0;
            // 
            // btnXacNhanXuat
            // 
            this.btnXacNhanXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXacNhanXuat.BackColor = System.Drawing.Color.ForestGreen;
            this.btnXacNhanXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhanXuat.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnXacNhanXuat.ForeColor = System.Drawing.Color.White;
            this.btnXacNhanXuat.Location = new System.Drawing.Point(825, 4);
            this.btnXacNhanXuat.Name = "btnXacNhanXuat";
            this.btnXacNhanXuat.Size = new System.Drawing.Size(122, 30);
            this.btnXacNhanXuat.TabIndex = 8;
            this.btnXacNhanXuat.Text = "✓ Xác nhận xuất";
            this.btnXacNhanXuat.UseVisualStyleBackColor = false;
            this.btnXacNhanXuat.Click += new System.EventHandler(this.btnXacNhanXuat_Click);
            // 
            // btnLapDSLayHang
            // 
            this.btnLapDSLayHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLapDSLayHang.BackColor = System.Drawing.Color.DarkOrange;
            this.btnLapDSLayHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapDSLayHang.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLapDSLayHang.ForeColor = System.Drawing.Color.White;
            this.btnLapDSLayHang.Location = new System.Drawing.Point(685, 4);
            this.btnLapDSLayHang.Name = "btnLapDSLayHang";
            this.btnLapDSLayHang.Size = new System.Drawing.Size(134, 30);
            this.btnLapDSLayHang.TabIndex = 7;
            this.btnLapDSLayHang.Text = "📋 Lập DS Lấy Hàng";
            this.btnLapDSLayHang.UseVisualStyleBackColor = false;
            this.btnLapDSLayHang.Click += new System.EventHandler(this.btnLapDSLayHang_Click);
            // 
            // btnDuyetPhieu
            // 
            this.btnDuyetPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDuyetPhieu.BackColor = System.Drawing.Color.Teal;
            this.btnDuyetPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuyetPhieu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnDuyetPhieu.ForeColor = System.Drawing.Color.White;
            this.btnDuyetPhieu.Location = new System.Drawing.Point(575, 4);
            this.btnDuyetPhieu.Name = "btnDuyetPhieu";
            this.btnDuyetPhieu.Size = new System.Drawing.Size(104, 30);
            this.btnDuyetPhieu.TabIndex = 6;
            this.btnDuyetPhieu.Text = "✔ Duyệt Phiếu";
            this.btnDuyetPhieu.UseVisualStyleBackColor = false;
            this.btnDuyetPhieu.Click += new System.EventHandler(this.btnDuyetPhieu_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhat.BackColor = System.Drawing.Color.DimGray;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(475, 4);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(94, 30);
            this.btnCapNhat.TabIndex = 5;
            this.btnCapNhat.Text = "✏ Cập Nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // cboFilterTrangThai
            // 
            this.cboFilterTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFilterTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterTrangThai.FormattingEnabled = true;
            this.cboFilterTrangThai.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Chờ duyệt",
            "Đã duyệt",
            "Đang lấy hàng",
            "Hoàn tất xuất",
            "Đã hủy"});
            this.cboFilterTrangThai.Location = new System.Drawing.Point(750, 65);
            this.cboFilterTrangThai.Name = "cboFilterTrangThai";
            this.cboFilterTrangThai.Size = new System.Drawing.Size(197, 25);
            this.cboFilterTrangThai.TabIndex = 4;
            this.cboFilterTrangThai.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // cboFilterLoaiXuat
            // 
            this.cboFilterLoaiXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFilterLoaiXuat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterLoaiXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterLoaiXuat.FormattingEnabled = true;
            this.cboFilterLoaiXuat.Items.AddRange(new object[] {
            "Tất cả bộ phận",
            "Phân xưởng 1",
            "Phân xưởng 2",
            "Bộ phận bán hàng"});
            this.cboFilterLoaiXuat.Location = new System.Drawing.Point(535, 65);
            this.cboFilterLoaiXuat.Name = "cboFilterLoaiXuat";
            this.cboFilterLoaiXuat.Size = new System.Drawing.Size(205, 25);
            this.cboFilterLoaiXuat.TabIndex = 3;
            this.cboFilterLoaiXuat.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(0, 65);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(525, 24);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Tìm theo mã phiếu, bộ phận yêu cầu...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // btnTaoPhieuXuat
            // 
            this.btnTaoPhieuXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaoPhieuXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnTaoPhieuXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoPhieuXuat.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnTaoPhieuXuat.ForeColor = System.Drawing.Color.White;
            this.btnTaoPhieuXuat.Location = new System.Drawing.Point(345, 4);
            this.btnTaoPhieuXuat.Name = "btnTaoPhieuXuat";
            this.btnTaoPhieuXuat.Size = new System.Drawing.Size(124, 30);
            this.btnTaoPhieuXuat.TabIndex = 1;
            this.btnTaoPhieuXuat.Text = "+ Lập Lệnh Xuất";
            this.btnTaoPhieuXuat.UseVisualStyleBackColor = false;
            this.btnTaoPhieuXuat.Click += new System.EventHandler(this.btnTaoPhieuXuat_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(295, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Xuất Kho && Lấy Hàng";
            // 
            // FrQLXuatkhovaLayhang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMainContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrQLXuatkhovaLayhang";
            this.Load += new System.EventHandler(this.FrQLXuatkhovaLayhang_Load);
            this.pnlMainContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvXuatKho)).EndInit();
            this.pnlActionTool.ResumeLayout(false);
            this.pnlActionTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlActionTool;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnTaoPhieuXuat;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboFilterLoaiXuat;
        private System.Windows.Forms.ComboBox cboFilterTrangThai;
        private System.Windows.Forms.DataGridView dgvXuatKho;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnDuyetPhieu;
        private System.Windows.Forms.Button btnLapDSLayHang;
        private System.Windows.Forms.Button btnXacNhanXuat;
    }
}
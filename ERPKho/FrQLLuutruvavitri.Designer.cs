namespace ERPKho
{
    partial class FrQLLuutruvavitri
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.dgvViTri = new System.Windows.Forms.DataGridView();
            this.dgvChiTietLoHang = new System.Windows.Forms.DataGridView();
            this.pnlLoHangHeader = new System.Windows.Forms.Panel();
            this.lblLoHangTitle = new System.Windows.Forms.Label();
            this.pnlActionTool = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnTaoViTri = new System.Windows.Forms.Button();
            this.btnCapNhatViTri = new System.Windows.Forms.Button();
            this.btnKiemTraSucChua = new System.Windows.Forms.Button();
            this.btnPhanBoViTri = new System.Windows.Forms.Button();
            this.btnChuyenViTri = new System.Windows.Forms.Button();
            this.cboFilterKho = new System.Windows.Forms.ComboBox();
            this.cboFilterTrangThai = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlMainContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViTri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietLoHang)).BeginInit();
            this.pnlLoHangHeader.SuspendLayout();
            this.pnlActionTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Controls.Add(this.splMain);
            this.pnlMainContent.Controls.Add(this.pnlActionTool);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 0);
            this.pnlMainContent.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.pnlMainContent.Size = new System.Drawing.Size(984, 650);
            this.pnlMainContent.TabIndex = 2;
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.Location = new System.Drawing.Point(15, 106);
            this.splMain.Name = "splMain";
            this.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.dgvViTri);
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.dgvChiTietLoHang);
            this.splMain.Panel2.Controls.Add(this.pnlLoHangHeader);
            this.splMain.Size = new System.Drawing.Size(954, 528);
            this.splMain.SplitterDistance = 300;
            this.splMain.TabIndex = 1;
            // 
            // dgvViTri
            // 
            this.dgvViTri.AllowUserToAddRows = false;
            this.dgvViTri.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvViTri.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvViTri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvViTri.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvViTri.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvViTri.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvViTri.ColumnHeadersHeight = 35;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvViTri.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvViTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvViTri.EnableHeadersVisualStyles = false;
            this.dgvViTri.GridColor = System.Drawing.Color.LightGray;
            this.dgvViTri.Location = new System.Drawing.Point(0, 0);
            this.dgvViTri.MultiSelect = false;
            this.dgvViTri.Name = "dgvViTri";
            this.dgvViTri.ReadOnly = true;
            this.dgvViTri.RowHeadersVisible = false;
            this.dgvViTri.RowTemplate.Height = 35;
            this.dgvViTri.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvViTri.Size = new System.Drawing.Size(954, 300);
            this.dgvViTri.TabIndex = 0;
            this.dgvViTri.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvViTri_CellFormatting);
            this.dgvViTri.SelectionChanged += new System.EventHandler(this.dgvViTri_SelectionChanged);
            // 
            // dgvChiTietLoHang
            // 
            this.dgvChiTietLoHang.AllowUserToAddRows = false;
            this.dgvChiTietLoHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietLoHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietLoHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTietLoHang.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvChiTietLoHang.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.dgvChiTietLoHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvChiTietLoHang.ColumnHeadersHeight = 30;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietLoHang.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvChiTietLoHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietLoHang.EnableHeadersVisualStyles = false;
            this.dgvChiTietLoHang.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvChiTietLoHang.Location = new System.Drawing.Point(0, 30);
            this.dgvChiTietLoHang.Name = "dgvChiTietLoHang";
            this.dgvChiTietLoHang.ReadOnly = true;
            this.dgvChiTietLoHang.RowHeadersVisible = false;
            this.dgvChiTietLoHang.RowTemplate.Height = 30;
            this.dgvChiTietLoHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietLoHang.Size = new System.Drawing.Size(954, 194);
            this.dgvChiTietLoHang.TabIndex = 1;
            // 
            // pnlLoHangHeader
            // 
            this.pnlLoHangHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.pnlLoHangHeader.Controls.Add(this.lblLoHangTitle);
            this.pnlLoHangHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoHangHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlLoHangHeader.Name = "pnlLoHangHeader";
            this.pnlLoHangHeader.Size = new System.Drawing.Size(954, 30);
            this.pnlLoHangHeader.TabIndex = 0;
            // 
            // lblLoHangTitle
            // 
            this.lblLoHangTitle.AutoSize = true;
            this.lblLoHangTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLoHangTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.lblLoHangTitle.Location = new System.Drawing.Point(8, 6);
            this.lblLoHangTitle.Name = "lblLoHangTitle";
            this.lblLoHangTitle.Size = new System.Drawing.Size(347, 17);
            this.lblLoHangTitle.TabIndex = 0;
            this.lblLoHangTitle.Text = "📦 Các lô hàng hiện đang lưu trữ tại ô chứa được chọn:";
            // 
            // pnlActionTool
            // 
            this.pnlActionTool.Controls.Add(this.lblTitle);
            this.pnlActionTool.Controls.Add(this.btnTaoViTri);
            this.pnlActionTool.Controls.Add(this.btnCapNhatViTri);
            this.pnlActionTool.Controls.Add(this.btnKiemTraSucChua);
            this.pnlActionTool.Controls.Add(this.btnPhanBoViTri);
            this.pnlActionTool.Controls.Add(this.btnChuyenViTri);
            this.pnlActionTool.Controls.Add(this.cboFilterKho);
            this.pnlActionTool.Controls.Add(this.cboFilterTrangThai);
            this.pnlActionTool.Controls.Add(this.txtSearch);
            this.pnlActionTool.Controls.Add(this.btnLamMoi);
            this.pnlActionTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActionTool.Location = new System.Drawing.Point(15, 16);
            this.pnlActionTool.Margin = new System.Windows.Forms.Padding(2);
            this.pnlActionTool.Name = "pnlActionTool";
            this.pnlActionTool.Size = new System.Drawing.Size(954, 90);
            this.pnlActionTool.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý lưu trữ && vị trí kho";
            // 
            // btnTaoViTri
            // 
            this.btnTaoViTri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaoViTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnTaoViTri.FlatAppearance.BorderSize = 0;
            this.btnTaoViTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoViTri.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnTaoViTri.ForeColor = System.Drawing.Color.White;
            this.btnTaoViTri.Location = new System.Drawing.Point(220, 8);
            this.btnTaoViTri.Name = "btnTaoViTri";
            this.btnTaoViTri.Size = new System.Drawing.Size(90, 28);
            this.btnTaoViTri.TabIndex = 1;
            this.btnTaoViTri.Text = "+ Tạo vị trí";
            this.btnTaoViTri.UseVisualStyleBackColor = false;
            this.btnTaoViTri.Click += new System.EventHandler(this.btnTaoViTri_Click);
            // 
            // btnCapNhatViTri
            // 
            this.btnCapNhatViTri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhatViTri.BackColor = System.Drawing.Color.White;
            this.btnCapNhatViTri.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCapNhatViTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhatViTri.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnCapNhatViTri.Location = new System.Drawing.Point(321, 8);
            this.btnCapNhatViTri.Name = "btnCapNhatViTri";
            this.btnCapNhatViTri.Size = new System.Drawing.Size(85, 28);
            this.btnCapNhatViTri.TabIndex = 2;
            this.btnCapNhatViTri.Text = "Cập nhật";
            this.btnCapNhatViTri.UseVisualStyleBackColor = true;
            this.btnCapNhatViTri.Click += new System.EventHandler(this.btnCapNhatViTri_Click);
            // 
            // btnKiemTraSucChua
            // 
            this.btnKiemTraSucChua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKiemTraSucChua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnKiemTraSucChua.FlatAppearance.BorderSize = 0;
            this.btnKiemTraSucChua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKiemTraSucChua.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnKiemTraSucChua.ForeColor = System.Drawing.Color.Black;
            this.btnKiemTraSucChua.Location = new System.Drawing.Point(417, 8);
            this.btnKiemTraSucChua.Name = "btnKiemTraSucChua";
            this.btnKiemTraSucChua.Size = new System.Drawing.Size(147, 28);
            this.btnKiemTraSucChua.TabIndex = 3;
            this.btnKiemTraSucChua.Text = "📊 Kiểm tra sức chứa";
            this.btnKiemTraSucChua.UseVisualStyleBackColor = false;
            this.btnKiemTraSucChua.Click += new System.EventHandler(this.btnKiemTraSucChua_Click);
            // 
            // btnPhanBoViTri
            // 
            this.btnPhanBoViTri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPhanBoViTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnPhanBoViTri.FlatAppearance.BorderSize = 0;
            this.btnPhanBoViTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhanBoViTri.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnPhanBoViTri.ForeColor = System.Drawing.Color.White;
            this.btnPhanBoViTri.Location = new System.Drawing.Point(575, 8);
            this.btnPhanBoViTri.Name = "btnPhanBoViTri";
            this.btnPhanBoViTri.Size = new System.Drawing.Size(185, 28);
            this.btnPhanBoViTri.TabIndex = 4;
            this.btnPhanBoViTri.Text = "📌 Phân bổ cho lô hàng";
            this.btnPhanBoViTri.UseVisualStyleBackColor = false;
            this.btnPhanBoViTri.Click += new System.EventHandler(this.btnPhanBoViTri_Click);
            // 
            // btnChuyenViTri
            // 
            this.btnChuyenViTri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChuyenViTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnChuyenViTri.FlatAppearance.BorderSize = 0;
            this.btnChuyenViTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuyenViTri.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnChuyenViTri.ForeColor = System.Drawing.Color.White;
            this.btnChuyenViTri.Location = new System.Drawing.Point(771, 8);
            this.btnChuyenViTri.Name = "btnChuyenViTri";
            this.btnChuyenViTri.Size = new System.Drawing.Size(180, 28);
            this.btnChuyenViTri.TabIndex = 5;
            this.btnChuyenViTri.Text = "🔄 Chuyển vị trí lô hàng";
            this.btnChuyenViTri.UseVisualStyleBackColor = false;
            this.btnChuyenViTri.Click += new System.EventHandler(this.btnChuyenViTri_Click);
            // 
            // cboFilterKho
            // 
            this.cboFilterKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboFilterKho.FormattingEnabled = true;
            this.cboFilterKho.Items.AddRange(new object[] {
            "-- Tất cả các kho --",
            "Kho Thành Phẩm B2",
            "Kho Nguyên Liệu A1",
            "Kho Bao Bì C1"});
            this.cboFilterKho.Location = new System.Drawing.Point(0, 50);
            this.cboFilterKho.Name = "cboFilterKho";
            this.cboFilterKho.Size = new System.Drawing.Size(180, 23);
            this.cboFilterKho.TabIndex = 6;
            this.cboFilterKho.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // cboFilterTrangThai
            // 
            this.cboFilterTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboFilterTrangThai.FormattingEnabled = true;
            this.cboFilterTrangThai.Items.AddRange(new object[] {
            "-- Tất cả trạng thái --",
            "Khả dụng",
            "Đầy",
            "Đang bảo trì"});
            this.cboFilterTrangThai.Location = new System.Drawing.Point(190, 50);
            this.cboFilterTrangThai.Name = "cboFilterTrangThai";
            this.cboFilterTrangThai.Size = new System.Drawing.Size(160, 23);
            this.cboFilterTrangThai.TabIndex = 7;
            this.cboFilterTrangThai.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(364, 50);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(505, 23);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.Text = "Tìm mã vị trí, khu vực, kệ, ô chứa...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnLamMoi.Location = new System.Drawing.Point(879, 48);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(75, 26);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // FrQLLuutruvavitri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(984, 650);
            this.Controls.Add(this.pnlMainContent);
            this.Name = "FrQLLuutruvavitri";
            this.Text = "Quản Lý Lưu Trữ & Vị Trí (FR-02)";
            this.Load += new System.EventHandler(this.FrQLLuutruvavitri_Load);
            this.pnlMainContent.ResumeLayout(false);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViTri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietLoHang)).EndInit();
            this.pnlLoHangHeader.ResumeLayout(false);
            this.pnlLoHangHeader.PerformLayout();
            this.pnlActionTool.ResumeLayout(false);
            this.pnlActionTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.SplitContainer splMain;
        private System.Windows.Forms.DataGridView dgvViTri;
        private System.Windows.Forms.Panel pnlLoHangHeader;
        private System.Windows.Forms.Label lblLoHangTitle;
        private System.Windows.Forms.DataGridView dgvChiTietLoHang;

        private System.Windows.Forms.Panel pnlActionTool;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnTaoViTri;
        private System.Windows.Forms.Button btnCapNhatViTri;
        private System.Windows.Forms.Button btnKiemTraSucChua;
        private System.Windows.Forms.Button btnPhanBoViTri;
        private System.Windows.Forms.Button btnChuyenViTri;

        private System.Windows.Forms.ComboBox cboFilterKho;
        private System.Windows.Forms.ComboBox cboFilterTrangThai;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnLamMoi;
    }
}
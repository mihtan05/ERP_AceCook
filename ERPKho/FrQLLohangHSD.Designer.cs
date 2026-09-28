namespace ERPKho
{
    partial class FrQLLohangHSD
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.dgvLoHang = new System.Windows.Forms.DataGridView();
            this.pnlActionTool = new System.Windows.Forms.Panel();
            this.btnKhoaLoHang = new System.Windows.Forms.Button();
            this.btnCanhBaoHSD = new System.Windows.Forms.Button();
            this.btnSuaLoHang = new System.Windows.Forms.Button();
            this.btnThemLoHang = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboFilterHSD = new System.Windows.Forms.ComboBox();
            this.cboFilterKho = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMainContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoHang)).BeginInit();
            this.pnlActionTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Controls.Add(this.dgvLoHang);
            this.pnlMainContent.Controls.Add(this.pnlActionTool);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 0);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMainContent.Size = new System.Drawing.Size(980, 609);
            this.pnlMainContent.TabIndex = 0;
            this.pnlMainContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMainContent_Paint);
            // 
            // dgvLoHang
            // 
            this.dgvLoHang.AllowUserToAddRows = false;
            this.dgvLoHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoHang.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvLoHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLoHang.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLoHang.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLoHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLoHang.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLoHang.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLoHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoHang.EnableHeadersVisualStyles = false;
            this.dgvLoHang.GridColor = System.Drawing.Color.LightGray;
            this.dgvLoHang.Location = new System.Drawing.Point(15, 115);
            this.dgvLoHang.Name = "dgvLoHang";
            this.dgvLoHang.ReadOnly = true;
            this.dgvLoHang.RowHeadersVisible = false;
            this.dgvLoHang.RowTemplate.Height = 42;
            this.dgvLoHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoHang.Size = new System.Drawing.Size(950, 479);
            this.dgvLoHang.TabIndex = 1;
            this.dgvLoHang.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoHang_CellDoubleClick);
            this.dgvLoHang.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLoHang_CellFormatting);
            // 
            // pnlActionTool
            // 
            this.pnlActionTool.Controls.Add(this.btnKhoaLoHang);
            this.pnlActionTool.Controls.Add(this.btnCanhBaoHSD);
            this.pnlActionTool.Controls.Add(this.btnSuaLoHang);
            this.pnlActionTool.Controls.Add(this.btnThemLoHang);
            this.pnlActionTool.Controls.Add(this.txtSearch);
            this.pnlActionTool.Controls.Add(this.cboFilterHSD);
            this.pnlActionTool.Controls.Add(this.cboFilterKho);
            this.pnlActionTool.Controls.Add(this.lblTitle);
            this.pnlActionTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActionTool.Location = new System.Drawing.Point(15, 15);
            this.pnlActionTool.Name = "pnlActionTool";
            this.pnlActionTool.Size = new System.Drawing.Size(950, 100);
            this.pnlActionTool.TabIndex = 0;
            // 
            // btnKhoaLoHang
            // 
            this.btnKhoaLoHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKhoaLoHang.BackColor = System.Drawing.Color.Crimson;
            this.btnKhoaLoHang.FlatAppearance.BorderSize = 0;
            this.btnKhoaLoHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhoaLoHang.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnKhoaLoHang.ForeColor = System.Drawing.Color.White;
            this.btnKhoaLoHang.Location = new System.Drawing.Point(560, 58);
            this.btnKhoaLoHang.Name = "btnKhoaLoHang";
            this.btnKhoaLoHang.Size = new System.Drawing.Size(95, 28);
            this.btnKhoaLoHang.TabIndex = 7;
            this.btnKhoaLoHang.Text = "🔒 Khóa Lô";
            this.btnKhoaLoHang.UseVisualStyleBackColor = false;
            this.btnKhoaLoHang.Click += new System.EventHandler(this.btnKhoaLoHang_Click);
            // 
            // btnCanhBaoHSD
            // 
            this.btnCanhBaoHSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCanhBaoHSD.BackColor = System.Drawing.Color.DarkOrange;
            this.btnCanhBaoHSD.FlatAppearance.BorderSize = 0;
            this.btnCanhBaoHSD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanhBaoHSD.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCanhBaoHSD.ForeColor = System.Drawing.Color.White;
            this.btnCanhBaoHSD.Location = new System.Drawing.Point(661, 58);
            this.btnCanhBaoHSD.Name = "btnCanhBaoHSD";
            this.btnCanhBaoHSD.Size = new System.Drawing.Size(105, 28);
            this.btnCanhBaoHSD.TabIndex = 6;
            this.btnCanhBaoHSD.Text = "⚠️ Cảnh Báo HSD";
            this.btnCanhBaoHSD.UseVisualStyleBackColor = false;
            this.btnCanhBaoHSD.Click += new System.EventHandler(this.btnCanhBaoHSD_Click);
            // 
            // btnSuaLoHang
            // 
            this.btnSuaLoHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuaLoHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnSuaLoHang.FlatAppearance.BorderSize = 0;
            this.btnSuaLoHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuaLoHang.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSuaLoHang.ForeColor = System.Drawing.Color.White;
            this.btnSuaLoHang.Location = new System.Drawing.Point(772, 58);
            this.btnSuaLoHang.Name = "btnSuaLoHang";
            this.btnSuaLoHang.Size = new System.Drawing.Size(85, 28);
            this.btnSuaLoHang.TabIndex = 5;
            this.btnSuaLoHang.Text = "✏️ Cập Nhật";
            this.btnSuaLoHang.UseVisualStyleBackColor = false;
            this.btnSuaLoHang.Click += new System.EventHandler(this.btnSuaLoHang_Click);
            // 
            // btnThemLoHang
            // 
            this.btnThemLoHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemLoHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThemLoHang.FlatAppearance.BorderSize = 0;
            this.btnThemLoHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemLoHang.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemLoHang.ForeColor = System.Drawing.Color.White;
            this.btnThemLoHang.Location = new System.Drawing.Point(863, 58);
            this.btnThemLoHang.Name = "btnThemLoHang";
            this.btnThemLoHang.Size = new System.Drawing.Size(84, 28);
            this.btnThemLoHang.TabIndex = 4;
            this.btnThemLoHang.Text = "+ Thêm Lô";
            this.btnThemLoHang.UseVisualStyleBackColor = false;
            this.btnThemLoHang.Click += new System.EventHandler(this.btnThemLoHang_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(340, 60);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(210, 24);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.Text = "Tìm kiếm theo mã lô, mã vật tư, tên sản phẩm...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // cboFilterHSD
            // 
            this.cboFilterHSD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterHSD.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterHSD.FormattingEnabled = true;
            this.cboFilterHSD.Items.AddRange(new object[] {
            "-- Tất cả tình trạng HSD --",
            "Còn hạn an toàn",
            "Sắp hết hạn",
            "Đã quá hạn",
            "Cần ưu tiên xuất (FEFO)"});
            this.cboFilterHSD.Location = new System.Drawing.Point(170, 60);
            this.cboFilterHSD.Name = "cboFilterHSD";
            this.cboFilterHSD.Size = new System.Drawing.Size(160, 25);
            this.cboFilterHSD.TabIndex = 2;
            this.cboFilterHSD.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // cboFilterKho
            // 
            this.cboFilterKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterKho.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterKho.FormattingEnabled = true;
            this.cboFilterKho.Location = new System.Drawing.Point(0, 60);
            this.cboFilterKho.Name = "cboFilterKho";
            this.cboFilterKho.Size = new System.Drawing.Size(160, 25);
            this.cboFilterKho.TabIndex = 1;
            this.cboFilterKho.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(241, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Lô Hàng & HSD";
            // 
            // FrQLLohangHSD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(980, 609);
            this.Controls.Add(this.pnlMainContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrQLLohangHSD";
            this.Text = "Quản lý Lô Hàng & HSD";
            this.Load += new System.EventHandler(this.FrQLLohangHSD_Load);
            this.pnlMainContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoHang)).EndInit();
            this.pnlActionTool.ResumeLayout(false);
            this.pnlActionTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlActionTool;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cboFilterKho;
        private System.Windows.Forms.ComboBox cboFilterHSD;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnThemLoHang;
        private System.Windows.Forms.Button btnSuaLoHang;
        private System.Windows.Forms.Button btnCanhBaoHSD;
        private System.Windows.Forms.Button btnKhoaLoHang;
        private System.Windows.Forms.DataGridView dgvLoHang;
    }
}
namespace ERPKho
{
    partial class FrQLTonkho
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.pnlActionTool = new System.Windows.Forms.Panel();
            this.btnDuyetDieuChinh = new System.Windows.Forms.Button();
            this.btnNhapKiemKe = new System.Windows.Forms.Button();
            this.btnTaoKiemKe = new System.Windows.Forms.Button();
            this.btnCanhBao = new System.Windows.Forms.Button();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboFilterCanhBao = new System.Windows.Forms.ComboBox();
            this.cboFilterKho = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMainContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.pnlActionTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Controls.Add(this.dgvTonKho);
            this.pnlMainContent.Controls.Add(this.pnlActionTool);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 0);
            this.pnlMainContent.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.pnlMainContent.Size = new System.Drawing.Size(960, 609);
            this.pnlMainContent.TabIndex = 2;
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.AllowUserToAddRows = false;
            this.dgvTonKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTonKho.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvTonKho.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTonKho.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTonKho.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTonKho.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTonKho.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTonKho.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTonKho.EnableHeadersVisualStyles = false;
            this.dgvTonKho.GridColor = System.Drawing.Color.LightGray;
            this.dgvTonKho.Location = new System.Drawing.Point(15, 128);
            this.dgvTonKho.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.ReadOnly = true;
            this.dgvTonKho.RowHeadersVisible = false;
            this.dgvTonKho.RowHeadersWidth = 51;
            this.dgvTonKho.RowTemplate.Height = 42;
            this.dgvTonKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTonKho.Size = new System.Drawing.Size(930, 465);
            this.dgvTonKho.TabIndex = 1;
            this.dgvTonKho.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTonKho_CellFormatting);
            // 
            // pnlActionTool
            // 
            this.pnlActionTool.Controls.Add(this.btnDuyetDieuChinh);
            this.pnlActionTool.Controls.Add(this.btnNhapKiemKe);
            this.pnlActionTool.Controls.Add(this.btnTaoKiemKe);
            this.pnlActionTool.Controls.Add(this.btnCanhBao);
            this.pnlActionTool.Controls.Add(this.btnTraCuu);
            this.pnlActionTool.Controls.Add(this.txtSearch);
            this.pnlActionTool.Controls.Add(this.cboFilterCanhBao);
            this.pnlActionTool.Controls.Add(this.cboFilterKho);
            this.pnlActionTool.Controls.Add(this.lblTitle);
            this.pnlActionTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActionTool.Location = new System.Drawing.Point(15, 16);
            this.pnlActionTool.Margin = new System.Windows.Forms.Padding(2);
            this.pnlActionTool.Name = "pnlActionTool";
            this.pnlActionTool.Size = new System.Drawing.Size(930, 112);
            this.pnlActionTool.TabIndex = 0;
            // 
            // btnDuyetDieuChinh
            // 
            this.btnDuyetDieuChinh.BackColor = System.Drawing.Color.Purple;
            this.btnDuyetDieuChinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuyetDieuChinh.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnDuyetDieuChinh.ForeColor = System.Drawing.Color.White;
            this.btnDuyetDieuChinh.Location = new System.Drawing.Point(760, 12);
            this.btnDuyetDieuChinh.Name = "btnDuyetDieuChinh";
            this.btnDuyetDieuChinh.Size = new System.Drawing.Size(160, 30);
            this.btnDuyetDieuChinh.TabIndex = 8;
            this.btnDuyetDieuChinh.Text = "5. Duyệt điều chỉnh";
            this.btnDuyetDieuChinh.UseVisualStyleBackColor = false;
            this.btnDuyetDieuChinh.Click += new System.EventHandler(this.btnDuyetDieuChinh_Click);
            // 
            // btnNhapKiemKe
            // 
            this.btnNhapKiemKe.BackColor = System.Drawing.Color.Teal;
            this.btnNhapKiemKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapKiemKe.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnNhapKiemKe.ForeColor = System.Drawing.Color.White;
            this.btnNhapKiemKe.Location = new System.Drawing.Point(595, 12);
            this.btnNhapKiemKe.Name = "btnNhapKiemKe";
            this.btnNhapKiemKe.Size = new System.Drawing.Size(155, 30);
            this.btnNhapKiemKe.TabIndex = 7;
            this.btnNhapKiemKe.Text = "4. Nhập KQ kiểm kê";
            this.btnNhapKiemKe.UseVisualStyleBackColor = false;
            this.btnNhapKiemKe.Click += new System.EventHandler(this.btnNhapKiemKe_Click);
            // 
            // btnTaoKiemKe
            // 
            this.btnTaoKiemKe.BackColor = System.Drawing.Color.Green;
            this.btnTaoKiemKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoKiemKe.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnTaoKiemKe.ForeColor = System.Drawing.Color.White;
            this.btnTaoKiemKe.Location = new System.Drawing.Point(440, 12);
            this.btnTaoKiemKe.Name = "btnTaoKiemKe";
            this.btnTaoKiemKe.Size = new System.Drawing.Size(145, 30);
            this.btnTaoKiemKe.TabIndex = 6;
            this.btnTaoKiemKe.Text = "3. Tạo phiếu kiểm kê";
            this.btnTaoKiemKe.UseVisualStyleBackColor = false;
            this.btnTaoKiemKe.Click += new System.EventHandler(this.btnTaoKiemKe_Click);
            // 
            // btnCanhBao
            // 
            this.btnCanhBao.BackColor = System.Drawing.Color.DarkOrange;
            this.btnCanhBao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanhBao.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnCanhBao.ForeColor = System.Drawing.Color.White;
            this.btnCanhBao.Location = new System.Drawing.Point(295, 12);
            this.btnCanhBao.Name = "btnCanhBao";
            this.btnCanhBao.Size = new System.Drawing.Size(135, 30);
            this.btnCanhBao.TabIndex = 5;
            this.btnCanhBao.Text = "2. Cảnh báo tồn thấp";
            this.btnCanhBao.UseVisualStyleBackColor = false;
            this.btnCanhBao.Click += new System.EventHandler(this.btnCanhBao_Click);
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnTraCuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraCuu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnTraCuu.ForeColor = System.Drawing.Color.White;
            this.btnTraCuu.Location = new System.Drawing.Point(170, 12);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(115, 30);
            this.btnTraCuu.TabIndex = 4;
            this.btnTraCuu.Text = "1. Tra cứu tồn";
            this.btnTraCuu.UseVisualStyleBackColor = false;
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(340, 56);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(324, 25);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // cboFilterCanhBao
            // 
            this.cboFilterCanhBao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterCanhBao.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterCanhBao.FormattingEnabled = true;
            this.cboFilterCanhBao.Items.AddRange(new object[] {
            "-- Trạng thái Cảnh báo --",
            "An toàn",
            "Cảnh báo thiếu",
            "Hết hàng",
            "Tồn kho quá tải"});
            this.cboFilterCanhBao.Location = new System.Drawing.Point(170, 56);
            this.cboFilterCanhBao.Margin = new System.Windows.Forms.Padding(2);
            this.cboFilterCanhBao.Name = "cboFilterCanhBao";
            this.cboFilterCanhBao.Size = new System.Drawing.Size(158, 25);
            this.cboFilterCanhBao.TabIndex = 2;
            this.cboFilterCanhBao.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // cboFilterKho
            // 
            this.cboFilterKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterKho.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterKho.FormattingEnabled = true;
            this.cboFilterKho.Items.AddRange(new object[] {
            "-- Tất cả Kho --"});
            this.cboFilterKho.Location = new System.Drawing.Point(0, 56);
            this.cboFilterKho.Margin = new System.Windows.Forms.Padding(2);
            this.cboFilterKho.Name = "cboFilterKho";
            this.cboFilterKho.Size = new System.Drawing.Size(158, 25);
            this.cboFilterKho.TabIndex = 1;
            this.cboFilterKho.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(-4, 12);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(162, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Tồn Kho";
            // 
            // FrQLTonkho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(960, 609);
            this.Controls.Add(this.pnlMainContent);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrQLTonkho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Tồn Kho (FR-03)";
            this.Load += new System.EventHandler(this.FrQLTonkho_Load);
            this.pnlMainContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.pnlActionTool.ResumeLayout(false);
            this.pnlActionTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlActionTool;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cboFilterKho;
        private System.Windows.Forms.ComboBox cboFilterCanhBao;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.Button btnCanhBao;
        private System.Windows.Forms.Button btnTaoKiemKe;
        private System.Windows.Forms.Button btnNhapKiemKe;
        private System.Windows.Forms.Button btnDuyetDieuChinh;
    }
}
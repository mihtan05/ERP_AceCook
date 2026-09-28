namespace ERP_Khach
{
    partial class TaoYeuCauTraHang
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDonHang = new System.Windows.Forms.Label();
            this.cboDonHang = new System.Windows.Forms.ComboBox();
            this.lblLoaiYC = new System.Windows.Forms.Label();
            this.cboLoaiYeuCau = new System.Windows.Forms.ComboBox();
            this.lblMoTaChung = new System.Windows.Forms.Label();
            this.txtMoTaChung = new System.Windows.Forms.TextBox();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.btnLuuYeuCau = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.pnlTopHeader = new System.Windows.Forms.Panel();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.pnlTopHeader.SuspendLayout();
            this.pnlMainContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopHeader
            // 
            this.pnlTopHeader.BackColor = System.Drawing.Color.White;
            this.pnlTopHeader.Controls.Add(this.lblTitle);
            this.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTopHeader.Name = "pnlTopHeader";
            this.pnlTopHeader.Size = new System.Drawing.Size(942, 60);
            this.pnlTopHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Crimson;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(534, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TẠO YÊU CẦU ĐỔI TRẢ / BẢO HÀNH HÀNG";
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Controls.Add(this.lblDonHang);
            this.pnlMainContent.Controls.Add(this.cboDonHang);
            this.pnlMainContent.Controls.Add(this.lblLoaiYC);
            this.pnlMainContent.Controls.Add(this.cboLoaiYeuCau);
            this.pnlMainContent.Controls.Add(this.lblMoTaChung);
            this.pnlMainContent.Controls.Add(this.txtMoTaChung);
            this.pnlMainContent.Controls.Add(this.dgvSanPham);
            this.pnlMainContent.Controls.Add(this.btnLuuYeuCau);
            this.pnlMainContent.Controls.Add(this.btnHuy);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 60);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Padding = new System.Windows.Forms.Padding(25);
            this.pnlMainContent.Size = new System.Drawing.Size(942, 573);
            this.pnlMainContent.TabIndex = 1;
            // 
            // lblDonHang
            // 
            this.lblDonHang.AutoSize = true;
            this.lblDonHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDonHang.Location = new System.Drawing.Point(25, 25);
            this.lblDonHang.Name = "lblDonHang";
            this.lblDonHang.Size = new System.Drawing.Size(142, 23);
            this.lblDonHang.TabIndex = 0;
            this.lblDonHang.Text = "Chọn đơn hàng:";
            // 
            // cboDonHang
            // 
            this.cboDonHang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDonHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDonHang.FormattingEnabled = true;
            this.cboDonHang.Location = new System.Drawing.Point(180, 22);
            this.cboDonHang.Name = "cboDonHang";
            this.cboDonHang.Size = new System.Drawing.Size(730, 31);
            this.cboDonHang.TabIndex = 1;
            this.cboDonHang.SelectedIndexChanged += new System.EventHandler(this.cboDonHang_SelectedIndexChanged);
            // 
            // lblLoaiYC
            // 
            this.lblLoaiYC.AutoSize = true;
            this.lblLoaiYC.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoaiYC.Location = new System.Drawing.Point(25, 70);
            this.lblLoaiYC.Name = "lblLoaiYC";
            this.lblLoaiYC.Size = new System.Drawing.Size(117, 23);
            this.lblLoaiYC.TabIndex = 2;
            this.lblLoaiYC.Text = "Loại yêu cầu:";
            // 
            // cboLoaiYeuCau
            // 
            this.cboLoaiYeuCau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiYeuCau.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiYeuCau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiYeuCau.FormattingEnabled = true;
            this.cboLoaiYeuCau.Location = new System.Drawing.Point(180, 67);
            this.cboLoaiYeuCau.Name = "cboLoaiYeuCau";
            this.cboLoaiYeuCau.Size = new System.Drawing.Size(730, 31);
            this.cboLoaiYeuCau.TabIndex = 3;
            // 
            // lblMoTaChung
            // 
            this.lblMoTaChung.AutoSize = true;
            this.lblMoTaChung.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMoTaChung.Location = new System.Drawing.Point(25, 115);
            this.lblMoTaChung.Name = "lblMoTaChung";
            this.lblMoTaChung.Size = new System.Drawing.Size(130, 23);
            this.lblMoTaChung.TabIndex = 4;
            this.lblMoTaChung.Text = "Ghi chú chung:";
            // 
            // txtMoTaChung
            // 
            this.txtMoTaChung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMoTaChung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMoTaChung.Location = new System.Drawing.Point(180, 112);
            this.txtMoTaChung.Multiline = true;
            this.txtMoTaChung.Name = "txtMoTaChung";
            this.txtMoTaChung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTaChung.Size = new System.Drawing.Size(730, 65);
            this.txtMoTaChung.TabIndex = 5;
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.AllowUserToDeleteRows = false;
            this.dgvSanPham.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPham.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvSanPham.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSanPham.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSanPham.ColumnHeadersHeight = 38;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSanPham.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSanPham.Location = new System.Drawing.Point(28, 195);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.RowHeadersVisible = false;
            this.dgvSanPham.RowHeadersWidth = 51;
            this.dgvSanPham.RowTemplate.Height = 38;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(882, 305);
            this.dgvSanPham.TabIndex = 6;
            // 
            // btnLuuYeuCau
            // 
            this.btnLuuYeuCau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuYeuCau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLuuYeuCau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuuYeuCau.FlatAppearance.BorderSize = 0;
            this.btnLuuYeuCau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuYeuCau.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuuYeuCau.ForeColor = System.Drawing.Color.White;
            this.btnLuuYeuCau.Location = new System.Drawing.Point(620, 515);
            this.btnLuuYeuCau.Name = "btnLuuYeuCau";
            this.btnLuuYeuCau.Size = new System.Drawing.Size(160, 42);
            this.btnLuuYeuCau.TabIndex = 7;
            this.btnLuuYeuCau.Text = "💾 Gửi yêu cầu";
            this.btnLuuYeuCau.UseVisualStyleBackColor = false;
            this.btnLuuYeuCau.Click += new System.EventHandler(this.btnLuuYeuCau_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(792, 515);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(118, 42);
            this.btnHuy.TabIndex = 8;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // TaoYeuCauTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(942, 633);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlTopHeader);
            this.MinimumSize = new System.Drawing.Size(960, 680);
            this.Name = "TaoYeuCauTraHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP - Tạo phiếu yêu cầu đổi trả / bảo hành";
            this.Load += new System.EventHandler(this.TaoYeuCauTraHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.pnlTopHeader.ResumeLayout(false);
            this.pnlTopHeader.PerformLayout();
            this.pnlMainContent.ResumeLayout(false);
            this.pnlMainContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopHeader;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDonHang;
        private System.Windows.Forms.ComboBox cboDonHang;
        private System.Windows.Forms.Label lblLoaiYC;
        private System.Windows.Forms.ComboBox cboLoaiYeuCau;
        private System.Windows.Forms.Label lblMoTaChung;
        private System.Windows.Forms.TextBox txtMoTaChung;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.Button btnLuuYeuCau;
        private System.Windows.Forms.Button btnHuy;
    }
}
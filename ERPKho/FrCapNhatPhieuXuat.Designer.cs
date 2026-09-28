namespace ERPKho
{
    partial class FrCapNhatPhieuXuat
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblMaPX = new System.Windows.Forms.Label();
            this.txtMaPX = new System.Windows.Forms.TextBox();
            this.lblBoPhan = new System.Windows.Forms.Label();
            this.txtBoPhan = new System.Windows.Forms.TextBox();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblTrangThaiVal = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuyPhieu = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblHeader);
            this.pnlTop.Controls.Add(this.lblMaPX);
            this.pnlTop.Controls.Add(this.txtMaPX);
            this.pnlTop.Controls.Add(this.lblBoPhan);
            this.pnlTop.Controls.Add(this.txtBoPhan);
            this.pnlTop.Controls.Add(this.lblNgayLap);
            this.pnlTop.Controls.Add(this.dtpNgayLap);
            this.pnlTop.Controls.Add(this.lblTrangThai);
            this.pnlTop.Controls.Add(this.lblTrangThaiVal);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 135;
            this.pnlTop.Padding = new System.Windows.Forms.Padding(15);

            // lblHeader
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHeader.Location = new System.Drawing.Point(15, 10);
            this.lblHeader.Text = "CẬP NHẬT PHIẾU XUẤT KHO";

            // lblMaPX & txtMaPX
            this.lblMaPX.AutoSize = true;
            this.lblMaPX.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMaPX.Location = new System.Drawing.Point(15, 48);
            this.lblMaPX.Text = "Mã phiếu xuất:";

            this.txtMaPX.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaPX.Location = new System.Drawing.Point(120, 45);
            this.txtMaPX.ReadOnly = true;
            this.txtMaPX.Width = 220;

            // lblBoPhan & txtBoPhan
            this.lblBoPhan.AutoSize = true;
            this.lblBoPhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBoPhan.Location = new System.Drawing.Point(15, 83);
            this.lblBoPhan.Text = "Bộ phận yêu cầu:";

            this.txtBoPhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtBoPhan.Location = new System.Drawing.Point(120, 80);
            this.txtBoPhan.Width = 220;

            // lblNgayLap & dtpNgayLap
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayLap.Location = new System.Drawing.Point(380, 48);
            this.lblNgayLap.Text = "Ngày lập:";

            this.dtpNgayLap.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayLap.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayLap.Location = new System.Drawing.Point(460, 45);
            this.dtpNgayLap.Width = 220;

            // lblTrangThai & lblTrangThaiVal
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTrangThai.Location = new System.Drawing.Point(380, 83);
            this.lblTrangThai.Text = "Trạng thái:";

            this.lblTrangThaiVal.AutoSize = true;
            this.lblTrangThaiVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiVal.Location = new System.Drawing.Point(460, 83);
            this.lblTrangThaiVal.Text = "---";

            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.RowTemplate.Height = 32;

            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnLuu);
            this.pnlBottom.Controls.Add(this.btnHuyPhieu);
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 60;

            // btnLuu
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(370, 12);
            this.btnLuu.Size = new System.Drawing.Size(120, 36);
            this.btnLuu.Text = "Cập Nhật";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            // btnHuyPhieu
            this.btnHuyPhieu.BackColor = System.Drawing.Color.Crimson;
            this.btnHuyPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHuyPhieu.ForeColor = System.Drawing.Color.White;
            this.btnHuyPhieu.Location = new System.Drawing.Point(500, 12);
            this.btnHuyPhieu.Size = new System.Drawing.Size(130, 36);
            this.btnHuyPhieu.Text = "Hủy Phiếu Xuất";
            this.btnHuyPhieu.Click += new System.EventHandler(this.btnHuyPhieu_Click);

            // btnDong
            this.btnDong.BackColor = System.Drawing.Color.Gray;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(640, 12);
            this.btnDong.Size = new System.Drawing.Size(100, 36);
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            // 
            // FrCapNhatPhieuXuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "FrCapNhatPhieuXuat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật phiếu xuất kho";
            this.Load += new System.EventHandler(this.FrCapNhatPhieuXuat_Load);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblMaPX;
        private System.Windows.Forms.TextBox txtMaPX;
        private System.Windows.Forms.Label lblBoPhan;
        private System.Windows.Forms.TextBox txtBoPhan;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblTrangThaiVal;

        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuyPhieu;
        private System.Windows.Forms.Button btnDong;
    }
}
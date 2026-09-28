namespace ERP
{
    partial class FrmThemDonVanChuyen
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

        private void InitializeComponent()
        {
            this.lblDonHang = new System.Windows.Forms.Label();
            this.cboDonHang = new System.Windows.Forms.ComboBox();
            this.lblIDDonVC = new System.Windows.Forms.Label();
            this.txtIDDonVC = new System.Windows.Forms.TextBox();
            this.lblMaDVC = new System.Windows.Forms.Label();
            this.cboMaDVC = new System.Windows.Forms.ComboBox();
            this.lblDiaChiGiao = new System.Windows.Forms.Label();
            this.txtDiaChiGiao = new System.Windows.Forms.TextBox();
            this.lblBienSoXe = new System.Windows.Forms.Label();
            this.cboBienSoXe = new System.Windows.Forms.ComboBox();
            this.lblTaiTrongXe = new System.Windows.Forms.Label();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblSoLuongGiao = new System.Windows.Forms.Label();
            this.txtSoLuongGiao = new System.Windows.Forms.TextBox();
            this.lblThoiGianKhoiHanh = new System.Windows.Forms.Label();
            this.dtpThoiGianKhoiHanh = new System.Windows.Forms.DateTimePicker();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDonHang
            // 
            this.lblDonHang.AutoSize = true;
            this.lblDonHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDonHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblDonHang.Location = new System.Drawing.Point(30, 20);
            this.lblDonHang.Name = "lblDonHang";
            this.lblDonHang.Size = new System.Drawing.Size(145, 20);
            this.lblDonHang.TabIndex = 0;
            this.lblDonHang.Text = "Đơn hàng Bán hàng:";
            // 
            // cboDonHang
            // 
            this.cboDonHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonHang.DropDownWidth = 430;
            this.cboDonHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboDonHang.FormattingEnabled = true;
            this.cboDonHang.Location = new System.Drawing.Point(180, 17);
            this.cboDonHang.Name = "cboDonHang";
            this.cboDonHang.Size = new System.Drawing.Size(280, 28);
            this.cboDonHang.TabIndex = 0;
            this.cboDonHang.SelectedIndexChanged += new System.EventHandler(this.cboDonHang_SelectedIndexChanged);
            // 
            // lblIDDonVC
            // 
            this.lblIDDonVC.AutoSize = true;
            this.lblIDDonVC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblIDDonVC.Location = new System.Drawing.Point(30, 60);
            this.lblIDDonVC.Name = "lblIDDonVC";
            this.lblIDDonVC.Size = new System.Drawing.Size(127, 20);
            this.lblIDDonVC.TabIndex = 1;
            this.lblIDDonVC.Text = "Mã đơn VC (*):";
            // 
            // txtIDDonVC
            // 
            this.txtIDDonVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtIDDonVC.Location = new System.Drawing.Point(180, 57);
            this.txtIDDonVC.Name = "txtIDDonVC";
            this.txtIDDonVC.Size = new System.Drawing.Size(280, 27);
            this.txtIDDonVC.TabIndex = 1;
            // 
            // lblMaDVC
            // 
            this.lblMaDVC.AutoSize = true;
            this.lblMaDVC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaDVC.Location = new System.Drawing.Point(30, 100);
            this.lblMaDVC.Name = "lblMaDVC";
            this.lblMaDVC.Size = new System.Drawing.Size(147, 20);
            this.lblMaDVC.TabIndex = 2;
            this.lblMaDVC.Text = "Điểm giao nhận (*):";
            // 
            // cboMaDVC
            // 
            this.cboMaDVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaDVC.DropDownWidth = 350;
            this.cboMaDVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboMaDVC.FormattingEnabled = true;
            this.cboMaDVC.Location = new System.Drawing.Point(180, 97);
            this.cboMaDVC.Name = "cboMaDVC";
            this.cboMaDVC.Size = new System.Drawing.Size(280, 28);
            this.cboMaDVC.TabIndex = 2;
            this.cboMaDVC.SelectedIndexChanged += new System.EventHandler(this.cboMaDVC_SelectedIndexChanged);
            // 
            // lblDiaChiGiao
            // 
            this.lblDiaChiGiao.AutoSize = true;
            this.lblDiaChiGiao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiaChiGiao.Location = new System.Drawing.Point(30, 140);
            this.lblDiaChiGiao.Name = "lblDiaChiGiao";
            this.lblDiaChiGiao.Size = new System.Drawing.Size(135, 20);
            this.lblDiaChiGiao.TabIndex = 3;
            this.lblDiaChiGiao.Text = "Địa chỉ giao (*):";
            // 
            // txtDiaChiGiao
            // 
            this.txtDiaChiGiao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.txtDiaChiGiao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiaChiGiao.Location = new System.Drawing.Point(180, 137);
            this.txtDiaChiGiao.Name = "txtDiaChiGiao";
            this.txtDiaChiGiao.ReadOnly = true;
            this.txtDiaChiGiao.Size = new System.Drawing.Size(280, 27);
            this.txtDiaChiGiao.TabIndex = 3;
            // 
            // lblBienSoXe
            // 
            this.lblBienSoXe.AutoSize = true;
            this.lblBienSoXe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBienSoXe.Location = new System.Drawing.Point(30, 180);
            this.lblBienSoXe.Name = "lblBienSoXe";
            this.lblBienSoXe.Size = new System.Drawing.Size(126, 20);
            this.lblBienSoXe.TabIndex = 4;
            this.lblBienSoXe.Text = "Phương tiện (*):";
            // 
            // cboBienSoXe
            // 
            this.cboBienSoXe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBienSoXe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboBienSoXe.FormattingEnabled = true;
            this.cboBienSoXe.Location = new System.Drawing.Point(180, 177);
            this.cboBienSoXe.Name = "cboBienSoXe";
            this.cboBienSoXe.Size = new System.Drawing.Size(280, 28);
            this.cboBienSoXe.TabIndex = 4;
            this.cboBienSoXe.SelectedIndexChanged += new System.EventHandler(this.cboBienSoXe_SelectedIndexChanged);
            // 
            // lblTaiTrongXe
            // 
            this.lblTaiTrongXe.AutoSize = true;
            this.lblTaiTrongXe.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblTaiTrongXe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTaiTrongXe.Location = new System.Drawing.Point(180, 208);
            this.lblTaiTrongXe.Name = "lblTaiTrongXe";
            this.lblTaiTrongXe.Size = new System.Drawing.Size(160, 19);
            this.lblTaiTrongXe.TabIndex = 5;
            this.lblTaiTrongXe.Text = "🚛 Tải trọng xe: Đang kiểm tra...";
            // 
            // lblSanPham
            // 
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSanPham.Location = new System.Drawing.Point(30, 236);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(107, 20);
            this.lblSanPham.TabIndex = 6;
            this.lblSanPham.Text = "Hàng hóa (*):";
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.DropDownWidth = 350;
            this.cboSanPham.Enabled = false;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Location = new System.Drawing.Point(180, 233);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(280, 28);
            this.cboSanPham.TabIndex = 6;
            // 
            // lblSoLuongGiao
            // 
            this.lblSoLuongGiao.AutoSize = true;
            this.lblSoLuongGiao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoLuongGiao.Location = new System.Drawing.Point(30, 276);
            this.lblSoLuongGiao.Name = "lblSoLuongGiao";
            this.lblSoLuongGiao.Size = new System.Drawing.Size(130, 20);
            this.lblSoLuongGiao.TabIndex = 7;
            this.lblSoLuongGiao.Text = "Số lượng giao (*):";
            // 
            // txtSoLuongGiao
            // 
            this.txtSoLuongGiao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.txtSoLuongGiao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtSoLuongGiao.Location = new System.Drawing.Point(180, 273);
            this.txtSoLuongGiao.Name = "txtSoLuongGiao";
            this.txtSoLuongGiao.ReadOnly = true;
            this.txtSoLuongGiao.Size = new System.Drawing.Size(280, 27);
            this.txtSoLuongGiao.TabIndex = 7;
            this.txtSoLuongGiao.TextChanged += new System.EventHandler(this.txtSoLuongGiao_TextChanged);
            // 
            // lblThoiGianKhoiHanh
            // 
            this.lblThoiGianKhoiHanh.AutoSize = true;
            this.lblThoiGianKhoiHanh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblThoiGianKhoiHanh.Location = new System.Drawing.Point(30, 316);
            this.lblThoiGianKhoiHanh.Name = "lblThoiGianKhoiHanh";
            this.lblThoiGianKhoiHanh.Size = new System.Drawing.Size(147, 20);
            this.lblThoiGianKhoiHanh.TabIndex = 8;
            this.lblThoiGianKhoiHanh.Text = "Thời gian giao (*):";
            // 
            // dtpThoiGianKhoiHanh
            // 
            this.dtpThoiGianKhoiHanh.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpThoiGianKhoiHanh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpThoiGianKhoiHanh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThoiGianKhoiHanh.Location = new System.Drawing.Point(180, 313);
            this.dtpThoiGianKhoiHanh.Name = "dtpThoiGianKhoiHanh";
            this.dtpThoiGianKhoiHanh.Size = new System.Drawing.Size(280, 27);
            this.dtpThoiGianKhoiHanh.TabIndex = 8;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.Location = new System.Drawing.Point(30, 356);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(84, 20);
            this.lblTrangThai.TabIndex = 9;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.txtTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.txtTrangThai.Location = new System.Drawing.Point(180, 353);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.ReadOnly = true;
            this.txtTrangThai.Size = new System.Drawing.Size(280, 27);
            this.txtTrangThai.TabIndex = 9;
            this.txtTrangThai.Text = "Khởi tạo";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(240, 405);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(105, 36);
            this.btnLuu.TabIndex = 10;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(245)))));
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.btnHuy.Location = new System.Drawing.Point(355, 405);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(105, 36);
            this.btnHuy.TabIndex = 11;
            this.btnHuy.Text = "❌ Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrmThemDonVanChuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(490, 465);
            this.Controls.Add(this.lblTaiTrongXe);
            this.Controls.Add(this.txtDiaChiGiao);
            this.Controls.Add(this.lblDiaChiGiao);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.dtpThoiGianKhoiHanh);
            this.Controls.Add(this.lblThoiGianKhoiHanh);
            this.Controls.Add(this.txtSoLuongGiao);
            this.Controls.Add(this.lblSoLuongGiao);
            this.Controls.Add(this.cboSanPham);
            this.Controls.Add(this.lblSanPham);
            this.Controls.Add(this.cboBienSoXe);
            this.Controls.Add(this.lblBienSoXe);
            this.Controls.Add(this.cboMaDVC);
            this.Controls.Add(this.lblMaDVC);
            this.Controls.Add(this.txtIDDonVC);
            this.Controls.Add(this.lblIDDonVC);
            this.Controls.Add(this.cboDonHang);
            this.Controls.Add(this.lblDonHang);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmThemDonVanChuyen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Đơn Cần Vận Chuyển";
            this.Load += new System.EventHandler(this.FrmThemDonVanChuyen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblDonHang;
        private System.Windows.Forms.ComboBox cboDonHang;
        private System.Windows.Forms.Label lblIDDonVC;
        private System.Windows.Forms.TextBox txtIDDonVC;
        private System.Windows.Forms.Label lblMaDVC;
        private System.Windows.Forms.ComboBox cboMaDVC;
        private System.Windows.Forms.Label lblDiaChiGiao;
        private System.Windows.Forms.TextBox txtDiaChiGiao;
        private System.Windows.Forms.Label lblBienSoXe;
        private System.Windows.Forms.ComboBox cboBienSoXe;
        private System.Windows.Forms.Label lblTaiTrongXe;
        private System.Windows.Forms.Label lblSanPham;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblSoLuongGiao;
        private System.Windows.Forms.TextBox txtSoLuongGiao;
        private System.Windows.Forms.Label lblThoiGianKhoiHanh;
        private System.Windows.Forms.DateTimePicker dtpThoiGianKhoiHanh;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}

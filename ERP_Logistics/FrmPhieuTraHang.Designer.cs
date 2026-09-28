namespace ERP
{
    partial class FrmPhieuTraHang
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.cboID_CTYC = new System.Windows.Forms.ComboBox();
            this.lblID_CTYC = new System.Windows.Forms.Label();
            
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtID_PhieuTra = new System.Windows.Forms.TextBox();
            this.lblNgayTra = new System.Windows.Forms.Label();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.lblLoaiXe = new System.Windows.Forms.Label();
            this.cboLoaiXe = new System.Windows.Forms.ComboBox();
            this.lblTrongTai = new System.Windows.Forms.Label();
            this.cboTrongTai = new System.Windows.Forms.ComboBox();
            this.lblBienSoXe = new System.Windows.Forms.Label();
            this.cboBienSoXe = new System.Windows.Forms.ComboBox();
            this.btnDatLaiXe = new System.Windows.Forms.Button();
            this.lblTaiXe = new System.Windows.Forms.Label();
            this.txtTaiXe = new System.Windows.Forms.TextBox();
            this.lblMaDVC = new System.Windows.Forms.Label();
            this.cboMaDVC = new System.Windows.Forms.ComboBox();
            this.lblTenDVC = new System.Windows.Forms.Label();
            this.cboTenDVC = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            
            // groupBox1
            this.groupBox1.Controls.Add(this.cboID_CTYC);
            this.groupBox1.Controls.Add(this.lblID_CTYC);
            this.groupBox1.Controls.Add(this.txtTenSP);
            this.groupBox1.Controls.Add(this.lblTenSP);
            this.groupBox1.Controls.Add(this.txtSoLuong);
            this.groupBox1.Controls.Add(this.lblSoLuong);
            this.groupBox1.Controls.Add(this.txtLyDo);
            this.groupBox1.Controls.Add(this.lblLyDo);
            this.groupBox1.Controls.Add(this.txtKhachHang);
            this.groupBox1.Controls.Add(this.lblKhachHang);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(20, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 260);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin yêu cầu (Chỉ đọc)";
            
            // lblID_CTYC
            this.lblID_CTYC.AutoSize = true;
            this.lblID_CTYC.Location = new System.Drawing.Point(20, 38);
            this.lblID_CTYC.Name = "lblID_CTYC";
            this.lblID_CTYC.Size = new System.Drawing.Size(133, 20);
            this.lblID_CTYC.TabIndex = 0;
            this.lblID_CTYC.Text = "Chi tiết yêu cầu *";
            
            // cboID_CTYC
            this.cboID_CTYC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboID_CTYC.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboID_CTYC.Location = new System.Drawing.Point(165, 35);
            this.cboID_CTYC.Name = "cboID_CTYC";
            this.cboID_CTYC.Size = new System.Drawing.Size(495, 29);
            this.cboID_CTYC.TabIndex = 1;
            this.cboID_CTYC.SelectedIndexChanged += new System.EventHandler(this.cboID_CTYC_SelectedIndexChanged);
            
            // lblTenSP
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Location = new System.Drawing.Point(20, 83);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(106, 20);
            this.lblTenSP.TabIndex = 2;
            this.lblTenSP.Text = "Tên sản phẩm";
            
            // txtTenSP
            this.txtTenSP.BackColor = System.Drawing.Color.LightGray;
            this.txtTenSP.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTenSP.Location = new System.Drawing.Point(165, 80);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.ReadOnly = true;
            this.txtTenSP.Size = new System.Drawing.Size(495, 29);
            this.txtTenSP.TabIndex = 3;
            
            // lblSoLuong
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(20, 128);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(71, 20);
            this.lblSoLuong.TabIndex = 4;
            this.lblSoLuong.Text = "Số lượng";
            
            // txtSoLuong
            this.txtSoLuong.BackColor = System.Drawing.Color.LightGray;
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSoLuong.Location = new System.Drawing.Point(165, 125);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.ReadOnly = true;
            this.txtSoLuong.Size = new System.Drawing.Size(495, 29);
            this.txtSoLuong.TabIndex = 5;
            
            // lblLyDo
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(20, 173);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(65, 20);
            this.lblLyDo.TabIndex = 6;
            this.lblLyDo.Text = "Lý do lỗi";
            
            // txtLyDo
            this.txtLyDo.BackColor = System.Drawing.Color.LightGray;
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLyDo.Location = new System.Drawing.Point(165, 170);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.ReadOnly = true;
            this.txtLyDo.Size = new System.Drawing.Size(495, 29);
            this.txtLyDo.TabIndex = 7;
            
            // lblKhachHang
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(20, 218);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(90, 20);
            this.lblKhachHang.TabIndex = 8;
            this.lblKhachHang.Text = "Khách hàng";
            
            // txtKhachHang
            this.txtKhachHang.BackColor = System.Drawing.Color.LightGray;
            this.txtKhachHang.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtKhachHang.Location = new System.Drawing.Point(165, 215);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.Size = new System.Drawing.Size(495, 29);
            this.txtKhachHang.TabIndex = 9;
            
            // groupBox2
            this.groupBox2.Controls.Add(this.cboTrangThai);
            this.groupBox2.Controls.Add(this.lblTrangThai);
            this.groupBox2.Controls.Add(this.cboTenDVC);
            this.groupBox2.Controls.Add(this.lblTenDVC);
            this.groupBox2.Controls.Add(this.cboMaDVC);
            this.groupBox2.Controls.Add(this.lblMaDVC);
            this.groupBox2.Controls.Add(this.txtTaiXe);
            this.groupBox2.Controls.Add(this.lblTaiXe);
            this.groupBox2.Controls.Add(this.btnDatLaiXe);
            this.groupBox2.Controls.Add(this.cboBienSoXe);
            this.groupBox2.Controls.Add(this.lblBienSoXe);
            this.groupBox2.Controls.Add(this.cboTrongTai);
            this.groupBox2.Controls.Add(this.lblTrongTai);
            this.groupBox2.Controls.Add(this.cboLoaiXe);
            this.groupBox2.Controls.Add(this.lblLoaiXe);
            this.groupBox2.Controls.Add(this.dtpNgayTra);
            this.groupBox2.Controls.Add(this.lblNgayTra);
            this.groupBox2.Controls.Add(this.txtID_PhieuTra);
            this.groupBox2.Controls.Add(this.lblID);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(20, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(680, 480);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lệnh điều xe";
            
            // lblID
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(20, 38);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(117, 20);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "Mã Phiếu Trả *";
            
            // txtID_PhieuTra
            this.txtID_PhieuTra.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtID_PhieuTra.Location = new System.Drawing.Point(165, 35);
            this.txtID_PhieuTra.Name = "txtID_PhieuTra";
            this.txtID_PhieuTra.Size = new System.Drawing.Size(495, 29);
            this.txtID_PhieuTra.TabIndex = 1;
            
            // lblNgayTra
            this.lblNgayTra.AutoSize = true;
            this.lblNgayTra.Location = new System.Drawing.Point(20, 83);
            this.lblNgayTra.Name = "lblNgayTra";
            this.lblNgayTra.Size = new System.Drawing.Size(71, 20);
            this.lblNgayTra.TabIndex = 2;
            this.lblNgayTra.Text = "Ngày trả";
            
            // dtpNgayTra
            this.dtpNgayTra.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayTra.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayTra.Location = new System.Drawing.Point(165, 80);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(495, 29);
            this.dtpNgayTra.TabIndex = 3;
            
            // lblLoaiXe
            this.lblLoaiXe.AutoSize = true;
            this.lblLoaiXe.Location = new System.Drawing.Point(20, 128);
            this.lblLoaiXe.Name = "lblLoaiXe";
            this.lblLoaiXe.Size = new System.Drawing.Size(69, 20);
            this.lblLoaiXe.TabIndex = 4;
            this.lblLoaiXe.Text = "Loại xe *";
            
            // cboLoaiXe
            this.cboLoaiXe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiXe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLoaiXe.Location = new System.Drawing.Point(165, 125);
            this.cboLoaiXe.Name = "cboLoaiXe";
            this.cboLoaiXe.Size = new System.Drawing.Size(495, 29);
            this.cboLoaiXe.TabIndex = 5;
            this.cboLoaiXe.SelectedIndexChanged += new System.EventHandler(this.cboLoaiXe_SelectedIndexChanged);

            // lblTrongTai
            this.lblTrongTai.AutoSize = true;
            this.lblTrongTai.Location = new System.Drawing.Point(20, 173);
            this.lblTrongTai.Name = "lblTrongTai";
            this.lblTrongTai.Size = new System.Drawing.Size(93, 20);
            this.lblTrongTai.TabIndex = 20;
            this.lblTrongTai.Text = "Trọng tải *";
            
            // cboTrongTai
            this.cboTrongTai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrongTai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboTrongTai.Location = new System.Drawing.Point(165, 170);
            this.cboTrongTai.Name = "cboTrongTai";
            this.cboTrongTai.Size = new System.Drawing.Size(495, 29);
            this.cboTrongTai.TabIndex = 21;
            this.cboTrongTai.SelectedIndexChanged += new System.EventHandler(this.cboTrongTai_SelectedIndexChanged);

            // lblBienSoXe
            this.lblBienSoXe.AutoSize = true;
            this.lblBienSoXe.Location = new System.Drawing.Point(20, 218);
            this.lblBienSoXe.Name = "lblBienSoXe";
            this.lblBienSoXe.Size = new System.Drawing.Size(93, 20);
            this.lblBienSoXe.TabIndex = 4;
            this.lblBienSoXe.Text = "Biển số xe *";
            
            // cboBienSoXe
            this.cboBienSoXe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBienSoXe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboBienSoXe.Location = new System.Drawing.Point(165, 215);
            this.cboBienSoXe.Name = "cboBienSoXe";
            this.cboBienSoXe.Size = new System.Drawing.Size(385, 29);
            this.cboBienSoXe.TabIndex = 5;
            this.cboBienSoXe.SelectedIndexChanged += new System.EventHandler(this.cboBienSoXe_SelectedIndexChanged);

            // btnDatLaiXe
            this.btnDatLaiXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnDatLaiXe.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDatLaiXe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDatLaiXe.Location = new System.Drawing.Point(560, 214);
            this.btnDatLaiXe.Name = "btnDatLaiXe";
            this.btnDatLaiXe.Size = new System.Drawing.Size(100, 31);
            this.btnDatLaiXe.TabIndex = 6;
            this.btnDatLaiXe.Text = "Đặt lại xe";
            this.btnDatLaiXe.UseVisualStyleBackColor = false;
            this.btnDatLaiXe.Click += new System.EventHandler(this.btnDatLaiXe_Click);

            // lblTaiXe
            this.lblTaiXe.AutoSize = true;
            this.lblTaiXe.Location = new System.Drawing.Point(20, 263);
            this.lblTaiXe.Name = "lblTaiXe";
            this.lblTaiXe.Size = new System.Drawing.Size(93, 20);
            this.lblTaiXe.TabIndex = 22;
            this.lblTaiXe.Text = "Tên tài xế";
            
            // txtTaiXe
            this.txtTaiXe.BackColor = System.Drawing.Color.LightGray;
            this.txtTaiXe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTaiXe.Location = new System.Drawing.Point(165, 260);
            this.txtTaiXe.Name = "txtTaiXe";
            this.txtTaiXe.ReadOnly = true;
            this.txtTaiXe.Size = new System.Drawing.Size(495, 29);
            this.txtTaiXe.TabIndex = 23;
            
            // lblMaDVC
            this.lblMaDVC.AutoSize = true;
            this.lblMaDVC.Location = new System.Drawing.Point(20, 308);
            this.lblMaDVC.Name = "lblMaDVC";
            this.lblMaDVC.Size = new System.Drawing.Size(126, 20);
            this.lblMaDVC.TabIndex = 6;
            this.lblMaDVC.Text = "Mã điểm VC *";
            
            // cboMaDVC
            this.cboMaDVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaDVC.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboMaDVC.Location = new System.Drawing.Point(165, 305);
            this.cboMaDVC.Name = "cboMaDVC";
            this.cboMaDVC.Size = new System.Drawing.Size(495, 29);
            this.cboMaDVC.TabIndex = 7;
            this.cboMaDVC.SelectedIndexChanged += new System.EventHandler(this.cboMaDVC_SelectedIndexChanged);
            
            // lblTenDVC
            this.lblTenDVC.AutoSize = true;
            this.lblTenDVC.Location = new System.Drawing.Point(20, 353);
            this.lblTenDVC.Name = "lblTenDVC";
            this.lblTenDVC.Size = new System.Drawing.Size(126, 20);
            this.lblTenDVC.TabIndex = 14;
            this.lblTenDVC.Text = "Tên điểm VC";
            
            // cboTenDVC
            this.cboTenDVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTenDVC.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboTenDVC.Location = new System.Drawing.Point(165, 350);
            this.cboTenDVC.Name = "cboTenDVC";
            this.cboTenDVC.Size = new System.Drawing.Size(495, 29);
            this.cboTenDVC.TabIndex = 15;
            this.cboTenDVC.SelectedIndexChanged += new System.EventHandler(this.cboTenDVC_SelectedIndexChanged);
            
            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(20, 398);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(91, 20);
            this.lblTrangThai.TabIndex = 12;
            this.lblTrangThai.Text = "Trạng thái *";
            
            // cboTrangThai
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboTrangThai.Location = new System.Drawing.Point(165, 395);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(495, 29);
            this.cboTrangThai.TabIndex = 13;
            
            // btnLuu
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(245, 780);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(105, 36);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            
            // btnHuy
            this.btnHuy.BackColor = System.Drawing.Color.Crimson;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(370, 780);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(105, 36);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            
            // FrmPhieuTraHang
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(720, 835);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPhieuTraHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lệnh Điều Xe Thu Hồi Hàng";
            this.Load += new System.EventHandler(this.FrmPhieuTraHang_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.TextBox txtKhachHang;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label lblID_CTYC;
        private System.Windows.Forms.ComboBox cboID_CTYC;
        
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID_PhieuTra;
        private System.Windows.Forms.Label lblNgayTra;
        private System.Windows.Forms.DateTimePicker dtpNgayTra;
        private System.Windows.Forms.Label lblBienSoXe;
        private System.Windows.Forms.ComboBox cboBienSoXe;
        private System.Windows.Forms.Button btnDatLaiXe;
        private System.Windows.Forms.Label lblLoaiXe;
        private System.Windows.Forms.ComboBox cboLoaiXe;
        private System.Windows.Forms.Label lblMaDVC;
        private System.Windows.Forms.ComboBox cboMaDVC;
        private System.Windows.Forms.Label lblTenDVC;
        private System.Windows.Forms.ComboBox cboTenDVC;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        
        private System.Windows.Forms.Label lblTrongTai;
        private System.Windows.Forms.ComboBox cboTrongTai;
        private System.Windows.Forms.Label lblTaiXe;
        private System.Windows.Forms.TextBox txtTaiXe;
        
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}


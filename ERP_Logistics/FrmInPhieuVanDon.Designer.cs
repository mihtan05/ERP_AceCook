namespace ERP
{
    partial class FrmInPhieuVanDon
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
            this.pnlPaper = new System.Windows.Forms.Panel();
            this.pnlTopBorder = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.lblMaDon = new System.Windows.Forms.Label();
            this.lblNgayGio = new System.Windows.Forms.Label();
            this.grpKhachHang = new System.Windows.Forms.GroupBox();
            this.lblValDiaChi = new System.Windows.Forms.Label();
            this.lblLblDiaChi = new System.Windows.Forms.Label();
            this.lblValSDT = new System.Windows.Forms.Label();
            this.lblLblSDT = new System.Windows.Forms.Label();
            this.lblValKhachHang = new System.Windows.Forms.Label();
            this.lblLblKhachHang = new System.Windows.Forms.Label();
            this.lblValMaDH = new System.Windows.Forms.Label();
            this.lblLblMaDH = new System.Windows.Forms.Label();
            this.grpVanChuyen = new System.Windows.Forms.GroupBox();
            this.lblValTrangThai = new System.Windows.Forms.Label();
            this.lblLblTrangThai = new System.Windows.Forms.Label();
            this.lblValDiemVC = new System.Windows.Forms.Label();
            this.lblLblDiemVC = new System.Windows.Forms.Label();
            this.lblValBienSo = new System.Windows.Forms.Label();
            this.lblLblBienSo = new System.Windows.Forms.Label();
            this.grpHangHoa = new System.Windows.Forms.GroupBox();
            this.dgvHangHoa = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlKyTen = new System.Windows.Forms.Panel();
            this.lblKyNguoiLap = new System.Windows.Forms.Label();
            this.lblSubNguoiLap = new System.Windows.Forms.Label();
            this.lblKyTaiXe = new System.Windows.Forms.Label();
            this.lblSubTaiXe = new System.Windows.Forms.Label();
            this.lblKyNguoiNhan = new System.Windows.Forms.Label();
            this.lblSubNguoiNhan = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlPaper.SuspendLayout();
            this.grpKhachHang.SuspendLayout();
            this.grpVanChuyen.SuspendLayout();
            this.grpHangHoa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangHoa)).BeginInit();
            this.pnlKyTen.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPaper
            // 
            this.pnlPaper.BackColor = System.Drawing.Color.White;
            this.pnlPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPaper.Controls.Add(this.pnlTopBorder);
            this.pnlPaper.Controls.Add(this.lblHeaderTitle);
            this.pnlPaper.Controls.Add(this.lblSubHeader);
            this.pnlPaper.Controls.Add(this.lblMaDon);
            this.pnlPaper.Controls.Add(this.lblNgayGio);
            this.pnlPaper.Controls.Add(this.grpKhachHang);
            this.pnlPaper.Controls.Add(this.grpVanChuyen);
            this.pnlPaper.Controls.Add(this.grpHangHoa);
            this.pnlPaper.Controls.Add(this.pnlKyTen);
            this.pnlPaper.Location = new System.Drawing.Point(20, 15);
            this.pnlPaper.Name = "pnlPaper";
            this.pnlPaper.Size = new System.Drawing.Size(680, 710);
            this.pnlPaper.TabIndex = 0;
            // 
            // pnlTopBorder
            // 
            this.pnlTopBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.pnlTopBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBorder.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBorder.Name = "pnlTopBorder";
            this.pnlTopBorder.Size = new System.Drawing.Size(678, 6);
            this.pnlTopBorder.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(10, 15);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(658, 35);
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "PHIẾU GIAO HÀNG & VẬN ĐƠN";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblSubHeader.ForeColor = System.Drawing.Color.Gray;
            this.lblSubHeader.Location = new System.Drawing.Point(10, 48);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(658, 20);
            this.lblSubHeader.TabIndex = 2;
            this.lblSubHeader.Text = "Hệ Thống Quản Lý Doanh Nghiệp ERP - Phân Hệ Logistics";
            this.lblSubHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMaDon
            // 
            this.lblMaDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblMaDon.Location = new System.Drawing.Point(20, 75);
            this.lblMaDon.Name = "lblMaDon";
            this.lblMaDon.Size = new System.Drawing.Size(320, 24);
            this.lblMaDon.TabIndex = 3;
            this.lblMaDon.Text = "MÃ VẬN ĐƠN: VC_001";
            // 
            // lblNgayGio
            // 
            this.lblNgayGio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayGio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.lblNgayGio.Location = new System.Drawing.Point(350, 75);
            this.lblNgayGio.Name = "lblNgayGio";
            this.lblNgayGio.Size = new System.Drawing.Size(310, 24);
            this.lblNgayGio.TabIndex = 4;
            this.lblNgayGio.Text = "Thời gian xuất phát: 23/09/2026 15:30";
            this.lblNgayGio.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpKhachHang
            // 
            this.grpKhachHang.Controls.Add(this.lblValDiaChi);
            this.grpKhachHang.Controls.Add(this.lblLblDiaChi);
            this.grpKhachHang.Controls.Add(this.lblValSDT);
            this.grpKhachHang.Controls.Add(this.lblLblSDT);
            this.grpKhachHang.Controls.Add(this.lblValKhachHang);
            this.grpKhachHang.Controls.Add(this.lblLblKhachHang);
            this.grpKhachHang.Controls.Add(this.lblValMaDH);
            this.grpKhachHang.Controls.Add(this.lblLblMaDH);
            this.grpKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.grpKhachHang.Location = new System.Drawing.Point(20, 105);
            this.grpKhachHang.Name = "grpKhachHang";
            this.grpKhachHang.Size = new System.Drawing.Size(640, 115);
            this.grpKhachHang.TabIndex = 5;
            this.grpKhachHang.TabStop = false;
            this.grpKhachHang.Text = "Thông Tin Người Nhận / Đơn Hàng";
            // 
            // lblValDiaChi
            // 
            this.lblValDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblValDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.lblValDiaChi.Location = new System.Drawing.Point(145, 82);
            this.lblValDiaChi.Name = "lblValDiaChi";
            this.lblValDiaChi.Size = new System.Drawing.Size(480, 24);
            this.lblValDiaChi.TabIndex = 7;
            this.lblValDiaChi.Text = "Số 123 Đường Cầu Giấy, Hà Nội";
            // 
            // lblLblDiaChi
            // 
            this.lblLblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(125)))));
            this.lblLblDiaChi.Location = new System.Drawing.Point(15, 82);
            this.lblLblDiaChi.Name = "lblLblDiaChi";
            this.lblLblDiaChi.Size = new System.Drawing.Size(125, 24);
            this.lblLblDiaChi.TabIndex = 6;
            this.lblLblDiaChi.Text = "Địa chỉ nhận:";
            // 
            // lblValSDT
            // 
            this.lblValSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblValSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.lblValSDT.Location = new System.Drawing.Point(445, 54);
            this.lblValSDT.Name = "lblValSDT";
            this.lblValSDT.Size = new System.Drawing.Size(180, 24);
            this.lblValSDT.TabIndex = 5;
            this.lblValSDT.Text = "0988.123.456";
            // 
            // lblLblSDT
            // 
            this.lblLblSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLblSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(125)))));
            this.lblLblSDT.Location = new System.Drawing.Point(340, 54);
            this.lblLblSDT.Name = "lblLblSDT";
            this.lblLblSDT.Size = new System.Drawing.Size(100, 24);
            this.lblLblSDT.TabIndex = 4;
            this.lblLblSDT.Text = "Số điện thoại:";
            // 
            // lblValKhachHang
            // 
            this.lblValKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblValKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.lblValKhachHang.Location = new System.Drawing.Point(145, 54);
            this.lblValKhachHang.Name = "lblValKhachHang";
            this.lblValKhachHang.Size = new System.Drawing.Size(180, 24);
            this.lblValKhachHang.TabIndex = 3;
            this.lblValKhachHang.Text = "Công ty TNHH ABC";
            // 
            // lblLblKhachHang
            // 
            this.lblLblKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLblKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(125)))));
            this.lblLblKhachHang.Location = new System.Drawing.Point(15, 54);
            this.lblLblKhachHang.Name = "lblLblKhachHang";
            this.lblLblKhachHang.Size = new System.Drawing.Size(125, 24);
            this.lblLblKhachHang.TabIndex = 2;
            this.lblLblKhachHang.Text = "Khách hàng:";
            // 
            // lblValMaDH
            // 
            this.lblValMaDH.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblValMaDH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblValMaDH.Location = new System.Drawing.Point(145, 26);
            this.lblValMaDH.Name = "lblValMaDH";
            this.lblValMaDH.Size = new System.Drawing.Size(480, 24);
            this.lblValMaDH.TabIndex = 1;
            this.lblValMaDH.Text = "DH019 (Phân hệ Bán Hàng)";
            // 
            // lblLblMaDH
            // 
            this.lblLblMaDH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLblMaDH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(125)))));
            this.lblLblMaDH.Location = new System.Drawing.Point(15, 26);
            this.lblLblMaDH.Name = "lblLblMaDH";
            this.lblLblMaDH.Size = new System.Drawing.Size(125, 24);
            this.lblLblMaDH.TabIndex = 0;
            this.lblLblMaDH.Text = "Đơn hàng Bán:";
            // 
            // grpVanChuyen
            // 
            this.grpVanChuyen.Controls.Add(this.lblValTrangThai);
            this.grpVanChuyen.Controls.Add(this.lblLblTrangThai);
            this.grpVanChuyen.Controls.Add(this.lblValDiemVC);
            this.grpVanChuyen.Controls.Add(this.lblLblDiemVC);
            this.grpVanChuyen.Controls.Add(this.lblValBienSo);
            this.grpVanChuyen.Controls.Add(this.lblLblBienSo);
            this.grpVanChuyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpVanChuyen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.grpVanChuyen.Location = new System.Drawing.Point(20, 226);
            this.grpVanChuyen.Name = "grpVanChuyen";
            this.grpVanChuyen.Size = new System.Drawing.Size(640, 85);
            this.grpVanChuyen.TabIndex = 6;
            this.grpVanChuyen.TabStop = false;
            this.grpVanChuyen.Text = "Phương Tiện & Tuyến Đường";
            // 
            // lblValTrangThai
            // 
            this.lblValTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblValTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblValTrangThai.Location = new System.Drawing.Point(445, 26);
            this.lblValTrangThai.Name = "lblValTrangThai";
            this.lblValTrangThai.Size = new System.Drawing.Size(180, 24);
            this.lblValTrangThai.TabIndex = 5;
            this.lblValTrangThai.Text = "Đang vận chuyển";
            // 
            // lblLblTrangThai
            // 
            this.lblLblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(125)))));
            this.lblLblTrangThai.Location = new System.Drawing.Point(340, 26);
            this.lblLblTrangThai.Name = "lblLblTrangThai";
            this.lblLblTrangThai.Size = new System.Drawing.Size(100, 24);
            this.lblLblTrangThai.TabIndex = 4;
            this.lblLblTrangThai.Text = "Trạng thái đơn:";
            // 
            // lblValDiemVC
            // 
            this.lblValDiemVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblValDiemVC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.lblValDiemVC.Location = new System.Drawing.Point(145, 54);
            this.lblValDiemVC.Name = "lblValDiemVC";
            this.lblValDiemVC.Size = new System.Drawing.Size(480, 24);
            this.lblValDiemVC.TabIndex = 3;
            this.lblValDiemVC.Text = "DVC01 (Tuyến kho Hà Nội - Hải Phòng)";
            // 
            // lblLblDiemVC
            // 
            this.lblLblDiemVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLblDiemVC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(125)))));
            this.lblLblDiemVC.Location = new System.Drawing.Point(15, 54);
            this.lblLblDiemVC.Name = "lblLblDiemVC";
            this.lblLblDiemVC.Size = new System.Drawing.Size(125, 24);
            this.lblLblDiemVC.TabIndex = 2;
            this.lblLblDiemVC.Text = "Điểm giao nhận:";
            // 
            // lblValBienSo
            // 
            this.lblValBienSo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblValBienSo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.lblValBienSo.Location = new System.Drawing.Point(145, 26);
            this.lblValBienSo.Name = "lblValBienSo";
            this.lblValBienSo.Size = new System.Drawing.Size(180, 24);
            this.lblValBienSo.TabIndex = 1;
            this.lblValBienSo.Text = "29C-123.45 (Xe 2.5 Tấn)";
            // 
            // lblLblBienSo
            // 
            this.lblLblBienSo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLblBienSo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(125)))));
            this.lblLblBienSo.Location = new System.Drawing.Point(15, 26);
            this.lblLblBienSo.Name = "lblLblBienSo";
            this.lblLblBienSo.Size = new System.Drawing.Size(125, 24);
            this.lblLblBienSo.TabIndex = 0;
            this.lblLblBienSo.Text = "Biển số xe:";
            // 
            // grpHangHoa
            // 
            this.grpHangHoa.Controls.Add(this.dgvHangHoa);
            this.grpHangHoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpHangHoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.grpHangHoa.Location = new System.Drawing.Point(20, 318);
            this.grpHangHoa.Name = "grpHangHoa";
            this.grpHangHoa.Size = new System.Drawing.Size(640, 180);
            this.grpHangHoa.TabIndex = 7;
            this.grpHangHoa.TabStop = false;
            this.grpHangHoa.Text = "Chi Tiết Hàng Hóa Vận Chuyển";
            // 
            // dgvHangHoa
            // 
            this.dgvHangHoa.AllowUserToAddRows = false;
            this.dgvHangHoa.AllowUserToDeleteRows = false;
            this.dgvHangHoa.AllowUserToResizeRows = false;
            this.dgvHangHoa.BackgroundColor = System.Drawing.Color.White;
            this.dgvHangHoa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHangHoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHangHoa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colMaSP,
            this.colTenHang,
            this.colSoLuong});
            this.dgvHangHoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHangHoa.Location = new System.Drawing.Point(3, 23);
            this.dgvHangHoa.Name = "dgvHangHoa";
            this.dgvHangHoa.ReadOnly = true;
            this.dgvHangHoa.RowHeadersVisible = false;
            this.dgvHangHoa.RowHeadersWidth = 51;
            this.dgvHangHoa.RowTemplate.Height = 28;
            this.dgvHangHoa.Size = new System.Drawing.Size(634, 154);
            this.dgvHangHoa.TabIndex = 0;
            // 
            // colSTT
            // 
            this.colSTT.HeaderText = "STT";
            this.colSTT.MinimumWidth = 6;
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            this.colSTT.Width = 50;
            // 
            // colMaSP
            // 
            this.colMaSP.HeaderText = "MÃ SP";
            this.colMaSP.MinimumWidth = 6;
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.ReadOnly = true;
            this.colMaSP.Width = 110;
            // 
            // colTenHang
            // 
            this.colTenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenHang.HeaderText = "TÊN HÀNG HÓA";
            this.colTenHang.MinimumWidth = 6;
            this.colTenHang.Name = "colTenHang";
            this.colTenHang.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.HeaderText = "SL GIAO";
            this.colSoLuong.MinimumWidth = 6;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            this.colSoLuong.Width = 140;
            // 
            // pnlKyTen
            // 
            this.pnlKyTen.Controls.Add(this.lblKyNguoiLap);
            this.pnlKyTen.Controls.Add(this.lblSubNguoiLap);
            this.pnlKyTen.Controls.Add(this.lblKyTaiXe);
            this.pnlKyTen.Controls.Add(this.lblSubTaiXe);
            this.pnlKyTen.Controls.Add(this.lblKyNguoiNhan);
            this.pnlKyTen.Controls.Add(this.lblSubNguoiNhan);
            this.pnlKyTen.Location = new System.Drawing.Point(20, 510);
            this.pnlKyTen.Name = "pnlKyTen";
            this.pnlKyTen.Size = new System.Drawing.Size(640, 180);
            this.pnlKyTen.TabIndex = 8;
            // 
            // lblKyNguoiLap
            // 
            this.lblKyNguoiLap.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKyNguoiLap.Location = new System.Drawing.Point(10, 10);
            this.lblKyNguoiLap.Name = "lblKyNguoiLap";
            this.lblKyNguoiLap.Size = new System.Drawing.Size(190, 24);
            this.lblKyNguoiLap.TabIndex = 0;
            this.lblKyNguoiLap.Text = "NGƯỜI LẬP PHIẾU";
            this.lblKyNguoiLap.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSubNguoiLap
            // 
            this.lblSubNguoiLap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblSubNguoiLap.ForeColor = System.Drawing.Color.Gray;
            this.lblSubNguoiLap.Location = new System.Drawing.Point(10, 34);
            this.lblSubNguoiLap.Name = "lblSubNguoiLap";
            this.lblSubNguoiLap.Size = new System.Drawing.Size(190, 20);
            this.lblSubNguoiLap.TabIndex = 1;
            this.lblSubNguoiLap.Text = "(Ký và ghi rõ họ tên)";
            this.lblSubNguoiLap.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblKyTaiXe
            // 
            this.lblKyTaiXe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKyTaiXe.Location = new System.Drawing.Point(225, 10);
            this.lblKyTaiXe.Name = "lblKyTaiXe";
            this.lblKyTaiXe.Size = new System.Drawing.Size(190, 24);
            this.lblKyTaiXe.TabIndex = 2;
            this.lblKyTaiXe.Text = "TÀI XẾ / NGƯỜI GIAO";
            this.lblKyTaiXe.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSubTaiXe
            // 
            this.lblSubTaiXe.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblSubTaiXe.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTaiXe.Location = new System.Drawing.Point(225, 34);
            this.lblSubTaiXe.Name = "lblSubTaiXe";
            this.lblSubTaiXe.Size = new System.Drawing.Size(190, 20);
            this.lblSubTaiXe.TabIndex = 3;
            this.lblSubTaiXe.Text = "(Ký và ghi rõ họ tên)";
            this.lblSubTaiXe.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblKyNguoiNhan
            // 
            this.lblKyNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKyNguoiNhan.Location = new System.Drawing.Point(440, 10);
            this.lblKyNguoiNhan.Name = "lblKyNguoiNhan";
            this.lblKyNguoiNhan.Size = new System.Drawing.Size(190, 24);
            this.lblKyNguoiNhan.TabIndex = 4;
            this.lblKyNguoiNhan.Text = "NGƯỜI NHẬN HÀNG";
            this.lblKyNguoiNhan.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSubNguoiNhan
            // 
            this.lblSubNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblSubNguoiNhan.ForeColor = System.Drawing.Color.Gray;
            this.lblSubNguoiNhan.Location = new System.Drawing.Point(440, 34);
            this.lblSubNguoiNhan.Name = "lblSubNguoiNhan";
            this.lblSubNguoiNhan.Size = new System.Drawing.Size(190, 20);
            this.lblSubNguoiNhan.TabIndex = 5;
            this.lblSubNguoiNhan.Text = "(Ký nhận đã đủ hàng)";
            this.lblSubNguoiNhan.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnPrint);
            this.pnlActions.Controls.Add(this.btnClose);
            this.pnlActions.Location = new System.Drawing.Point(20, 735);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(680, 50);
            this.pnlActions.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(380, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 38);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "🖨️ In Phiếu (PDF)";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(545, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(125, 38);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "❌ Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmInPhieuVanDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(720, 795);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlPaper);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmInPhieuVanDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem Trước & In Phiếu Vận Đơn Giao Hàng";
            this.pnlPaper.ResumeLayout(false);
            this.grpKhachHang.ResumeLayout(false);
            this.grpVanChuyen.ResumeLayout(false);
            this.grpHangHoa.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangHoa)).EndInit();
            this.pnlKyTen.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlPaper;
        private System.Windows.Forms.Panel pnlTopBorder;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.Label lblMaDon;
        private System.Windows.Forms.Label lblNgayGio;
        private System.Windows.Forms.GroupBox grpKhachHang;
        private System.Windows.Forms.Label lblLblMaDH;
        private System.Windows.Forms.Label lblValMaDH;
        private System.Windows.Forms.Label lblLblKhachHang;
        private System.Windows.Forms.Label lblValKhachHang;
        private System.Windows.Forms.Label lblLblSDT;
        private System.Windows.Forms.Label lblValSDT;
        private System.Windows.Forms.Label lblLblDiaChi;
        private System.Windows.Forms.Label lblValDiaChi;
        private System.Windows.Forms.GroupBox grpVanChuyen;
        private System.Windows.Forms.Label lblLblBienSo;
        private System.Windows.Forms.Label lblValBienSo;
        private System.Windows.Forms.Label lblLblDiemVC;
        private System.Windows.Forms.Label lblValDiemVC;
        private System.Windows.Forms.Label lblLblTrangThai;
        private System.Windows.Forms.Label lblValTrangThai;
        private System.Windows.Forms.GroupBox grpHangHoa;
        private System.Windows.Forms.DataGridView dgvHangHoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.Panel pnlKyTen;
        private System.Windows.Forms.Label lblKyNguoiLap;
        private System.Windows.Forms.Label lblSubNguoiLap;
        private System.Windows.Forms.Label lblKyTaiXe;
        private System.Windows.Forms.Label lblSubTaiXe;
        private System.Windows.Forms.Label lblKyNguoiNhan;
        private System.Windows.Forms.Label lblSubNguoiNhan;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
    }
}

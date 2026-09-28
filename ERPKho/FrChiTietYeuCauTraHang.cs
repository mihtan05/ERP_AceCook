using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrChiTietYeuCauTraHang : Form
    {
        private string _maYC;
        private string _boPhan;
        private string _nguoiGui;
        private string _maLo;
        private string _tenSP;
        private string _soLuong;
        private string _lyDo;
        private string _ngayGui;
        private string _trangThai;
        private string _ghiChu;

        public FrChiTietYeuCauTraHang(string maYC, string boPhan, string nguoiGui, string maLo, string tenSP, string soLuong, string lyDo, string ngayGui, string trangThai, string ghiChu)
        {
            InitializeComponent();
            _maYC = maYC;
            _boPhan = boPhan;
            _nguoiGui = nguoiGui;
            _maLo = maLo;
            _tenSP = tenSP;
            _soLuong = soLuong;
            _lyDo = lyDo;
            _ngayGui = ngayGui;
            _trangThai = trangThai;
            _ghiChu = ghiChu;
        }

        private void FrChiTietYeuCauTraHang_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Tài khoản của bạn không có quyền xem chi tiết yêu cầu!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            lblMaYCVal.Text = _maYC;
            lblBoPhanVal.Text = _boPhan;
            lblNguoiGuiVal.Text = _nguoiGui;
            lblNgayGuiVal.Text = _ngayGui;
            lblTrangThaiVal.Text = _trangThai;
            txtMoTaGhiChu.Text = _ghiChu;

            LoadChiTietThucTeTuCSDL();
        }

        private bool IsPermittedUser()
        {
            string chucVu = UserSession.ChucVu ?? "";
            return chucVu.Equals("Nhân viên kho", StringComparison.OrdinalIgnoreCase) ||
                   chucVu.Equals("Quản lý kho", StringComparison.OrdinalIgnoreCase) ||
                   chucVu.Equals("Quản lý", StringComparison.OrdinalIgnoreCase) ||
                   chucVu.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                   chucVu.Equals("Quản trị viên", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Truy vấn 100% dữ liệu thực từ CSDL PostgreSQL (Neon Cloud)
        /// </summary>
        private void LoadChiTietThucTeTuCSDL()
        {
            try
            {
                string sqlChiTiet = @"
                    SELECT 
                        sp.id_sp AS ""MaLoHang"",
                        hh.tenhang AS ""TenSanPham"",
                        ctyc.soluong AS ""SoLuong"",
                        COALESCE(ctyc.tinhtrang, yc.mota) AS ""TinhTrang""
                    FROM chitietyeucau ctyc
                    INNER JOIN yeucausaubanhang yc ON ctyc.id_yc = yc.id_yc
                    INNER JOIN sanpham sp ON ctyc.id_sp = sp.id_sp
                    INNER JOIN hanghoa hh ON sp.mahang = hh.mahang
                    WHERE ctyc.id_yc = @MaYC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaYC", _maYC)
                };

                DataTable dtMatHang = DatabaseHelper.ExecuteQuery(sqlChiTiet, parameters);
                dgvChiTiet.DataSource = dtMatHang;
                FormatGridChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL khi tải chi tiết: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGridChiTiet()
        {
            if (dgvChiTiet.Columns.Contains("MaLoHang")) dgvChiTiet.Columns["MaLoHang"].HeaderText = "Mã SP / Lô";
            if (dgvChiTiet.Columns.Contains("TenSanPham")) dgvChiTiet.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            if (dgvChiTiet.Columns.Contains("SoLuong"))
            {
                dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvChiTiet.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvChiTiet.Columns["SoLuong"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgvChiTiet.Columns.Contains("TinhTrang")) dgvChiTiet.Columns["TinhTrang"].HeaderText = "Tình Trạng / Lý Do";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
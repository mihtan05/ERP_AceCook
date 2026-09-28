using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrQLTraHangVaTieuHuy : Form
    {
        private DataTable dtYeuCau;
        private DataTable dtLichSu;

        public FrQLTraHangVaTieuHuy()
        {
            InitializeComponent();
        }

        private void FrQLTraHangVaTieuHuy_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Tải dữ liệu lọc duy nhất trạng thái 'Đang thu hồi' cho Tab 1
        /// và trạng thái 'Đã xử lý' cho Tab 2 (Lịch sử)
        /// </summary>
        private void LoadData()
        {
            try
            {
                // 1. Tab 1: Lấy danh sách yêu cầu có trạng thái DUY NHẤT là 'Đang thu hồi' (Ép hiển thị 'Đang xử lý' trên giao diện)
                string sqlYeuCau = @"
                    SELECT 
                        yc.id_yc AS ""MaYeuCau"",
                        COALESCE(nv.phongban, 'Bộ phận Kinh Doanh') AS ""BoPhanGui"",
                        COALESCE(nv.tennv, kh.nguoidaidien, 'N/A') AS ""NguoiGui"",
                        sp.id_sp AS ""MaLoHang"",
                        hh.tenhang AS ""TenSanPhaM"",
                        ctyc.soluong AS ""SoLuong"",
                        COALESCE(ctyc.tinhtrang, yc.mota, '') AS ""LyDo"",
                        TO_CHAR(yc.ngayyeucau, 'DD/MM/YYYY') AS ""NgayGui"",
                        'Đang xử lý' AS ""TrangThai"",
                        yc.mota AS ""GhiChuChiTiet""
                    FROM yeucausaubanhang yc
                    JOIN chitietyeucau ctyc ON yc.id_yc = ctyc.id_yc
                    JOIN sanpham sp ON ctyc.id_sp = sp.id_sp
                    JOIN hanghoa hh ON sp.mahang = hh.mahang
                    LEFT JOIN khachhang kh ON yc.id_kh = kh.id_kh
                    LEFT JOIN nhanvien nv ON yc.id_nv = nv.id_nv
                    LEFT JOIN donhang dh ON yc.id_dh = dh.id_dh
                    LEFT JOIN phieutrahang pt ON ctyc.id_ctyc = pt.id_ctyc
                    WHERE yc.trangthai = 'Đang thu hồi' 
                       OR dh.trangthai = 'Đang thu hồi'
                       OR pt.trangthai = 'Đang thu hồi'
                    ORDER BY yc.ngayyeucau DESC";

                dtYeuCau = DatabaseHelper.ExecuteQuery(sqlYeuCau);
                dgvYeuCau.DataSource = dtYeuCau;
                FormatGridYeuCau();

                // 2. Tab 2: Lịch sử chỉ lấy các yêu cầu có trạng thái 'Đã xử lý'
                string sqlLichSu = @"
                    SELECT 
                        yc.id_yc AS ""MaYeuCau"",
                        COALESCE(nv.phongban, 'Bộ phận Kinh Doanh') AS ""BoPhanGui"",
                        sp.id_sp AS ""MaLoHang"",
                        hh.tenhang AS ""TenSanPhaM"",
                        ctyc.soluong AS ""SoLuong"",
                        COALESCE(ctyc.tinhtrang, yc.mota, '') AS ""LyDo"",
                        TO_CHAR(yc.ngayyeucau, 'DD/MM/YYYY') AS ""NgayXuLy"",
                        yc.trangthai AS ""KetQuaXuLy""
                    FROM yeucausaubanhang yc
                    JOIN chitietyeucau ctyc ON yc.id_yc = ctyc.id_yc
                    JOIN sanpham sp ON ctyc.id_sp = sp.id_sp
                    JOIN hanghoa hh ON sp.mahang = hh.mahang
                    LEFT JOIN nhanvien nv ON yc.id_nv = nv.id_nv
                    LEFT JOIN donhang dh ON yc.id_dh = dh.id_dh
                    LEFT JOIN phieutrahang pt ON ctyc.id_ctyc = pt.id_ctyc
                    WHERE yc.trangthai = 'Đã xử lý' 
                       OR dh.trangthai = 'Đã xử lý'
                       OR pt.trangthai = 'Đã xử lý'
                    ORDER BY yc.ngayyeucau DESC";

                dtLichSu = DatabaseHelper.ExecuteQuery(sqlLichSu);
                dgvLichSu.DataSource = dtLichSu;
                FormatGridLichSu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối CSDL và lấy danh sách yêu cầu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGridYeuCau()
        {
            if (dgvYeuCau.Columns.Contains("GhiChuChiTiet")) dgvYeuCau.Columns["GhiChuChiTiet"].Visible = false;

            if (dgvYeuCau.Columns.Contains("MaYeuCau")) dgvYeuCau.Columns["MaYeuCau"].HeaderText = "Mã Yêu Cầu";
            if (dgvYeuCau.Columns.Contains("BoPhanGui")) dgvYeuCau.Columns["BoPhanGui"].HeaderText = "Bộ Phận Gửi";
            if (dgvYeuCau.Columns.Contains("NguoiGui")) dgvYeuCau.Columns["NguoiGui"].HeaderText = "Người Gửi";
            if (dgvYeuCau.Columns.Contains("MaLoHang")) dgvYeuCau.Columns["MaLoHang"].HeaderText = "Mã SP / Lô";
            if (dgvYeuCau.Columns.Contains("TenSanPhaM")) dgvYeuCau.Columns["TenSanPhaM"].HeaderText = "Tên Sản Phẩm";
            if (dgvYeuCau.Columns.Contains("SoLuong"))
            {
                dgvYeuCau.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvYeuCau.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvYeuCau.Columns["SoLuong"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgvYeuCau.Columns.Contains("LyDo")) dgvYeuCau.Columns["LyDo"].HeaderText = "Lý Do / Tình Trạng";
            if (dgvYeuCau.Columns.Contains("NgayGui")) dgvYeuCau.Columns["NgayGui"].HeaderText = "Ngày Gửi";
            if (dgvYeuCau.Columns.Contains("TrangThai"))
            {
                dgvYeuCau.Columns["TrangThai"].HeaderText = "Trạng Thái";
                dgvYeuCau.Columns["TrangThai"].DefaultCellStyle.Font = new Font(dgvYeuCau.Font, FontStyle.Bold);
            }
        }

        private void FormatGridLichSu()
        {
            if (dgvLichSu.Columns.Contains("MaYeuCau")) dgvLichSu.Columns["MaYeuCau"].HeaderText = "Mã Yêu Cầu";
            if (dgvLichSu.Columns.Contains("BoPhanGui")) dgvLichSu.Columns["BoPhanGui"].HeaderText = "Bộ Phận Gửi";
            if (dgvLichSu.Columns.Contains("MaLoHang")) dgvLichSu.Columns["MaLoHang"].HeaderText = "Mã SP / Lô";
            if (dgvLichSu.Columns.Contains("TenSanPhaM")) dgvLichSu.Columns["TenSanPhaM"].HeaderText = "Tên Sản Phẩm";
            if (dgvLichSu.Columns.Contains("SoLuong"))
            {
                dgvLichSu.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvLichSu.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLichSu.Columns["SoLuong"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgvLichSu.Columns.Contains("LyDo")) dgvLichSu.Columns["LyDo"].HeaderText = "Lý Do";
            if (dgvLichSu.Columns.Contains("NgayXuLy")) dgvLichSu.Columns["NgayXuLy"].HeaderText = "Ngày Xử Lý";
            if (dgvLichSu.Columns.Contains("KetQuaXuLy"))
            {
                dgvLichSu.Columns["KetQuaXuLy"].HeaderText = "Kết Quả Xử Lý";
                dgvLichSu.Columns["KetQuaXuLy"].DefaultCellStyle.Font = new Font(dgvLichSu.Font, FontStyle.Bold);
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            XemChiTietDon();
        }

        private void dgvYeuCau_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                XemChiTietDon();
            }
        }

        private void XemChiTietDon()
        {
            if (dgvYeuCau.CurrentRow == null || dgvYeuCau.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvYeuCau.CurrentRow;

            string maYC = row.Cells["MaYeuCau"].Value?.ToString();
            string boPhan = row.Cells["BoPhanGui"].Value?.ToString();
            string nguoiGui = row.Cells["NguoiGui"]?.Value?.ToString() ?? "N/A";
            string maLo = row.Cells["MaLoHang"].Value?.ToString();
            string tenSP = row.Cells["TenSanPhaM"].Value?.ToString();
            string soLuong = row.Cells["SoLuong"].Value?.ToString();
            string lyDo = row.Cells["LyDo"].Value?.ToString();
            string ngayGui = row.Cells["NgayGui"].Value?.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString();
            string ghiChu = row.Cells["GhiChuChiTiet"]?.Value?.ToString() ?? "Không có";

            FrChiTietYeuCauTraHang frm = new FrChiTietYeuCauTraHang(maYC, boPhan, nguoiGui, maLo, tenSP, soLuong, lyDo, ngayGui, trangThai, ghiChu);
            frm.ShowDialog();
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

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Tài khoản của bạn không có quyền phê duyệt yêu cầu!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvYeuCau.CurrentRow == null || dgvYeuCau.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu cần phê duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = dgvYeuCau.CurrentRow.DataBoundItem as DataRowView;
            if (drv != null)
            {
                DataRow row = drv.Row;
                string maYC = row["MaYeuCau"].ToString();

                FrPheDuyetYeuCau frm = new FrPheDuyetYeuCau(row, dtLichSu);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string sqlUpdate = @"
                        UPDATE yeucausaubanhang SET trangthai = 'Đã xử lý' WHERE id_yc = @MaYC;
                        UPDATE donhang SET trangthai = 'Đã xử lý' WHERE id_dh = (SELECT id_dh FROM yeucausaubanhang WHERE id_yc = @MaYC AND id_dh IS NOT NULL);
                        UPDATE phieutrahang SET trangthai = 'Đã xử lý' WHERE id_ctyc IN (SELECT id_ctyc FROM chitietyeucau WHERE id_yc = @MaYC);";

                    DatabaseHelper.ExecuteNonQuery(sqlUpdate, new SqlParameter[] { new SqlParameter("@MaYC", maYC) });

                    LoadData();
                }
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Tài khoản của bạn không có quyền từ chối yêu cầu!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvYeuCau.CurrentRow == null || dgvYeuCau.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu cần từ chối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = dgvYeuCau.CurrentRow.DataBoundItem as DataRowView;
            if (drv != null)
            {
                DataRow row = drv.Row;
                string maYC = row["MaYeuCau"].ToString();

                FrTuChoiYeuCau frm = new FrTuChoiYeuCau(row, dtLichSu);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string sqlUpdate = @"
                        UPDATE yeucausaubanhang SET trangthai = 'Từ chối' WHERE id_yc = @MaYC;
                        UPDATE phieutrahang SET trangthai = 'Từ chối' WHERE id_ctyc IN (SELECT id_ctyc FROM chitietyeucau WHERE id_yc = @MaYC);";

                    DatabaseHelper.ExecuteNonQuery(sqlUpdate, new SqlParameter[] { new SqlParameter("@MaYC", maYC) });

                    LoadData();
                }
            }
        }
    }
}
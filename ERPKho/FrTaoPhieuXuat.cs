using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrTaoPhieuXuat : Form
    {
        private decimal _tonKhoHienTai = 0;

        public FrTaoPhieuXuat()
        {
            InitializeComponent();
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

        private void FrTaoPhieuXuat_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo phiếu xuất kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            // Rút ngắn định dạng mã phiếu để không vượt quá giới hạn VARCHAR(20) của CSDL
            txtMaPhieu.Text = "PX-" + DateTime.Now.ToString("yyMMddHHmmss");
            dtpNgayXuat.Value = DateTime.Now;

            LoadHangHoaDaNhapKhoThucTe();
            cboHangHoa.SelectedIndexChanged += cboHangHoa_SelectedIndexChanged;
        }

        /// <summary>
        /// Nạp danh sách lô hàng đã nhập kho, hỗ trợ chuẩn hóa TRIM/LOWER chống lệch dữ liệu CSDL
        /// </summary>
        private void LoadHangHoaDaNhapKhoThucTe()
        {
            try
            {
                string sql = @"
                    SELECT DISTINCT
                        lh.malo,
                        hh.mahang, 
                        hh.tenhang, 
                        lh.soluongcon,
                        COALESCE(hh.donvitinh, 'Kg') AS donvitinh,
                        (hh.tenhang || ' (Lô: ' || lh.malo || ' - Tồn: ' || CAST(lh.soluongcon AS VARCHAR) || ' ' || COALESCE(hh.donvitinh, 'Kg') || ')') AS hienthi
                    FROM phieunhap pn
                    INNER JOIN chitietphieunhap ctpn ON TRIM(pn.maphieunhap) = TRIM(ctpn.maphieunhap)
                    INNER JOIN lohang lh ON TRIM(ctpn.malo) = TRIM(lh.malo)
                    INNER JOIN hanghoa hh ON TRIM(lh.mahang) = TRIM(hh.mahang)
                    WHERE (TRIM(LOWER(pn.trangthai)) LIKE '%nhập kho%' OR TRIM(LOWER(pn.trangthai)) LIKE '%đã duyệt%')
                      AND TRIM(LOWER(pn.trangthai)) NOT LIKE '%hủy%'
                      AND lh.soluongcon > 0
                      AND TRIM(LOWER(COALESCE(lh.trangthai, ''))) <> 'phong tỏa'
                    ORDER BY hh.tenhang ASC, lh.malo ASC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                if (dt == null || dt.Rows.Count == 0)
                {
                    string sqlFallback = @"
                        SELECT 
                            lh.malo,
                            hh.mahang, 
                            hh.tenhang, 
                            lh.soluongcon,
                            COALESCE(hh.donvitinh, 'Kg') AS donvitinh,
                            (hh.tenhang || ' (Lô: ' || lh.malo || ' - Tồn: ' || CAST(lh.soluongcon AS VARCHAR) || ' ' || COALESCE(hh.donvitinh, 'Kg') || ')') AS hienthi
                        FROM lohang lh
                        INNER JOIN hanghoa hh ON TRIM(lh.mahang) = TRIM(hh.mahang)
                        WHERE lh.soluongcon > 0
                          AND TRIM(LOWER(COALESCE(lh.trangthai, ''))) <> 'phong tỏa'
                        ORDER BY hh.tenhang ASC, lh.malo ASC";

                    dt = DatabaseHelper.ExecuteQuery(sqlFallback);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    cboHangHoa.DataSource = dt;
                    cboHangHoa.DisplayMember = "hienthi";
                    cboHangHoa.ValueMember = "malo";
                    cboHangHoa.SelectedIndex = 0;
                    CapNhatThongTinTonKho();
                }
                else
                {
                    cboHangHoa.DataSource = null;
                    cboHangHoa.Text = "Không có mặt hàng/lô nào khả dụng!";
                    _tonKhoHienTai = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hàng hóa đã nhập kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatThongTinTonKho();
        }

        private void CapNhatThongTinTonKho()
        {
            if (cboHangHoa.SelectedItem is DataRowView drv)
            {
                if (drv["soluongcon"] != DBNull.Value)
                {
                    _tonKhoHienTai = Convert.ToDecimal(drv["soluongcon"]);
                    if (numSoLuong.Value > _tonKhoHienTai && _tonKhoHienTai > 0)
                    {
                        numSoLuong.Value = _tonKhoHienTai;
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string maPX = txtMaPhieu.Text.Trim();
            string boPhan = txtBoPhanYeuCau.Text.Trim();

            if (string.IsNullOrEmpty(maPX) || string.IsNullOrEmpty(boPhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã phiếu xuất và Bộ phận yêu cầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboHangHoa.SelectedValue == null || !(cboHangHoa.SelectedItem is DataRowView selectedRow))
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần xuất kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal soLuongXuat = numSoLuong.Value;
            if (soLuongXuat <= 0)
            {
                MessageBox.Show("Số lượng xuất phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (soLuongXuat > _tonKhoHienTai)
            {
                MessageBox.Show($"Số lượng xuất ({soLuongXuat:#,##0}) vượt quá số lượng tồn kho hiện tại ({_tonKhoHienTai:#,##0})!",
                                "Cảnh báo vượt tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSoLuong.Value = _tonKhoHienTai;
                numSoLuong.Focus();
                return;
            }

            try
            {
                string sqlPX = @"
                    INSERT INTO phieuxuat (maphieuxuat, ngaylap, bophanyeucau, nguoilap, trangthai)
                    VALUES (@MaPX, @NgayLap, @BoPhan, 'ND_ADMIN', 'Chờ duyệt')";

                SqlParameter[] pPX = new SqlParameter[]
                {
                    new SqlParameter("@MaPX", maPX),
                    new SqlParameter("@NgayLap", dtpNgayXuat.Value),
                    new SqlParameter("@BoPhan", boPhan)
                };

                DatabaseHelper.ExecuteNonQuery(sqlPX, pPX);

                string maHang = selectedRow["mahang"].ToString();
                string maLo = selectedRow["malo"].ToString();

                // Rút ngắn mã chi tiết phiếu xuất để vừa vặn với kiểu dữ liệu VARCHAR(20)
                string maCT = "CT-" + DateTime.Now.ToString("yyMMddHHmmss");

                string sqlCT = @"
                    INSERT INTO chitietphieuxuat (mactpx, maphieuxuat, mahang, soluongyeucau)
                    VALUES (@MaCT, @MaPX, @MaHang, @SoLuong)";

                SqlParameter[] pCT = new SqlParameter[]
                {
                    new SqlParameter("@MaCT", maCT),
                    new SqlParameter("@MaPX", maPX),
                    new SqlParameter("@MaHang", maHang),
                    new SqlParameter("@SoLuong", soLuongXuat)
                };

                DatabaseHelper.ExecuteNonQuery(sqlCT, pCT);

                MessageBox.Show($"Lập lệnh xuất kho [{maPX}] cho lô [{maLo}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu phiếu xuất: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
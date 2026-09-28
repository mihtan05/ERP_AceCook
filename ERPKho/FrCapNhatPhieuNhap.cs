using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrCapNhatPhieuNhap : Form
    {
        private readonly string _maPhieu;
        private readonly FrQLNhapKho _parentForm;

        public FrCapNhatPhieuNhap(string maPhieu, FrQLNhapKho parent)
        {
            InitializeComponent();
            _maPhieu = maPhieu;
            _parentForm = parent;
        }

        private void FrCapNhatPhieuNhap_Load(object sender, EventArgs e)
        {
            // Kiểm tra phân quyền: Từ chối truy cập và đóng form ngay nếu không có quyền
            if (!IsPermittedUser())
            {
                MessageBox.Show("Tài khoản của bạn không có quyền cập nhật phiếu nhập kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadComboboxLoaiNhap();
            LoadDataPhieuNhap();
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

        private void LoadComboboxLoaiNhap()
        {
            cboLoaiNhap.Items.Clear();
            cboLoaiNhap.Items.AddRange(new object[] { "Thành phẩm", "Nguyên liệu", "Bao bì", "Vật tư" });
        }

        private void LoadDataPhieuNhap()
        {
            try
            {
                string sql = @"
                    SELECT pn.maphieunhap, pn.loainhap, ctpn.soluong, ctpn.dongia, pn.trangthai
                    FROM phieunhap pn
                    LEFT JOIN chitietphieunhap ctpn ON pn.maphieunhap = ctpn.maphieunhap
                    WHERE pn.maphieunhap = @MaPhieu";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaPhieu", _maPhieu) });

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtMaPhieu.Text = dr["maphieunhap"].ToString();
                    txtSoLuong.Text = dr["soluong"] != DBNull.Value ? Convert.ToDecimal(dr["soluong"]).ToString("0") : "0";
                    txtDonGia.Text = dr["dongia"] != DBNull.Value ? Convert.ToDecimal(dr["dongia"]).ToString("0") : "0";

                    string loaiNhap = dr["loainhap"]?.ToString();
                    if (!string.IsNullOrEmpty(loaiNhap) && cboLoaiNhap.Items.Contains(loaiNhap))
                    {
                        cboLoaiNhap.SelectedItem = loaiNhap;
                    }

                    string trangThai = dr["trangthai"].ToString();
                    if (trangThai != "Chờ duyệt")
                    {
                        btnLuu.Enabled = false;
                        MessageBox.Show("Chỉ phiếu ở trạng thái 'Chờ duyệt' mới được phép chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền cập nhật phiếu nhập kho!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!decimal.TryParse(txtSoLuong.Text.Trim(), out decimal sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal gia) || gia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sqlCT = "UPDATE chitietphieunhap SET soluong = @SL, dongia = @Gia WHERE maphieunhap = @MaPN";
                DatabaseHelper.ExecuteNonQuery(sqlCT, new SqlParameter[] {
                    new SqlParameter("@SL", sl),
                    new SqlParameter("@Gia", gia),
                    new SqlParameter("@MaPN", _maPhieu)
                });

                string sqlPN = "UPDATE phieunhap SET loainhap = @LoaiNhap WHERE maphieunhap = @MaPN";
                DatabaseHelper.ExecuteNonQuery(sqlPN, new SqlParameter[] {
                    new SqlParameter("@LoaiNhap", cboLoaiNhap.SelectedItem?.ToString() ?? ""),
                    new SqlParameter("@MaPN", _maPhieu)
                });

                MessageBox.Show("Cập nhật phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
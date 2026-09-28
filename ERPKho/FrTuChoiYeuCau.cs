using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrTuChoiYeuCau : Form
    {
        private DataRow requestRow;
        private DataTable dtLichSu;

        public FrTuChoiYeuCau(DataRow row, DataTable lichSuTable)
        {
            InitializeComponent();
            requestRow = row;
            dtLichSu = lichSuTable;
        }

        private void FrTuChoiYeuCau_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Tài khoản của bạn không có quyền từ chối yêu cầu!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            if (requestRow != null)
            {
                txtThongTin.Text = $"Mã phiếu trả: {requestRow["MaYeuCau"]}\r\n" +
                                   $"Bên gửi: {requestRow["BoPhanGui"]}\r\n" +
                                   $"Sản phẩm: {requestRow["MaLoHang"]} - {requestRow["TenSanPham"]}\r\n" +
                                   $"Số lượng: {requestRow["SoLuong"]}\r\n" +
                                   $"Lý do đề xuất ban đầu: {requestRow["LyDo"]}";
            }
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

        private void btnXacNhanTuChoi_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (requestRow == null) return;

            string lyDoTuChoi = txtLyDoTuChoi.Text.Trim();

            if (string.IsNullOrWhiteSpace(lyDoTuChoi))
            {
                MessageBox.Show("Vui lòng nhập lý do từ chối trước khi xác nhận!",
                                "Cảnh báo thiếu thông tin",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtLyDoTuChoi.Focus();
                return;
            }

            string maPT = requestRow["MaYeuCau"].ToString();

            try
            {
                // Cập nhật trạng thái 'Từ chối' đồng bộ cho phieutrahang và yeucausaubanhang
                string sqlUpdate = @"
                    UPDATE phieutrahang 
                    SET trangthai = 'Từ chối' 
                    WHERE id_phieutra = @MaPT;

                    UPDATE yeucausaubanhang 
                    SET trangthai = 'Từ chối',
                        mota = COALESCE(mota, '') || @LyDoTuChoi
                    WHERE id_yc = @MaPT 
                       OR id_yc = (
                           SELECT ctyc.id_yc 
                           FROM chitietyeucau ctyc 
                           JOIN phieutrahang pt ON ctyc.id_ctyc = pt.id_ctyc 
                           WHERE pt.id_phieutra = @MaPT 
                           LIMIT 1
                       );";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LyDoTuChoi", " | Lý do từ chối: " + lyDoTuChoi),
                    new SqlParameter("@MaPT", maPT)
                };

                int result = DatabaseHelper.ExecuteNonQuery(sqlUpdate, parameters);

                if (result > 0)
                {
                    MessageBox.Show($"Đã từ chối phiếu trả [{maPT}] thành công!\nLý do từ chối: {lyDoTuChoi}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phiếu trả để từ chối!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi CSDL khi từ chối phiếu trả: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
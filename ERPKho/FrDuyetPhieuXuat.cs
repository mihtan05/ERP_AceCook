using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrDuyetPhieuXuat : Form
    {
        private DataRow targetRow;
        private string maPhieuXuat;

        public FrDuyetPhieuXuat(DataRow row)
        {
            InitializeComponent();
            targetRow = row;
            if (targetRow != null)
            {
                if (targetRow.Table.Columns.Contains("MaPhieuXuat"))
                    maPhieuXuat = targetRow["MaPhieuXuat"]?.ToString();
                else if (targetRow.Table.Columns.Contains("maPhieuXuat"))
                    maPhieuXuat = targetRow["maPhieuXuat"]?.ToString();
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

        private bool KiemTraQuyenQuanLyHoacAdmin(string tenChucNang)
        {
            string chucVu = UserSession.ChucVu ?? "";
            bool isToanQuyen = chucVu.Equals("Quản lý kho", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản lý", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản trị viên", StringComparison.OrdinalIgnoreCase);

            if (!isToanQuyen)
            {
                MessageBox.Show($"Tài khoản vai trò ({chucVu}) không có quyền thực hiện [{tenChucNang}]!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void FrDuyetPhieuXuat_Load(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Duyệt phiếu xuất kho"))
            {
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            if (targetRow != null)
            {
                string doiTac = targetRow.Table.Columns.Contains("DoiTacNhan") ? targetRow["DoiTacNhan"]?.ToString() : "";
                string maDonHang = targetRow.Table.Columns.Contains("MaDonHang") ? targetRow["MaDonHang"]?.ToString() : "";
                string ngayXuat = targetRow.Table.Columns.Contains("NgayXuat") && targetRow["NgayXuat"] != DBNull.Value ? Convert.ToDateTime(targetRow["NgayXuat"]).ToString("dd/MM/yyyy HH:mm") : "";
                string loaiXuat = targetRow.Table.Columns.Contains("LoaiXuat") ? targetRow["LoaiXuat"]?.ToString() : "";
                string nguoiLap = targetRow.Table.Columns.Contains("NguoiLap") ? targetRow["NguoiLap"]?.ToString() : "";
                string soLuong = targetRow.Table.Columns.Contains("SoLuong") ? targetRow["SoLuong"]?.ToString() : "";
                string trangThai = targetRow.Table.Columns.Contains("TrangThai") ? targetRow["TrangThai"]?.ToString() : "";

                lblInfo.Text = $"Đang xét duyệt phiếu: {maPhieuXuat} - Đơn vị nhận: {doiTac}";
                txtChiTiet.Text = $"Mã phiếu: {maPhieuXuat}\r\n" +
                                  $"Mã đơn hàng: {maDonHang}\r\n" +
                                  $"Ngày xuất: {ngayXuat}\r\n" +
                                  $"Loại xuất: {loaiXuat}\r\n" +
                                  $"Người lập: {nguoiLap}\r\n" +
                                  $"Số lượng tổng: {soLuong}\r\n" +
                                  $"Trạng thái hiện tại: {trangThai}";
            }
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Duyệt phiếu xuất kho")) return;

            if (string.IsNullOrEmpty(maPhieuXuat))
            {
                MessageBox.Show("Không tìm thấy mã phiếu xuất!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nguoiDuyet = UserSession.MaNguoiDung ?? "ND_ADMIN";

            try
            {
                string sql = "UPDATE phieuxuat SET trangthai = 'Đã duyệt', nguoiduyet = @NguoiDuyet WHERE maphieuxuat = @MaPX";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@NguoiDuyet", nguoiDuyet),
                    new SqlParameter("@MaPX", maPhieuXuat)
                };

                DatabaseHelper.ExecuteNonQuery(sql, parameters);

                MessageBox.Show("Phiếu xuất kho đã được duyệt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi duyệt phiếu xuất: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Từ chối phiếu xuất kho")) return;

            if (string.IsNullOrEmpty(maPhieuXuat))
            {
                MessageBox.Show("Không tìm thấy mã phiếu xuất!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nguoiDuyet = UserSession.MaNguoiDung ?? "ND_ADMIN";

            try
            {
                string sql = "UPDATE phieuxuat SET trangthai = 'Đã hủy', nguoiduyet = @NguoiDuyet WHERE maphieuxuat = @MaPX";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@NguoiDuyet", nguoiDuyet),
                    new SqlParameter("@MaPX", maPhieuXuat)
                };

                DatabaseHelper.ExecuteNonQuery(sql, parameters);

                MessageBox.Show("Đã từ chối phiếu xuất kho này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi từ chối phiếu xuất: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
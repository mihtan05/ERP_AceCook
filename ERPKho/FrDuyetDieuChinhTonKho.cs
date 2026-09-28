using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrDuyetDieuChinhTonKho : Form
    {
        public FrDuyetDieuChinhTonKho()
        {
            InitializeComponent();
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

        private void FrDuyetDieuChinhTonKho_Load(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Phê duyệt điều chỉnh tồn kho"))
            {
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            dgvDuyet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDuyet.MultiSelect = false;

            LoadDanhSachPhieuKiemKe();
        }

        private void LoadDanhSachPhieuKiemKe()
        {
            try
            {
                // Thay ngoặc vuông [] bằng ngoặc kép "" chuẩn PostgreSQL
                string sql = @"
                    SELECT 
                        pk.maphieukk AS ""MaPhieu"", 
                        k.tenkho AS ""Kho"", 
                        pk.nguoilap AS ""NguoiLap"", 
                        pk.ngaykiemke AS ""NgayKiemKe"",
                        pk.trangthai AS ""TrangThaiDuyet"" 
                    FROM phieukiemke pk
                    INNER JOIN kho k ON TRIM(pk.makho) = TRIM(k.makho)
                    WHERE pk.trangthai = 'Chờ duyệt'";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvDuyet.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách chờ duyệt: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedMaPhieu()
        {
            if (dgvDuyet.CurrentRow != null && dgvDuyet.CurrentRow.Index >= 0)
            {
                if (dgvDuyet.Columns.Contains("MaPhieu"))
                {
                    return dgvDuyet.CurrentRow.Cells["MaPhieu"].Value?.ToString();
                }
                return dgvDuyet.CurrentRow.Cells[0].Value?.ToString();
            }
            return null;
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Phê duyệt điều chỉnh tồn kho")) return;

            string maPhieu = GetSelectedMaPhieu();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng chọn phiếu cần phê duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "UPDATE phieukiemke SET trangthai = 'Đã duyệt' WHERE TRIM(maphieukk) = TRIM(@Ma)";
                int rows = DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@Ma", maPhieu) });

                if (rows > 0)
                {
                    MessageBox.Show($"Đã phê duyệt điều chỉnh cho phiếu [{maPhieu}] thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi phê duyệt: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Từ chối điều chỉnh tồn kho")) return;

            string maPhieu = GetSelectedMaPhieu();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng chọn phiếu cần từ chối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "UPDATE phieukiemke SET trangthai = 'Đã từ chối' WHERE TRIM(maphieukk) = TRIM(@Ma)";
                DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@Ma", maPhieu) });

                MessageBox.Show($"Đã từ chối phiếu điều chỉnh [{maPhieu}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadDanhSachPhieuKiemKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi từ chối: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrTaoPhieuKiemKe : Form
    {
        public FrTaoPhieuKiemKe()
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

        private void FrTaoPhieuKiemKe_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo phiếu kiểm kê!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            // Sinh mã dạng PKK-yyMMddHHmmssfff (Đúng 19 ký tự, không vượt quá VARCHAR(20))
            txtMaPhieu.Text = "PKK-" + DateTime.Now.ToString("yyMMddHHmmssfff");
            txtMaPhieu.ReadOnly = true;
            txtMaPhieu.TabStop = false;

            LoadDanhSachKho();
        }

        private void LoadDanhSachKho()
        {
            try
            {
                string sql = "SELECT makho, tenkho FROM kho";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                cboKho.DataSource = dt;
                cboKho.DisplayMember = "tenkho";
                cboKho.ValueMember = "makho";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaoPhieu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo phiếu kiểm kê!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = txtMaPhieu.Text.Trim();
            string maKho = cboKho.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(maPhieu) || string.IsNullOrEmpty(maKho))
            {
                MessageBox.Show("Vui lòng chọn nhà kho hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sqlInsertPhieu = @"
                    INSERT INTO phieukiemke (maphieukk, makho, ngaykiemke, nguoilap, trangthai)
                    VALUES (@MaPhieu, @MaKho, (NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'), 'ND_ADMIN', 'Chờ duyệt')";

                SqlParameter[] p1 = new SqlParameter[]
                {
                    new SqlParameter("@MaPhieu", maPhieu),
                    new SqlParameter("@MaKho", maKho)
                };

                DatabaseHelper.ExecuteNonQuery(sqlInsertPhieu, p1);

                string sqlInsertChiTiet = @"
                    INSERT INTO chitietkiemke (mactkk, maphieukk, maton, soluonghethong, soluongthucte, chenhlech)
                    SELECT 
                        'CTKK-' || SUBSTRING(MD5(RANDOM()::TEXT || CLOCK_TIMESTAMP()::TEXT), 1, 10),
                        @MaPhieu,
                        tk.maton,
                        COALESCE(tk.soluongton, 0),
                        COALESCE(tk.soluongton, 0),
                        0
                    FROM tonkho tk
                    INNER JOIN vitriluutru vt ON TRIM(tk.mavitri) = TRIM(vt.mavitri)
                    WHERE TRIM(vt.makho) = TRIM(@MaKho)";

                SqlParameter[] p2 = new SqlParameter[]
                {
                    new SqlParameter("@MaPhieu", maPhieu),
                    new SqlParameter("@MaKho", maKho)
                };

                DatabaseHelper.ExecuteNonQuery(sqlInsertChiTiet, p2);

                MessageBox.Show($"Đã tạo thành công phiếu kiểm kê [{maPhieu}]!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo phiếu kiểm kê: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboKhoKiemKe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
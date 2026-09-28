using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrTraCuuTonKho : Form
    {
        public FrTraCuuTonKho()
        {
            InitializeComponent();
        }

        
        private bool KiemTraQuyenTraCuu(string tenChucNang)
        {
            string chucVu = UserSession.ChucVu ?? "";
            bool isPermitted = chucVu.Equals("Nhân viên kho", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản lý kho", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản lý", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản trị viên", StringComparison.OrdinalIgnoreCase);

            if (!isPermitted)
            {
                MessageBox.Show($"Tài khoản với vai trò ({chucVu}) không có quyền thực hiện [{tenChucNang}]!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void FrTraCuuTonKho_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền truy cập khi tải Form
            if (!KiemTraQuyenTraCuu("Tra cứu tồn kho"))
            {
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadDuLieuTraCuu("");
        }

        private void LoadDuLieuTraCuu(string keyword)
        {
            try
            {
                // Truy vấn chuẩn PostgreSQL: Dùng COALESCE, ILIKE và ngoặc kép "" cho Alias cột
                string sql = @"
                    SELECT 
                        hh.mahang AS ""MaVatTu"",
                        hh.tenhang AS ""TenVatTu"",
                        COALESCE(k.tenkho, 'Chưa phân bổ') AS ""KhoLuuTru"",
                        COALESCE(hh.tonkho, 0) AS ""TonKho"",
                        COALESCE(hh.donvitinh, 'Kg') AS ""DVT"",
                        COALESCE(vt.mavitri, 'Chưa xếp kệ') AS ""ViTri""
                    FROM hanghoa hh
                    LEFT JOIN lohang lh ON TRIM(hh.mahang) = TRIM(lh.mahang)
                    LEFT JOIN tonkho tk ON TRIM(lh.malo) = TRIM(tk.malo)
                    LEFT JOIN vitriluutru vt ON TRIM(tk.mavitri) = TRIM(vt.mavitri)
                    LEFT JOIN kho k ON TRIM(vt.makho) = TRIM(k.makho)
                    WHERE hh.mahang ILIKE @Key OR hh.tenhang ILIKE @Key OR k.tenkho ILIKE @Key";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Key", "%" + keyword.Trim() + "%")
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                dgvTraCuu.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tra cứu tồn kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenTraCuu("Tìm kiếm tồn kho")) return;

            string keyword = txtKeyword.Text.Trim();
            LoadDuLieuTraCuu(keyword);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
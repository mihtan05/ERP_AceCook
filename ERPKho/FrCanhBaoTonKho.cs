using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrCanhBaoTonKho : Form
    {
        public FrCanhBaoTonKho()
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

        private void FrCanhBaoTonKho_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền truy cập cảnh báo tồn kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadDuLieuCanhBao();
        }

        private void LoadDuLieuCanhBao()
        {
            try
            {
                string sql = @"
                        SELECT 
                            hh.mahang AS MaVatTu,
                            hh.tenhang AS TenVatTu,
                            k.tenkho AS TenKho,
                            vt.mavitri AS ViTri,
                            COALESCE(tk.soluongton, 0) AS SoLuongTon,
                            COALESCE(hh.donvitinh, 'Kg') AS DonViTinh,
                            CASE 
                                WHEN hh.mahang LIKE 'HH%' THEN 500
                                WHEN hh.mahang LIKE 'NL%' THEN 100
                                ELSE 200
                            END AS NguongToiThieu,
                            'Cảnh báo thiếu' AS TrangThaiTon
                        FROM hanghoa hh
                        INNER JOIN lohang lh ON TRIM(hh.mahang) = TRIM(lh.mahang)
                        INNER JOIN tonkho tk ON TRIM(lh.malo) = TRIM(tk.malo)
                        INNER JOIN vitriluutru vt ON TRIM(tk.mavitri) = TRIM(vt.mavitri)
                        INNER JOIN kho k ON TRIM(vt.makho) = TRIM(k.makho)
                        WHERE COALESCE(tk.soluongton, 0) < (
                            CASE 
                                WHEN hh.mahang LIKE 'HH%' THEN 500
                                WHEN hh.mahang LIKE 'NL%' THEN 100
                                ELSE 200
                            END
                        )";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvCanhBao.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách cảnh báo: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuiCanhBao_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền gửi cảnh báo tồn kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Đã gửi thông báo cảnh báo tồn kho thấp đến bộ phận mua hàng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
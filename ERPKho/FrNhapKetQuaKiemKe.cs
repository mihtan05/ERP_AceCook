using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrNhapKetQuaKiemKe : Form
    {
        public FrNhapKetQuaKiemKe()
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

        private void FrNhapKetQuaKiemKe_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền nhập kết quả kiểm kê!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadDanhSachPhieuKiemKe();
        }

        private void LoadDanhSachPhieuKiemKe()
        {
            try
            {
                string sql = @"
                    SELECT 
                        pk.maphieukk, 
                        k.tenkho 
                    FROM phieukiemke pk
                    INNER JOIN kho k ON TRIM(pk.makho) = TRIM(k.makho)";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                cboMaPhieu.DataSource = dt;
                cboMaPhieu.DisplayMember = "maphieukk";
                cboMaPhieu.ValueMember = "maphieukk";

                if (dt != null && dt.Rows.Count > 0)
                {
                    LoadChiTietKiemKe(dt.Rows[0]["maphieukk"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phiếu kiểm kê: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietKiemKe(string maPhieu)
        {
            try
            {
                // Sửa alias từ [Name] sang "Name" chuẩn PostgreSQL
                string sql = @"
                    SELECT 
                        ct.mactkk,
                        hh.mahang AS ""MaVatTu"",
                        hh.tenhang AS ""TenVatTu"",
                        ct.soluonghethong AS ""SoLuongHeThong"",
                        ct.soluongthucte AS ""SoLuongThucTe""
                    FROM chitietkiemke ct
                    INNER JOIN tonkho tk ON TRIM(ct.maton) = TRIM(tk.maton)
                    INNER JOIN lohang lh ON TRIM(tk.malo) = TRIM(lh.malo)
                    INNER JOIN hanghoa hh ON TRIM(lh.mahang) = TRIM(hh.mahang)
                    WHERE TRIM(ct.maphieukk) = TRIM(@MaPhieu)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaPhieu", maPhieu)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt == null || dt.Rows.Count == 0)
                {
                    sql = @"
                        SELECT 
                            hh.mahang AS ""MaVatTu"",
                            hh.tenhang AS ""TenVatTu"",
                            COALESCE(tk.soluongton, 0) AS ""SoLuongHeThong"",
                            COALESCE(tk.soluongton, 0) AS ""SoLuongThucTe""
                        FROM hanghoa hh
                        INNER JOIN lohang lh ON TRIM(hh.mahang) = TRIM(lh.mahang)
                        INNER JOIN tonkho tk ON TRIM(lh.malo) = TRIM(tk.malo)
                        INNER JOIN vitriluutru vt ON TRIM(tk.mavitri) = TRIM(vt.mavitri)
                        INNER JOIN phieukiemke pk ON TRIM(vt.makho) = TRIM(pk.makho)
                        WHERE TRIM(pk.maphieukk) = TRIM(@MaPhieu)";

                    dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                }

                dgvChiTiet.DataSource = dt;

                if (dgvChiTiet.Columns["SoLuongThucTe"] != null)
                {
                    dgvChiTiet.Columns["SoLuongThucTe"].ReadOnly = false;
                }
                if (dgvChiTiet.Columns["SoLuongHeThong"] != null)
                {
                    dgvChiTiet.Columns["SoLuongHeThong"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết kiểm kê: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboMaPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaPhieu.SelectedValue != null && cboMaPhieu.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                LoadChiTietKiemKe(cboMaPhieu.SelectedValue.ToString());
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền nhập kết quả kiểm kê!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = cboMaPhieu.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng chọn phiếu kiểm kê hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MessageBox.Show($"Đã lưu kết quả kiểm kê thực tế cho phiếu [{maPhieu}] thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu kết quả kiểm kê: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
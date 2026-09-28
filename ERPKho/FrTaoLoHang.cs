using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrTaoLoHang : Form
    {
        public FrTaoLoHang()
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

        /// <summary>
        /// Sinh mã lô tự động đảm bảo độ dài 19 ký tự (<= 20)
        /// Định dạng: LO-yyMMdd-HHmmss-XX (Ví dụ: LO-260921-220932-A1)
        /// </summary>
        private string SinhMaLoTuDong()
        {
            string timeStamp = DateTime.Now.ToString("yyMMdd-HHmmss"); // 13 ký tự
            string randomSuffix = Guid.NewGuid().ToString("N").Substring(0, 2).ToUpper(); // 2 ký tự
            return $"LO-{timeStamp}-{randomSuffix}"; // Tổng: 3 + 13 + 1 + 2 = 19 ký tự
        }

        private void FrTaoLoHang_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo lô hàng mới!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            txtMaLo.Text = SinhMaLoTuDong();
            txtTenVatTu.Text = "";
            txtSoLuongCon.Text = "100";

            cboLoaiHang.Items.Clear();
            cboLoaiHang.Items.AddRange(new object[] { "Thành phẩm", "Nguyên liệu", "Bao bì", "Vật tư" });
            cboLoaiHang.SelectedIndex = 0;

            dtpNSX.Value = DateTime.Now;
            dtpHSD.Value = DateTime.Now.AddYears(1);
        }

        private string SinhMaHangTuDong(string loaiHang, string tenHang)
        {
            string prefix = "HH";
            if (loaiHang == "Nguyên liệu") prefix = "NL";
            else if (loaiHang == "Thành phẩm") prefix = "TP";
            else if (loaiHang == "Bao bì") prefix = "BB";
            else if (loaiHang == "Vật tư") prefix = "VT";

            string maKhongDau = BoDauTiengViet(tenHang).Replace(" ", "").ToUpper();
            maKhongDau = Regex.Replace(maKhongDau, @"[^A-Z0-9]", "");

            if (maKhongDau.Length > 8)
                maKhongDau = maKhongDau.Substring(0, 8);

            return prefix + maKhongDau;
        }

        private string BoDauTiengViet(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC).Replace('Đ', 'D').Replace('đ', 'd');
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string maLo = txtMaLo.Text.Trim();
            if (string.IsNullOrWhiteSpace(maLo) || maLo.Length > 20)
            {
                maLo = SinhMaLoTuDong();
                txtMaLo.Text = maLo;
            }

            string tenHang = txtTenVatTu.Text.Trim();
            string loaiHang = cboLoaiHang.SelectedItem?.ToString() ?? "Thành phẩm";

            if (string.IsNullOrWhiteSpace(tenHang))
            {
                MessageBox.Show("Vui lòng nhập Tên Hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenVatTu.Focus();
                return;
            }

            if (!decimal.TryParse(txtSoLuongCon.Text.Trim(), out decimal soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng lô hàng phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpHSD.Value <= dtpNSX.Value)
            {
                MessageBox.Show("Hạn sử dụng phải lớn hơn Ngày sản xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maHang = SinhMaHangTuDong(loaiHang, tenHang);

                // 1. Kiểm tra nếu Tên/Mã Hàng chưa có trong CSDL PostgreSQL -> Thêm mới vào bảng hanghoa
                string sqlCheck = "SELECT mahang FROM hanghoa WHERE tenhang = @TenHang OR mahang = @MaHang";
                DataTable dtCheck = DatabaseHelper.ExecuteQuery(sqlCheck, new SqlParameter[] {
                    new SqlParameter("@TenHang", tenHang),
                    new SqlParameter("@MaHang", maHang)
                });

                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    maHang = dtCheck.Rows[0]["mahang"].ToString();
                }
                else
                {
                    string sqlInsertHang = "INSERT INTO hanghoa (mahang, tenhang, donvitinh, tonkho) VALUES (@MaHang, @TenHang, 'Kg', 0)";
                    DatabaseHelper.ExecuteNonQuery(sqlInsertHang, new SqlParameter[] {
                        new SqlParameter("@MaHang", maHang),
                        new SqlParameter("@TenHang", tenHang)
                    });
                }

                // 2. Tự động kiểm tra trùng lặp mã lô trong CSDL PostgreSQL, nếu trùng sẽ tự cấp mã mới lập tức
                string sqlCheckLo = "SELECT COUNT(*) FROM lohang WHERE malo = @MaLo";
                DataTable dtCheckLo = DatabaseHelper.ExecuteQuery(sqlCheckLo, new SqlParameter[] { new SqlParameter("@MaLo", maLo) });
                if (dtCheckLo != null && dtCheckLo.Rows.Count > 0 && Convert.ToInt32(dtCheckLo.Rows[0][0]) > 0)
                {
                    maLo = SinhMaLoTuDong();
                    txtMaLo.Text = maLo;
                }

                // 3. Tạo Lô Hàng Mới chuẩn PostgreSQL
                string sqlInsertLo = @"
                    INSERT INTO lohang (malo, mahang, ngaysanxuat, hansudung, soluongcon, trangthai)
                    VALUES (@MaLo, @MaHang, @NSX, @HSD, @SoLuong, 'Khả dụng')";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLo", maLo),
                    new SqlParameter("@MaHang", maHang),
                    new SqlParameter("@NSX", dtpNSX.Value),
                    new SqlParameter("@HSD", dtpHSD.Value),
                    new SqlParameter("@SoLuong", soLuong)
                };

                int rows = DatabaseHelper.ExecuteNonQuery(sqlInsertLo, parameters);
                if (rows > 0)
                {
                    MessageBox.Show($"Tạo Lô Hàng [{maLo}] thuộc loại [{loaiHang}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu lô hàng: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e) => this.Close();
    }
}
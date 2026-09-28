using System;
using Npgsql;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ERP.DAL;

namespace ERP
{
    public partial class FrmThemNhaCungCap : Form
    {
        private string connectionString = DatabaseConfig.GetConnectionString();

        public FrmThemNhaCungCap()
        {
            InitializeComponent();
            dtpNgayCap.Checked = false;
            dtpNgayHetHan.Checked = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra các trường dữ liệu bắt buộc (Not Null trong CSDL)
            if (string.IsNullOrWhiteSpace(txtID.Text) ||
                string.IsNullOrWhiteSpace(txtTenNCC.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtTenCN.Text) ||
                string.IsNullOrWhiteSpace(txtSoHieuCN.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin bắt buộc (Mã NCC, Tên NCC, Địa chỉ, SĐT, Tên chứng nhận, Số chứng nhận)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sdt = txtSDT.Text.Trim();

            // 2. Kiểm tra số điện thoại phải đủ đúng 10 chữ số
            // Dùng Regex kiểm tra chuỗi chỉ bao gồm từ 0 đến 9 và có độ dài chính xác là 10
            if (!Regex.IsMatch(sdt, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Số điện thoại phải bao gồm đúng 10 chữ số.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                txtSDT.SelectAll();
                return;
            }

            // 3. Kiểm tra ràng buộc ngày (Ngày hết hạn >= Ngày cấp nếu cả 2 đều được nhập)
            if (dtpNgayCap.Checked && dtpNgayHetHan.Checked && dtpNgayHetHan.Value.Date < dtpNgayCap.Value.Date)
            {
                MessageBox.Show("Ngày hết hạn phải lớn hơn hoặc bằng Ngày cấp chứng nhận!", "Lỗi ngày tháng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayHetHan.Focus();
                return;
            }

            string idNcc = txtID.Text.Trim();
            string tenNcc = txtTenNCC.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string tenCN = txtTenCN.Text.Trim();
            string soHieuCN = txtSoHieuCN.Text.Trim();
            DateTime ngayCap = dtpNgayCap.Value.Date;
            DateTime ngayHetHan = dtpNgayHetHan.Value.Date;

            // 4. Câu lệnh SQL INSERT (Không còn FileScanCN)
            string query = @"INSERT INTO NhaCungCap 
                             (ID_NCC, TenNCC, DiaChi, SDT, TenChungNhan, SoHieuCN, NgayCap, NgayHetHan) 
                             VALUES 
                             (@ID_NCC, @TenNCC, @DiaChi, @SDT, @TenChungNhan, @SoHieuCN, @NgayCap, @NgayHetHan)";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

                    // Truyền tham số an toàn chống SQL Injection
                    cmd.Parameters.AddWithValue("@ID_NCC", idNcc);
                    cmd.Parameters.AddWithValue("@TenNCC", tenNcc);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@SDT", sdt);

                    // Xử lý các giá trị có thể để trống (NULL)
                    cmd.Parameters.AddWithValue("@TenChungNhan", string.IsNullOrEmpty(tenCN) ? (object)DBNull.Value : tenCN);
                    cmd.Parameters.AddWithValue("@SoHieuCN", string.IsNullOrEmpty(soHieuCN) ? (object)DBNull.Value : soHieuCN);
                    cmd.Parameters.AddWithValue("@NgayCap", dtpNgayCap.Checked ? (object)ngayCap : DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHan", dtpNgayHetHan.Checked ? (object)ngayHetHan : DBNull.Value);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (PostgresException ex)
                {
                    if (ex.SqlState == "23505") // Lỗi trùng khóa chính (Primary Key)
                    {
                        MessageBox.Show($"Mã nhà cung cấp '{idNcc}' đã tồn tại trong hệ thống. Vui lòng nhập mã khác!", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if ((ex.SqlState == "23503" || ex.SqlState == "23514")) // Lỗi vi phạm CHECK constraint CK_NhaCungCap_Ngay
                    {
                        MessageBox.Show("Ngày hết hạn phải lớn hơn hoặc bằng Ngày cấp chứng nhận!", "Lỗi ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}



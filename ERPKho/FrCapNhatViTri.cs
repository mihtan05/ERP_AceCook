using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrCapNhatViTri : Form
    {
        private readonly DataRow currentRow;

        public FrCapNhatViTri(DataRow row)
        {
            InitializeComponent();
            currentRow = row;
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

        private void FrCapNhatViTri_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền cập nhật vị trí lưu trữ!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadTrangThaiCombobox();
            LoadDataFromSelectedRow();
        }

        private void LoadTrangThaiCombobox()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Khả dụng");
            cboTrangThai.Items.Add("Đầy");
            cboTrangThai.Items.Add("Đang bảo trì");
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadDataFromSelectedRow()
        {
            if (currentRow == null) return;

            string maViTri = currentRow["MaViTri"]?.ToString() ?? "";
            lblMaViTriText.Text = $"Đang sửa vị trí: [{maViTri}]";

            // Truy vấn dữ liệu thực mới nhất từ CSDL PostgreSQL
            string sql = "SELECT succhuatoida, trangthai FROM vitriluutru WHERE mavitri = @MaVT";
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaVT", maViTri) });

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["succhuatoida"] != DBNull.Value)
                    numSucChuaToida.Value = Convert.ToDecimal(dt.Rows[0]["succhuatoida"]);

                string currentStatus = dt.Rows[0]["trangthai"]?.ToString() ?? "Khả dụng";
                if (cboTrangThai.Items.Contains(currentStatus))
                    cboTrangThai.SelectedItem = currentStatus;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (currentRow == null) return;

            string maViTri = currentRow["MaViTri"].ToString();
            decimal newCap = numSucChuaToida.Value;

            // Kiểm tra tổng số lượng hàng hiện đang thực tế lưu trữ tại ô này trong tonkho (Chuẩn PostgreSQL COALESCE)
            string sqlCheckUsed = "SELECT COALESCE(SUM(soluongton), 0) FROM tonkho WHERE mavitri = @MaVT";
            DataTable dtUsed = DatabaseHelper.ExecuteQuery(sqlCheckUsed, new SqlParameter[] { new SqlParameter("@MaVT", maViTri) });
            decimal currentlyUsed = (dtUsed != null && dtUsed.Rows.Count > 0) ? Convert.ToDecimal(dtUsed.Rows[0][0]) : 0;

            if (newCap < currentlyUsed)
            {
                MessageBox.Show($"Không thể giảm sức chứa xuống {newCap:N0} vì ô chứa đang lưu trữ thực tế {currentlyUsed:N0} lượng hàng!",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string query = "UPDATE vitriluutru SET succhuatoida = @Cap, trangthai = @TrangThai WHERE mavitri = @MaViTri";
                SqlParameter[] parameters = {
                    new SqlParameter("@Cap", newCap),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Khả dụng"),
                    new SqlParameter("@MaViTri", maViTri)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật CSDL: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrChuyenViTri : Form
    {
        private readonly DataRow selectedLoHang;

        public FrChuyenViTri(DataRow loHangRow)
        {
            InitializeComponent();
            selectedLoHang = loHangRow;
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

        private void FrChuyenViTri_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền chuyển vị trí lưu trữ!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadInfoAndTargetLocations();
        }

        private void LoadInfoAndTargetLocations()
        {
            if (selectedLoHang == null) return;

            string oldVT = selectedLoHang["MaViTri"]?.ToString() ?? "";
            string maLo = selectedLoHang["MaLo"]?.ToString() ?? "";
            string tenHang = selectedLoHang["TenHang"]?.ToString() ?? "";
            string donVi = selectedLoHang["DonViTinh"]?.ToString() ?? "";
            decimal qty = selectedLoHang["SoLuong"] != DBNull.Value ? Convert.ToDecimal(selectedLoHang["SoLuong"]) : 0;

            lblInfo.Text = $"• Lô hàng: [{maLo}] - {tenHang}\n• Số lượng: {qty:N0} {donVi}\n• Vị trí hiện tại: [{oldVT}]";

            // Truy vấn lấy các ô chứa khác ô hiện tại và không ở trạng thái 'Đang bảo trì' (Chuẩn PostgreSQL)
            cboNewViTri.Items.Clear();
            string sql = "SELECT mavitri FROM vitriluutru WHERE mavitri != @OldVT AND trangthai != 'Đang bảo trì'";
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@OldVT", oldVT) });

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cboNewViTri.Items.Add(row["mavitri"].ToString());
                }
                cboNewViTri.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Không tìm thấy vị trí ô chứa khác khả dụng để chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (cboNewViTri.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn vị trí mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string oldVT = selectedLoHang["MaViTri"].ToString();
            string newVT = cboNewViTri.SelectedItem.ToString();
            string maLo = selectedLoHang["MaLo"].ToString();

            try
            {
                // Cập nhật mavitri mới trong bảng tonkho cho lô hàng được chọn
                string query = "UPDATE tonkho SET mavitri = @NewVT WHERE malo = @MaLo AND mavitri = @OldVT";
                SqlParameter[] parameters = {
                    new SqlParameter("@NewVT", newVT),
                    new SqlParameter("@MaLo", maLo),
                    new SqlParameter("@OldVT", oldVT)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);

                MessageBox.Show($"Đã chuyển thành công lô hàng [{maLo}] từ ô [{oldVT}] sang ô [{newVT}]!",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chuyển vị trí trong CSDL: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
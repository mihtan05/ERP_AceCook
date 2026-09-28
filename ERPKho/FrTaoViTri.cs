using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrTaoViTri : Form
    {
        public FrTaoViTri()
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

        private void FrTaoViTri_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo vị trí lưu trữ mới!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadDanhSachKhoFromDB();

            if (cboKhuVuc.Items.Count > 0 && cboKhuVuc.SelectedIndex < 0)
                cboKhuVuc.SelectedIndex = 0;

            numSucChua.Value = 1000;
        }

        private void LoadDanhSachKhoFromDB()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT makho, tenkho FROM kho");
                if (dt != null && dt.Rows.Count > 0)
                {
                    cboKho.DataSource = dt;
                    cboKho.DisplayMember = "tenkho";
                    cboKho.ValueMember = "makho";
                    cboKho.SelectedIndex = 0;
                }
                else
                {
                    cboKho.Items.Clear();
                    cboKho.Items.Add("Kho Nguyên Liệu A1");
                    cboKho.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách Kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string maViTriInput = txtMaViTri.Text.Trim();

            if (string.IsNullOrWhiteSpace(maViTriInput))
            {
                MessageBox.Show("Mã vị trí không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. KIỂM TRA TRÙNG MÃ VỊ TRÍ TRONG CSDL
                string sqlCheckExist = "SELECT COUNT(*) FROM vitriluutru WHERE mavitri = @MaVT";
                DataTable dtCheck = DatabaseHelper.ExecuteQuery(sqlCheckExist, new SqlParameter[] { new SqlParameter("@MaVT", maViTriInput) });

                if (dtCheck != null && dtCheck.Rows.Count > 0 && Convert.ToInt32(dtCheck.Rows[0][0]) > 0)
                {
                    MessageBox.Show($"Mã vị trí [{maViTriInput}] đã tồn tại trong hệ thống!\nVui lòng đặt tên mã vị trí khác.",
                                    "Cảnh Báo Trùng Mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaViTri.Focus();
                    return;
                }

                // 2. LẤY MÃ KHO HỢP LỆ (Chuẩn PostgreSQL)
                string maKho = cboKho.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maKho))
                {
                    DataTable dtKho = DatabaseHelper.ExecuteQuery("SELECT makho FROM kho LIMIT 1");
                    maKho = (dtKho != null && dtKho.Rows.Count > 0) ? dtKho.Rows[0]["makho"].ToString() : "KHO01";
                }

                // 3. THỰC HIỆN THÊM VỊ TRÍ MỚI
                string query = @"INSERT INTO vitriluutru (mavitri, makho, khuvuc, ke, ochua, succhuatoida, trangthai) 
                                 VALUES (@MaViTri, @MaKho, @KhuVuc, @Ke, @OChua, @SucChua, 'Khả dụng')";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaViTri", maViTriInput),
                    new SqlParameter("@MaKho", maKho),
                    new SqlParameter("@KhuVuc", cboKhuVuc.SelectedItem?.ToString() ?? "Zone A"),
                    new SqlParameter("@Ke", txtKe.Text.Trim()),
                    new SqlParameter("@OChua", txtOChua.Text.Trim()),
                    new SqlParameter("@SucChua", (decimal)numSucChua.Value)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm vị trí: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
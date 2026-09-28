using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrPheDuyetYeuCau : Form
    {
        private DataRow requestRow;
        private DataTable dtLichSu;

        public FrPheDuyetYeuCau(DataRow row, DataTable lichSuTable)
        {
            InitializeComponent();
            requestRow = row;
            dtLichSu = lichSuTable;
        }

        private void FrPheDuyetYeuCau_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Tài khoản của bạn không có quyền phê duyệt yêu cầu!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            cboHuongXuLy.Items.Clear();
            cboHuongXuLy.Items.Add("Trả lại Nhà cung cấp (NCC)");
            cboHuongXuLy.Items.Add("Đưa vào danh sách Tiêu hủy tại Kho");
            cboHuongXuLy.Items.Add("Nhập kho lại sản phẩm");
            cboHuongXuLy.SelectedIndex = 0;

            if (requestRow != null)
            {
                txtThongTin.Text = $"Mã phiếu trả: {requestRow["MaYeuCau"]}\r\n" +
                                   $"Bên gửi: {requestRow["BoPhanGui"]}\r\n" +
                                   $"Sản phẩm: {requestRow["MaLoHang"]} - {requestRow["TenSanPham"]}\r\n" +
                                   $"Số lượng: {requestRow["SoLuong"]}\r\n" +
                                   $"Lý do đề xuất: {requestRow["LyDo"]}\r\n" +
                                   $"Ngày trả: {requestRow["NgayGui"]}";
            }
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

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (requestRow == null) return;

            string maPT = requestRow["MaYeuCau"].ToString();
            string maLoHang = requestRow["MaLoHang"].ToString();
            decimal soLuong = Convert.ToDecimal(requestRow["SoLuong"]);
            string luachon = cboHuongXuLy.SelectedItem.ToString();
            string ghiChu = txtGhiChu.Text.Trim();

            try
            {
                // Nếu chọn "Nhập kho lại sản phẩm" -> Thực hiện cộng tồn kho đảo ngược logic xuất kho
                if (luachon == "Nhập kho lại sản phẩm")
                {
                    XuLyNhapLaiKho(maLoHang, soLuong);
                }

                // Cập nhật trạng thái 'Đã xử lý' đồng bộ cho phieutrahang, yeucausaubanhang và donhang
                string sqlUpdate = @"
                    UPDATE phieutrahang 
                    SET trangthai = 'Đã xử lý' 
                    WHERE id_phieutra = @MaPT;

                    UPDATE yeucausaubanhang 
                    SET trangthai = 'Đã xử lý',
                        mota = COALESCE(mota, '') || @GhiChu
                    WHERE id_yc = @MaPT 
                       OR id_yc = (
                           SELECT ctyc.id_yc 
                           FROM chitietyeucau ctyc 
                           JOIN phieutrahang pt ON ctyc.id_ctyc = pt.id_ctyc 
                           WHERE pt.id_phieutra = @MaPT 
                           LIMIT 1
                       );

                    UPDATE donhang 
                    SET trangthai = 'Đã xử lý' 
                    WHERE id_dh = (
                        SELECT id_dh 
                        FROM yeucausaubanhang yc
                        JOIN chitietyeucau ctyc ON yc.id_yc = ctyc.id_yc
                        JOIN phieutrahang pt ON ctyc.id_ctyc = pt.id_ctyc
                        WHERE pt.id_phieutra = @MaPT AND yc.id_dh IS NOT NULL
                        LIMIT 1
                    );";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@GhiChu", string.IsNullOrWhiteSpace(ghiChu) ? $" | [Hướng xử lý: {luachon}]" : $" | [Hướng xử lý: {luachon}] - " + ghiChu),
                    new SqlParameter("@MaPT", maPT)
                };

                int result = DatabaseHelper.ExecuteNonQuery(sqlUpdate, parameters);

                if (result > 0)
                {
                    MessageBox.Show($"Phê duyệt phiếu trả [{maPT}] thành công!\nTrạng thái chuyển thành: [Đã xử lý]\nHướng xử lý: [{luachon}]",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi CSDL khi xử lý nhập kho/phê duyệt: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cộng tồn kho đối ứng chuẩn xác theo các bảng trong FrXacNhanXuatKho (hanghoa, lohang, tonkho, vitriluutru)
        /// </summary>
        private void XuLyNhapLaiKho(string inputCode, decimal soLuong)
        {
            if (soLuong <= 0) return;

            string maHang = "";
            string maLo = "";

            inputCode = inputCode?.Trim() ?? "";

            // 1. Quy đổi mã đầu vào (có thể là id_sp từ sanpham, malo từ lohang, hoặc mahang từ hanghoa)
            string sqlSP = "SELECT TRIM(mahang) AS mahang FROM sanpham WHERE TRIM(id_sp) = TRIM(@Code)";
            DataTable dtSP = DatabaseHelper.ExecuteQuery(sqlSP, new SqlParameter[] { new SqlParameter("@Code", inputCode) });
            if (dtSP != null && dtSP.Rows.Count > 0 && dtSP.Rows[0]["mahang"] != DBNull.Value)
            {
                maHang = dtSP.Rows[0]["mahang"].ToString().Trim();
            }

            if (string.IsNullOrEmpty(maHang))
            {
                string sqlLH = "SELECT TRIM(mahang) AS mahang, TRIM(malo) AS malo FROM lohang WHERE TRIM(malo) = TRIM(@Code)";
                DataTable dtLH = DatabaseHelper.ExecuteQuery(sqlLH, new SqlParameter[] { new SqlParameter("@Code", inputCode) });
                if (dtLH != null && dtLH.Rows.Count > 0)
                {
                    maHang = dtLH.Rows[0]["mahang"]?.ToString()?.Trim() ?? "";
                    maLo = dtLH.Rows[0]["malo"]?.ToString()?.Trim() ?? "";
                }
            }

            if (string.IsNullOrEmpty(maHang))
            {
                string sqlHH = "SELECT TRIM(mahang) AS mahang FROM hanghoa WHERE TRIM(mahang) = TRIM(@Code)";
                DataTable dtHH = DatabaseHelper.ExecuteQuery(sqlHH, new SqlParameter[] { new SqlParameter("@Code", inputCode) });
                if (dtHH != null && dtHH.Rows.Count > 0)
                {
                    maHang = dtHH.Rows[0]["mahang"]?.ToString()?.Trim() ?? "";
                }
            }

            if (string.IsNullOrEmpty(maHang))
            {
                maHang = inputCode;
            }

            // 2. CỘNG TỒN KHO HÀNG HÓA (bảng hanghoa - cột tonkho)
            string sqlUpHH = @"
                UPDATE hanghoa
                SET tonkho = COALESCE(tonkho, 0) + CAST(@SL AS INT)
                WHERE TRIM(mahang) = TRIM(@MaHang)";
            DatabaseHelper.ExecuteNonQuery(sqlUpHH, new SqlParameter[] {
                new SqlParameter("@SL", soLuong),
                new SqlParameter("@MaHang", maHang)
            });

            // 3. Tìm mã Lô hàng nếu chưa xác định
            if (string.IsNullOrEmpty(maLo))
            {
                string sqlFindLo = "SELECT TRIM(malo) AS malo FROM lohang WHERE TRIM(mahang) = TRIM(@MaHang) ORDER BY ngaysanxuat DESC LIMIT 1";
                DataTable dtFindLo = DatabaseHelper.ExecuteQuery(sqlFindLo, new SqlParameter[] { new SqlParameter("@MaHang", maHang) });
                if (dtFindLo != null && dtFindLo.Rows.Count > 0)
                {
                    maLo = dtFindLo.Rows[0]["malo"]?.ToString()?.Trim() ?? "";
                }
            }

            // 4. CỘNG TỒN KHO LÔ HÀNG (bảng lohang - cột soluongcon)
            if (!string.IsNullOrEmpty(maLo))
            {
                string sqlUpLo = @"
                    UPDATE lohang
                    SET soluongcon = COALESCE(soluongcon, 0) + @SL,
                        trangthai = 'Khả dụng'
                    WHERE TRIM(malo) = TRIM(@MaLo)";
                DatabaseHelper.ExecuteNonQuery(sqlUpLo, new SqlParameter[] {
                    new SqlParameter("@SL", soLuong),
                    new SqlParameter("@MaLo", maLo)
                });

                // 5. CỘNG TỒN KHO VỊ TRÍ (bảng tonkho - cột soluongton & soluongkhadung)
                string sqlUpTK = @"
                    UPDATE tonkho
                    SET soluongton = COALESCE(soluongton, 0) + @SL,
                        soluongkhadung = COALESCE(soluongkhadung, 0) + @SL
                    WHERE TRIM(malo) = TRIM(@MaLo)";
                DatabaseHelper.ExecuteNonQuery(sqlUpTK, new SqlParameter[] {
                    new SqlParameter("@SL", soLuong),
                    new SqlParameter("@MaLo", maLo)
                });

                // 6. Cập nhật vị trí kệ kho (bảng vitriluutru) thành 'Đang sử dụng'
                string sqlUpVT = @"
                    UPDATE vitriluutru
                    SET trangthai = 'Đang sử dụng'
                    WHERE TRIM(mavitri) IN (
                        SELECT DISTINCT TRIM(mavitri) 
                        FROM tonkho 
                        WHERE TRIM(malo) = TRIM(@MaLo)
                    )";
                DatabaseHelper.ExecuteNonQuery(sqlUpVT, new SqlParameter[] { new SqlParameter("@MaLo", maLo) });
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
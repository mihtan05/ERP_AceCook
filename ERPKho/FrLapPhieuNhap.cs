using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrLapPhieuNhap : Form
    {
        private readonly FrQLNhapKho _parentForm;
        private object paramData = null;
        private decimal _soLuongTonLoHienTai = 0;

        public FrLapPhieuNhap()
        {
            InitializeComponent();
        }

        public FrLapPhieuNhap(FrQLNhapKho parent) : this()
        {
            _parentForm = parent;
        }

        public FrLapPhieuNhap(object data) : this()
        {
            paramData = data;
        }

        private void FrLapPhieuNhap_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Tài khoản của bạn không có quyền lập phiếu nhập kho mới!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            // Tự động sinh mã phiếu và KHÓA CỨNG không cho phép xóa/sửa
            txtMaPhieu.Text = "PN-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            txtMaPhieu.ReadOnly = true;
            txtMaPhieu.TabStop = false;

            // Khóa cứng mã lô tự chọn từ danh sách
            txtMaLo.ReadOnly = true;
            txtMaLo.TabStop = false;

            cboLoaiNhap.Items.Clear();
            cboLoaiNhap.Items.AddRange(new object[] { "Thành phẩm", "Nguyên liệu", "Bao bì", "Vật tư" });

            cboLoaiNhap.SelectedIndexChanged += cboLoaiNhap_SelectedIndexChanged;
            cboLoaiNhap.SelectedIndex = 0;

            dtpNSX.Value = DateTime.Now;
            dtpHSD.Value = DateTime.Now.AddYears(1);
            txtDonGia.Text = "15000";

            if (paramData is DataRow row)
            {
                ApplyDataFromParent(row);
            }
            else if (paramData is DataRowView drv)
            {
                ApplyDataFromParent(drv.Row);
            }

            cboHangHoa.SelectedIndexChanged += cboHangHoa_SelectedIndexChanged;
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

        private void cboLoaiNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiNhap.SelectedItem == null) return;
            string loaiNhap = cboLoaiNhap.SelectedItem.ToString();
            LoadDanhSachLoHangTheoLoai(loaiNhap);
        }

        /// <summary>
        /// Chỉ nạp danh sách những lô hàng CHƯA CÓ TRẠNG THÁI TRONG PHIẾU NHẬP (Chưa được lập phiếu nhập)
        /// </summary>
        private void LoadDanhSachLoHangTheoLoai(string loaiNhap)
        {
            try
            {
                // Điều kiện loại trừ các lô hàng ĐÃ TỒN TẠI trong bảng chitietphieunhap (trừ phiếu bị Hủy)
                string sqlWhere = @"
                    WHERE lh.soluongcon > 0 
                      AND lh.trangthai <> 'Phong tỏa' 
                      AND lh.hansudung >= CURRENT_TIMESTAMP
                      AND lh.malo NOT IN (
                          SELECT ctpn.malo 
                          FROM chitietphieunhap ctpn
                          INNER JOIN phieunhap pn ON ctpn.maphieunhap = pn.maphieunhap
                          WHERE pn.trangthai <> 'Đã hủy'
                      )";

                if (loaiNhap == "Thành phẩm")
                {
                    sqlWhere += @" AND (
                        hh.mahang IN (SELECT mahang FROM sanpham) 
                        OR hh.tenhang LIKE '%Mì%' 
                        OR hh.tenhang LIKE '%Miến%' 
                        OR hh.mahang LIKE 'HH%' 
                        OR hh.mahang LIKE 'SP%'
                    )";
                }
                else if (loaiNhap == "Nguyên liệu")
                {
                    sqlWhere += @" AND (
                        (hh.mahang LIKE 'NL%' AND hh.mahang NOT IN (SELECT mahang FROM sanpham))
                        OR hh.tenhang LIKE '%Bột%' 
                        OR hh.tenhang LIKE '%Gia vị%' 
                        OR hh.tenhang LIKE '%Dầu%'
                    )";
                }
                else if (loaiNhap == "Bao bì")
                {
                    sqlWhere += @" AND (
                        hh.tenhang LIKE '%Bao%' 
                        OR hh.tenhang LIKE '%Bì%' 
                        OR hh.tenhang LIKE '%Thùng%' 
                        OR hh.tenhang LIKE '%Màng%' 
                        OR hh.mahang LIKE 'BB%'
                    )";
                }
                else if (loaiNhap == "Vật tư")
                {
                    sqlWhere += @" AND (
                        hh.tenhang LIKE '%Vật tư%' 
                        OR hh.tenhang LIKE '%Băng keo%' 
                        OR hh.mahang LIKE 'VT%'
                    )";
                }

                string sql = $@"
                    SELECT 
                        lh.malo, 
                        hh.mahang, 
                        hh.tenhang, 
                        lh.soluongcon, 
                        lh.ngaysanxuat, 
                        lh.hansudung,
                        lh.trangthai,
                        (lh.malo || ' - ' || hh.tenhang || ' (Tồn: ' || CAST(lh.soluongcon AS VARCHAR) || ' Kg)') AS ""HienThiLo""
                    FROM lohang lh
                    INNER JOIN hanghoa hh ON lh.mahang = hh.mahang
                    {sqlWhere}
                    ORDER BY lh.malo DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cboHangHoa.DataSource = dt;
                    cboHangHoa.DisplayMember = "HienThiLo";
                    cboHangHoa.ValueMember = "malo";
                    cboHangHoa.SelectedIndex = 0;
                    cboHangHoa_SelectedIndexChanged(null, null);
                }
                else
                {
                    cboHangHoa.DataSource = null;
                    cboHangHoa.Text = "";
                    txtMaLo.Text = "";
                    txtSoLuong.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lô hàng chưa lập phiếu nhập: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyDataFromParent(DataRow row)
        {
            if (row == null) return;

            string trangThai = GetStringValue(row, "TrangThai", "trangThai");
            if (!string.IsNullOrEmpty(trangThai) && trangThai.Equals("Phong tỏa", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Lô hàng này đang ở trạng thái PHONG TỎA, không thể lập phiếu nhập!", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            DateTime? hsdCheck = GetDateTimeValue(row, "HanSuDung", "hanSuDung");
            if (hsdCheck.HasValue && hsdCheck.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Lô hàng này đã QUÁ HẠN SỬ DỤNG, không thể lập phiếu nhập!", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            string maLo = GetStringValue(row, "MaLo", "malo", "MaLoHang", "Mã Lô Hàng");
            if (!string.IsNullOrEmpty(maLo)) txtMaLo.Text = maLo;

            string soLuong = GetStringValue(row, "SoLuong", "soluongcon", "SoLuongTonLo", "Tồn Theo Lô");
            if (!string.IsNullOrEmpty(soLuong) && decimal.TryParse(soLuong, out decimal sl))
            {
                if (sl <= 0)
                {
                    MessageBox.Show("Lô hàng này có số lượng tồn bằng 0kg, không thể chọn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
                txtSoLuong.Text = sl.ToString("0");
                _soLuongTonLoHienTai = sl;
            }

            DateTime? nsx = GetDateTimeValue(row, "NgaySanXuat", "ngaysanxuat");
            if (nsx.HasValue) dtpNSX.Value = nsx.Value;

            if (hsdCheck.HasValue) dtpHSD.Value = hsdCheck.Value;

            string loai = GetStringValue(row, "LoaiNhap", "loaihang");
            if (!string.IsNullOrEmpty(loai) && cboLoaiNhap.Items.Contains(loai))
            {
                cboLoaiNhap.SelectedItem = loai;
            }
        }

        private string GetStringValue(DataRow row, params string[] columnNames)
        {
            foreach (string col in columnNames)
            {
                if (row.Table.Columns.Contains(col) && row[col] != DBNull.Value)
                    return row[col].ToString();
            }
            return null;
        }

        private DateTime? GetDateTimeValue(DataRow row, params string[] columnNames)
        {
            foreach (string col in columnNames)
            {
                if (row.Table.Columns.Contains(col) && row[col] != DBNull.Value)
                {
                    if (DateTime.TryParse(row[col].ToString(), out DateTime dt))
                        return dt;
                }
            }
            return null;
        }

        private void cboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedItem is DataRowView drv)
            {
                txtMaLo.Text = drv["malo"]?.ToString() ?? "";

                if (drv["soluongcon"] != DBNull.Value)
                {
                    decimal ton = Convert.ToDecimal(drv["soluongcon"]);
                    txtSoLuong.Text = ton.ToString("0");
                    _soLuongTonLoHienTai = ton;
                }

                if (drv["ngaysanxuat"] != DBNull.Value)
                    dtpNSX.Value = Convert.ToDateTime(drv["ngaysanxuat"]);

                if (drv["hansudung"] != DBNull.Value)
                    dtpHSD.Value = Convert.ToDateTime(drv["hansudung"]);

                string maHang = drv["mahang"]?.ToString();
                if (!string.IsNullOrEmpty(maHang))
                {
                    string sqlPrice = @"SELECT ctpn.dongia 
                                        FROM chitietphieunhap ctpn
                                        INNER JOIN lohang lh ON ctpn.malo = lh.malo
                                        WHERE lh.mahang = @MaHang 
                                        ORDER BY ctpn.maphieunhap DESC 
                                        LIMIT 1";

                    DataTable dtPrice = DatabaseHelper.ExecuteQuery(sqlPrice, new SqlParameter[] { new SqlParameter("@MaHang", maHang) });
                    if (dtPrice != null && dtPrice.Rows.Count > 0 && dtPrice.Rows[0][0] != DBNull.Value)
                    {
                        txtDonGia.Text = Convert.ToDecimal(dtPrice.Rows[0][0]).ToString("0");
                    }
                }
            }
        }

        private string LayMaNguoiDungHopLe()
        {
            try
            {
                string currentUserId = UserSession.MaNguoiDung;

                if (!string.IsNullOrEmpty(currentUserId))
                {
                    string sqlCheck = "SELECT COUNT(*) FROM nguoidung WHERE manguoidung = @Ma";
                    DataTable dtCheck = DatabaseHelper.ExecuteQuery(sqlCheck, new SqlParameter[] { new SqlParameter("@Ma", currentUserId) });

                    if (dtCheck != null && dtCheck.Rows.Count > 0 && Convert.ToInt32(dtCheck.Rows[0][0]) > 0)
                    {
                        return currentUserId;
                    }
                }

                string sqlFirst = "SELECT manguoidung FROM nguoidung LIMIT 1";
                DataTable dtFirst = DatabaseHelper.ExecuteQuery(sqlFirst);

                if (dtFirst != null && dtFirst.Rows.Count > 0 && dtFirst.Rows[0][0] != DBNull.Value)
                {
                    return dtFirst.Rows[0]["manguoidung"].ToString();
                }

                string sqlEnsureAdmin = @"
                    INSERT INTO nguoidung (manguoidung, hoten, vaitro, trangthai)
                    VALUES ('ND_ADMIN', 'Quản trị hệ thống', 'Admin', true)
                    ON CONFLICT (manguoidung) DO NOTHING;";
                DatabaseHelper.ExecuteNonQuery(sqlEnsureAdmin);

                return "ND_ADMIN";
            }
            catch
            {
                return "ND_ADMIN";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền lập phiếu nhập kho!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string maPhieu = txtMaPhieu.Text.Trim();
            string maLo = txtMaLo.Text.Trim();

            if (string.IsNullOrWhiteSpace(maPhieu) || string.IsNullOrWhiteSpace(maLo))
            {
                MessageBox.Show("Vui lòng chọn Lô hàng hợp lệ để lập phiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSoLuong.Text.Trim(), out decimal soLuongNhap) || soLuongNhap <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlCheckStatus = "SELECT soluongcon, trangthai, hansudung FROM lohang WHERE malo = @MaLo";
            DataTable dtStatus = DatabaseHelper.ExecuteQuery(sqlCheckStatus, new SqlParameter[] { new SqlParameter("@MaLo", maLo) });
            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                string trangThaiDb = dtStatus.Rows[0]["trangthai"]?.ToString() ?? "";
                if (trangThaiDb.Equals("Phong tỏa", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"Lô hàng [{maLo}] đang ở trạng thái PHONG TỎA, không thể lập phiếu nhập!", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (dtStatus.Rows[0]["hansudung"] != DBNull.Value)
                {
                    DateTime hsdDb = Convert.ToDateTime(dtStatus.Rows[0]["hansudung"]);
                    if (hsdDb.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show($"Lô hàng [{maLo}] đã QUÁ HẠN SỬ DỤNG, không thể lập phiếu nhập!", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }

            if (soLuongNhap > _soLuongTonLoHienTai)
            {
                string msg = $"Số lượng lập phiếu nhập đang vượt quá hạn mức cho phép!\n\n" +
                             $"- Lô hàng chọn: [{maLo}]\n" +
                             $"- Số lượng tồn thực tế: {_soLuongTonLoHienTai:#,##0} Kg\n" +
                             $"- Số lượng bạn vừa nhập: {soLuongNhap:#,##0} Kg\n\n" +
                             $"Vui lòng điều chỉnh lại số lượng nhỏ hơn hoặc bằng số lượng tồn của lô.";

                MessageBox.Show(msg, "⚠️ Cảnh Báo Vượt Hạn Mức Tồn Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtSoLuong.Focus();
                txtSoLuong.SelectAll();
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá nhập không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string loaiNhap = cboLoaiNhap.SelectedItem?.ToString() ?? "Thành phẩm";
            string nguoiLap = LayMaNguoiDungHopLe();

            if (string.IsNullOrEmpty(nguoiLap))
            {
                MessageBox.Show("Không tìm thấy thông tin Người lập hợp lệ trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Sử dụng (NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh') để ghi nhận chuẩn múi giờ Việt Nam
                string sqlPhieu = @"INSERT INTO phieunhap (maphieunhap, loainhap, ngaylap, trangthai, nguoilap) 
                                    VALUES (@MaPhieu, @LoaiNhap, (NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'), 'Chờ duyệt', @NguoiLap)";
                DatabaseHelper.ExecuteNonQuery(sqlPhieu, new SqlParameter[] {
                    new SqlParameter("@MaPhieu", maPhieu),
                    new SqlParameter("@LoaiNhap", loaiNhap),
                    new SqlParameter("@NguoiLap", nguoiLap)
                });

                string maCTPN = "CT" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string sqlCT = @"INSERT INTO chitietphieunhap (mactpn, maphieunhap, malo, soluong, dongia)
                                 VALUES (@MaCTPN, @MaPhieu, @MaLo, @SL, @Gia)";
                DatabaseHelper.ExecuteNonQuery(sqlCT, new SqlParameter[] {
                    new SqlParameter("@MaCTPN", maCTPN),
                    new SqlParameter("@MaPhieu", maPhieu),
                    new SqlParameter("@MaLo", maLo),
                    new SqlParameter("@SL", soLuongNhap),
                    new SqlParameter("@Gia", donGia)
                });

                MessageBox.Show($"Lập phiếu nhập kho [{maPhieu}] gắn với lô [{maLo}] thành công!",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập phiếu nhập: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
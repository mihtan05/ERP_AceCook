using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrPhanBoViTri : Form
    {
        public FrPhanBoViTri()
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

        private void FrPhanBoViTri_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền phân bổ vị trí lưu trữ!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            LoadViTriKhaDung();

            cboViTriTarget.SelectedIndexChanged += cboViTriTarget_SelectedIndexChanged;
            cboViTriTarget_SelectedIndexChanged(null, null);
        }

        private void LoadViTriKhaDung()
        {
            try
            {
                cboViTriTarget.Items.Clear();
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT mavitri FROM vitriluutru WHERE trangthai != 'Đang bảo trì' AND trangthai != 'Đầy'");
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cboViTriTarget.Items.Add(row["mavitri"].ToString());
                    }
                }
                if (cboViTriTarget.Items.Count > 0) cboViTriTarget.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải vị trí lưu trữ: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboViTriTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboViTriTarget.SelectedItem == null) return;

            string maVT = cboViTriTarget.SelectedItem.ToString();

            string sqlKho = @"
                SELECT k.tenkho 
                FROM vitriluutru vt
                INNER JOIN kho k ON vt.makho = k.makho
                WHERE vt.mavitri = @MaVT";

            DataTable dtKho = DatabaseHelper.ExecuteQuery(sqlKho, new SqlParameter[] { new SqlParameter("@MaVT", maVT) });
            string tenKho = (dtKho != null && dtKho.Rows.Count > 0) ? dtKho.Rows[0]["tenkho"].ToString() : "";

            LoadDanhSachLoHangTheoTenKho(tenKho);
        }

        private void LoadDanhSachLoHangTheoTenKho(string tenKho)
        {
            try
            {
                string sqlWhere = "WHERE lh.soluongcon > 0 AND (pn.trangthai = 'Đã duyệt' OR pn.trangthai = 'Đã nhập kho')";

                // 1. KHO THÀNH PHẨM
                if (tenKho.IndexOf("Thành Phẩm", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    sqlWhere += @" AND (
                        pn.loainhap = 'Thành phẩm' 
                        OR hh.mahang IN (SELECT mahang FROM sanpham)
                        OR hh.mahang LIKE 'SP%' 
                        OR hh.mahang LIKE 'HH%'
                        OR hh.tenhang LIKE '%Mì%' 
                        OR hh.tenhang LIKE '%Miến%'
                    )";
                }
                // 2. KHO NGUYÊN LIỆU
                else if (tenKho.IndexOf("Nguyên Liệu", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    sqlWhere += @" AND (
                        pn.loainhap = 'Nguyên liệu' 
                        OR (hh.mahang LIKE 'NL%' AND hh.mahang NOT IN (SELECT mahang FROM sanpham))
                        OR hh.tenhang LIKE '%Nguyên liệu%' 
                        OR hh.tenhang LIKE '%Bột%' 
                        OR hh.tenhang LIKE '%Gia vị%' 
                        OR hh.tenhang LIKE '%Dầu%'
                    )";
                }
                // 3. KHO BAO BÌ
                else if (tenKho.IndexOf("Bao Bì", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    sqlWhere += @" AND (
                        pn.loainhap = 'Bao bì' 
                        OR hh.mahang LIKE 'BB%'
                        OR hh.tenhang LIKE '%Bao%' 
                        OR hh.tenhang LIKE '%Thùng%' 
                        OR hh.tenhang LIKE '%Màng%'
                    )";
                }
                // 4. KHO VẬT TƯ
                else if (tenKho.IndexOf("Vật Tư", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    sqlWhere += @" AND (
                        pn.loainhap = 'Vật tư' 
                        OR hh.mahang LIKE 'VT%'
                        OR hh.tenhang LIKE '%Vật tư%' 
                        OR hh.tenhang LIKE '%Băng keo%'
                    )";
                }

                string sql = $@"
                    SELECT DISTINCT
                        lh.malo, 
                        hh.tenhang AS ""TenHang"", 
                        lh.soluongcon, 
                        COALESCE(hh.donvitinh, 'Kg') AS ""DonViTinh""
                    FROM lohang lh
                    INNER JOIN hanghoa hh ON lh.mahang = hh.mahang
                    LEFT JOIN chitietphieunhap ctpn ON lh.malo = ctpn.malo
                    LEFT JOIN phieunhap pn ON ctpn.maphieunhap = pn.maphieunhap
                    {sqlWhere}
                    ORDER BY lh.malo DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cboMaLo.DataSource = dt;
                    cboMaLo.DisplayMember = "malo";
                    cboMaLo.ValueMember = "malo";
                    cboMaLo.SelectedIndex = 0;
                }
                else
                {
                    cboMaLo.DataSource = null;
                    txtTenHang.Text = "";
                    txtSoLuong.Text = "0";
                    txtDonVi.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lô hàng theo kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboMaLo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaLo.SelectedItem is DataRowView drv)
            {
                txtTenHang.Text = drv["TenHang"]?.ToString() ?? "";
                txtSoLuong.Text = drv["soluongcon"] != DBNull.Value ? Convert.ToDecimal(drv["soluongcon"]).ToString("0") : "0";
                txtDonVi.Text = drv["DonViTinh"]?.ToString() ?? "";
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (cboMaLo.SelectedValue == null)
            {
                MessageBox.Show("Không có lô hàng phù hợp với loại kho này để phân bổ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboViTriTarget.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Ô chứa kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLo = cboMaLo.SelectedValue.ToString();
            string maVT = cboViTriTarget.SelectedItem.ToString();

            if (!decimal.TryParse(txtSoLuong.Text.Trim(), out decimal qty) || qty <= 0)
            {
                MessageBox.Show("Số lượng lưu trữ phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sqlCheckLo = "SELECT soluongcon FROM lohang WHERE malo = @MaLo";
                DataTable dtLo = DatabaseHelper.ExecuteQuery(sqlCheckLo, new SqlParameter[] { new SqlParameter("@MaLo", maLo) });

                if (dtLo == null || dtLo.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu lô hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal soLuongLoConLai = Convert.ToDecimal(dtLo.Rows[0]["soluongcon"]);

                if (qty > soLuongLoConLai)
                {
                    MessageBox.Show($"Lô hàng [{maLo}] chỉ còn tối đa {soLuongLoConLai:N0} ({txtDonVi.Text}) chưa phân bổ!",
                                    "Cảnh Báo Tồn Kho Lô Hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoLuong.Text = soLuongLoConLai.ToString("0");
                    return;
                }

                string sqlCheckCap = @"
                    SELECT vt.succhuatoida, COALESCE(SUM(tk.soluongton), 0) AS DaDung
                    FROM vitriluutru vt
                    LEFT JOIN tonkho tk ON vt.mavitri = tk.mavitri
                    WHERE vt.mavitri = @MaVT
                    GROUP BY vt.succhuatoida";

                DataTable dtCap = DatabaseHelper.ExecuteQuery(sqlCheckCap, new SqlParameter[] { new SqlParameter("@MaVT", maVT) });

                if (dtCap != null && dtCap.Rows.Count > 0)
                {
                    decimal toiDa = Convert.ToDecimal(dtCap.Rows[0]["succhuatoida"]);
                    decimal daDung = Convert.ToDecimal(dtCap.Rows[0]["DaDung"]);
                    decimal conLaiViTri = toiDa - daDung;

                    if (qty > conLaiViTri)
                    {
                        MessageBox.Show($"Vị trí [{maVT}] chỉ có thể lưu trữ thêm tối đa {conLaiViTri:N0} ({txtDonVi.Text}) nữa!",
                                        "Cảnh Báo Vượt Sức Chứa Ô Chứa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sqlInsertTon = @"INSERT INTO tonkho (maton, malo, mavitri, soluongton, soluongkhadung)
                                            VALUES (@MaTon, @MaLo, @MaViTri, @SL, @SL)";

                    DatabaseHelper.ExecuteNonQuery(sqlInsertTon, new SqlParameter[] {
                        new SqlParameter("@MaTon", "TK-" + Guid.NewGuid().ToString().Substring(0, 8)),
                        new SqlParameter("@MaLo", maLo),
                        new SqlParameter("@MaViTri", maVT),
                        new SqlParameter("@SL", qty)
                    });

                    // Đã loại bỏ lệnh UPDATE trừ soluongcon của bảng lohang tại đây để bảo toàn tồn kho lô hàng

                    decimal newTotalUsed = daDung + qty;
                    if (newTotalUsed >= toiDa)
                    {
                        string sqlUpdateStatus = "UPDATE vitriluutru SET trangthai = 'Đầy' WHERE mavitri = @MaVT AND trangthai != 'Đang bảo trì'";
                        DatabaseHelper.ExecuteNonQuery(sqlUpdateStatus, new SqlParameter[] { new SqlParameter("@MaVT", maVT) });
                    }

                    MessageBox.Show($"Đã phân bổ thành công {qty:N0} ({txtDonVi.Text}) của lô [{maLo}] vào vị trí [{maVT}]!",
                                    "Phân Bổ Vị Trí Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi phân bổ: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrLapDanhSachLayHangFEFO : Form
    {
        private readonly DataRow targetRow;
        private string maPhieuXuat;
        private DataTable dtPickingAllocated;
        private bool isDuHangXuat = true;
        private bool isReadOnlyMode = false;

        public FrLapDanhSachLayHangFEFO(DataRow row)
        {
            InitializeComponent();
            targetRow = row;
        }

        public FrLapDanhSachLayHangFEFO(string maPX)
        {
            InitializeComponent();
            this.maPhieuXuat = maPX;
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

        private void FrLapDanhSachLayHangFEFO_Load(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền lập danh sách lấy hàng!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            if (targetRow != null)
            {
                if (targetRow.Table.Columns.Contains("MaPhieuXuat"))
                    maPhieuXuat = targetRow["MaPhieuXuat"]?.ToString();
                else if (targetRow.Table.Columns.Contains("maPhieuXuat"))
                    maPhieuXuat = targetRow["maPhieuXuat"]?.ToString();
            }

            if (string.IsNullOrEmpty(maPhieuXuat))
            {
                MessageBox.Show("Mã phiếu xuất không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            KiemTraTrangThaiAndLoadData();
        }

        private void KiemTraTrangThaiAndLoadData()
        {
            try
            {
                string sqlCheckPX = "SELECT boPhanYeuCau, trangThai FROM PhieuXuat WHERE maPhieuXuat = @MaPX";
                DataTable dtPX = DatabaseHelper.ExecuteQuery(sqlCheckPX, new SqlParameter[] { new SqlParameter("@MaPX", maPhieuXuat) });

                string trangThaiPX = "";
                string boPhan = "";

                if (dtPX != null && dtPX.Rows.Count > 0)
                {
                    trangThaiPX = dtPX.Rows[0]["trangThai"]?.ToString() ?? "";
                    boPhan = dtPX.Rows[0]["boPhanYeuCau"]?.ToString() ?? "";
                }
                else if (targetRow != null)
                {
                    boPhan = targetRow["BoPhanYeuCau"]?.ToString() ?? targetRow["boPhanYeuCau"]?.ToString() ?? "";
                    trangThaiPX = targetRow["TrangThai"]?.ToString() ?? targetRow["trangThai"]?.ToString() ?? "";
                }

                lblPhieuInfo.Text = $"Mã phiếu xuất: {maPhieuXuat} | Bộ phận yêu cầu: {boPhan}";

                string sqlCheckDS = "SELECT maDanhSach FROM DanhSachLayHang WHERE maPhieuXuat = @MaPX";
                DataTable dtDS = DatabaseHelper.ExecuteQuery(sqlCheckDS, new SqlParameter[] { new SqlParameter("@MaPX", maPhieuXuat) });
                bool daCoDanhSach = (dtDS != null && dtDS.Rows.Count > 0);

                if (trangThaiPX == "Hoàn tất xuất" || trangThaiPX == "Đã hủy" || daCoDanhSach)
                {
                    isReadOnlyMode = true;
                    btnXacNhanVaIn.Enabled = false;
                    btnXacNhanVaIn.BackColor = Color.Gray;
                    btnXacNhanVaIn.Text = "Đã Tạo DS";

                    lblStatusNotice.Visible = true;
                    if (trangThaiPX == "Hoàn tất xuất")
                        lblStatusNotice.Text = "🔒 CHẾ ĐỘ CHỈ XEM: Phiếu xuất đã [Hoàn tất xuất]!";
                    else if (trangThaiPX == "Đã hủy")
                        lblStatusNotice.Text = "🔒 CHẾ ĐỘ CHỈ XEM: Phiếu xuất đã [Đã hủy]!";
                    else
                        lblStatusNotice.Text = "🔒 CHẾ ĐỘ CHỈ XEM: Phiếu xuất đã lập danh sách lấy hàng.";
                }
                else
                {
                    isReadOnlyMode = false;
                    btnXacNhanVaIn.Enabled = true;
                    btnXacNhanVaIn.BackColor = Color.DarkOrange;
                    btnXacNhanVaIn.Text = "Tạo Danh Sách";
                    lblStatusNotice.Visible = false;
                }

                if (daCoDanhSach)
                {
                    LoadSavedPickingList(maPhieuXuat);
                }
                else
                {
                    TinhToanLayHangFEFO(maPhieuXuat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra trạng thái phiếu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSavedPickingList(string maPX)
        {
            try
            {
                string sqlSaved = @"
                    SELECT 
                        tk.maTon AS MaTon,
                        hh.MaHang,
                        hh.TenHang,
                        vt.maViTri AS MaViTri,
                        lh.maLo AS MaLo,
                        lh.hanSuDung AS HanSuDung,
                        ctlh.soLuongCanLay AS SoLuongCanLay
                    FROM ChiTietLayHang ctlh
                    INNER JOIN DanhSachLayHang dslh ON ctlh.maDanhSach = dslh.maDanhSach
                    INNER JOIN TonKho tk ON ctlh.maTon = tk.maTon
                    INNER JOIN LoHang lh ON tk.maLo = lh.maLo
                    INNER JOIN HangHoa hh ON lh.MaHang = hh.MaHang
                    INNER JOIN ViTriLuuTru vt ON tk.maViTri = vt.maViTri
                    WHERE dslh.maPhieuXuat = @MaPX
                    ORDER BY lh.hanSuDung ASC";

                dtPickingAllocated = DatabaseHelper.ExecuteQuery(sqlSaved, new SqlParameter[] { new SqlParameter("@MaPX", maPX) });
                dgvPickingList.DataSource = dtPickingAllocated;
                DinhDangLuoiPicking();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách lấy hàng đã lưu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TinhToanLayHangFEFO(string maPX)
        {
            try
            {
                dtPickingAllocated = new DataTable();
                dtPickingAllocated.Columns.Add("MaTon", typeof(string));
                dtPickingAllocated.Columns.Add("MaHang", typeof(string));
                dtPickingAllocated.Columns.Add("TenHang", typeof(string));
                dtPickingAllocated.Columns.Add("MaViTri", typeof(string));
                dtPickingAllocated.Columns.Add("MaLo", typeof(string));
                dtPickingAllocated.Columns.Add("HanSuDung", typeof(DateTime));
                dtPickingAllocated.Columns.Add("SoLuongCanLay", typeof(decimal));

                string sqlCTPX = @"
                    SELECT ctx.MaHang, hh.TenHang, ctx.soLuongYeuCau
                    FROM ChiTietPhieuXuat ctx
                    INNER JOIN HangHoa hh ON ctx.MaHang = hh.MaHang
                    WHERE ctx.maPhieuXuat = @MaPX";

                DataTable dtYeuCau = DatabaseHelper.ExecuteQuery(sqlCTPX, new SqlParameter[] { new SqlParameter("@MaPX", maPX) });

                if (dtYeuCau.Rows.Count == 0)
                {
                    MessageBox.Show("Phiếu xuất này chưa có chi tiết mặt hàng yêu cầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnXacNhanVaIn.Enabled = false;
                    return;
                }

                isDuHangXuat = true;
                string thongBaoThieuHang = "";

                foreach (DataRow rowYC in dtYeuCau.Rows)
                {
                    string maHang = rowYC["MaHang"].ToString();
                    string tenHang = rowYC["TenHang"].ToString();
                    decimal soLuongCanXuat = Convert.ToDecimal(rowYC["soLuongYeuCau"]);
                    decimal soLuongDaPhanBo = 0;

                    string sqlKho = @"
                        SELECT tk.maTon, tk.maViTri, tk.maLo, lh.hanSuDung, tk.soLuongKhaDung
                        FROM TonKho tk
                        INNER JOIN LoHang lh ON tk.maLo = lh.maLo
                        WHERE lh.MaHang = @MaHang 
                          AND COALESCE(tk.soLuongKhaDung, 0) > 0
                          AND (lh.trangThai IS NULL OR lh.trangThai != 'Phong tỏa')
                        ORDER BY lh.hanSuDung ASC";

                    DataTable dtTon = DatabaseHelper.ExecuteQuery(sqlKho, new SqlParameter[] { new SqlParameter("@MaHang", maHang) });

                    foreach (DataRow rowTon in dtTon.Rows)
                    {
                        if (soLuongDaPhanBo >= soLuongCanXuat) break;

                        decimal tonKhaDung = Convert.ToDecimal(rowTon["soLuongKhaDung"]);
                        decimal conThieu = soLuongCanXuat - soLuongDaPhanBo;
                        decimal layTuLoNay = Math.Min(conThieu, tonKhaDung);

                        dtPickingAllocated.Rows.Add(
                            rowTon["maTon"].ToString(),
                            maHang,
                            tenHang,
                            rowTon["maViTri"].ToString(),
                            rowTon["maLo"].ToString(),
                            Convert.ToDateTime(rowTon["hanSuDung"]),
                            layTuLoNay
                        );

                        soLuongDaPhanBo += layTuLoNay;
                    }

                    if (soLuongDaPhanBo < soLuongCanXuat)
                    {
                        isDuHangXuat = false;
                        thongBaoThieuHang += $"- {tenHang} (Mã: {maHang}): Yêu cầu {soLuongCanXuat:#,##0.##}, Kho chỉ có {soLuongDaPhanBo:#,##0.##} (Thiếu {soLuongCanXuat - soLuongDaPhanBo:#,##0.##})\n";
                    }
                }

                dgvPickingList.DataSource = dtPickingAllocated;
                DinhDangLuoiPicking();

                if (!isDuHangXuat && !isReadOnlyMode)
                {
                    MessageBox.Show("CẢNH BÁO: Tồn kho không đủ để đáp ứng lệnh xuất!\n\nChi tiết thiếu hàng:\n" + thongBaoThieuHang, "Không đủ hàng tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnXacNhanVaIn.Enabled = false;
                    btnXacNhanVaIn.BackColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính toán FEFO: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangLuoiPicking()
        {
            if (dgvPickingList.Columns.Contains("MaTon")) dgvPickingList.Columns["MaTon"].Visible = false;
            if (dgvPickingList.Columns.Contains("MaHang")) dgvPickingList.Columns["MaHang"].HeaderText = "Mã SP";
            if (dgvPickingList.Columns.Contains("TenHang")) dgvPickingList.Columns["TenHang"].HeaderText = "Tên Sản Phẩm";
            if (dgvPickingList.Columns.Contains("MaViTri")) dgvPickingList.Columns["MaViTri"].HeaderText = "Vị Trí Kệ";
            if (dgvPickingList.Columns.Contains("MaLo")) dgvPickingList.Columns["MaLo"].HeaderText = "Số Lô";
            if (dgvPickingList.Columns.Contains("HanSuDung"))
            {
                dgvPickingList.Columns["HanSuDung"].HeaderText = "Hạn Sử Dụng";
                dgvPickingList.Columns["HanSuDung"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvPickingList.Columns.Contains("SoLuongCanLay"))
            {
                dgvPickingList.Columns["SoLuongCanLay"].HeaderText = "Số Lượng Cần Lấy";
                dgvPickingList.Columns["SoLuongCanLay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPickingList.Columns["SoLuongCanLay"].DefaultCellStyle.Format = "#,##0.##";
                dgvPickingList.Columns["SoLuongCanLay"].DefaultCellStyle.Font = new Font(dgvPickingList.Font, FontStyle.Bold);
            }
        }

        private void btnXacNhanVaIn_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show("Bạn không có quyền thực hiện chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (isReadOnlyMode)
            {
                MessageBox.Show("Phiếu xuất này đã hoàn tất hoặc đã tạo danh sách trước đó, không thể bấm tạo lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isDuHangXuat || dtPickingAllocated == null || dtPickingAllocated.Rows.Count == 0)
            {
                MessageBox.Show("Không thể tạo danh sách lấy hàng do thiếu tồn kho hoặc chưa có dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Sinh mã danh sách an toàn chống trùng lặp theo chuẩn thời gian thực (rút gọn để vừa vặn kiểu VARCHAR(20))
            string maDS = "DS-" + DateTime.Now.ToString("yyMMddHHmmss");

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string sqlDS = @"
                        INSERT INTO DanhSachLayHang (maDanhSach, maPhieuXuat, ngayLap, trangThai)
                        VALUES (@MaDS, @MaPX, CURRENT_TIMESTAMP, 'Đang lấy hàng')";

                    SqlCommand cmdDS = new SqlCommand(sqlDS, conn, transaction);
                    cmdDS.Parameters.AddWithValue("@MaDS", maDS);
                    cmdDS.Parameters.AddWithValue("@MaPX", maPhieuXuat);
                    cmdDS.ExecuteNonQuery();

                    foreach (DataRow row in dtPickingAllocated.Rows)
                    {
                        // Sinh mã chi tiết an toàn chống trùng lặp
                        string maCTLH = "CL-" + DateTime.Now.ToString("yyMMddHHmmssfff") + new Random().Next(10, 99);
                        if (maCTLH.Length > 20) maCTLH = maCTLH.Substring(0, 20);

                        string sqlCTLH = @"
                            INSERT INTO ChiTietLayHang (maCTLH, maDanhSach, maTon, soLuongCanLay, soLuongThucLay)
                            VALUES (@MaCTLH, @MaDS, @MaTon, @SLCanLay, @SLThucLay)";

                        SqlCommand cmdCTLH = new SqlCommand(sqlCTLH, conn, transaction);
                        cmdCTLH.Parameters.AddWithValue("@MaCTLH", maCTLH);
                        cmdCTLH.Parameters.AddWithValue("@MaDS", maDS);
                        cmdCTLH.Parameters.AddWithValue("@MaTon", row["MaTon"].ToString());
                        cmdCTLH.Parameters.AddWithValue("@SLCanLay", Convert.ToDecimal(row["SoLuongCanLay"]));
                        cmdCTLH.Parameters.AddWithValue("@SLThucLay", Convert.ToDecimal(row["SoLuongCanLay"]));
                        cmdCTLH.ExecuteNonQuery();
                    }

                    string sqlPX = "UPDATE PhieuXuat SET trangThai = 'Đang lấy hàng' WHERE maPhieuXuat = @MaPX";
                    SqlCommand cmdPX = new SqlCommand(sqlPX, conn, transaction);
                    cmdPX.Parameters.AddWithValue("@MaPX", maPhieuXuat);
                    cmdPX.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show($"Lập Danh sách lấy hàng [{maDS}] theo chuẩn FEFO thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
               catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi lưu Danh sách lấy hàng: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e) => this.Close();
    }
}
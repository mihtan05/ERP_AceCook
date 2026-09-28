using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrQLTonkho : Form
    {
        private const string PLACEHOLDER_TEXT = "Tìm kiếm theo mã vật tư, tên vật tư, đơn vị tính, vị trí...";

        public FrQLTonkho()
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

        private bool KiemTraQuyenQuanLyHoacAdmin(string tenChucNang)
        {
            string chucVu = UserSession.ChucVu ?? "";
            bool isToanQuyen = chucVu.Equals("Quản lý kho", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản lý", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                               chucVu.Equals("Quản trị viên", StringComparison.OrdinalIgnoreCase);

            if (!isToanQuyen)
            {
                MessageBox.Show($"Tài khoản vai trò ({chucVu}) không có quyền thực hiện [{tenChucNang}]!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void FrQLTonkho_Load(object sender, EventArgs e)
        {
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            this.VisibleChanged += FrQLTonkho_VisibleChanged;
            this.Activated += FrQLTonkho_Activated;

            InitGridColumns();
            LoadComboboxKho();

            if (cboFilterCanhBao.Items.Count > 0)
                cboFilterCanhBao.SelectedIndex = 0;

            txtSearch.Text = PLACEHOLDER_TEXT;
            txtSearch.ForeColor = Color.Gray;

            LoadDataTonKho();
        }

        private void FrQLTonkho_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadDataTonKho();
            }
        }

        private void FrQLTonkho_Activated(object sender, EventArgs e)
        {
            LoadDataTonKho();
        }

        private void InitGridColumns()
        {
            dgvTonKho.DataSource = null;
            dgvTonKho.Columns.Clear();
            dgvTonKho.AutoGenerateColumns = false;

            dgvTonKho.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvTonKho.GridColor = Color.FromArgb(220, 224, 230);

            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaVatTu",
                DataPropertyName = "MaVatTu",
                HeaderText = "Mã Vật Tư / SP",
                FillWeight = 85
            });

            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenVatTu",
                DataPropertyName = "TenVatTu",
                HeaderText = "Tên Vật Tư / Sản Phẩm",
                FillWeight = 160
            });

            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenKho",
                DataPropertyName = "TenKho",
                HeaderText = "Nhà Kho",
                FillWeight = 110
            });

            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ViTri",
                DataPropertyName = "ViTri",
                HeaderText = "Vị Trí Lưu Trữ",
                FillWeight = 95
            });

            var colSoLuong = new DataGridViewTextBoxColumn
            {
                Name = "SoLuongTon",
                DataPropertyName = "SoLuongTon",
                HeaderText = "Tồn Thực Tế",
                FillWeight = 90
            };
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuong.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuong.DefaultCellStyle.Format = "#,##0.##";
            dgvTonKho.Columns.Add(colSoLuong);

            var colDVT = new DataGridViewTextBoxColumn
            {
                Name = "DonViTinh",
                DataPropertyName = "DonViTinh",
                HeaderText = "ĐVT",
                FillWeight = 50
            };
            colDVT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDVT.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTonKho.Columns.Add(colDVT);

            var colMin = new DataGridViewTextBoxColumn
            {
                Name = "NguongToiThieu",
                DataPropertyName = "NguongToiThieu",
                HeaderText = "Min (Tối thiểu)",
                FillWeight = 80
            };
            colMin.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colMin.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            colMin.DefaultCellStyle.Format = "#,##0.##";
            dgvTonKho.Columns.Add(colMin);

            var colMax = new DataGridViewTextBoxColumn
            {
                Name = "NguongToiDa",
                DataPropertyName = "NguongToiDa",
                HeaderText = "Max (Tối đa)",
                FillWeight = 80
            };
            colMax.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colMax.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            colMax.DefaultCellStyle.Format = "#,##0.##";
            dgvTonKho.Columns.Add(colMax);

            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThaiTon",
                DataPropertyName = "TrangThaiTon",
                HeaderText = "Trạng Thái Cảnh Báo",
                FillWeight = 110
            });
        }

        private void LoadComboboxKho()
        {
            try
            {
                string sql = "SELECT makho, tenkho FROM kho ORDER BY makho";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                cboFilterKho.Items.Clear();
                cboFilterKho.Items.Add("-- Tất cả Kho --");

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cboFilterKho.Items.Add(row["tenkho"]?.ToString()?.Trim());
                    }
                }
                cboFilterKho.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadDataTonKho()
        {
            try
            {
                string query = @"
                    SELECT 
                        hh.mahang AS MaVatTu,
                        hh.tenhang AS TenVatTu,
                        k.tenkho AS TenKho,
                        vt.mavitri AS ViTri,
                        COALESCE(tk.soluongton, 0) AS SoLuongTon,
                        COALESCE(hh.donvitinh, 'Kg') AS DonViTinh,
                        CASE 
                            WHEN hh.mahang LIKE 'HH%' THEN 500
                            WHEN hh.mahang LIKE 'NL%' THEN 100
                            ELSE 200
                        END AS NguongToiThieu,
                        COALESCE(vt.succhuatoida, 10000) AS NguongToiDa,
                        CASE 
                            WHEN COALESCE(tk.soluongton, 0) = 0 THEN 'Hết hàng'
                            WHEN COALESCE(tk.soluongton, 0) < (CASE WHEN hh.mahang LIKE 'HH%' THEN 500 WHEN hh.mahang LIKE 'NL%' THEN 100 ELSE 200 END) THEN 'Cảnh báo thiếu'
                            WHEN COALESCE(tk.soluongton, 0) > COALESCE(vt.succhuatoida, 10000) THEN 'Tồn kho quá tải'
                            ELSE 'An toàn'
                        END AS TrangThaiTon
                    FROM hanghoa hh
                    INNER JOIN lohang lh ON TRIM(hh.mahang) = TRIM(lh.mahang)
                    INNER JOIN tonkho tk ON TRIM(lh.malo) = TRIM(tk.malo)
                    INNER JOIN vitriluutru vt ON TRIM(tk.mavitri) = TRIM(vt.mavitri)
                    INNER JOIN kho k ON TRIM(vt.makho) = TRIM(k.makho)
                    WHERE 1=1";

                if (cboFilterKho.SelectedIndex > 0)
                {
                    query += " AND TRIM(k.tenkho) = TRIM(@TenKho)";
                }

                string searchKey = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(searchKey) && searchKey != PLACEHOLDER_TEXT)
                {
                    query += " AND (hh.mahang ILIKE @Search OR hh.tenhang ILIKE @Search OR hh.donvitinh ILIKE @Search OR vt.mavitri ILIKE @Search)";
                }

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenKho", cboFilterKho.SelectedItem?.ToString() ?? ""),
                    new SqlParameter("@Search", "%" + searchKey + "%")
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                dgvTonKho.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tồn kho: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            LoadDataTonKho();

            if (cboFilterCanhBao.SelectedIndex > 0 && dgvTonKho.DataSource is DataTable table)
            {
                string selectedStatus = cboFilterCanhBao.SelectedItem.ToString();
                table.DefaultView.RowFilter = "TrangThaiTon = '" + selectedStatus + "'";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Filter_Changed(sender, e);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == PLACEHOLDER_TEXT)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = PLACEHOLDER_TEXT;
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void dgvTonKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvTonKho.Columns[e.ColumnIndex].DataPropertyName == "TrangThaiTon" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "An toàn")
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
                else if (status == "Cảnh báo thiếu")
                {
                    e.CellStyle.ForeColor = Color.OrangeRed;
                    e.CellStyle.Font = new Font(dgvTonKho.Font, FontStyle.Bold);
                }
                else if (status == "Hết hàng")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvTonKho.Font, FontStyle.Bold);
                }
                else if (status == "Tồn kho quá tải")
                {
                    e.CellStyle.ForeColor = Color.DarkMagenta;
                    e.CellStyle.Font = new Font(dgvTonKho.Font, FontStyle.Bold);
                }
            }
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            FrTraCuuTonKho frm = new FrTraCuuTonKho();
            frm.ShowDialog();
        }

        private void btnCanhBao_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền truy cập chức năng cảnh báo tồn kho!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrCanhBaoTonKho frm = new FrCanhBaoTonKho();
            frm.ShowDialog();
        }

        private void btnTaoKiemKe_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền tạo phiếu kiểm kê!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrTaoPhieuKiemKe frm = new FrTaoPhieuKiemKe();
            if (frm.ShowDialog() == DialogResult.OK) LoadDataTonKho();
        }

        private void btnNhapKiemKe_Click(object sender, EventArgs e)
        {
            if (!IsPermittedUser())
            {
                MessageBox.Show($"Tài khoản vai trò ({UserSession.ChucVu}) không có quyền nhập kết quả kiểm kê!",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrNhapKetQuaKiemKe frm = new FrNhapKetQuaKiemKe();
            frm.ShowDialog();
        }

        private void btnDuyetDieuChinh_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanLyHoacAdmin("Duyệt điều chỉnh tồn kho")) return;

            FrDuyetDieuChinhTonKho frm = new FrDuyetDieuChinhTonKho();
            if (frm.ShowDialog() == DialogResult.OK) LoadDataTonKho();
        }
    }
}

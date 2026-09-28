using System;
using System.Drawing;
using System.Windows.Forms;
using HR_Management.BLL;

namespace HR_Management.GUI.UserControls
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class ucDashboard : UserControl
    {
        private readonly ThongKeBaoCaoBLL _bllBaoCao = new ThongKeBaoCaoBLL();
        private readonly HopDongBLL _bllHopDong = new HopDongBLL();
        private DataGridView dgvExpiringContracts = null!;

        private Label lblTotalEmployees = null!;
        private Label lblTotalDepartments = null!;
        private Label lblActiveContracts = null!;
        private Label lblExpiringContracts = null!;

        public ucDashboard()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColor.BodyBg;
            this.Padding = new Padding(16, 12, 16, 16);

            // KPI Cards row
            TableLayoutPanel pnlCards = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 4,
                RowCount = 1,
                Padding = new Padding(0, 0, 0, 10)
            };
            pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCards.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            lblTotalEmployees = new Label();
            lblTotalDepartments = new Label();
            lblActiveContracts = new Label();
            lblExpiringContracts = new Label();

            pnlCards.Controls.Add(UIHelper.CreateKpiCard("👥 TỔNG NHÂN SỰ", lblTotalEmployees, ThemeColor.Primary, "Nhân sự trong toàn công ty"), 0, 0);
            pnlCards.Controls.Add(UIHelper.CreateKpiCard("🏢 PHÒNG BAN", lblTotalDepartments, ThemeColor.Success, "Bộ máy phòng ban hoạt động"), 1, 0);
            pnlCards.Controls.Add(UIHelper.CreateKpiCard("📝 HĐ HIỆU LỰC", lblActiveContracts, ThemeColor.Info, "Hợp đồng lao động còn hạn"), 2, 0);
            pnlCards.Controls.Add(UIHelper.CreateKpiCard("⚠ SẮP HẾT HẠN (30 NGÀY)", lblExpiringContracts, ThemeColor.Warning, "Cần theo dõi & gia hạn sớm"), 3, 0);

            // Warning Table Card Container
            Panel pnlTableContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(18, 16, 18, 16)
            };
            pnlTableContainer.Paint += (s, e) =>
            {
                using (Pen p = new Pen(ThemeColor.CardBorder, 1))
                {
                    e.Graphics.DrawRectangle(p, 0, 0, pnlTableContainer.Width - 1, pnlTableContainer.Height - 1);
                }
            };

            Panel pnlTableHeader = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 0, 0, 16)
            };

            Label lblTableTitle = new Label
            {
                Text = "⚡ DANH SÁCH HỢP ĐỒNG SẮP HẾT HẠN CẦN XỬ LÝ GIA HẠN",
                Font = ThemeColor.SubHeaderFont,
                ForeColor = ThemeColor.TextPrimary,
                Dock = DockStyle.Top,
                AutoSize = true
            };
            pnlTableHeader.Controls.Add(lblTableTitle);

            dgvExpiringContracts = new DataGridView { Dock = DockStyle.Fill };
            UIHelper.StyleDataGridView(dgvExpiringContracts);
            dgvExpiringContracts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvExpiringContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã HĐ", DataPropertyName = "MaHopDong", FillWeight = 16 });
            dgvExpiringContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã NV", DataPropertyName = "ID_NV", FillWeight = 14 });
            dgvExpiringContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ và Tên", DataPropertyName = "TenNV", FillWeight = 28 });
            dgvExpiringContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Loại Hợp Đồng", DataPropertyName = "LoaiHopDong", FillWeight = 28 });
            dgvExpiringContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Bắt Đầu", DataPropertyName = "NgayBatDau", FillWeight = 18, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvExpiringContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hết Hạn", DataPropertyName = "NgayKetThuc", FillWeight = 18, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvExpiringContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng Thái", DataPropertyName = "TrangThai", FillWeight = 22 });

            pnlTableContainer.Controls.Add(dgvExpiringContracts);
            pnlTableContainer.Controls.Add(pnlTableHeader);
            pnlTableHeader.SendToBack();
            dgvExpiringContracts.BringToFront();

            this.Controls.Add(pnlCards);
            this.Controls.Add(pnlTableContainer);
            pnlCards.SendToBack();
            pnlTableContainer.BringToFront();
        }

        public void LoadData()
        {
            try
            {
                var overview = _bllBaoCao.GetOverview();
                lblTotalEmployees.Text = overview.TongNhanVien.ToString("N0");
                lblTotalDepartments.Text = overview.TongPhongBan.ToString("N0");
                lblActiveContracts.Text = overview.HopDongHieuLuc.ToString("N0");
                lblExpiringContracts.Text = overview.HopDongSapHetHan.ToString("N0");

                var expiringList = _bllHopDong.GetExpiringContracts(30);
                dgvExpiringContracts.DataSource = expiringList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin tổng quan: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

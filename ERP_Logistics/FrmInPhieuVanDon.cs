using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ERP.DTO;

namespace ERP
{
    public partial class FrmInPhieuVanDon : Form
    {
        private readonly DonVanChuyen don;
        private PrintDocument printDoc;

        public FrmInPhieuVanDon(DonVanChuyen donVanChuyen)
        {
            InitializeComponent();
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplyActionButton(this.btnPrint, ButtonRole.Report);
            UIThemeHelper.ApplyActionButton(this.btnClose, ButtonRole.Secondary);
            this.don = donVanChuyen;
            LoadThongTinVanDon();
        }

        private void LoadThongTinVanDon()
        {
            if (don == null) return;

            // Mã vận đơn & ngày giờ
            lblMaDon.Text = $"MÃ VẬN ĐƠN: {don.ID_DonVC}";
            lblNgayGio.Text = $"Thời gian xuất phát: {don.ThoiGianKhoiHanh:dd/MM/yyyy HH:mm}";

            // Khách hàng / Đơn hàng Bán
            if (!string.IsNullOrWhiteSpace(don.ID_DH))
            {
                lblValMaDH.Text = $"{don.ID_DH}";
            }
            else
            {
                lblValMaDH.Text = "-- Chuyến giao tự do (Không liên kết ĐH) --";
                lblValMaDH.ForeColor = Color.Gray;
            }

            lblValKhachHang.Text = !string.IsNullOrWhiteSpace(don.TenKhachHang) ? don.TenKhachHang : "Khách hàng vãng lai";
            lblValSDT.Text = !string.IsNullOrWhiteSpace(don.SDTKhachHang) ? don.SDTKhachHang : "Chưa cập nhật";
            lblValDiaChi.Text = !string.IsNullOrWhiteSpace(don.DiaChiGiao) ? don.DiaChiGiao : "Theo lộ trình thỏa thuận";

            // Phương tiện & Tuyến đường
            lblValBienSo.Text = !string.IsNullOrWhiteSpace(don.BienSoXe) ? don.BienSoXe : "Chưa gán xe";
            lblValDiemVC.Text = !string.IsNullOrWhiteSpace(don.TenDVC) ? don.TenDVC : (!string.IsNullOrWhiteSpace(don.MaDVC) ? don.MaDVC : "Chưa xác định");
            lblValTrangThai.Text = !string.IsNullOrWhiteSpace(don.TrangThaiDon) ? don.TrangThaiDon : "Khởi tạo";

            // Màu trạng thái
            if (don.TrangThaiDon == "Đang vận chuyển")
            {
                lblValTrangThai.ForeColor = Color.FromArgb(255, 152, 0);
            }
            else if (don.TrangThaiDon == "Hoàn thành")
            {
                lblValTrangThai.ForeColor = Color.FromArgb(40, 167, 69);
            }
            else if (don.TrangThaiDon == "Đã hủy")
            {
                lblValTrangThai.ForeColor = Color.FromArgb(220, 53, 69);
            }

            // Chi tiết hàng hóa
            dgvHangHoa.Rows.Clear();
            string tenSP = !string.IsNullOrWhiteSpace(don.TenHang) ? don.TenHang : don.ID_SP;
            dgvHangHoa.Rows.Add("1", don.ID_SP, tenSP, $"{don.SoLuongGiao:N0} kiện");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                printDoc = new PrintDocument();
                printDoc.DocumentName = $"VanDon_{don?.ID_DonVC}";
                printDoc.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDoc;
                printDialog.UseEXDialog = true;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                    MessageBox.Show("Lệnh in đã được gửi thành công đến máy in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện in ấn: " + ex.Message, "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Chụp toàn bộ nội dung mẫu phiếu (pnlPaper) thành hình ảnh chuẩn và in ra trang giấy
            Bitmap bmp = new Bitmap(pnlPaper.Width, pnlPaper.Height);
            pnlPaper.DrawToBitmap(bmp, new Rectangle(0, 0, pnlPaper.Width, pnlPaper.Height));

            // Căn chỉnh tỷ lệ phù hợp với trang in A4
            Rectangle pageBounds = e.MarginBounds;
            float scale = Math.Min((float)pageBounds.Width / bmp.Width, (float)pageBounds.Height / bmp.Height);
            int printWidth = (int)(bmp.Width * scale);
            int printHeight = (int)(bmp.Height * scale);
            int posX = pageBounds.Left + (pageBounds.Width - printWidth) / 2;
            int posY = pageBounds.Top;

            e.Graphics.DrawImage(bmp, posX, posY, printWidth, printHeight);
            e.HasMorePages = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

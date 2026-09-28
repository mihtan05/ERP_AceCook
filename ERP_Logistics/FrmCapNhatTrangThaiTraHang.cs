using System;
using System.Drawing;
using System.Windows.Forms;
using ERP.BLL;
using ERP.DTO;

namespace ERP
{
    public partial class FrmCapNhatTrangThaiTraHang : Form
    {
        private readonly PhieuTraHangBLL bll = new PhieuTraHangBLL();
        private readonly string idPhieuTra;
        private readonly string trangThaiHienTai;

        public FrmCapNhatTrangThaiTraHang(string id, string currentStatus)
        {
            InitializeComponent();
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplyActionButton(this.btnLuu, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnHuy, ButtonRole.Secondary);
            idPhieuTra = id;
            trangThaiHienTai = currentStatus;

            lblInfo.Text = $"Phiếu trả hàng: [{idPhieuTra}]";
            lblCurrent.Text = $"Trạng thái hiện tại: {trangThaiHienTai}";

            // Thiết lập danh sách trạng thái kế tiếp theo State Machine chuẩn
            cboTrangThaiMoi.Items.Clear();

            if (trangThaiHienTai == "Đã xử lý" || trangThaiHienTai == "Đã nhập kho")
            {
                cboTrangThaiMoi.Items.Add(trangThaiHienTai);
                cboTrangThaiMoi.SelectedIndex = 0;
                cboTrangThaiMoi.Enabled = false;
                btnLuu.Enabled = false;
                btnLuu.Text = "🔒 Đã xử lý";
                lblCurrent.Text = $"Trạng thái: {trangThaiHienTai} 🔒 (Kho đã duyệt / Hoàn tất)";
                lblCurrent.ForeColor = Color.ForestGreen;
            }
            else if (trangThaiHienTai == "Đã hủy" || trangThaiHienTai == "Từ chối")
            {
                cboTrangThaiMoi.Items.Add(trangThaiHienTai);
                cboTrangThaiMoi.SelectedIndex = 0;
                cboTrangThaiMoi.Enabled = false;
                btnLuu.Enabled = false;
                btnLuu.Text = "🚫 Đã kết thúc";
                lblCurrent.Text = $"Trạng thái: {trangThaiHienTai} 🚫 (Kho từ chối / Đã hủy)";
                lblCurrent.ForeColor = Color.Crimson;
            }
            else if (trangThaiHienTai == "Chờ xử lý")
            {
                // Chờ xử lý -> Đang thu hồi hoặc Đã hủy
                cboTrangThaiMoi.Items.Add("Đang thu hồi");
                cboTrangThaiMoi.Items.Add("Đã hủy");
                cboTrangThaiMoi.SelectedIndex = 0;
            }
            else if (trangThaiHienTai == "Đang thu hồi" || trangThaiHienTai == "Đang lấy hàng")
            {
                // Đang thu hồi -> Đã xử lý hoặc Đã hủy
                cboTrangThaiMoi.Items.Add("Đã xử lý");
                cboTrangThaiMoi.Items.Add("Đã hủy");
                cboTrangThaiMoi.SelectedIndex = 0;
            }
            else
            {
                cboTrangThaiMoi.Items.Add("Chờ xử lý");
                cboTrangThaiMoi.Items.Add("Đang thu hồi");
                cboTrangThaiMoi.Items.Add("Đã xử lý");
                cboTrangThaiMoi.Items.Add("Đã hủy");
                cboTrangThaiMoi.SelectedItem = trangThaiHienTai;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (trangThaiHienTai == "Đã xử lý" || trangThaiHienTai == "Đã nhập kho")
            {
                MessageBox.Show("Phiếu trả hàng này đã HOÀN THÀNH / ĐƯỢC KHO DUYỆT nên không thể thay đổi trạng thái!", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (trangThaiHienTai == "Đã hủy" || trangThaiHienTai == "Từ chối")
            {
                MessageBox.Show("Phiếu trả hàng này ĐÃ HỦY / TỪ CHỐI nên không thể thay đổi trạng thái!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboTrangThaiMoi.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string statusMoi = cboTrangThaiMoi.SelectedItem.ToString();
            if (statusMoi == trangThaiHienTai)
            {
                MessageBox.Show("Trạng thái mới trùng với trạng thái hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                PhieuTraHang p = bll.GetByID(idPhieuTra);
                if (p == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin phiếu trả hàng trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Nếu chuyển sang Đang thu hồi: kiểm tra xe có bận không
                if (statusMoi == "Đang thu hồi" || statusMoi == "Đang lấy hàng")
                {
                    if (bll.CheckXeDangBan(p.BienSoXe, p.ID_PhieuTra, out string lyDoBan))
                    {
                        MessageBox.Show($"Không thể chuyển sang trạng thái '{statusMoi}'!\n\nLý do: {lyDoBan}.\n\nVui lòng đợi phương tiện rảnh hoặc đổi xe khác trước khi điều xe đi thu hồi.", "Cảnh báo phương tiện đang bận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                p.TrangThai = statusMoi;
                bll.SuaPhieu(p);

                MessageBox.Show($"Cập nhật trạng thái Phiếu trả hàng [{idPhieuTra}] sang [{statusMoi}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

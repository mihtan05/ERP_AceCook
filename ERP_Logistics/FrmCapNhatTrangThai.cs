using System;
using System.Windows.Forms;
using ERP.BLL;
using ERP.DTO;

namespace ERP
{
    public partial class FrmCapNhatTrangThai : Form
    {
        private readonly DonVanChuyenBLL bll = new DonVanChuyenBLL();
        private readonly string idDonVC;
        private readonly string trangThaiHienTai;

        public FrmCapNhatTrangThai(string id, string currentStatus)
        {
            InitializeComponent();
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplyActionButton(this.btnLuu, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnHuy, ButtonRole.Secondary);
            idDonVC = id;
            trangThaiHienTai = currentStatus;

            lblInfo.Text = $"Đơn vận chuyển: [{idDonVC}]";
            lblCurrent.Text = $"Trạng thái hiện tại: {trangThaiHienTai}";

            // Thiết lập danh sách trạng thái kế tiếp hợp lệ theo mô hình State Machine
            cboTrangThaiMoi.Items.Clear();

            if (trangThaiHienTai == "Hoàn thành")
            {
                cboTrangThaiMoi.Items.Add("Hoàn thành");
                cboTrangThaiMoi.SelectedIndex = 0;
                cboTrangThaiMoi.Enabled = false;
                btnLuu.Enabled = false;
                btnLuu.Text = "🔒 Đã hoàn tất";
                lblCurrent.Text = $"Trạng thái: {trangThaiHienTai} 🔒 (Không được thay đổi)";
                lblCurrent.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else if (trangThaiHienTai == "Đã hủy")
            {
                cboTrangThaiMoi.Items.Add("Đã hủy");
                cboTrangThaiMoi.SelectedIndex = 0;
                cboTrangThaiMoi.Enabled = false;
                btnLuu.Enabled = false;
                btnLuu.Text = "🚫 Đã hủy";
                lblCurrent.Text = $"Trạng thái: {trangThaiHienTai} 🚫 (Không thể thay đổi)";
                lblCurrent.ForeColor = System.Drawing.Color.Crimson;
            }
            else if (trangThaiHienTai == "Khởi tạo")
            {
                // Từ Khởi tạo -> chỉ có thể sang 'Đang vận chuyển' hoặc 'Đã hủy'
                cboTrangThaiMoi.Items.Add("Đang vận chuyển");
                cboTrangThaiMoi.Items.Add("Đã hủy");
                cboTrangThaiMoi.SelectedIndex = 0;
            }
            else if (trangThaiHienTai == "Đang vận chuyển")
            {
                // Từ Đang vận chuyển -> chỉ có thể sang 'Hoàn thành' hoặc 'Đã hủy' (không thể quay về 'Khởi tạo')
                cboTrangThaiMoi.Items.Add("Hoàn thành");
                cboTrangThaiMoi.Items.Add("Đã hủy");
                cboTrangThaiMoi.SelectedIndex = 0;
            }
            else
            {
                cboTrangThaiMoi.Items.Add("Khởi tạo");
                cboTrangThaiMoi.Items.Add("Đang vận chuyển");
                cboTrangThaiMoi.Items.Add("Hoàn thành");
                cboTrangThaiMoi.Items.Add("Đã hủy");
                cboTrangThaiMoi.SelectedItem = trangThaiHienTai;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (trangThaiHienTai == "Hoàn thành")
            {
                MessageBox.Show("Đơn vận chuyển đã HOÀN THÀNH nên không thể thay đổi trạng thái!\n\nNếu khách hàng muốn đổi trả hàng, vui lòng sử dụng chức năng 'Quản lý trả hàng' để lập phiếu thu hồi.", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (trangThaiHienTai == "Đã hủy")
            {
                MessageBox.Show("Đơn vận chuyển đã ĐÃ HỦY nên không thể thay đổi trạng thái!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Kiểm tra quy tắc chuyển đổi trạng thái
            if (trangThaiHienTai == "Khởi tạo" && statusMoi == "Hoàn thành")
            {
                MessageBox.Show("Đơn vận chuyển chưa xuất phát ('Khởi tạo') không thể chuyển trực tiếp sang 'Hoàn thành'!\nVui lòng chuyển sang 'Đang vận chuyển' trước khi hoàn tất giao hàng.", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (trangThaiHienTai == "Đang vận chuyển" && statusMoi == "Khởi tạo")
            {
                MessageBox.Show("Đơn vận chuyển đã rời kho ('Đang vận chuyển') không thể quay ngược về trạng thái 'Khởi tạo'!", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DonVanChuyen don = bll.LayDonTheoID(idDonVC);
                if (don != null)
                {
                    if (statusMoi == "Đang vận chuyển")
                    {
                        if (bll.KiemTraXeDangBan(don.BienSoXe, don.ID_DonVC, out string lyDo))
                        {
                            MessageBox.Show($"Không thể chuyển đơn sang trạng thái 'Đang vận chuyển'!\n\nLý do: {lyDo}.\n\nVui lòng chỉnh sửa đổi xe khác cho đơn hàng này trước khi chuyển trạng thái hoặc đợi xe hoàn thành nhiệm vụ.", "Cảnh báo trùng xe / Phương tiện bận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    don.TrangThaiDon = statusMoi;
                    bll.SuaDon(don);
                    MessageBox.Show($"Cập nhật trạng thái Đơn VC [{idDonVC}] sang [{statusMoi}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đơn vận chuyển trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

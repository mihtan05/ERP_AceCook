using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ERP.BLL;
using ERP.DTO;

namespace ERP
{
    public partial class FrmSuaDonVanChuyen : Form
    {
        private readonly DonVanChuyenBLL bll = new DonVanChuyenBLL();
        private readonly string idDonVCToEdit;
        private string trangThaiBanDau = string.Empty;

        public FrmSuaDonVanChuyen(string idDonVC)
        {
            InitializeComponent();
            idDonVCToEdit = idDonVC;
        }

        private void FrmSuaDonVanChuyen_Load(object sender, EventArgs e)
        {
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplyActionButton(this.btnLuu, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnHuy, ButtonRole.Secondary);

            LoadDanhMuc();
            LoadThongTinDon();
        }

        private void LoadDanhMuc()
        {
            try
            {
                // 1. Điểm vận chuyển (hiển thị Tên thay vì Mã)
                List<DiemVanChuyenComboItem> dsDVC = bll.LayDanhSachDiemVanChuyenCombo();
                cboMaDVC.Items.Clear();
                foreach (DiemVanChuyenComboItem dvc in dsDVC)
                {
                    cboMaDVC.Items.Add(dvc);
                }

                // 2. Xe khả dụng kèm tải trọng
                List<XeKhaDungItem> dsXe = bll.LayDanhSachXeKhaDungKemTaiTrong();
                cboBienSoXe.Items.Clear();
                foreach (XeKhaDungItem xe in dsXe)
                {
                    cboBienSoXe.Items.Add(xe);
                }

                // 3. Sản phẩm (Mã hàng kèm Tên hàng)
                List<SanPhamComboItem> dsSP = bll.LayDanhSachSanPhamWithTen();
                cboSanPham.Items.Clear();
                foreach (SanPhamComboItem sp in dsSP)
                {
                    cboSanPham.Items.Add(sp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongTinDon()
        {
            try
            {
                DonVanChuyen don = bll.LayDonTheoID(idDonVCToEdit);
                if (don != null)
                {
                    // 1. Đơn hàng Bán hàng
                    if (!string.IsNullOrWhiteSpace(don.ID_DH))
                    {
                        txtDonHang.Text = !string.IsNullOrWhiteSpace(don.TenKhachHang)
                            ? $"{don.ID_DH} | {don.TenKhachHang}"
                            : don.ID_DH;
                    }
                    else
                    {
                        txtDonHang.Text = "-- Không chọn đơn hàng (Giao tự do) --";
                        txtDonHang.ForeColor = System.Drawing.Color.Gray;
                    }

                    // 2. Mã đơn VC
                    txtIDDonVC.Text = don.ID_DonVC;

                    // 3. Điểm giao nhận
                    bool dvcFound = false;
                    foreach (object item in cboMaDVC.Items)
                    {
                        if (item is DiemVanChuyenComboItem dvcItem && dvcItem.MaDVC == don.MaDVC)
                        {
                            cboMaDVC.SelectedItem = item;
                            dvcFound = true;
                            break;
                        }
                    }
                    if (!dvcFound && !string.IsNullOrEmpty(don.MaDVC))
                    {
                        DiemVanChuyenComboItem fallbackDvc = new DiemVanChuyenComboItem 
                        { 
                            MaDVC = don.MaDVC, 
                            TenDVC = !string.IsNullOrWhiteSpace(don.TenDVC) ? don.TenDVC : don.MaDVC 
                        };
                        cboMaDVC.Items.Add(fallbackDvc);
                        cboMaDVC.SelectedItem = fallbackDvc;
                    }

                    // 4. Địa chỉ giao
                    txtDiaChiGiao.Text = !string.IsNullOrWhiteSpace(don.DiaChiGiao) 
                        ? don.DiaChiGiao 
                        : ((cboMaDVC.SelectedItem as DiemVanChuyenComboItem)?.DiaChiDVC ?? string.Empty);

                    // 5. Phương tiện & Tải trọng xe
                    bool xeFound = false;
                    foreach (object item in cboBienSoXe.Items)
                    {
                        if (item is XeKhaDungItem xeItem && xeItem.BienSoXe == don.BienSoXe)
                        {
                            cboBienSoXe.SelectedItem = item;
                            xeFound = true;
                            break;
                        }
                    }
                    if (!xeFound && !string.IsNullOrEmpty(don.BienSoXe))
                    {
                        XeKhaDungItem currentXe = bll.LayThongTinXe(don.BienSoXe) 
                            ?? new XeKhaDungItem { BienSoXe = don.BienSoXe, LoaiXe = "Xe hiện tại", TaiTrong = 0 };
                        cboBienSoXe.Items.Add(currentXe);
                        cboBienSoXe.SelectedItem = currentXe;
                    }
                    CapNhatHienThiTaiTrongXe();

                    // 6. Hàng hóa
                    bool spFound = false;
                    foreach (object item in cboSanPham.Items)
                    {
                        if (item is SanPhamComboItem spItem && spItem.ID_SP == don.ID_SP)
                        {
                            cboSanPham.SelectedItem = item;
                            spFound = true;
                            break;
                        }
                    }
                    if (!spFound && !string.IsNullOrEmpty(don.ID_SP))
                    {
                        SanPhamComboItem fallbackItem = new SanPhamComboItem { ID_SP = don.ID_SP, TenHang = don.TenHang ?? "" };
                        cboSanPham.Items.Add(fallbackItem);
                        cboSanPham.SelectedItem = fallbackItem;
                    }

                    // 7. Số lượng giao
                    txtSoLuongGiao.Text = don.SoLuongGiao.ToString();

                    // 8. Thời gian giao
                    if (don.ThoiGianKhoiHanh != DateTime.MinValue)
                    {
                        dtpThoiGianKhoiHanh.Value = don.ThoiGianKhoiHanh;
                    }

                    // 9. Trạng thái
                    trangThaiBanDau = don.TrangThaiDon ?? "Khởi tạo";
                    cboTrangThaiDon.Items.Clear();

                    if (trangThaiBanDau == "Hoàn thành")
                    {
                        cboTrangThaiDon.Items.Add("Hoàn thành");
                        cboTrangThaiDon.SelectedIndex = 0;
                        cboMaDVC.Enabled = false;
                        cboBienSoXe.Enabled = false;
                        dtpThoiGianKhoiHanh.Enabled = false;
                        cboTrangThaiDon.Enabled = false;
                        btnLuu.Enabled = false;
                        btnLuu.Text = "🔒 Đã hoàn thành (Chỉ xem)";
                    }
                    else if (trangThaiBanDau == "Đã hủy")
                    {
                        cboTrangThaiDon.Items.Add("Đã hủy");
                        cboTrangThaiDon.SelectedIndex = 0;
                        cboMaDVC.Enabled = false;
                        cboBienSoXe.Enabled = false;
                        dtpThoiGianKhoiHanh.Enabled = false;
                        cboTrangThaiDon.Enabled = false;
                        btnLuu.Enabled = false;
                        btnLuu.Text = "🚫 Đã hủy (Chỉ xem)";
                    }
                    else if (trangThaiBanDau == "Đang vận chuyển")
                    {
                        cboTrangThaiDon.Items.AddRange(new object[] { "Đang vận chuyển", "Hoàn thành", "Đã hủy" });
                        cboTrangThaiDon.SelectedItem = "Đang vận chuyển";
                    }
                    else
                    {
                        cboTrangThaiDon.Items.AddRange(new object[] { "Khởi tạo", "Đang vận chuyển", "Đã hủy" });
                        cboTrangThaiDon.SelectedItem = "Khởi tạo";
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn vận chuyển cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatHienThiTaiTrongXe()
        {
            if (cboBienSoXe.SelectedItem is XeKhaDungItem xe)
            {
                lblTaiTrongXe.Text = xe.TaiTrongKg > 0 
                    ? $"🚛 Tải trọng tối đa: {xe.TaiTrongKg:N0} kg ({xe.LoaiXe})"
                    : $"🚛 Xe được phân công: {xe.BienSoXe}";
            }
            else
            {
                lblTaiTrongXe.Text = "🚛 Tải trọng xe: Vui lòng chọn xe";
            }
        }

        private void cboBienSoXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatHienThiTaiTrongXe();
        }

        private void cboMaDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaDVC.SelectedItem is DiemVanChuyenComboItem diem)
            {
                txtDiaChiGiao.Text = !string.IsNullOrWhiteSpace(diem.DiaChiDVC) ? diem.DiaChiDVC : string.Empty;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maDVC = (cboMaDVC.SelectedItem as DiemVanChuyenComboItem)?.MaDVC ?? cboMaDVC.SelectedItem?.ToString();
            string bienSo = (cboBienSoXe.SelectedItem as XeKhaDungItem)?.BienSoXe ?? cboBienSoXe.SelectedItem?.ToString();
            string sanPham = (cboSanPham.SelectedItem as SanPhamComboItem)?.ID_SP ?? cboSanPham.SelectedItem?.ToString();
            string strSoLuong = txtSoLuongGiao.Text.Trim();
            DateTime thoiGianGiao = dtpThoiGianKhoiHanh.Value;
            string trangThai = cboTrangThaiDon.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(maDVC))
            {
                MessageBox.Show("Vui lòng chọn Điểm giao nhận (Tuyến đường)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaDVC.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(bienSo))
            {
                MessageBox.Show("Vui lòng chọn Phương tiện / Đơn vị vận chuyển!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBienSoXe.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(sanPham))
            {
                MessageBox.Show("Vui lòng chọn Hàng hóa / Sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSanPham.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(strSoLuong) || !int.TryParse(strSoLuong, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng giao phải là số nguyên dương lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongGiao.Focus();
                txtSoLuongGiao.SelectAll();
                return;
            }

            if (trangThaiBanDau == "Hoàn thành")
            {
                MessageBox.Show("Đơn vận chuyển đã HOÀN THÀNH nên không thể chỉnh sửa!\n\nNếu khách hàng muốn đổi trả hàng, vui lòng sử dụng chức năng 'Quản lý trả hàng' để lập phiếu thu hồi.", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (trangThaiBanDau == "Đã hủy")
            {
                MessageBox.Show("Đơn vận chuyển đã ĐÃ HỦY nên không thể chỉnh sửa!", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(trangThai))
            {
                trangThai = "Khởi tạo";
            }

            // Kiểm tra quy tắc chuyển trạng thái
            if (trangThaiBanDau == "Khởi tạo" && trangThai == "Hoàn thành")
            {
                MessageBox.Show("Đơn vận chuyển chưa xuất phát ('Khởi tạo') không thể chuyển trực tiếp sang 'Hoàn thành'!\nVui lòng chuyển sang 'Đang vận chuyển' trước khi hoàn tất giao hàng.", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTrangThaiDon.Focus();
                return;
            }

            if (trangThaiBanDau == "Đang vận chuyển" && trangThai == "Khởi tạo")
            {
                MessageBox.Show("Đơn vận chuyển đã rời kho ('Đang vận chuyển') không thể quay ngược về trạng thái 'Khởi tạo'!", "Cảnh báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTrangThaiDon.Focus();
                return;
            }

            if (trangThai == "Đang vận chuyển")
            {
                if (bll.KiemTraXeDangBan(bienSo, idDonVCToEdit, out string lyDo))
                {
                    MessageBox.Show($"Không thể lưu đơn ở trạng thái 'Đang vận chuyển'!\n\nLý do: {lyDo}.\n\nVui lòng chọn xe khác hoặc chuyển sang trạng thái khác.", "Cảnh báo trùng xe / Phương tiện bận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBienSoXe.Focus();
                    return;
                }
            }

            try
            {
                string idDH = null;
                if (!string.IsNullOrWhiteSpace(txtDonHang.Text))
                {
                    string[] parts = txtDonHang.Text.Split('|');
                    if (parts.Length > 0 && !string.IsNullOrWhiteSpace(parts[0]))
                    {
                        idDH = parts[0].Trim();
                    }
                }

                DonVanChuyen don = new DonVanChuyen
                {
                    ID_DonVC = idDonVCToEdit,
                    ID_DH = idDH,
                    MaDVC = maDVC,
                    BienSoXe = bienSo,
                    ID_SP = sanPham,
                    SoLuongGiao = soLuong,
                    ThoiGianKhoiHanh = thoiGianGiao,
                    TrangThaiDon = trangThai
                };

                bll.SuaDon(don);

                MessageBox.Show("Cập nhật đơn vận chuyển thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

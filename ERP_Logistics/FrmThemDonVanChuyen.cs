using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ERP.BLL;
using ERP.DTO;

namespace ERP
{
    public partial class FrmThemDonVanChuyen : Form
    {
        private readonly DonVanChuyenBLL bll = new DonVanChuyenBLL();

        public FrmThemDonVanChuyen()
        {
            InitializeComponent();
        }

        private void FrmThemDonVanChuyen_Load(object sender, EventArgs e)
        {
            UIThemeHelper.ApplyFormStyle(this);
            UIThemeHelper.ApplyActionButton(this.btnLuu, ButtonRole.Primary);
            UIThemeHelper.ApplyActionButton(this.btnHuy, ButtonRole.Secondary);

            dtpThoiGianKhoiHanh.Value = DateTime.Now.AddHours(1); // Mặc định 1 tiếng sau thời điểm hiện tại
            txtTrangThai.Text = "Khởi tạo";

            LoadDanhMuc();
        }

        private void LoadDanhMuc()
        {
            try
            {
                // 1. Nạp danh sách điểm vận chuyển (hiển thị Tên điểm thay vì Mã)
                List<DiemVanChuyenComboItem> dsDVC = bll.LayDanhSachDiemVanChuyenCombo();
                cboMaDVC.Items.Clear();
                foreach (DiemVanChuyenComboItem dvc in dsDVC)
                {
                    cboMaDVC.Items.Add(dvc);
                }
                if (cboMaDVC.Items.Count > 0)
                {
                    cboMaDVC.SelectedIndex = 0;
                    CapNhatHienThiDiaChiDVC();
                }

                // 2. Nạp danh sách xe khả dụng kèm tải trọng
                List<XeKhaDungItem> dsXe = bll.LayDanhSachXeKhaDungKemTaiTrong();
                cboBienSoXe.Items.Clear();
                foreach (XeKhaDungItem xe in dsXe)
                {
                    cboBienSoXe.Items.Add(xe);
                }
                if (cboBienSoXe.Items.Count > 0)
                {
                    cboBienSoXe.SelectedIndex = 0;
                    CapNhatHienThiTaiTrongXe();
                }

                // 3. Nạp danh sách sản phẩm (Mã hàng kèm Tên hàng)
                List<SanPhamComboItem> dsSP = bll.LayDanhSachSanPhamWithTen();
                cboSanPham.Items.Clear();
                foreach (SanPhamComboItem sp in dsSP)
                {
                    cboSanPham.Items.Add(sp);
                }
                if (cboSanPham.Items.Count > 0) cboSanPham.SelectedIndex = 0;

                // 4. Nạp danh sách đơn hàng từ Bán Hàng đang chờ giao
                List<DonHangChoGiaoItem> dsDH = bll.LayDanhSachDonHangChoGiao();
                cboDonHang.Items.Clear();
                cboDonHang.Items.Add(new DonHangChoGiaoItem { ID_DH = "", TenKhachHang = "-- Không chọn đơn hàng (Giao tự do) --" });
                foreach (DonHangChoGiaoItem dh in dsDH)
                {
                    cboDonHang.Items.Add(dh);
                }
                cboDonHang.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatHienThiDiaChiDVC()
        {
            if (cboMaDVC.SelectedItem is DiemVanChuyenComboItem diem)
            {
                txtDiaChiGiao.Text = !string.IsNullOrWhiteSpace(diem.DiaChiDVC)
                    ? diem.DiaChiDVC
                    : string.Empty;
            }
            else
            {
                txtDiaChiGiao.Text = string.Empty;
            }
        }

        private void cboMaDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatHienThiDiaChiDVC();
        }

        private void CapNhatHienThiTaiTrongXe()
        {
            if (cboBienSoXe.SelectedItem is XeKhaDungItem xe)
            {
                lblTaiTrongXe.Text = $"🚛 Tải trọng tối đa: {xe.TaiTrongKg:N0} kg ({xe.LoaiXe})";
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

        private void txtSoLuongGiao_TextChanged(object sender, EventArgs e)
        {
        }

        private void cboDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDonHang.SelectedItem is DonHangChoGiaoItem selectedDH && !string.IsNullOrWhiteSpace(selectedDH.ID_DH))
            {
                // Tự động gợi ý mã chuyến duy nhất, tránh trùng lặp nếu đơn cũ đã từng bị hủy
                string baseMa = "VC_" + selectedDH.ID_DH.Trim();
                string candidateMa = baseMa;
                int suffix = 1;
                while (bll.KiemTraTonTai(candidateMa))
                {
                    candidateMa = $"{baseMa}_{suffix}";
                    if (candidateMa.Length > 20)
                    {
                        candidateMa = $"V{suffix}_{selectedDH.ID_DH.Trim()}";
                        if (candidateMa.Length > 20) candidateMa = candidateMa.Substring(0, 20);
                    }
                    suffix++;
                }
                txtIDDonVC.Text = candidateMa;

                // Tự động điền địa chỉ giao hàng và khóa
                txtDiaChiGiao.Text = !string.IsNullOrWhiteSpace(selectedDH.DiaChiGiao) ? selectedDH.DiaChiGiao : string.Empty;

                // Tự động điền số lượng đặt từ đơn bán hàng (ô số lượng đã bị khóa)
                txtSoLuongGiao.Text = selectedDH.SoLuongDat.ToString();

                // Tự động chọn sản phẩm tương ứng trong cboSanPham (đã khóa không cho sửa)
                if (!string.IsNullOrEmpty(selectedDH.ID_SP))
                {
                    for (int i = 0; i < cboSanPham.Items.Count; i++)
                    {
                        if (cboSanPham.Items[i] is SanPhamComboItem sp && sp.ID_SP == selectedDH.ID_SP)
                        {
                            cboSanPham.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // TỰ ĐỘNG THÊM / CHỌN ĐIỂM NHẬN HÀNG LÀ ĐỊA CHỈ KHÁCH HÀNG TỪ ĐƠN BÁN HÀNG
                try
                {
                    DiemVanChuyenComboItem diemKhach = bll.LayHoacTaoDiemNhanHangChoKhachHang(
                        selectedDH.TenKhachHang,
                        selectedDH.DiaChiGiao,
                        selectedDH.SDT
                    );

                    if (diemKhach != null)
                    {
                        int existingIndex = -1;
                        for (int i = 0; i < cboMaDVC.Items.Count; i++)
                        {
                            if (cboMaDVC.Items[i] is DiemVanChuyenComboItem it && it.MaDVC == diemKhach.MaDVC)
                            {
                                existingIndex = i;
                                break;
                            }
                        }

                        if (existingIndex >= 0)
                        {
                            cboMaDVC.SelectedIndex = existingIndex;
                        }
                        else
                        {
                            cboMaDVC.Items.Add(diemKhach);
                            cboMaDVC.SelectedItem = diemKhach;
                        }

                        if (!string.IsNullOrWhiteSpace(diemKhach.DiaChiDVC))
                        {
                            txtDiaChiGiao.Text = diemKhach.DiaChiDVC;
                        }
                    }
                }
                catch
                {
                    // Bỏ qua ngoại lệ nếu có lỗi kết nối tạm thời
                }
            }
            else
            {
                txtDiaChiGiao.Text = string.Empty;
                txtSoLuongGiao.Text = string.Empty;
                cboSanPham.SelectedIndex = -1;
                CapNhatHienThiDiaChiDVC();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maDon = txtIDDonVC.Text.Trim();
            string maDVC = (cboMaDVC.SelectedItem as DiemVanChuyenComboItem)?.MaDVC ?? cboMaDVC.SelectedItem?.ToString();
            string bienSo = (cboBienSoXe.SelectedItem as XeKhaDungItem)?.BienSoXe ?? cboBienSoXe.SelectedItem?.ToString();
            string sanPham = (cboSanPham.SelectedItem as SanPhamComboItem)?.ID_SP ?? cboSanPham.SelectedItem?.ToString();
            string strSoLuong = txtSoLuongGiao.Text.Trim();
            DateTime thoiGianGiao = dtpThoiGianKhoiHanh.Value;

            // ==========================================================
            // LUỒNG NGOẠI LỆ A1: Bỏ trống thông tin bắt buộc
            // ==========================================================
            if (string.IsNullOrWhiteSpace(maDon))
            {
                MessageBox.Show("Vui lòng nhập Mã đơn vận chuyển (Mã chuyến)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIDDonVC.Focus();
                return;
            }

            if (maDon.Length > 20)
            {
                MessageBox.Show("Mã đơn vận chuyển không được vượt quá 20 ký tự!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIDDonVC.Focus();
                txtIDDonVC.SelectAll();
                return;
            }

            if (string.IsNullOrWhiteSpace(maDVC))
            {
                MessageBox.Show("Vui lòng chọn Điểm giao nhận (Tuyến đường)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaDVC.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChiGiao.Text.Trim()))
            {
                MessageBox.Show("Địa chỉ giao hàng không được để trống! Vui lòng chọn Đơn hàng hoặc Điểm giao nhận có địa chỉ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDonHang.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(bienSo))
            {
                MessageBox.Show("Vui lòng chọn Phương tiện / Đơn vị vận chuyển!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBienSoXe.Focus();
                return;
            }

            if (bll.KiemTraXeDangBan(bienSo, maDon, out string lyDoXe))
            {
                MessageBox.Show($"Phương tiện [{bienSo}] hiện không khả dụng!\n\nLý do: {lyDoXe}.\n\nVui lòng chọn phương tiện khác đang rảnh.", "Cảnh báo phương tiện bận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBienSoXe.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(sanPham))
            {
                MessageBox.Show("Vui lòng chọn Đơn hàng Bán hàng để lấy thông tin Hàng hóa!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDonHang.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(strSoLuong) || !int.TryParse(strSoLuong, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Vui lòng chọn Đơn hàng Bán hàng có số lượng giao hợp lệ (> 0)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDonHang.Focus();
                return;
            }



            // ==========================================================
            // LUỒNG NGOẠI LỆ A3: Sai định dạng thời gian
            // ==========================================================
            if (thoiGianGiao < DateTime.Now.AddMinutes(-2)) // Cho phép sai lệch 2 phút thao tác form
            {
                MessageBox.Show("Thời gian khởi hành / giao dự kiến không được nhỏ hơn thời gian hiện tại!", "Sai định dạng thời gian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpThoiGianKhoiHanh.Focus();
                return;
            }

            // ==========================================================
            // LUỒNG NGOẠI LỆ A2: Trùng lặp mã chuyến xe / mã đơn
            // ==========================================================
            try
            {
                DonVanChuyen tonTai = bll.LayDonTheoID(maDon);
                if (tonTai != null)
                {
                    MessageBox.Show($"Mã chuyến vận hàng '{maDon}' đã tồn tại trong hệ thống. Vui lòng nhập mã khác!", "Trùng lặp mã chuyến xe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIDDonVC.Focus();
                    txtIDDonVC.SelectAll();
                    return;
                }
            }
            catch
            {
                // Bỏ qua nếu có lỗi đọc tạm thời, sẽ được bll.ThemDon bắt lại
            }

            // ==========================================================
            // LUỒNG CHÍNH: Lưu dữ liệu
            // ==========================================================
            try
            {
                string idDH = (cboDonHang.SelectedItem as DonHangChoGiaoItem)?.ID_DH;
                if (string.IsNullOrWhiteSpace(idDH)) idDH = null;

                DonVanChuyen don = new DonVanChuyen
                {
                    ID_DonVC = maDon,
                    ID_DH = idDH,
                    MaDVC = maDVC,
                    BienSoXe = bienSo,
                    ID_SP = sanPham,
                    SoLuongGiao = soLuong,
                    TrongLuong = 0,
                    ThoiGianKhoiHanh = thoiGianGiao,
                    TrangThaiDon = "Khởi tạo"
                };

                bll.ThemDon(don);

                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu đơn vận chuyển: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

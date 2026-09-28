using System;
using System.Windows.Forms;
using ERP.BLL;
using ERP.DTO;
using System.Collections.Generic;

namespace ERP
{
    public partial class FrmPhieuTraHang : Form
    {
        private string idPhieuTra;
        private readonly PhieuTraHangBLL bll = new PhieuTraHangBLL();
        private bool isSyncingDVC = false;
        private System.Data.DataTable dtPhuongTienRanh;
        private bool isFilteringXe = false;

        private const string ALL_LOAI_XE = "-- Tất cả loại xe --";
        private const string ALL_TRONG_TAI = "-- Tất cả tải trọng --";
        private const string CHON_BIEN_SO = "-- Chọn biển số xe --";

        public FrmPhieuTraHang(string idPhieuTra = null)
        {
            InitializeComponent();
            this.idPhieuTra = idPhieuTra;
        }

        private void FrmPhieuTraHang_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();

            if (!string.IsNullOrEmpty(idPhieuTra))
            {
                this.Text = "Cập nhật Lệnh Điều Xe";
                txtID_PhieuTra.Text = idPhieuTra;
                txtID_PhieuTra.Enabled = false; 

                try
                {
                    PhieuTraHang p = bll.GetByID(idPhieuTra);
                    if (p != null)
                    {
                        dtpNgayTra.Value = p.NgayTra;
                        
                        int foundIdx = -1;
                        for (int i = 0; i < cboID_CTYC.Items.Count; i++)
                        {
                            string cid = (cboID_CTYC.Items[i] as ChiTietYeuCauComboItem)?.ID_CTYC ?? cboID_CTYC.Items[i].ToString();
                            if (cid == p.ID_CTYC)
                            {
                                foundIdx = i;
                                break;
                            }
                        }
                        if (foundIdx >= 0)
                        {
                            cboID_CTYC.SelectedIndex = foundIdx;
                        }
                        else if (!string.IsNullOrEmpty(p.ID_CTYC))
                        {
                            cboID_CTYC.Items.Add(p.ID_CTYC);
                            cboID_CTYC.SelectedItem = p.ID_CTYC;
                        }
                        
                        if (!cboMaDVC.Items.Contains(p.MaDVC) && !string.IsNullOrEmpty(p.MaDVC))
                        {
                            cboMaDVC.Items.Add(p.MaDVC);
                            cboTenDVC.Items.Add(p.MaDVC); // Fallback to MaDVC if TenDVC is unknown
                        }
                        cboMaDVC.SelectedItem = p.MaDVC;
                        
                        if (!cboTrangThai.Items.Contains(p.TrangThai) && !string.IsNullOrEmpty(p.TrangThai))
                            cboTrangThai.Items.Add(p.TrangThai);
                        string ttHienThi = p.TrangThai == "Đã nhập kho" ? "Đã xử lý" : p.TrangThai;
                        if (!cboTrangThai.Items.Contains(ttHienThi) && !string.IsNullOrEmpty(ttHienThi)) cboTrangThai.Items.Add(ttHienThi);
                        cboTrangThai.SelectedItem = ttHienThi;
                        
                        // Initialize correctly for editing
                        isFilteringXe = true;
                        
                        string bx = p.BienSoXe;
                        if (!string.IsNullOrEmpty(bx))
                        {
                            System.Data.DataRow xeRow = null;
                            if (dtPhuongTienRanh != null)
                            {
                                foreach (System.Data.DataRow row in dtPhuongTienRanh.Rows)
                                {
                                    if (row["BienSoXe"].ToString() == bx)
                                    {
                                        xeRow = row;
                                        break;
                                    }
                                }
                            }

                            if (xeRow != null)
                            {
                                string lx = xeRow["LoaiXe"].ToString();
                                string tt = xeRow["TaiTrong"].ToString();

                                if (cboLoaiXe.Items.Contains(lx))
                                    cboLoaiXe.SelectedItem = lx;

                                // Lọc trọng tải theo lx
                                cboTrongTai.Items.Clear();
                                cboTrongTai.Items.Add(ALL_TRONG_TAI);
                                var listTrongTai = new List<string>();
                                foreach (System.Data.DataRow r in dtPhuongTienRanh.Rows)
                                {
                                    if (r["LoaiXe"].ToString() == lx)
                                    {
                                        string t = r["TaiTrong"].ToString();
                                        if (!string.IsNullOrEmpty(t) && !listTrongTai.Contains(t))
                                            listTrongTai.Add(t);
                                    }
                                }
                                listTrongTai.Sort();
                                foreach (var t in listTrongTai) cboTrongTai.Items.Add(t);
                                if (cboTrongTai.Items.Contains(tt)) cboTrongTai.SelectedItem = tt;

                                // Lọc biển số
                                cboBienSoXe.Items.Clear();
                                cboBienSoXe.Items.Add(CHON_BIEN_SO);
                                foreach (System.Data.DataRow r in dtPhuongTienRanh.Rows)
                                {
                                    if (r["LoaiXe"].ToString() == lx && r["TaiTrong"].ToString() == tt)
                                        cboBienSoXe.Items.Add(r["BienSoXe"].ToString());
                                }
                                if (!cboBienSoXe.Items.Contains(bx)) cboBienSoXe.Items.Add(bx);
                                cboBienSoXe.SelectedItem = bx;
                                txtTaiXe.Text = xeRow["TenTaiXe"].ToString();
                            }
                            else
                            {
                                if (!cboBienSoXe.Items.Contains(bx)) cboBienSoXe.Items.Add(bx);
                                cboBienSoXe.SelectedItem = bx;
                            }
                        }
                        
                        isFilteringXe = false;
                        KhoaThongTinTheoTrangThai(p.TrangThai);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải thông tin lệnh điều xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.Text = "Thêm mới Lệnh Điều Xe";
                
                try
                {
                    txtID_PhieuTra.Text = bll.SinhMaPhieu();
                }
                catch
                {
                    txtID_PhieuTra.Text = "PTH001";
                }
                txtID_PhieuTra.Enabled = false; 

                dtpNgayTra.Value = DateTime.Now;
                if (cboTrangThai.Items.Count > 0)
                    cboTrangThai.SelectedIndex = 0;
                KhoaThongTinTheoTrangThai("Chờ xử lý");
            }
        }

        private void KhoaThongTinTheoTrangThai(string trangThai)
        {
            bool choSuaThongTin = string.IsNullOrEmpty(idPhieuTra) || trangThai == "Chờ xử lý";
            bool daKetThuc = trangThai == "Đã xử lý" || trangThai == "Đã nhập kho" || trangThai == "Đã hủy" || trangThai == "Từ chối";
            btnLuu.Enabled = !daKetThuc;
            if (daKetThuc) btnLuu.Text = "🔒 Đã khóa";

            cboID_CTYC.Enabled = choSuaThongTin;
            cboMaDVC.Enabled = choSuaThongTin;
            cboTenDVC.Enabled = choSuaThongTin;
            dtpNgayTra.Enabled = choSuaThongTin;
            cboLoaiXe.Enabled = choSuaThongTin;
            cboTrongTai.Enabled = choSuaThongTin;
            cboBienSoXe.Enabled = choSuaThongTin;
            btnDatLaiXe.Enabled = choSuaThongTin;
            cboTrangThai.Enabled = !daKetThuc;
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load CTYC dạng đầy đủ thông tin nhận diện
                cboID_CTYC.Items.Clear();
                List<ChiTietYeuCauComboItem> dsCTYC = bll.GetDanhSachChiTietYeuCauFull();
                foreach (var item in dsCTYC) cboID_CTYC.Items.Add(item);
                if (cboID_CTYC.Items.Count > 0) cboID_CTYC.SelectedIndex = 0;

                // Load DVC (Mã và Tên)
                cboMaDVC.Items.Clear();
                cboTenDVC.Items.Clear();
                Dictionary<string, string> dsDVC = bll.GetDanhSachDVC();
                foreach (var kvp in dsDVC)
                {
                    cboMaDVC.Items.Add(kvp.Key);
                    cboTenDVC.Items.Add(kvp.Value);
                }
                
                if (cboMaDVC.Items.Count > 0)
                {
                    cboMaDVC.SelectedIndex = 0;
                    cboTenDVC.SelectedIndex = 0;
                }

                // Load PhuongTien
                dtPhuongTienRanh = bll.GetAllPhuongTienRanh(idPhieuTra);
                ResetDanhSachXe();

                // Load TrangThai
                cboTrangThai.Items.Clear();
                cboTrangThai.Items.AddRange(new string[] { "Chờ xử lý", "Đang thu hồi", "Đã xử lý", "Đã hủy" });
                cboTrangThai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetDanhSachXe()
        {
            if (dtPhuongTienRanh == null) return;
            isFilteringXe = true;

            // Nạp Loại xe: giữ nguyên danh mục tất cả loại xe có trong DB
            cboLoaiXe.Items.Clear();
            cboLoaiXe.Items.Add(ALL_LOAI_XE);
            var loaiXeList = new List<string>();
            foreach (System.Data.DataRow row in dtPhuongTienRanh.Rows)
            {
                string lx = row["LoaiXe"].ToString();
                if (!string.IsNullOrEmpty(lx) && !loaiXeList.Contains(lx))
                    loaiXeList.Add(lx);
            }
            loaiXeList.Sort();
            foreach (var lx in loaiXeList) cboLoaiXe.Items.Add(lx);
            cboLoaiXe.SelectedIndex = 0;

            // Nạp Trọng tải: tất cả các trọng tải
            cboTrongTai.Items.Clear();
            cboTrongTai.Items.Add(ALL_TRONG_TAI);
            var trongTaiList = new List<string>();
            foreach (System.Data.DataRow row in dtPhuongTienRanh.Rows)
            {
                string tt = row["TaiTrong"].ToString();
                if (!string.IsNullOrEmpty(tt) && !trongTaiList.Contains(tt))
                    trongTaiList.Add(tt);
            }
            trongTaiList.Sort();
            foreach (var tt in trongTaiList) cboTrongTai.Items.Add(tt);
            cboTrongTai.SelectedIndex = 0;

            // Nạp Biển số xe: tất cả các biển số xe rảnh
            cboBienSoXe.Items.Clear();
            cboBienSoXe.Items.Add(CHON_BIEN_SO);
            foreach (System.Data.DataRow row in dtPhuongTienRanh.Rows)
            {
                string bs = row["BienSoXe"].ToString();
                if (!string.IsNullOrEmpty(bs))
                    cboBienSoXe.Items.Add(bs);
            }
            cboBienSoXe.SelectedIndex = 0;
            txtTaiXe.Text = "";

            isFilteringXe = false;
        }

        private void cboMaDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isSyncingDVC) return;
            isSyncingDVC = true;
            if (cboMaDVC.SelectedIndex >= 0 && cboMaDVC.SelectedIndex < cboTenDVC.Items.Count)
            {
                cboTenDVC.SelectedIndex = cboMaDVC.SelectedIndex;
            }
            isSyncingDVC = false;
        }

        private void cboTenDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isSyncingDVC) return;
            isSyncingDVC = true;
            if (cboTenDVC.SelectedIndex >= 0 && cboTenDVC.SelectedIndex < cboMaDVC.Items.Count)
            {
                cboMaDVC.SelectedIndex = cboTenDVC.SelectedIndex;
            }
            isSyncingDVC = false;
        }

        private void cboID_CTYC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboID_CTYC.SelectedItem != null)
            {
                string idCTYC = (cboID_CTYC.SelectedItem as ChiTietYeuCauComboItem)?.ID_CTYC
                                ?? cboID_CTYC.SelectedItem.ToString().Split('|')[0].Trim();
                try
                {
                    ThongTinYeuCau tt = bll.GetThongTinYeuCau(idCTYC);
                    if (tt != null)
                    {
                        txtTenSP.Text = tt.TenHang;
                        txtSoLuong.Text = tt.SoLuong.ToString();
                        txtLyDo.Text = tt.LyDo;
                        txtKhachHang.Text = tt.KhachHang;

                        // Tự động nhận diện và gán Điểm giao nhận theo địa chỉ khách hàng
                        if (!string.IsNullOrWhiteSpace(tt.DiaChiKhachHang))
                        {
                            try
                            {
                                DiemVanChuyenComboItem diem = bll.LayHoacTaoDiemThuHoiChoKhachHang(tt.KhachHang, tt.DiaChiKhachHang, tt.SDTKhachHang);
                                if (diem != null)
                                {
                                    int idx = cboMaDVC.Items.IndexOf(diem.MaDVC);
                                    if (idx >= 0)
                                    {
                                        cboMaDVC.SelectedIndex = idx;
                                    }
                                    else
                                    {
                                        cboMaDVC.Items.Add(diem.MaDVC);
                                        cboTenDVC.Items.Add(diem.TenDVC);
                                        cboMaDVC.SelectedItem = diem.MaDVC;
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        txtTenSP.Text = "";
                        txtSoLuong.Text = "";
                        txtLyDo.Text = "";
                        txtKhachHang.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải thông tin chi tiết yêu cầu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFilteringXe || dtPhuongTienRanh == null) return;
            isFilteringXe = true;

            string selectedLoai = cboLoaiXe.SelectedItem?.ToString();
            bool isAllLoai = string.IsNullOrEmpty(selectedLoai) || selectedLoai == ALL_LOAI_XE;

            // Lọc lại Trọng tải theo Loại xe đang chọn
            string prevTrongTai = cboTrongTai.SelectedItem?.ToString();
            cboTrongTai.Items.Clear();
            cboTrongTai.Items.Add(ALL_TRONG_TAI);
            var listTrongTai = new List<string>();
            foreach (System.Data.DataRow row in dtPhuongTienRanh.Rows)
            {
                if (isAllLoai || row["LoaiXe"].ToString() == selectedLoai)
                {
                    string tt = row["TaiTrong"].ToString();
                    if (!string.IsNullOrEmpty(tt) && !listTrongTai.Contains(tt))
                        listTrongTai.Add(tt);
                }
            }
            listTrongTai.Sort();
            foreach (var tt in listTrongTai) cboTrongTai.Items.Add(tt);

            if (!string.IsNullOrEmpty(prevTrongTai) && cboTrongTai.Items.Contains(prevTrongTai))
                cboTrongTai.SelectedItem = prevTrongTai;
            else
                cboTrongTai.SelectedIndex = 0;

            // Lọc lại Biển số xe theo Loại xe và Trọng tải
            FilterBienSoXe();

            isFilteringXe = false;
        }

        private void cboTrongTai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFilteringXe || dtPhuongTienRanh == null) return;
            isFilteringXe = true;

            FilterBienSoXe();

            isFilteringXe = false;
        }

        private void FilterBienSoXe()
        {
            string selectedLoai = cboLoaiXe.SelectedItem?.ToString();
            string selectedTrongTai = cboTrongTai.SelectedItem?.ToString();
            string prevBienSo = cboBienSoXe.SelectedItem?.ToString();

            bool isAllLoai = string.IsNullOrEmpty(selectedLoai) || selectedLoai == ALL_LOAI_XE;
            bool isAllTrongTai = string.IsNullOrEmpty(selectedTrongTai) || selectedTrongTai == ALL_TRONG_TAI;

            cboBienSoXe.Items.Clear();
            cboBienSoXe.Items.Add(CHON_BIEN_SO);

            foreach (System.Data.DataRow row in dtPhuongTienRanh.Rows)
            {
                bool matchLoai = isAllLoai || row["LoaiXe"].ToString() == selectedLoai;
                bool matchTrongTai = isAllTrongTai || row["TaiTrong"].ToString() == selectedTrongTai;

                if (matchLoai && matchTrongTai)
                {
                    cboBienSoXe.Items.Add(row["BienSoXe"].ToString());
                }
            }

            if (!string.IsNullOrEmpty(prevBienSo) && cboBienSoXe.Items.Contains(prevBienSo) && prevBienSo != CHON_BIEN_SO)
            {
                cboBienSoXe.SelectedItem = prevBienSo;
            }
            else
            {
                cboBienSoXe.SelectedIndex = 0;
                txtTaiXe.Text = "";
            }
        }

        private void cboBienSoXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFilteringXe || dtPhuongTienRanh == null) return;

            string selectedBienSo = cboBienSoXe.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedBienSo) || selectedBienSo == CHON_BIEN_SO)
            {
                txtTaiXe.Text = "";
                return;
            }

            // Tự động nhận diện và đồng bộ Loại xe, Trọng tải, Tài xế
            foreach (System.Data.DataRow row in dtPhuongTienRanh.Rows)
            {
                if (row["BienSoXe"].ToString() == selectedBienSo)
                {
                    isFilteringXe = true;
                    string lx = row["LoaiXe"].ToString();
                    string tt = row["TaiTrong"].ToString();
                    txtTaiXe.Text = row["TenTaiXe"].ToString();

                    // Đồng bộ Loại xe nếu khác
                    if (cboLoaiXe.Items.Contains(lx) && cboLoaiXe.SelectedItem?.ToString() != lx)
                    {
                        cboLoaiXe.SelectedItem = lx;

                        // Đồng bộ lại danh sách trọng tải tương ứng
                        cboTrongTai.Items.Clear();
                        cboTrongTai.Items.Add(ALL_TRONG_TAI);
                        var listTrongTai = new List<string>();
                        foreach (System.Data.DataRow r in dtPhuongTienRanh.Rows)
                        {
                            if (r["LoaiXe"].ToString() == lx)
                            {
                                string t = r["TaiTrong"].ToString();
                                if (!string.IsNullOrEmpty(t) && !listTrongTai.Contains(t))
                                    listTrongTai.Add(t);
                            }
                        }
                        listTrongTai.Sort();
                        foreach (var t in listTrongTai) cboTrongTai.Items.Add(t);
                    }

                    // Đồng bộ Trọng tải
                    if (cboTrongTai.Items.Contains(tt))
                    {
                        cboTrongTai.SelectedItem = tt;
                    }

                    isFilteringXe = false;
                    break;
                }
            }
        }

        private void btnDatLaiXe_Click(object sender, EventArgs e)
        {
            ResetDanhSachXe();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string idCTYC = (cboID_CTYC.SelectedItem as ChiTietYeuCauComboItem)?.ID_CTYC
                                ?? cboID_CTYC.SelectedItem?.ToString().Split('|')[0].Trim()
                                ?? cboID_CTYC.Text.Trim();

                string bienSo = cboBienSoXe.SelectedItem?.ToString() ?? cboBienSoXe.Text.Trim();
                if (string.IsNullOrWhiteSpace(bienSo) || bienSo == CHON_BIEN_SO)
                {
                    MessageBox.Show("Vui lòng chọn biển số xe điều phối!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBienSoXe.Focus();
                    return;
                }

                PhieuTraHang p = new PhieuTraHang
                {
                    ID_PhieuTra = txtID_PhieuTra.Text.Trim(),
                    NgayTra = dtpNgayTra.Value,
                    MaDVC = cboMaDVC.SelectedItem?.ToString() ?? cboMaDVC.Text.Trim(),
                    ID_CTYC = idCTYC,
                    TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Chờ xử lý",
                    BienSoXe = bienSo
                };

                if (string.IsNullOrEmpty(idPhieuTra))
                {
                    // Thêm mới
                    bll.ThemPhieu(p);
                    MessageBox.Show("Thêm lệnh điều xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Sửa
                    bll.SuaPhieu(p);
                    MessageBox.Show("Cập nhật lệnh điều xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


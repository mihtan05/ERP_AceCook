using System;
using System.Collections.Generic;
using System.Data;
using ERP.DAL;
using ERP.DTO;

namespace ERP.BLL
{
    public class DonVanChuyenBLL
    {
        private readonly DonVanChuyenDAL dal;

        public DonVanChuyenBLL()
        {
            dal = new DonVanChuyenDAL();
        }

        public DonVanChuyenBLL(string connectionString)
        {
            dal = new DonVanChuyenDAL(connectionString);
        }

        public List<DonVanChuyen> LayDanhSach()
        {
            return dal.GetAll();
        }

        public DonVanChuyen LayDonTheoID(string id)
        {
            return dal.GetByID(id);
        }

        public List<string> LayDanhSachXeKhaDung()
        {
            return dal.GetDanhSachXeKhaDung();
        }

        public List<string> LayDanhSachDVC()
        {
            return dal.GetDanhSachDVC();
        }

        public List<DiemVanChuyenComboItem> LayDanhSachDiemVanChuyenCombo()
        {
            return dal.GetDanhSachDiemVanChuyen();
        }

        public DiemVanChuyenComboItem LayHoacTaoDiemNhanHangChoKhachHang(string tenKhachHang, string diaChiGiao, string sdt)
        {
            return dal.LayHoacTaoDiemNhanHangChoKhachHang(tenKhachHang, diaChiGiao, sdt);
        }

        public List<string> LayDanhSachSanPham()
        {
            return dal.GetDanhSachSanPham();
        }

        public List<SanPhamComboItem> LayDanhSachSanPhamWithTen()
        {
            return dal.GetDanhSachSanPhamWithTen();
        }

        public bool KiemTraXeDangBan(string bienSoXe, string excludeIdDonVC, out string lyDoBan)
        {
            return dal.KiemTraXeDangBan(bienSoXe, excludeIdDonVC, out lyDoBan);
        }

        public List<DonHangChoGiaoItem> LayDanhSachDonHangChoGiao()
        {
            return dal.GetDanhSachDonHangChoGiao();
        }

        public bool KiemTraDonHangDangVanChuyen(string idDH, string excludeIdDonVC, out string lyDo)
        {
            return dal.KiemTraDonHangDangVanChuyen(idDH, excludeIdDonVC, out lyDo);
        }

        public int LaySoLuongDatCuaDonHang(string idDH, string idSP)
        {
            return dal.LaySoLuongDatCuaDonHang(idDH, idSP);
        }

        public List<XeKhaDungItem> LayDanhSachXeKhaDungKemTaiTrong()
        {
            return dal.GetDanhSachXeKhaDungKemTaiTrong();
        }

        public decimal LayTaiTrongXe(string bienSoXe)
        {
            return dal.LayTaiTrongXe(bienSoXe);
        }

        public XeKhaDungItem LayThongTinXe(string bienSoXe)
        {
            return dal.GetThongTinXe(bienSoXe);
        }

        public bool ThemDon(DonVanChuyen don)
        {
            if (don == null)
                throw new ArgumentNullException(nameof(don), "Dữ liệu đơn vận chuyển không được để trống.");

            if (string.IsNullOrWhiteSpace(don.ID_DonVC))
                throw new ArgumentException("Mã đơn vận chuyển không được để trống.");

            if (dal.IsExist(don.ID_DonVC))
                throw new ArgumentException($"Mã đơn vận chuyển '{don.ID_DonVC}' đã tồn tại trong hệ thống.");

            // Kiểm tra trùng Đơn hàng Bán hàng
            if (!string.IsNullOrWhiteSpace(don.ID_DH))
            {
                if (dal.KiemTraDonHangDangVanChuyen(don.ID_DH, null, out string lyDoDH))
                {
                    throw new InvalidOperationException(lyDoDH);
                }

                // Kiểm tra số lượng giao so với số lượng đặt
                int slDat = dal.LaySoLuongDatCuaDonHang(don.ID_DH, don.ID_SP);
                if (slDat > 0 && don.SoLuongGiao > slDat)
                {
                    throw new ArgumentException($"Số lượng giao ({don.SoLuongGiao}) không được vượt quá số lượng đặt ({slDat}) của đơn hàng [{don.ID_DH}].");
                }
            }

            if (string.IsNullOrWhiteSpace(don.BienSoXe))
                throw new ArgumentException("Biển số xe không được để trống.");

            // Kiểm tra tải trọng xe (Chặn quá tải)
            if (!string.IsNullOrWhiteSpace(don.BienSoXe) && don.TrongLuong > 0)
            {
                decimal taiTrongXeKg = dal.LayTaiTrongXe(don.BienSoXe);
                if (taiTrongXeKg > 0 && don.TrongLuong > taiTrongXeKg)
                {
                    decimal vuotTai = don.TrongLuong - taiTrongXeKg;
                    throw new ArgumentException($"Tổng trọng lượng hàng ({don.TrongLuong:N0} kg) vượt quá tải trọng tối đa của xe [{don.BienSoXe}] ({taiTrongXeKg:N0} kg) là {vuotTai:N0} kg. Vui lòng chọn xe có tải trọng lớn hơn hoặc giảm lượng hàng!");
                }
            }

            if (string.IsNullOrWhiteSpace(don.MaDVC))
                throw new ArgumentException("Mã điểm vận chuyển không được để trống.");

            if (string.IsNullOrWhiteSpace(don.ID_SP))
                throw new ArgumentException("Mã sản phẩm không được để trống.");

            if (don.SoLuongGiao <= 0)
                throw new ArgumentException("Số lượng giao phải lớn hơn 0.");

            if (string.IsNullOrWhiteSpace(don.TrangThaiDon))
                don.TrangThaiDon = "Khởi tạo";

            if (don.TrangThaiDon == "Đang vận chuyển")
            {
                if (dal.KiemTraXeDangBan(don.BienSoXe, don.ID_DonVC, out string lyDo))
                {
                    throw new InvalidOperationException($"Không thể tạo đơn ở trạng thái 'Đang vận chuyển': {lyDo}.");
                }
            }

            bool success = dal.Insert(don);
            if (!success)
                throw new Exception("Thêm đơn vận chuyển thất bại. Vui lòng thử lại!");
            return success;
        }

        public bool SuaDon(DonVanChuyen don)
        {
            if (don == null)
                throw new ArgumentNullException(nameof(don), "Dữ liệu đơn vận chuyển không được để trống.");

            if (string.IsNullOrWhiteSpace(don.ID_DonVC))
                throw new ArgumentException("Mã đơn vận chuyển không được để trống.");

            if (!dal.IsExist(don.ID_DonVC))
                throw new ArgumentException($"Không tìm thấy đơn vận chuyển mã '{don.ID_DonVC}' để cập nhật.");

            // Kiểm tra trùng Đơn hàng Bán hàng
            if (!string.IsNullOrWhiteSpace(don.ID_DH))
            {
                if (dal.KiemTraDonHangDangVanChuyen(don.ID_DH, don.ID_DonVC, out string lyDoDH))
                {
                    throw new InvalidOperationException(lyDoDH);
                }

                // Kiểm tra số lượng giao so với số lượng đặt
                int slDat = dal.LaySoLuongDatCuaDonHang(don.ID_DH, don.ID_SP);
                if (slDat > 0 && don.SoLuongGiao > slDat)
                {
                    throw new ArgumentException($"Số lượng giao ({don.SoLuongGiao}) không được vượt quá số lượng đặt ({slDat}) của đơn hàng [{don.ID_DH}].");
                }
            }

            if (string.IsNullOrWhiteSpace(don.BienSoXe))
                throw new ArgumentException("Biển số xe không được để trống.");

            // Kiểm tra tải trọng xe (Chặn quá tải)
            if (!string.IsNullOrWhiteSpace(don.BienSoXe) && don.TrongLuong > 0)
            {
                decimal taiTrongXeKg = dal.LayTaiTrongXe(don.BienSoXe);
                if (taiTrongXeKg > 0 && don.TrongLuong > taiTrongXeKg)
                {
                    decimal vuotTai = don.TrongLuong - taiTrongXeKg;
                    throw new ArgumentException($"Tổng trọng lượng hàng ({don.TrongLuong:N0} kg) vượt quá tải trọng tối đa của xe [{don.BienSoXe}] ({taiTrongXeKg:N0} kg) là {vuotTai:N0} kg. Vui lòng chọn xe có tải trọng lớn hơn hoặc giảm lượng hàng!");
                }
            }

            if (string.IsNullOrWhiteSpace(don.MaDVC))
                throw new ArgumentException("Mã điểm vận chuyển không được để trống.");

            if (string.IsNullOrWhiteSpace(don.ID_SP))
                throw new ArgumentException("Mã sản phẩm không được để trống.");

            if (don.SoLuongGiao <= 0)
                throw new ArgumentException("Số lượng giao phải lớn hơn 0.");

            // Kiểm tra ràng buộc vòng đời trạng thái (State Machine)
            DonVanChuyen donCu = dal.GetByID(don.ID_DonVC);
            if (donCu != null)
            {
                if (donCu.TrangThaiDon == "Hoàn thành" && don.TrangThaiDon != "Hoàn thành")
                {
                    throw new InvalidOperationException("Đơn vận chuyển đã HOÀN THÀNH nên không thể thay đổi trạng thái!");
                }

                if (donCu.TrangThaiDon == "Đã hủy" && don.TrangThaiDon != "Đã hủy")
                {
                    throw new InvalidOperationException("Đơn vận chuyển đã ĐÃ HỦY nên không thể thay đổi trạng thái!");
                }

                if (donCu.TrangThaiDon == "Khởi tạo" && don.TrangThaiDon == "Hoàn thành")
                {
                    throw new InvalidOperationException("Đơn vận chuyển đang 'Khởi tạo' chưa thể chuyển trực tiếp sang 'Hoàn thành' mà phải chuyển sang 'Đang vận chuyển' trước!");
                }

                if (donCu.TrangThaiDon == "Đang vận chuyển" && don.TrangThaiDon == "Khởi tạo")
                {
                    throw new InvalidOperationException("Đơn vận chuyển đang 'Đang vận chuyển' không thể quay lại trạng thái 'Khởi tạo'!");
                }
            }

            if (don.TrangThaiDon == "Đang vận chuyển")
            {
                if (dal.KiemTraXeDangBan(don.BienSoXe, don.ID_DonVC, out string lyDo))
                {
                    throw new InvalidOperationException($"Không thể chuyển đơn sang trạng thái 'Đang vận chuyển': {lyDo}.");
                }
            }

            bool success = dal.Update(don);
            if (!success)
                throw new Exception("Cập nhật đơn vận chuyển thất bại. Vui lòng thử lại!");
            return success;
        }

        public bool XoaDon(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Mã đơn vận chuyển cần xóa không được để trống.");

            if (!dal.IsExist(id))
                throw new ArgumentException($"Không tìm thấy đơn vận chuyển mã '{id}' để xóa.");

            DonVanChuyen don = dal.GetByID(id);
            if (don != null)
            {
                if (don.TrangThaiDon == "Hoàn thành")
                {
                    throw new InvalidOperationException("Không thể xóa đơn vận chuyển đã 'Hoàn thành' để đảm bảo tính toàn vẹn dữ liệu đối soát!");
                }

                if (don.TrangThaiDon == "Đang vận chuyển")
                {
                    throw new InvalidOperationException("Không thể xóa đơn vận chuyển đang 'Đang vận chuyển' khi phương tiện đang làm nhiệm vụ!");
                }
            }

            bool success = dal.Delete(id);
            if (!success)
                throw new Exception("Xóa đơn vận chuyển thất bại. Vui lòng thử lại!");
            return success;
        }

        public List<DonVanChuyen> TimKiem(string keyword)
        {
            return dal.Search(keyword);
        }

        public DataTable GetTatCaDonVanChuyen()
        {
            return dal.GetTatCaDonVanChuyen();
        }

        public bool KiemTraTonTai(string id)
        {
            return dal.IsExist(id);
        }

        public void DongBoTrangThaiXeToanHeThong()
        {
            dal.DongBoTrangThaiXeToanHeThong();
        }

        public void DongBoLienKetBanHangVaLogistics()
        {
            dal.DongBoLienKetBanHangVaLogistics();
        }
    }
}

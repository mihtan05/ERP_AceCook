using System;
using System.Collections.Generic;
using ERP.DAL;
using ERP.DTO;
using System.Data;

namespace ERP.BLL
{
    public class PhieuTraHangBLL
    {
        private readonly PhieuTraHangDAL dal;

        public PhieuTraHangBLL()
        {
            dal = new PhieuTraHangDAL();
        }

        public PhieuTraHangBLL(string connectionString)
        {
            dal = new PhieuTraHangDAL(connectionString);
        }

        public string SinhMaPhieu()
        {
            return dal.GetNextID();
        }

        public List<PhieuTraHang> GetAll()
        {
            return dal.GetAll();
        }

        public PhieuTraHang GetByID(string id)
        {
            return dal.GetByID(id);
        }

        public List<string> GetDanhSachCTYC()
        {
            return dal.GetDanhSachCTYC();
        }

        public List<ChiTietYeuCauComboItem> GetDanhSachChiTietYeuCauFull()
        {
            return dal.GetDanhSachChiTietYeuCauFull();
        }

        public DiemVanChuyenComboItem LayHoacTaoDiemThuHoiChoKhachHang(string tenKhach, string diaChi, string sdt)
        {
            var donVCDAL = new DonVanChuyenDAL();
            return donVCDAL.LayHoacTaoDiemNhanHangChoKhachHang(tenKhach, diaChi, sdt);
        }

        public List<string> GetDanhSachMaDVC()
        {
            return dal.GetDanhSachMaDVC();
        }

        public Dictionary<string, string> GetDanhSachDVC()
        {
            return dal.GetDanhSachDVC();
        }

        public List<string> GetDanhSachXeKhaDung()
        {
            return dal.GetDanhSachXeKhaDung();
        }

        public List<string> GetDanhSachLoaiXeRanh()
        {
            return dal.GetDanhSachLoaiXeRanh();
        }

        public DataTable GetAllPhuongTienRanh(string idPhieuHienTai = null)
        {
            return dal.GetAllPhuongTienRanh(idPhieuHienTai);
        }

        public bool CheckXeDangBan(string bienSoXe, string idPhieuLoaiTru = null)
        {
            return dal.CheckXeDangBan(bienSoXe, idPhieuLoaiTru);
        }

        public bool CheckXeDangBan(string bienSoXe, string idPhieuLoaiTru, out string lyDoBan)
        {
            return dal.CheckXeDangBan(bienSoXe, idPhieuLoaiTru, out lyDoBan);
        }

        public ThongTinYeuCau GetThongTinYeuCau(string idCTYC)
        {
            return dal.GetThongTinYeuCau(idCTYC);
        }

        // [Nghiệp vụ A2]: Lọc tìm kiếm và kiểm tra ngày kết thúc >= ngày bắt đầu
        public List<PhieuTraHang> TimKiem(string keyword, DateTime tuNgay, DateTime denNgay, string trangThai)
        {
            if (tuNgay > denNgay)
            {
                throw new Exception("Lỗi định dạng bộ lọc: Ngày kết thúc không được nhỏ hơn ngày bắt đầu!");
            }
            return dal.Search(keyword, tuNgay, denNgay, trangThai);
        }

        // Thêm phiếu: Kiểm tra rỗng, kiểm tra trùng lặp thu hồi ID_CTYC
        public bool ThemPhieu(PhieuTraHang p)
        {
            if (p == null)
            {
                throw new Exception("Thông tin phiếu trả hàng không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(p.ID_PhieuTra))
            {
                throw new Exception("Mã phiếu trả không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(p.MaDVC))
            {
                throw new Exception("Mã điểm vận chuyển không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(p.ID_CTYC))
            {
                throw new Exception("Mã chi tiết yêu cầu không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(p.BienSoXe))
            {
                throw new Exception("Vui lòng chọn xe tải đi thu hồi!");
            }

            if (dal.GetByID(p.ID_PhieuTra.Trim()) != null)
            {
                throw new Exception($"Mã phiếu trả '{p.ID_PhieuTra}' đã tồn tại trong hệ thống!");
            }

            if (dal.CheckThuHoiDup(p.ID_CTYC.Trim()))
            {
                throw new Exception("Yêu cầu lỗi này đã được tạo phiếu thu hồi trước đó!");
            }

            if (dal.CheckXeDangBan(p.BienSoXe.Trim(), null, out string lyDoBan))
            {
                throw new Exception(lyDoBan);
            }

            p.TrangThai = ChuanHoaTrangThai(p.TrangThai);

            if (p.NgayTra == DateTime.MinValue)
            {
                p.NgayTra = DateTime.Now;
            }

            return dal.Insert(p);
        }

        public bool SuaPhieu(PhieuTraHang p)
        {
            if (p == null)
            {
                throw new Exception("Thông tin phiếu trả hàng không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(p.ID_PhieuTra))
            {
                throw new Exception("Mã phiếu trả không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(p.MaDVC))
            {
                throw new Exception("Mã điểm vận chuyển không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(p.ID_CTYC))
            {
                throw new Exception("Mã chi tiết yêu cầu không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(p.BienSoXe))
            {
                throw new Exception("Vui lòng chọn xe tải đi thu hồi!");
            }

            PhieuTraHang hienTai = dal.GetByID(p.ID_PhieuTra.Trim());
            if (hienTai == null)
            {
                throw new Exception($"Không tìm thấy phiếu trả '{p.ID_PhieuTra}' trong hệ thống để cập nhật!");
            }

            p.TrangThai = ChuanHoaTrangThai(p.TrangThai);
            string trangThaiCu = ChuanHoaTrangThai(hienTai.TrangThai);

            if (trangThaiCu == "Đã xử lý" || trangThaiCu == "Đã nhập kho" || trangThaiCu == "Đã hủy" || trangThaiCu == "Từ chối")
            {
                throw new Exception($"Phiếu trả hàng đã ở trạng thái '{trangThaiCu}' (Kho đã duyệt / kết thúc), không được sửa đổi!");
            }

            if (!HopLeChuyenTrangThai(trangThaiCu, p.TrangThai))
            {
                throw new Exception("Chỉ được chuyển trạng thái: Chờ xử lý → Đang thu hồi → Đã xử lý (hoặc Đã hủy). Phiếu đã được Kho duyệt hoặc từ chối sẽ bị khóa trạng thái!");
            }

            if (trangThaiCu != "Chờ xử lý")
            {
                if (p.ID_CTYC.Trim() != hienTai.ID_CTYC
                    || p.MaDVC.Trim() != (hienTai.MaDVC ?? string.Empty).Trim()
                    || p.BienSoXe.Trim() != (hienTai.BienSoXe ?? string.Empty).Trim())
                {
                    throw new Exception("Phiếu đang thu hồi chỉ được cập nhật trạng thái, không được đổi xe / điểm VC / mã CTYC.");
                }
            }
            else if (p.ID_CTYC.Trim() != hienTai.ID_CTYC && dal.CheckThuHoiDup(p.ID_CTYC.Trim()))
            {
                throw new Exception("Yêu cầu lỗi này đã được tạo phiếu thu hồi trước đó!");
            }

            if (p.TrangThai != "Đã hủy" && p.TrangThai != "Từ chối" && dal.CheckXeDangBan(p.BienSoXe.Trim(), p.ID_PhieuTra.Trim(), out string lyDoBan))
            {
                throw new Exception(lyDoBan);
            }

            return dal.Update(p);
        }

        private static string ChuanHoaTrangThai(string trangThai)
        {
            if (string.IsNullOrWhiteSpace(trangThai))
            {
                return "Chờ xử lý";
            }

            if (trangThai == "Đang lấy hàng")
            {
                return "Đang thu hồi";
            }

            if (trangThai == "Đã nhập kho")
            {
                return "Đã xử lý";
            }

            return trangThai;
        }

        private static bool HopLeChuyenTrangThai(string from, string to)
        {
            if (from == to)
            {
                return true;
            }

            if (from == "Chờ xử lý")
            {
                return to == "Đang thu hồi" || to == "Đã hủy";
            }

            if (from == "Đang thu hồi")
            {
                return to == "Đã xử lý" || to == "Đã nhập kho" || to == "Đã hủy" || to == "Từ chối";
            }

            return false;
        }

        // Xóa phiếu: Phải query lấy trạng thái hiện tại. Nếu TrangThai != "Chờ xử lý" -> throw
        public bool XoaPhieu(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception("Mã phiếu trả cần xóa không được để trống!");
            }

            PhieuTraHang hienTai = dal.GetByID(id.Trim());
            if (hienTai == null)
            {
                throw new Exception($"Không tìm thấy phiếu trả '{id}' trong hệ thống để xóa!");
            }

            if (hienTai.TrangThai != "Chờ xử lý")
            {
                throw new Exception("Chỉ được sửa/xóa phiếu khi đang ở trạng thái 'Chờ xử lý'!");
            }

            return dal.Delete(id.Trim());
        }
    }
}

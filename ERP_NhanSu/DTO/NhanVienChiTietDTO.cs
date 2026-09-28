using System;

namespace HR_Management.DTO
{
    public class NhanVienChiTietDTO
    {
        // Thuộc tính từ bảng NhanVien
        public string ID_NV { get; set; } = string.Empty;
        public string TenNV { get; set; } = string.Empty;
        public string? ChucVu { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; } = "Nam";
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? MaPhongBan { get; set; }
        public string? PhongBan { get; set; } // Tên phòng ban để hiển thị
        public decimal? LuongCoBan { get; set; }
        public string? TrangThai { get; set; } = "Đang làm việc";

        // Thuộc tính từ bảng ThongTinNhanVien
        public string? MaThongTin { get; set; }
        public string? SoCCCD { get; set; }
        public string? TrinhDo { get; set; }
        public string? ChuyenNganh { get; set; }
        public string? KinhNghiem { get; set; }
        public string? ThanhTich { get; set; }
    }
}

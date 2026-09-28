using System;

namespace HR_Management.DTO
{
    public class NhanVienDTO
    {
        public string ID_NV { get; set; } = string.Empty;
        public string TenNV { get; set; } = string.Empty;
        public string? ChucVu { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? MaPhongBan { get; set; }
        public string? PhongBan { get; set; }
        public decimal? LuongCoBan { get; set; }
        public string? TrangThai { get; set; } = "Đang làm việc";
    }
}

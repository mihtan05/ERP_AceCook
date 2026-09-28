using System;

namespace HR_Management.DTO
{
    public class HeThongDTO
    {
        public string MaTaiKhoan { get; set; } = string.Empty;
        public string ID_NV { get; set; } = string.Empty;
        public string? TenNV { get; set; } // Hiển thị trên UI
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string? VaiTro { get; set; } = "Nhân viên";
        public string TrangThai { get; set; } = "Hoạt động";
        public DateTime? LanDangNhapCuoi { get; set; }
        public string? QuyenHan { get; set; } = "XEM,THEM,SUA";
    }
}

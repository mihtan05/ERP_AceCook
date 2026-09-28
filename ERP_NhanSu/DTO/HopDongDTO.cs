using System;

namespace HR_Management.DTO
{
    public class HopDongDTO
    {
        public string MaHopDong { get; set; } = string.Empty;
        public string ID_NV { get; set; } = string.Empty;
        public string? TenNV { get; set; } // Hiển thị trên UI
        public string LoaiHopDong { get; set; } = "Hợp đồng xác định thời hạn";
        public DateTime NgayKy { get; set; } = DateTime.Today;
        public DateTime NgayBatDau { get; set; } = DateTime.Today;
        public DateTime? NgayKetThuc { get; set; }
        public decimal LuongCoBan { get; set; }
        public string TrangThai { get; set; } = "Hiệu lực";
    }
}

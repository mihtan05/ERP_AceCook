namespace HR_Management.DTO
{
    public class PhongBanDTO
    {
        public string MaPhongBan { get; set; } = string.Empty;
        public string TenPhongBan { get; set; } = string.Empty;
        public string? TruongPhong { get; set; }
        public int SoLuongNhanVien { get; set; }
        public string? MoTa { get; set; }
        public string TrangThai { get; set; } = "Hoạt động";
    }
}

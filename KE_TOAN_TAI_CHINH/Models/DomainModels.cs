using System;
using System.Collections.Generic;

namespace KeToanTaiChinh.Models
{
    internal sealed class LookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Display { get { return string.Equals(Id, Name, StringComparison.OrdinalIgnoreCase) ? Name : Id + " - " + Name; } }
    }

    internal sealed class ChungTuInput
    {
        public string MaPhieu { get; set; }
        public DateTime NgayLap { get; set; }
        public string MaDoiTac { get; set; }
        public decimal SoTien { get; set; }
        public string HinhThuc { get; set; }
        public string MaCongNo { get; set; }
        public string TaiKhoanNo { get; set; }
        public string TaiKhoanCo { get; set; }
        public string DienGiai { get; set; }
        public string TrangThai { get; set; }
    }

    internal sealed class CongNoInput
    {
        public string MaCongNo { get; set; }
        public string MaDoiTac { get; set; }
        public string LoaiCongNo { get; set; }
        public DateTime NgayPhatSinh { get; set; }
        public DateTime HanThanhToan { get; set; }
        public decimal SoTien { get; set; }
        public string SoChungTu { get; set; }
        public string DienGiai { get; set; }
    }

    internal sealed class DoiChieuDong
    {
        public string MaCongNo { get; set; }
        public decimal SoTienSoSach { get; set; }
        public decimal SoTienDoiTac { get; set; }
    }

    internal sealed class DoiChieuInput
    {
        public string MaBienBan { get; set; }
        public string MaDoiTac { get; set; }
        public DateTime NgayDoiChieu { get; set; }
        public string KyDoiChieu { get; set; }
        public string TrangThai { get; set; }
        public List<DoiChieuDong> ChiTiet { get; set; }
    }

    internal sealed class BangLuongInput
    {
        public string MaBangLuong { get; set; }
        public string TenBangLuong { get; set; }
        public string KyLuong { get; set; }
        public string PhongBan { get; set; }
        public string DonViTienTe { get; set; }
        public string GhiChu { get; set; }
    }
}

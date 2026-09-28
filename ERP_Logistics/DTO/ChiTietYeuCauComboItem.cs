using System;

namespace ERP.DTO
{
    public class ChiTietYeuCauComboItem
    {
        public string ID_CTYC { get; set; }
        public string ID_YC { get; set; }
        public string TenKhachHang { get; set; }
        public string TenHang { get; set; }
        public int SoLuong { get; set; }
        public string DiaChiGiao { get; set; }
        public string SDT { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(ID_CTYC)) return "-- Chọn yêu cầu trả hàng --";
            return $"{ID_CTYC} | {TenKhachHang} | {TenHang} (SL: {SoLuong})";
        }
    }
}

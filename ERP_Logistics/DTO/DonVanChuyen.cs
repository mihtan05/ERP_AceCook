using System;

namespace ERP.DTO
{
    public class DonVanChuyen
    {
        private string _trangThaiDon;

        public string ID_DonVC { get; set; }
        public string ID_DH { get; set; }
        public string BienSoXe { get; set; }
        public string MaDVC { get; set; }
        public string TenDVC { get; set; }
        public string ID_SP { get; set; }
        public string TenHang { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChiGiao { get; set; }
        public string SDTKhachHang { get; set; }
        public int SoLuongGiao { get; set; }
        public DateTime ThoiGianKhoiHanh { get; set; }
        public decimal TrongLuong { get; set; }
        public string TrangThaiDon
        {
            get { return _trangThaiDon; }
            set
            {
                if (!string.IsNullOrEmpty(value) &&
                    value != "Khởi tạo" &&
                    value != "Đang vận chuyển" &&
                    value != "Hoàn thành" &&
                    value != "Đã hủy")
                {
                    throw new ArgumentException("Trạng thái đơn chỉ nhận 1 trong 4 giá trị: 'Khởi tạo', 'Đang vận chuyển', 'Hoàn thành', 'Đã hủy'.");
                }
                _trangThaiDon = value;
            }
        }
    }

    public class XeKhaDungItem
    {
        public string BienSoXe { get; set; }
        public string LoaiXe { get; set; }
        public decimal TaiTrong { get; set; }

        public decimal TaiTrongKg
        {
            get
            {
                return TaiTrong <= 50 ? TaiTrong * 1000 : TaiTrong;
            }
        }

        public string DisplayText
        {
            get
            {
                if (TaiTrong <= 50)
                    return $"{BienSoXe} ({TaiTrong:0.##} tấn ~ {TaiTrongKg:N0} kg)";
                return $"{BienSoXe} ({TaiTrong:N0} kg)";
            }
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }

    public class DonHangChoGiaoItem
    {
        public string ID_DH { get; set; }
        public string ID_KH { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChiGiao { get; set; }
        public string SDT { get; set; }
        public string ID_SP { get; set; }
        public string TenSP { get; set; }
        public int SoLuongDat { get; set; }
        public string TrangThaiGiao { get; set; }

        public string DisplayText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ID_DH))
                    return "-- Không chọn đơn hàng (Giao tự do) --";
                return $"{ID_DH} | {TenKhachHang} | {TenSP} (SL: {SoLuongDat})";
            }
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }

    public class SanPhamComboItem
    {
        public string ID_SP { get; set; }
        public string TenHang { get; set; }

        public string DisplayText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TenHang))
                    return ID_SP;
                return $"{ID_SP} - {TenHang}";
            }
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }

    public class DiemVanChuyenComboItem
    {
        public string MaDVC { get; set; }
        public string TenDVC { get; set; }
        public string DiaChiDVC { get; set; }
        public string SDT { get; set; }

        public string DisplayText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(TenDVC))
                {
                    return TenDVC;
                }
                return MaDVC;
            }
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}

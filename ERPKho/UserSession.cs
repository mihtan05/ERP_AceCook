using System;

namespace ERPKho
{
    public static class UserSession
    {
        public static string MaNguoiDung { get; set; }
        public static string TenNguoiDung { get; set; }
        public static string ChucVu { get; set; } // Ví dụ: "Quản lý", "Nhân viên kho", "Kiểm kê"

        public static void ClearSession()
        {
            MaNguoiDung = null;
            TenNguoiDung = null;
            ChucVu = null;
        }
    }
}
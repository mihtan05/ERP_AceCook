namespace KeToanTaiChinh.Infrastructure
{
    public static class Session
    {
        private static string _maNv = "NV004";
        private static string _hoTen = "Phạm Thu Hà (NV004)";
        private static string _vaiTro = "Toàn quyền";

        public static string MaNhanVien { get => _maNv; set => _maNv = value; }
        public static string HoTen { get => _hoTen; set => _hoTen = value; }
        public static string VaiTro { get => _vaiTro; set => _vaiTro = value; }
        public static bool LaQuanLy { get; set; } = true;

        public static void SetUser(string maNv, string hoTen, string vaiTro)
        {
            if (!string.IsNullOrEmpty(maNv)) _maNv = maNv;
            if (!string.IsNullOrEmpty(hoTen)) _hoTen = hoTen;
            if (!string.IsNullOrEmpty(vaiTro)) _vaiTro = vaiTro;
        }
    }
}

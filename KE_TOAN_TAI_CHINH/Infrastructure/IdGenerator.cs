using System;

namespace KeToanTaiChinh.Infrastructure
{
    internal static class IdGenerator
    {
        // Các khóa hiện có là varchar(20); GUID rút gọn tránh va chạm giữa nhiều máy.
        public static string NewId(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix) || prefix.Length > 4)
                throw new ArgumentException("Tiền tố mã không hợp lệ.", "prefix");
            return prefix.ToUpperInvariant() + Guid.NewGuid().ToString("N").Substring(0, 20 - prefix.Length).ToUpperInvariant();
        }
    }
}

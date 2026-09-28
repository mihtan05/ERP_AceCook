using System;
using System.Collections.Generic;
using System.Data;
using KeToanTaiChinh.Infrastructure;
using Npgsql;

namespace KeToanTaiChinh.Services
{
    internal static class DatabaseCompatibilityService
    {
        private static readonly string[] RequiredTables = {
            "nhanvien", "khachhang", "taikhoanketoan", "khoancongno", "phieuthu",
            "chitietphieuthu", "phieuchi", "chitietphieuchi", "bienbandoichieu",
            "chitietdoichieu", "bangluong", "chitietluongnhanvien"
        };

        private static readonly string[] RequiredColumns = {
            "nhanvien.phongban", "nhanvien.luongcoban", "nhanvien.trangthai",
            "khachhang.hanmuctindung", "khoancongno.sochungtu", "khoancongno.diengiai",
            "phieuthu.nguoiduyet", "phieuthu.ngayduyet", "phieuthu.lydotuchoi",
            "phieuchi.nguoiduyet", "phieuchi.ngayduyet", "phieuchi.lydotuchoi",
            "bienbandoichieu.nguoiduyet", "bienbandoichieu.ngayduyet",
            "bangluong.phongban", "bangluong.donvitiente", "bangluong.ghichu",
            "bangluong.nguoilap", "bangluong.nguoiduyet", "bangluong.ngayduyet",
            "chitietluongnhanvien.luongcoban"
        };

        public static void KiemTra()
        {
            string name = Convert.ToString(Db.Scalar("SELECT current_database();"));
            string expectedName = new NpgsqlConnectionStringBuilder(Db.ConnectionString).Database;
            if (!string.Equals(name, expectedName, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Đang kết nối database '" + name + "'; cần chọn " + expectedName + ".");

            DataTable columns = Db.Query(@"
SELECT table_name, column_name
FROM information_schema.columns WHERE table_schema='public';");
            var present = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var tables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow row in columns.Rows)
            {
                string table = Convert.ToString(row["table_name"]);
                string column = Convert.ToString(row["column_name"]);
                bool required = Array.Exists(RequiredTables, t => string.Equals(t, table, StringComparison.OrdinalIgnoreCase));
                if (!required) continue;
                if (table != table.ToLowerInvariant() || column != column.ToLowerInvariant())
                    throw new InvalidOperationException("Neon có bảng/cột đặt tên chữ hoa trong dấu nháy (" +
                        table + "." + column + "). Cần chuyển schema sang tên chữ thường trước khi dùng ứng dụng.");
                tables.Add(table);
                present.Add(table + "." + column);
            }
            var missing = new List<string>();
            foreach (string table in RequiredTables)
                if (!tables.Contains(table)) missing.Add("bảng " + table);
            foreach (string column in RequiredColumns)
                if (!present.Contains(column)) missing.Add("cột " + column);
            if (missing.Count > 0)
                throw new InvalidOperationException("Neon thiếu " + string.Join(", ", missing) +
                    ". Hãy chạy Database/Neon_PostgreSQL.sql trong Neon SQL Editor.");

            int employee = Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM nhanvien WHERE id_nv='NV004';"));
            if (employee == 0)
                throw new InvalidOperationException("Chưa có NV004 trong bảng nhanvien. Xem Database/Neon_PostgreSQL.sql.");
            int accounts = Convert.ToInt32(Db.Scalar(@"
SELECT COUNT(*) FROM taikhoanketoan
WHERE mataikhoan IN ('1111','1121','1311','3311','5111','6411','6421','3341');"));
            if (accounts < 8)
                throw new InvalidOperationException("Chưa đủ 8 tài khoản kế toán. Xem Database/Neon_PostgreSQL.sql.");
        }
    }
}

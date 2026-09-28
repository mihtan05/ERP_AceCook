using System.Data;
using KeToanTaiChinh.Infrastructure;

namespace KeToanTaiChinh.Services
{
    internal sealed class BaoCaoService
    {
        public DataTable TongQuan()
        {
            return Db.Query(@"
SELECT
 (SELECT COALESCE(SUM(soTien),0) FROM PhieuThu WHERE trangThai='Đã duyệt'
    AND ngayLap >= date_trunc('month', CURRENT_TIMESTAMP) AND ngayLap < date_trunc('month', CURRENT_TIMESTAMP) + INTERVAL '1 month') AS ThuThang,
 (SELECT COALESCE(SUM(soTien),0) FROM PhieuChi WHERE trangThai='Đã duyệt'
    AND ngayLap >= date_trunc('month', CURRENT_TIMESTAMP) AND ngayLap < date_trunc('month', CURRENT_TIMESTAMP) + INTERVAL '1 month') AS ChiThang,
 (SELECT COALESCE(SUM(soDuConLai),0) FROM KhoanCongNo WHERE loaiCongNo='Phải thu') AS PhaiThu,
 (SELECT COALESCE(SUM(soDuConLai),0) FROM KhoanCongNo WHERE loaiCongNo='Phải trả') AS PhaiTra,
 (SELECT COUNT(*) FROM PhieuThu WHERE trangThai='Chờ duyệt') +
 (SELECT COUNT(*) FROM PhieuChi WHERE trangThai='Chờ duyệt') +
 (SELECT COUNT(*) FROM BangLuong WHERE trangThai='Chờ duyệt') AS ChoDuyet,
 (SELECT COUNT(*) FROM KhoanCongNo WHERE soDuConLai>0 AND hanThanhToan<CURRENT_DATE) AS NoQuaHan;");
        }

        public DataTable DongTienTheoThang()
        {
            return Db.Query(@"
SELECT Ky, SUM(Thu) AS TongThu, SUM(Chi) AS TongChi, SUM(Thu)-SUM(Chi) AS DongTienThuan
FROM (
 SELECT TO_CHAR(ngayLap,'YYYY-MM') AS Ky, soTien AS Thu, CAST(0 AS DECIMAL(18,2)) AS Chi
 FROM PhieuThu WHERE trangThai='Đã duyệt'
 UNION ALL
 SELECT TO_CHAR(ngayLap,'YYYY-MM'), 0, soTien FROM PhieuChi WHERE trangThai='Đã duyệt'
) x GROUP BY Ky ORDER BY Ky DESC;");
        }

        public DataTable CongNoTheoDoiTac()
        {
            return Db.Query(@"
SELECT k.ID_KH AS MaDoiTac,k.TenDoanhNghiep AS DoiTac,
 SUM(CASE WHEN c.loaiCongNo='Phải thu' THEN c.soDuConLai ELSE 0 END) AS PhaiThu,
 SUM(CASE WHEN c.loaiCongNo='Phải trả' THEN c.soDuConLai ELSE 0 END) AS PhaiTra,
 SUM(CASE WHEN c.soDuConLai>0 AND c.hanThanhToan<CURRENT_DATE THEN c.soDuConLai ELSE 0 END) AS QuaHan
FROM KhachHang k LEFT JOIN KhoanCongNo c ON c.ID_KH=k.ID_KH
GROUP BY k.ID_KH,k.TenDoanhNghiep ORDER BY k.TenDoanhNghiep;");
        }
    }
}

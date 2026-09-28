using System;
using System.Data;
using Npgsql;
using KeToanTaiChinh.Infrastructure;
using KeToanTaiChinh.Models;

namespace KeToanTaiChinh.Services
{
    internal sealed class CongNoService
    {
        public DataTable DanhSach(string tuKhoa, string loai, bool chiQuaHan)
        {
            return Db.Query(@"
SELECT c.maCongNo AS MaCongNo, c.soChungTu AS SoChungTu,
       k.TenDoanhNghiep AS DoiTac, c.loaiCongNo AS LoaiCongNo,
       c.ngayPhatSinh AS NgayPhatSinh, c.hanThanhToan AS HanThanhToan,
       c.soTienPhatSinh AS SoTienPhatSinh, c.soTienDaThanhToan AS DaThanhToan,
       c.soDuConLai AS SoDuConLai,
       CASE WHEN c.soDuConLai > 0 AND c.hanThanhToan < CURRENT_DATE
            THEN 'Quá hạn' ELSE c.trangThai END AS TrangThai,
       c.dienGiai AS DienGiai
FROM KhoanCongNo c INNER JOIN KhachHang k ON k.ID_KH=c.ID_KH
WHERE (@TuKhoa='' OR c.maCongNo ILIKE '%' || @TuKhoa || '%'
       OR COALESCE(c.soChungTu,'') ILIKE '%' || @TuKhoa || '%'
       OR k.TenDoanhNghiep ILIKE '%' || @TuKhoa || '%')
  AND (@Loai='' OR c.loaiCongNo=@Loai)
  AND (@QuaHan=0 OR (c.soDuConLai>0 AND c.hanThanhToan<CURRENT_DATE))
ORDER BY c.hanThanhToan, c.ngayPhatSinh DESC;",
                Db.P("@TuKhoa", tuKhoa ?? string.Empty), Db.P("@Loai", loai ?? string.Empty),
                Db.P("@QuaHan", chiQuaHan ? 1 : 0));
        }

        public bool VuotHanMuc(CongNoInput input, out decimal tongSauKhiTao, out decimal hanMuc)
        {
            tongSauKhiTao = 0m;
            hanMuc = 0m;
            if (input.LoaiCongNo != "Phải thu") return false;
            DataTable table = Db.Query(@"
SELECT COALESCE(k.hanMucTinDung,0) AS HanMuc,
       COALESCE(SUM(CASE WHEN c.loaiCongNo='Phải thu' THEN c.soDuConLai ELSE 0 END),0) AS TongNo
FROM KhachHang k LEFT JOIN KhoanCongNo c ON c.ID_KH=k.ID_KH
WHERE k.ID_KH=@Ma GROUP BY k.hanMucTinDung;", Db.P("@Ma", input.MaDoiTac));
            if (table.Rows.Count == 0) return false;
            hanMuc = Convert.ToDecimal(table.Rows[0]["HanMuc"]);
            tongSauKhiTao = Convert.ToDecimal(table.Rows[0]["TongNo"]) + input.SoTien;
            return hanMuc > 0 && tongSauKhiTao > hanMuc;
        }

        public void Tao(CongNoInput input)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (string.IsNullOrWhiteSpace(input.MaDoiTac) || string.IsNullOrWhiteSpace(input.LoaiCongNo) ||
                string.IsNullOrWhiteSpace(input.SoChungTu) || string.IsNullOrWhiteSpace(input.DienGiai))
                throw new InvalidOperationException("Vui lòng nhập đầy đủ loại công nợ, đối tác, chứng từ và diễn giải.");
            if (input.SoTien <= 0) throw new InvalidOperationException("Số tiền công nợ phải lớn hơn 0.");
            if (input.LoaiCongNo != "Phải thu" && input.LoaiCongNo != "Phải trả")
                throw new InvalidOperationException("Loại công nợ phải là Phải thu hoặc Phải trả.");
            if (input.HanThanhToan.Date < input.NgayPhatSinh.Date)
                throw new InvalidOperationException("Hạn thanh toán không được trước ngày phát sinh.");

            object duplicate = Db.Scalar("SELECT COUNT(*) FROM KhoanCongNo WHERE soChungTu=@SoChungTu",
                Db.P("@SoChungTu", input.SoChungTu.Trim()));
            if (Convert.ToInt32(duplicate) > 0)
                throw new InvalidOperationException("Số chứng từ đã tồn tại công nợ.");

            input.MaCongNo = IdGenerator.NewId("CN");
            try
            {
                Db.Execute(@"
INSERT INTO KhoanCongNo(maCongNo,ID_KH,loaiCongNo,ngayPhatSinh,hanThanhToan,
    soTienPhatSinh,soTienDaThanhToan,soDuConLai,trangThai,soChungTu,dienGiai)
VALUES(@Ma,@DoiTac,@Loai,@Ngay,@Han,@SoTien,0,@SoTien,'Mới tạo',@ChungTu,@DienGiai);",
                    Db.P("@Ma", input.MaCongNo), Db.P("@DoiTac", input.MaDoiTac),
                    Db.P("@Loai", input.LoaiCongNo), Db.P("@Ngay", input.NgayPhatSinh.Date),
                    Db.P("@Han", input.HanThanhToan.Date), Db.P("@SoTien", input.SoTien),
                    Db.P("@ChungTu", input.SoChungTu.Trim()), Db.P("@DienGiai", input.DienGiai.Trim()));
            }
            catch (PostgresException ex)
            {
                if (ex.SqlState == "23505")
                    throw new InvalidOperationException("Số chứng từ đã tồn tại công nợ.", ex);
                throw;
            }
        }
    }
}

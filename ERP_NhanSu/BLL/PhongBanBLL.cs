using System;
using System.Collections.Generic;
using HR_Management.DAL;
using HR_Management.DTO;

namespace HR_Management.BLL
{
    public class PhongBanBLL
    {
        private readonly PhongBanDAL _dal = new PhongBanDAL();

        public List<PhongBanDTO> GetAll()
        {
            return _dal.GetAll();
        }

        public PhongBanDTO? GetById(string maPhongBan)
        {
            if (string.IsNullOrWhiteSpace(maPhongBan)) return null;
            return _dal.GetById(maPhongBan);
        }

        public bool Save(PhongBanDTO pb, bool isNew, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(pb.MaPhongBan))
            {
                error = "Mã phòng ban không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(pb.TenPhongBan))
            {
                error = "Tên phòng ban không được để trống!";
                return false;
            }

            try
            {
                if (isNew)
                {
                    if (_dal.CheckExists(pb.MaPhongBan))
                    {
                        error = $"Mã phòng ban '{pb.MaPhongBan}' đã tồn tại trong hệ thống!";
                        return false;
                    }
                    return _dal.Insert(pb);
                }
                else
                {
                    return _dal.Update(pb);
                }
            }
            catch (Exception ex)
            {
                error = "Lỗi lưu phòng ban: " + ex.Message;
                return false;
            }
        }

        public bool Delete(string maPhongBan, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(maPhongBan))
            {
                error = "Vui lòng chọn phòng ban cần xóa!";
                return false;
            }

            try
            {
                return _dal.Delete(maPhongBan);
            }
            catch (Exception ex)
            {
                error = "Không thể xóa phòng ban đang có nhân viên trực thuộc! (" + ex.Message + ")";
                return false;
            }
        }
    }
}

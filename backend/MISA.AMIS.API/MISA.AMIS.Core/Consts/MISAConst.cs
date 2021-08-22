using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Consts
{
    public class MISAConst
    {
        #region Properties
        // thanh cong khi them moi, cap nhat, xoa du lieu
        public string MISACodeSuccess = "MISA-000";
        // loi 500
        public string MISACodeErrorException = "MISA500";
        // loi du lieu trống
        public string MISACodeNullError = "MISA-001";
        // lỗi mã trùng
        public string MISACodeExist = "MISA-002";
        // lỗi mã không tồn tại
        public string MISACodeNotExist = "MISA-003";
        // loi max length
        public string MISACodeMaxLength = "MISA-004";
        //loi du lieu khong hop le: pageNumber<0, ma khach hang/ ma nhan vien khong bat dau bang KH-/NV-
        public string MISADataInvalid = "MISA-005";
        // loi email khong dung dinh dang
        public string MISAEmailInValid = "MISA-006";
        // loi id khong đúng định dạng
        public string MISAIdInvalid = "MISA-007";
        // ngày không được nhỏ hơn hiện tại
        public string MISADateInvalid = "MISA-008";
        #endregion

        /// <summary>
        /// Hàm xử lý exception trong baseController
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="res"></param>
        /// <param name="e"></param>
        /// <param name="serviceResult"></param>
        /// <returns></returns>
        public dynamic catchException(dynamic res, Object DevMgs, ServiceResult serviceResult)
        {
            res.UserMgs = serviceResult.UserMgs;
            res.MISACode = serviceResult.MISACode;
            res.DevMgs = DevMgs;
            return res;
        }
    }

    #region EntityState
    /// <summary>
    /// trang thai cua entity
    /// </summary>
    public enum EntityState
    {
        Get = 0,
        Add = 1,
        Update = 2,
        Delete = 3
    }
    #endregion
    #region GenderName
    public enum NameGender
    {
        Male = 1,
        Female = 0,
        Other = 2
    }
    public enum NameWorkStatus
    {
        Working = 0,
        NotWorking = 1,
        Intern = 2,
        OnMaternity = 3,
        GoingAbroad = 4
    }
    #endregion
}

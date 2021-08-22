using MISA.AMIS.Core.Consts;
using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Core
{
    public class Employee:BaseEntity
    {
        #region Property
        //id nhan vien
        [PrimaryKey]
        [DisplayName("Id nhân viên")]
        public Guid EmployeeId { get; set; }

        // ma nhan vien
        [DisplayName("Mã nhân viên")]
        [Duplicated]
        [RequiredAttr]
        [MaxLengthAttr(20)]
        [StartWith("NV-")]
        public string EmployeeCode { get; set; }

        //ho ten nhan vien
        [DisplayName("Tên nhân viên")]
        [MaxLengthAttr(100)]
        public string EmployeeName { get; set; }

        //ngay sinh
        [DisplayName("Ngày sinh")]
        [DateValid]
        public DateTime? DateOfBirth { get; set; }

        // gioi tinh
        public int? Gender { get; set; }

        // so dien thoai cố định
        [DisplayName("Số điện thoại cố định")]
        [PhoneNumberAttr]
        public string TelephoneNumber { get; set; }

        // so dien thoai
        [DisplayName("Số điện thoại nhân viên")]
        [PhoneNumberAttr]
        public string PhoneNumber { get; set; }

        //email nhan vien
        [DisplayName("Email nhân viên")]
        [EmailFormat]
        public string Email { get; set; }

        // dia chi nhan vien
        public string Address { get; set; }

        // so chung minh thu nhan dan / can cuoc cong dan
        [DisplayName("Số CMTND/ CCCD")]
        [MaxLengthAttr(20)]
        public string IdentityNumber { get; set; }

        //ngay cap cmtnd/cccd
        [DisplayName("Ngày cấp CMND/CCCD")]
        [DateValid]
        public DateTime? IdentityDate { get; set; }

        //noi cap cmtnd/cccd
        public string IdentityPlace { get; set; }

        //id đơn vị
        [DisplayName("Đơn vị")]
        [RequiredAttr]
        public Guid? DepartmentId { get; set; }

        // chức danh
        public string EmployeePosition { get; set; }

        // ten phong ban
        public string DepartmentName { get; set; }

        //gioi tinh
        public string GenderName
        {
            set{}
            get
            {
                if (Gender == (int)NameGender.Male)
                    return "Nam";
                else if (Gender == (int)NameGender.Female)
                    return "Nữ";
                else if (Gender == (int)NameGender.Other)
                    return "Khác";
                else return null;
            }

        }

        [MaxLengthAttr(20)]
        [DisplayName("Tài khoản ngân hàng")]
        // tài khoản ngân hàng
        public string BankAccountNumber { get; set; }

        // tên ngân hàng
        public string BankName { get; set; }

        // tên chi nhánh ngan hang
        public string BankBranchName { get; set; }

        //
        public string BankProvinceName { get; set; }
        #endregion
    }
}

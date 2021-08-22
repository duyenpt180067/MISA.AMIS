using MISA.AMIS.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class BaseEntity
    {
        #region Validate Fields
        //khong cho phep trong
        [AttributeUsage(AttributeTargets.Property)]
        public class RequiredAttr:Attribute
        {

        }
        //khong cho phep trung
        [AttributeUsage(AttributeTargets.Property)]
        public class Duplicated : Attribute
        {

        }
        //khoa chinh
        [AttributeUsage(AttributeTargets.Property)]
        public class PrimaryKey : Attribute
        {

        }
        // do dai toi da la value
        [AttributeUsage(AttributeTargets.Property)]
        public class MaxLengthAttr: Attribute
        {
            // max length cho phep
            public int Value { get; set; }
            public MaxLengthAttr(int length)
            {
                this.Value = length;
            }
        }
        // format dung dinh dang email
        [AttributeUsage(AttributeTargets.Property)]
        public class EmailFormat : Attribute
        {

        }
        // format dung dinh dang so dien thoai 
        [AttributeUsage(AttributeTargets.Property)]
        public class PhoneNumberAttr : Attribute
        {
            public Regex phoneValue = new Regex(@"^[\+]?[(]?[0-9]{2,3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$");
        }
        // format dung dinh dang mã nhân viên/mã khách hàng
        [AttributeUsage(AttributeTargets.Property)]
        public class StartWith : Attribute
        {
            public string Value { get; set; }
            public StartWith(string value)
            {
                this.Value = value;
            }
        }
        // để validate ngày thàng nhỏ hơn hienj tại
        [AttributeUsage(AttributeTargets.Property)]
        public class DateValid : Attribute
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// trang thai cua entity, mac dinh la get
        /// </summary>
        public EntityState EntityState { get; set; } = EntityState.Get;
        /// <summary>
        /// ngay tao
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// nguoi tao
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// ngay cap nhat gan nhat
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// nguoi cap nhat gan nhat
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}

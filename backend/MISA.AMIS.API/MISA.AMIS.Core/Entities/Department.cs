using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Department : BaseEntity
    {
        #region Properties
        [PrimaryKey]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// ten phong ban
        /// </summary>
        [DisplayName("Tên phòng ban")]
        [RequiredAttr]
        [Duplicated]
        public string DepartmentName { get; set; }
        /// <summary>
        /// mo ta
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}

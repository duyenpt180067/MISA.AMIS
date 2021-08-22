using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// trang thai service: true-thanh cong, false: that bai
        /// </summary>
        public bool Success { get; set; } = false;
        public string MISACode { get; set; } = string.Empty;
        public object Data { get; set; }
        //public string DevMgs { get; set; }
        public string UserMgs { get; set; } = string.Empty;
        //public string ErrorCode { get; set; } = string.Empty;
    }
}

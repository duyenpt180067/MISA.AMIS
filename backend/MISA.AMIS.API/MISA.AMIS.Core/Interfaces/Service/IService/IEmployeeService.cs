using MISA.AMIS.Core;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// phan trang (co the theo cac tieu chi)
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="EmployeeFilter">ten/ma/sdt</param>
        /// <param name="PageNumber">so thu tu trang</param>
        /// <param name="PageSize">so ban ghi tren 1 trang</param>
        /// <returns>danh sach cac ban ghi phu hop</returns>
        ServiceResult GetEmployeeFilterPaging(string EmployeeFilter, string PageNumber, string PageSize);
    }
}

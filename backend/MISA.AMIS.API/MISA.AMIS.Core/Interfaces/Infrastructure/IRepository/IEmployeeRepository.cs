using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Infrastructure.IRepository
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// phan trang (co the theo cac tieu chi )
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="EmployeeFilter">ten/ma/sdt</param>
        /// <param name="pageNumber">stt trang</param>
        /// <param name="pageSize">so ban ghi/trang</param>
        /// <returns>danh sach ban ghi phu hop</returns>
        Object GetEmployeeFilterPaging(string EmployeeFilter, int pageNumber, int pageSize);

        /// <summary>
        /// lay ma nhan vien moi
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>ma nhan vien moi</returns>
        string GetNewEmployeeCode();

        /// <summary>
        /// lay du lieu obj theo entityCode
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entityCode">string</param>
        /// <returns>entity</returns>
        Employee GetObjByCode(string entityCode);
    }
}

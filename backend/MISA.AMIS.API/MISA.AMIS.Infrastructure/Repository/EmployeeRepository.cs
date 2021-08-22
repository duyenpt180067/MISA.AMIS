using Dapper;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Interfaces.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class EmployeeRepository:DBContext<Employee>,IEmployeeRepository
    {
        /// <summary>
        /// phan trang (co the theo cac tieu chi)
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="EmployeeFilter">ten/ma/sdt</param>
        /// <param name="PageNumber">so thu tu trang</param>
        /// <param name="PageSize">so ban ghi tren 1 trang</param>
        /// <returns>danh sach cac ban ghi phu hop</returns>
        public Object GetEmployeeFilterPaging(string EmployeeFilter, int PageNumber, int PageSize)
        {
            var TotalRecord = 0;
            var TotalPage = 0;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmployeeFilter", EmployeeFilter.Trim());
            parameters.Add("PageNumber", PageNumber);
            parameters.Add("PageSize", PageSize);
            parameters.Add("TotalRecord", dbType: System.Data.DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("TotalPage", dbType: System.Data.DbType.Int32, direction: ParameterDirection.Output);
            var employees = DBConnection.Query<Employee>("Proc_GetEmployeeFilterPaging", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            TotalRecord = parameters.Get<int>("TotalRecord");
            TotalPage = parameters.Get<int>("TotalPage");
            var obj = new
            {
                TotalPage = TotalPage,
                TotalRecord = TotalRecord,
                Data = employees
            };
            return obj;
        }

        /// <summary>
        /// lay ma nhan vien moi
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>ma nhan vien moi</returns>
        public string GetNewEmployeeCode()
        {
            var newCode = DBConnection.QueryFirstOrDefault<string>("Proc_GetNewEmployeeCode", commandType: CommandType.StoredProcedure);
            return newCode;
        }
    }
}

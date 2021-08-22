using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Infrastructure.IRepository;
using MISA.AMIS.Core.Interfaces.Service;
using MISA.AMIS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository) 
        { 
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// kiem tra du lieu dau vao cua ham 
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="EmployeeFilter"></param>
        /// <param name="strPageNumber"></param>
        /// <param name="strPageSize"></param>
        /// <returns>serviceResult</returns>
        public ServiceResult GetEmployeeFilterPaging(string EmployeeFilter, string strPageNumber, string strPageSize)
        {
            if((strPageNumber == null)||(strPageSize == null))
            {
                serviceResult.Success = false;
                serviceResult.MISACode = _const.MISADataInvalid;
                serviceResult.UserMgs = Core.Properties.Resources.DataInValid;
            }
            else
            {
                var PageNumber = Int32.Parse(strPageNumber);
                var PageSize = Int32.Parse(strPageSize);
                if ((PageNumber < 0) || (PageSize < 0))
                {
                    serviceResult.Success = false;
                    serviceResult.MISACode = _const.MISADataInvalid;
                    serviceResult.UserMgs = Core.Properties.Resources.DataInValid;
                }
                else
                {
                    var listFilter = _employeeRepository.GetEmployeeFilterPaging(EmployeeFilter, PageNumber, PageSize);
                    serviceResult.Success = true;
                    serviceResult.MISACode = _const.MISACodeSuccess;
                    serviceResult.Data = listFilter;
                    serviceResult.UserMgs = Core.Properties.Resources.GetSuccess;
                }
            }

            return serviceResult;
        }
    }
}

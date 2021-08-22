using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.API.Controllers;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Consts;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Infrastructure.IRepository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
           /* serviceResult = new ServiceResult();
            _const = new MISAConst();*/
        }



        /// <summary>
        /// lay nhan vien theo id
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="Code">ma code cua object hoac name cua customerGroup</param>
        /// <returns>status code va ket qua tra ve</returns>
        [HttpGet("code/{EmployeeCode}")]
        public virtual IActionResult GetObjByProp(string Code)
        {
            try
            {
                var entity = _employeeRepository.GetObjByCode(Code);
                return Ok(entity);
            }
            catch (Exception e)
            {
                res = _const.catchException(res, e.Message, serviceResult);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// phan trang (co the theo cac tieu chi)
        /// Create: PTDuyen(31/07/2021)
        /// </summary>
        /// <param name="EmployeeFilter">ten/ma/sdt</param>
        /// <param name="PageNumber">so thu tu trang</param>
        /// <param name="PageSize">so ban ghi tren 1 trang</param>
        /// <returns>danh sach cac ban ghi phu hop</returns>
        [HttpGet("Filter")]
        public IActionResult GetEmployeeFilterPaging(string EmployeeFilter, string PageNumber, string PageSize)
        {
            try
            {
                if (EmployeeFilter == null)
                {
                    EmployeeFilter = "";
                }
                var serviceResult = _employeeService.GetEmployeeFilterPaging(EmployeeFilter, PageNumber, PageSize);
                if (serviceResult.Success == true)
                {
                    if(serviceResult.Data != null)
                    {
                        return Ok(serviceResult.Data);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception e)
            {
                serviceResult.UserMgs = AMIS.Core.Properties.Resources.ExceptionError;
                serviceResult.MISACode = _const.MISACodeErrorException;
                res.UserMgs = serviceResult.UserMgs;
                res.MISACode = serviceResult.MISACode;
                res.DevMgs = e.Message;
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// lay ma nhan vien moi
        /// Create: PTDuyen(31/07/2021)
        /// </summary>
        /// <returns>ma nhan vien moi</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var newCode = _employeeRepository.GetNewEmployeeCode();
                if(newCode != null)
                {
                    return Ok(newCode);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                serviceResult.UserMgs = AMIS.Core.Properties.Resources.ExceptionError;
                serviceResult.MISACode = _const.MISACodeErrorException;
                res.UserMgs = serviceResult.UserMgs;
                res.MISACode = serviceResult.MISACode;
                res.DevMgs = e.Message;
                return StatusCode(500, res);
            }
        }
    }
}

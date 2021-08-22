using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Consts;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Infrastructure;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase
    {
        #region Fields
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;
        protected ServiceResult serviceResult;
        protected dynamic res;
        protected MISAConst _const;
        #endregion
        public BaseController(IBaseRepository<MISAEntity> baseRepository, IBaseService<MISAEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
            serviceResult = new ServiceResult();
            _const = new MISAConst();
            res = new ExpandoObject();
        }

        #region Methods
        /// <summary>
        /// lấy tất cả các object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>statusCode và serviceResult</returns>
        [HttpGet]
        public IActionResult GetAllObj()
        {
            try
            {
                var entities = _baseRepository.GetAllObj();
                if(entities.Count > 0)
                {
                    return Ok(entities);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                res = _const.catchException(res, e.Message, serviceResult);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// lay entities theo id
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="Id">Guid</param>
        /// <returns>status code va ket qua tra ve</returns>
        [HttpGet("{Id}")]
        public IActionResult GetObjById(string Id)
        {
            try
            {
                Guid entityId;
                if(Guid.TryParse(Id, out entityId))
                {
                    var entity = _baseRepository.GetObjById(entityId);
                    if (entity != null)
                    {
                        return Ok(entity);
                    }
                    else
                    {
                        res.UserMgs = AMIS.Core.Properties.Resources.DataNotExist;
                        res.MISACode = _const.MISACodeNotExist;
                        return StatusCode(200, res);
                    }
                }
                else
                {
                    serviceResult.UserMgs = AMIS.Core.Properties.Resources.DataNotExist;
                    serviceResult.MISACode = _const.MISACodeNotExist;
                    return StatusCode(400, serviceResult);
                }
            }
            catch (Exception e)
            {
                res = _const.catchException(res, e.Message, serviceResult);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Them moi du lieu
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">truyen du lieu muon them</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPost]
        public IActionResult AddObj([FromBody] MISAEntity entity)
        {
            try
            {
                serviceResult = _baseService.AddObj(entity);
                // neu success = false
                if(serviceResult.Success == false)
                {
                    // id khong dung dinh dang
                    if (serviceResult.MISACode == _const.MISAIdInvalid)
                    {
                        return BadRequest(serviceResult);
                    }
                    // Dữ liệu không hợp lệ
                    else if (serviceResult.MISACode == _const.MISADataInvalid)
                    {
                        return Ok(serviceResult);
                    }
                    // xay ra exception
                    else
                    {
                        res.MISACode = serviceResult.MISACode;
                        res.UserMgs = serviceResult.UserMgs;
                        return StatusCode(500, res);
                    }
                }
                // neu success = true
                else 
                {
                    return StatusCode(201, serviceResult);
                }
            }
            catch (Exception e)
            {
                serviceResult.UserMgs = AMIS.Core.Properties.Resources.ExceptionError;
                serviceResult.MISACode = _const.MISACodeErrorException;
                res = _const.catchException(res, e.Message, serviceResult);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Cap nhat du lieu
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">truyen du lieu muon cap nhat</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPut]
        public IActionResult UpdateObj([FromBody] MISAEntity entity)
        {
            try
            {
                
                serviceResult = _baseService.UpdateObj(entity);

                // 1. Validate du lieu (trong hoac bi trung)
                if (serviceResult.Success == false)
                {
                    // id khong dung dinh dang
                    if (serviceResult.MISACode == _const.MISAIdInvalid)
                    {
                        return BadRequest(serviceResult);
                    }
                    // Dữ liệu không hợp lệ
                    else if(serviceResult.MISACode == _const.MISADataInvalid)
                    {
                        return Ok(serviceResult);
                    }
                    // xay ra exception
                    else
                    {
                        res.MISACode = serviceResult.MISACode;
                        res.UserMgs = serviceResult.UserMgs;
                        return StatusCode(500, res);
                    }
                }
                // 2. Sua thanh cong
                else
                {
                    serviceResult.UserMgs = AMIS.Core.Properties.Resources.PutSuccess;
                    serviceResult.Data = (int)serviceResult.Data;
                    return StatusCode(202, serviceResult);
                }

            }
            catch (Exception e)
            {
                serviceResult.UserMgs = AMIS.Core.Properties.Resources.ExceptionError;
                serviceResult.MISACode = _const.MISACodeErrorException;
                res = _const.catchException(res, e.Message, serviceResult);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Xoa du lieu theo id
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="Id">id cua entity muon xoa</param>
        /// <returns>status code va serviceResult</returns>
        [HttpDelete("{Id}")]
        public IActionResult DeleteObj(string Id)
        {
            try
            {
                serviceResult = _baseService.DeleteObj(Id);
                if (serviceResult.Success == false)
                {
                    res.MISACode = serviceResult.MISACode;
                    res.UserMgs = serviceResult.UserMgs;
                    // id khong hop le
                    if (serviceResult.MISACode == _const.MISADataInvalid)
                    {
                        return BadRequest(res);
                    } 
                    // exception
                    else if(serviceResult.MISACode == _const.MISACodeErrorException)
                    {
                        res = _const.catchException(res, serviceResult.Data, serviceResult);
                        return StatusCode(500, res);
                    }
                    // id khong ton tai trong db
                    else
                    {
                        return Ok(res);
                    }
                }
                // xoa thanh cong
                else
                {
                    var rowAffect = (int)serviceResult.Data;
                    return StatusCode(203, rowAffect);
                }
            }
            catch (Exception e)
            {
                serviceResult.UserMgs = AMIS.Core.Properties.Resources.ExceptionError;
                serviceResult.MISACode = _const.MISACodeErrorException;
                res = _const.catchException(res, e.Message, serviceResult);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Xoa du lieu theo id
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang id cua entity muon xoa</param>
        /// <returns>status code va serviceResult</returns>
        [HttpDelete("DeleteMulti")]
        public IActionResult DeleteMultiObj(List<string> listEntityId)
        {
            try
            {
                serviceResult = _baseService.DeleteMultiObj(listEntityId);
                if (serviceResult.Success == false)
                {
                    //return BadRequest(serviceResult);
                    int rowEffect;
                    // nếu dữ liệu hợp lệ
                    if (Int32.TryParse(serviceResult.Data.ToString(), out rowEffect) == false)
                    {
                        res = _const.catchException(res, serviceResult.Data, serviceResult);
                        return StatusCode(400, res);
                    }
                    // nếu dữ liệu hợp lệ nhưng việc xóa nhiều bị lỗi
                    else return StatusCode(200, serviceResult);
                }
                else
                {
                    var rowAffect = (int)serviceResult.Data;    
                    return StatusCode(203, rowAffect);
                }
            }
            catch (Exception e)
            {
                serviceResult.UserMgs = AMIS.Core.Properties.Resources.ExceptionError;
                serviceResult.MISACode = _const.MISACodeErrorException;
                res = _const.catchException(res, e.Message, serviceResult);
                return StatusCode(500, res);
            }
        }
        #endregion
    }
}

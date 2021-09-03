using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<MISAEntity> : ControllerBase
    {
        #region DECLARE
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;
        ServiceResult _serviceResult;
        #endregion

        #region Constructor
        public BaseEntityController(IBaseService<MISAEntity> baseService, IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
            _serviceResult = new ServiceResult();
        }
        #endregion

        #region method GET
        /// <summary>
        /// Hàm lấy tất cả thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// CreateBy: Ngọc 12/8/2021
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            try
            {
                var entities = _baseRepository.GetAll();
                if (!entities.Any())// == entities.Count() == 0
                {
                    return NoContent();
                }
                return Ok(entities);
            }
            catch (Exception ex)
            {
                var newObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Error_Message_UserVN,
                    errorCode = "misa-001",
                    moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return BadRequest(newObj);
            }
        }

        /// <summary>
        /// Hàm lấy tất cả thực thể theo id
        /// </summary>
        /// <returns>Thực thể</returns>
        /// CreateBy: Ngọc 12/8/2021
        [HttpGet("{entityId}")]
        public virtual IActionResult GetById(Guid entityId)
        {
            try
            {
                var entity = _baseRepository.GetById(entityId);
                if (entity == null)
                {
                    return NoContent();
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                var newObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Error_Message_UserVN,
                    errorCode = "misa-001",
                    moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return BadRequest(newObj);
            }
        }

        #endregion

        #region method POST
        /// <summary>
        /// Hàm thêm khách hàng mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreateBy: Ngọc 12/8/2021
        [HttpPost]
        public virtual IActionResult InsertEntity(MISAEntity entity)
        {
            try
            {
                var serviceResult = _baseService.Add(entity);

                // Trả về cho client
                if (serviceResult.isValid == true)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    var errorObj = new
                    {                  
                        userMsg = serviceResult.Message,
                        errorCode = "misa-001",
                        moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(400, errorObj);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Core.Properties.ResourceVN.Error_Message_UserVN,
                    errorCode = "misa-001",
                    moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }

        }
        #endregion

        #region method PUT
        /// <summary>
        /// Hàm cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreateBy: Ngọc 12/8/2021
        [HttpPut("{entityId}")]
        public virtual IActionResult UpdateEntity(Guid entityId, MISAEntity entity)
        {
            try
            {
                var serviceResult = _baseService.Edit(entity, entityId);

                // Trả về cho client
                if (serviceResult.isValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errorobj = new
                    {
                        userMsg = serviceResult.Message,
                        errorCode = "misa-001",
                        moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "",
                    };
                    return BadRequest(errorobj);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Error_Message_UserVN,
                    errorCode = "misa-001",
                    moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }
        }
        #endregion

        #region method DELETE
        /// <summary>
        /// Hàm xóa khách hàng
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreateBy: Ngọc 12/8/2021
        [HttpDelete("{entityId}")]
        public IActionResult DeleteEntity(Guid entityId)
        {
            try
            {
                var rowEffects = _baseRepository.Delete(entityId);
                return Ok(rowEffects);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Error_Message_UserVN,
                };
                return StatusCode(500, errorObj);
            }
        }

        /// <summary>
        /// Hàm xóa nhiều dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created By: Ngọc 15/8/2021
        [HttpPost("Multiple/Delete")]
        public IActionResult MultiDelete(List<Guid> ids)
        {
            try
            {
                var serviceResult = _baseRepository.MultiDelete(ids);
                if (serviceResult.isValid)
                {
                    return Ok(serviceResult.Data);
                }
                else
                {
                    var errorObj = new
                    {
                        userMsg = serviceResult.Message,
                    };
                return BadRequest(errorObj);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Core.Properties.ResourceVN.Error_Message_UserVN,
                    errorCode = "misa-001",
                    moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }
        }
    
        #endregion
    }
}

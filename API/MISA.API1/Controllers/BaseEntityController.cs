using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MISA.API1.Controllers
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
        /// CreateBy: Dvanh 12/8/2021
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            try
            {

                _serviceResult = _baseService.GetAll();
                List<MISAEntity> entities = null;
                if (_serviceResult.IsValid == true)
                {
                    entities = (List<MISAEntity>)_serviceResult.Data;
                }
                
                // kiểm tra rỗng
                if (!entities.Any())
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
                    userMsg = Properties.Resources.Error_Message_UserVN,
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
        /// CreateBy: Dvanh 12/8/2021
        [HttpGet("{entityId}")]
        public virtual IActionResult GetById(Guid entityId)
        {
            try
            {
                _serviceResult = _baseService.GetById(entityId);
                
                var entity = (MISAEntity)_serviceResult.Data;
                    
                if(entity == null)
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
                    userMsg = Properties.Resources.Error_Message_UserVN,
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
        /// CreateBy: Dvanh 12/8/2021
        [HttpPost]
        public virtual IActionResult InsertEntity(MISAEntity entity)
        {
            try
            {
                var serviceResult = _baseService.Add(entity);

                // Trả về cho client
                if (serviceResult.IsValid == true)
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
                    userMsg = Properties.Resources.Error_Message_UserVN,
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
        /// CreateBy: Dvanh 12/8/2021
        [HttpPut("{entityId}")]
        public virtual IActionResult UpdateEntity(Guid entityId, MISAEntity entity)
        {
            try
            {
                _serviceResult = _baseService.Edit(entity, entityId);

                // Trả về cho client
                if (_serviceResult.IsValid == true)
                {
                    return StatusCode(200, _serviceResult.Data);
                }
                else
                {
                    var errorobj = new
                    {
                        userMsg = _serviceResult.Message,
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
                    userMsg = Properties.Resources.Error_Message_UserVN,
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
        /// CreateBy: Dvanh 12/8/2021
        [HttpDelete("{entityId}")]
        public IActionResult DeleteEntity(Guid entityId)
        {
            try
            {
                var rowEffects = _baseRepository.Delete(entityId);
                // trả về số dòng bị thay đổi
                return Ok(rowEffects);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Error_Message_UserVN,
                };
                return StatusCode(500, errorObj);
            }
        }

        

        #endregion
    }
}

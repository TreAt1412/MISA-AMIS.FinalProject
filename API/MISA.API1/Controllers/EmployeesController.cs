using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.API1.Controllers
{   
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLARE
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        IBaseRepository<Employee> _baseRepository;
        IBaseService<Employee> _baseService;
        ServiceResult _serviceResult;
        #endregion

        #region Constructor
        public EmployeesController(IBaseService<Employee> baseService, IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(baseService, baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _serviceResult = new ServiceResult();
        }
        #endregion




        

        #region Methods
        /// <summary>
        /// Phân trang nhân viên
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchContent"></param>
        /// <param name="positionId"></param>
        /// <param name="departmentId"></param>
        /// <returns>Danh sách nhân viên</returns>
        /// Created By: Dvanh 19/8/2021
        [HttpGet("Paging")]
        public IActionResult Pagination(int? pageSize, int? pageNumber, string searchContent)
        {
            try
            {
                var pagingResult = _employeeRepository.Pagination(pageSize, pageNumber, searchContent);
               
                return Ok(pagingResult);
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


        /// <summary>
        /// lấy tất cả dữ bản ghi của nhân viên
        /// </summary>
        /// <returns></returns>
        /// createdby Dvanh 1/9/2021
        [HttpGet]
        public override IActionResult  GetAll()
        {
            try
            {
                var employees = _employeeRepository.GetAll();
                // Trả về cho client
                if (employees.Count() == 0)
                {
                    return StatusCode(204);
                }
                return Ok(employees);
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

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: Dvanh 19/8/2021
        [HttpGet("NewEmployeeCode")]
        public IActionResult getNewCode()
        {
            try
            {
                var newEmployeeCode = _employeeRepository.GetNewCode();
                // Trả về cho client
                return Ok(newEmployeeCode);
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
        /// <summary>
        /// thêm 1 bản ghi nhân viên
        /// </summary>
        /// <param name="entity">nhân viên cần thêm</param>
        /// <returns></returns>
        /// createdby dvanh 31/8/2021
        public override IActionResult InsertEntity(Employee entity)
        {
            try
            {
                var serviceResult = _employeeService.Add(entity);

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
        /// <summary>
        /// sửa thông tin nhân viên
        /// </summary>
        /// <param name="entityId">id của nhân viên cần sửa</param>
        /// <param name="entity"> thông tin nhân viên cần sửa</param>
        /// <returns></returns>
        /// created by dvanh 31/8/2021
        public override IActionResult UpdateEntity(Guid entityId, Employee entity)
        {
            try
            {
                var serviceResult = _employeeService.Edit(entity, entityId);

                // Trả về cho client
                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
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

        /// <summary>
        /// Trả về file excel
        /// </summary>
        /// <returns></returns>
        /// created by dvanh 1/9/2021
        [HttpGet("Excel")]
        public IActionResult Export(string searchContent)
        {
            try
            {
                _serviceResult = _employeeService.Export(searchContent);
                MemoryStream stream = (MemoryStream)_serviceResult.Data;
                string excelName = "employee.xlsx";
                return File(stream, "apllication/vnd.openxmlformats-officedocument.speadsheetml.sheet", excelName);
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
    }
}

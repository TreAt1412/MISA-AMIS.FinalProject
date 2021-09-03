using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseEntityController<Department>
    {
        #region DECLARE
        //IDepartmentService _departmentService;
        IBaseRepository<Department> _baseRepository;
        IBaseService<Department> _baseService;
        #endregion

        #region Constructor
        public DepartmentsController(IBaseService<Department> baseService, IBaseRepository<Department> baseRepository) : base(baseService, baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }
        #endregion
    }
}

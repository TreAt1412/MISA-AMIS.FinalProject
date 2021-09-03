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

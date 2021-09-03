using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        //ICustomerRepository _customerRepository;
        ServiceResult _serviceResult;

        public DepartmentService(IBaseRepository<Department> baseRepository) : base(baseRepository)
        {
            _serviceResult = new ServiceResult();
            //_customerRepository = customerRepository;
        }

    }
}

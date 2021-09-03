using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <returns>service Result: kết quả xử lý nghiệp vụ</returns>
        /// createdBy: Dvanh 1/9/2021
        ServiceResult Export(string searchContent);
    }
}

using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;
        IDepartmentRepository _departmentRepository;
        public EmployeeService(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            _serviceResult = new ServiceResult();
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;

        }
        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">nhân viên</param>
        /// <returns>service result sử lý nghiệp vụ thêm mới nhân viên</returns>
        /// createdby Dvanh 1/9/2021
        public override ServiceResult Add(Employee employee)
        {
            try
            {
       
                var propValue = employee.GetType().GetProperty("EmployeeCode").GetValue(employee).ToString().Trim();

                if (string.IsNullOrEmpty(propValue))
                {
                    _serviceResult.Message = Properties.ResourceVN.Empty_EmployeeCode;
                    _serviceResult.IsValid = false;
                    return _serviceResult;
                }

                    
                return base.Add(employee);
            }
            catch(Exception)
            {
                _serviceResult.IsValid = false;
                _serviceResult.Message = Properties.ResourceVN.Error_Message_UserVN;
                return _serviceResult;
            }               
        }
        /// <summary>
        /// sửa nhân viên
        /// </summary>
        /// <param name="entity">nhân viên cần sửa</param>
        /// <param name="entityId">id của nhân viên</param>
        /// <returns>service result xử lý nghiệp vụ them</returns>
        /// createdby dvanh 1/9/2021
        public override ServiceResult Edit(Employee entity, Guid entityId)
        {
            try
            {

                return base.Edit(entity, entityId);
            }
            catch (Exception)
            {
                _serviceResult.IsValid = false;
                _serviceResult.Message = Properties.ResourceVN.Error_Message_UserVN;
                return _serviceResult;
            }

        }
        /// <summary>
        /// Xuất dữ liệu ra file exel
        /// </summary>
        /// <returns> service result xử lý nghiệp vụ xuất dữ liệu</returns>
        /// createdBy dvanh 1/9/2021
        public ServiceResult Export(string searchContent)
        {
            List<Employee> ds = null;
            if (searchContent == "" || searchContent == null)
            {
                ds = _employeeRepository.GetAll();
            }
            else
            {
                ds = _employeeRepository.Pagination(null, null, searchContent).Entities;
            }
            
            var stream = new MemoryStream();
      
            
            using (var excel = new ExcelPackage(stream))
            {

                ExcelWorksheet ws = excel.Workbook.Worksheets.Add("sheet1");
                ws.Cells["A1"].Value = "STT";
                ws.Cells["B1"].Value = "Mã nhân viên";
                ws.Cells["C1"].Value = "Tên nhân viên";
                ws.Cells["D1"].Value = "Giới tính";
                ws.Cells["E1"].Value = "Ngày sinh";
                ws.Cells["F1"].Value = "Chức danh";
                ws.Cells["G1"].Value = "Tên đơn vị";
                ws.Cells["H1"].Value = "Số tài khoản";
                ws.Cells["I1"].Value = "Tên ngân hàng";

                ws.Cells["A1:I1"].Style.Font.Bold= true;
                ws.Cells["A1:I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#B7DEE8"));

                ws.DefaultColWidth = 20;
                ws.Column(7).Width = 30;

                ws.Cells["A1:I" + (1 + ds.Count()).ToString()].AutoFitColumns();
                for (int i = 0; i < ds.Count(); i++)
                {
                    ws.Cells["A" + (i + 2).ToString()].Value = i + 1;
                    ws.Cells["B" + (i + 2).ToString()].Value = ds[i].EmployeeCode;
                    ws.Cells["C" + (i + 2).ToString()].Value = ds[i].EmployeeName;
                    ws.Cells["D" + (i + 2).ToString()].Value = getGenderName(ds[i].Gender);

                    if(ds[i].DateOfBirth != null)
                    {
                        DateTime dob = (DateTime)ds[i].DateOfBirth;
                        ws.Cells["E" + (i + 2).ToString()].Value = dob.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        ws.Cells["E" + (i + 2).ToString()].Value = ds[i].DateOfBirth;
                    }
                    
                    ws.Cells["F" + (i + 2).ToString()].Value = ds[i].Role;
                    ws.Cells["G" + (i + 2).ToString()].Value = getDepartmentName(ds[i].DepartmentId);
                    ws.Cells["H" + (i + 2).ToString()].Value = ds[i].BankAccount;
                    ws.Cells["I" + (i + 2).ToString()].Value = ds[i].BankName;
                }
                excel.Save();
            }
            stream.Position = 0;
            _serviceResult.Data =  stream;
            return _serviceResult ;
        }
        
        /// <summary>
        /// Trả về tên tương ứng với department id
        /// </summary>
        /// <param name="departmentID">id của đơn vị</param>
        /// <returns></returns>
        /// createdby Dvanh 1/9/2021
        public string getDepartmentName(Guid departmentID)
        {
            var listDepartment = _departmentRepository.GetAll();
            foreach(Department department in listDepartment)
            {
                if(department.DepartmentId == departmentID)
                {
                    return department.DepartmentName;
                }
            }
            return "";

               
        }
        /// <summary>
        /// trả về tên tương ứng với mã số giới tính
        /// </summary>
        /// <param name="genderNumber"> mã số giới tính</param>
        /// <returns></returns>
        /// createdBy dvanh 1/9/2021
        public string getGenderName(int? genderNumber)
        {
            if(genderNumber == null)
            {
                return "";
            }
            else
            {
                List<string> genderList = new List<string>() { "Nam", "Nữ", "Khác" };
                return genderList[(int)genderNumber];
            }
        }
    }

}

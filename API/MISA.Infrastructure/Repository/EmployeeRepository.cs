using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace MISA.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region DECLARE
        IDbConnection _dbConnection;
        //public readonly string _connectionString;
        PagingResult<Employee> _pagingResult;
        private readonly IHostingEnvironment _hostingEnvironment;
       
        #endregion

        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
            //_connectionString = configuration.GetConnectionString("LocalDatabase");
            _pagingResult = new PagingResult<Employee>();
           
        }
        #endregion

        #region Methods
        /// <summary>
        /// Phân trang nhân viên
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchContent"></param>
        /// <returns>Danh sách nhân viên</returns>
        /// Created By: Dvanh 19/8/2021
        public PagingResult<Employee> Pagination(int? pageSize, int? pageNumber, string searchContent)
        {
            _dbConnection = new MySqlConnection(_connectionString);

            DynamicParameters parameters = new DynamicParameters();

            var sqlCommand = $"SELECT * FROM Employee";
        

            //Thay đổi query nếu có lọc theo từ khóa
            if (!string.IsNullOrEmpty(searchContent))
            {
                
        
                parameters.Add("@searchContent", $"%{searchContent}%");
                sqlCommand += "  Where  (EmployeeCode LIKE @searchContent OR EmployeeName LIKE @searchContent OR PhoneNumber LIKE @searchContent) ";
            }


            ///  phân trang
            if (pageNumber != null && pageSize != null)
            {
                parameters.Add("@limit", pageSize);
                parameters.Add("@offset", (pageNumber - 1) * pageSize);
                sqlCommand += $" LIMIT @limit OFFSET @offset ";
            }

            var employees = _dbConnection.Query<Employee>(sqlCommand, param: parameters).ToList();
            _pagingResult.Entities = employees;
            _pagingResult.TotalRecord = employees.Count();

            return _pagingResult;
        }
        /// <summary>
        /// trả về mã nhân viên mới
        /// </summary>
        /// <returns> string </returns>
        /// createdBy dvanh 29/8/2021
        public string GetNewCode()
        {
            _dbConnection = new MySqlConnection(_connectionString);
            // lấy mã nhân viên có độ dài lớn nhất và là số lớn nhất
            string sqlCommand = "SELECT EmployeeCode FROM Employee ORDER BY  LENGTH(EmployeeCode) DESC, EmployeeCode DESC";
            var employeeCode = _dbConnection.QueryFirstOrDefault<string>(sqlCommand);

            int currentCodeMax = 0;
         
            int codeValue = int.Parse(employeeCode.ToString().Split("-")[1]);
            if (currentCodeMax < codeValue)
            {
                currentCodeMax = codeValue;
            }

            string newEmployeeCode = "NV-" + (currentCodeMax + 1);        
            return newEmployeeCode;
        }
        /// <summary>
        /// trả về file excel nhân viên
        /// </summary>
        /// <returns></returns>
        /// createdby Dvanh 01/09/2021
        public MemoryStream Export()
        {

            return new MemoryStream();
        }

        
        #endregion
    }
}

using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MISARequired ("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        [MISARequired ("Họ và tên")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }


        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số cmt
        /// </summary>

        public string IdentityNumber { get; set; }

        /// <summary>
        /// Nơi cấp cmt
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Ngày cấp cmt
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        
        [MISAEmail]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>


        public string FixedPhoneNumber { get; set; }


        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// chức vụ của nhân viên
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string BankBranch { get; set; }  
 
        #endregion
    }
}

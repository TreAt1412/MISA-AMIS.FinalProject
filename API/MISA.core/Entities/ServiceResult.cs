using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// có thành công hay không
        /// </summary>
        public bool IsValid { get; set; } = true;
        /// <summary>
        /// dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// tin nhắn
        /// </summary>
        public string Message { get; set; }
       
    }
}

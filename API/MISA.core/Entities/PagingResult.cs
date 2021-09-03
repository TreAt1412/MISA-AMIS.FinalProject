using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class PagingResult<MISAEntity>
    {
        /// <summary>
        /// tổng số lượng bản ghi
        /// </summary>
        public int TotalRecord { get; set; }
       
        /// <summary>
        /// danh sách thực thể trả về khi phân trang
        /// </summary>
        public List<MISAEntity> Entities { get; set; } = new List<MISAEntity>();
    }
}

using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        #region Methods
        /// <summary>
        /// Nghiệp vụ thêm mới đối tượng
        /// Author: Dvanh 13/8/2021
        /// </summary>
        /// <param name="entity">đối tượng cần thêm</param>
        /// <returns>ServiceResult: Kết quả xử lý nghiệp vụ</returns>
        ServiceResult Add(MISAEntity entity);


        /// <summary>
        /// Lấy tất cả bản ghi
        /// Author: Dvanh 13/8/2021
        /// </summary>
        /// <returns>ServiceResult: Kết quả xử lý nghiệp vụ</returns>
        /// createdby Dvanh 31/8/2021
        ServiceResult GetAll();

        /// <summary>
        /// Lấy bản ghi theo id
        /// Author: Dvanh 13/8/2021
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>ServiceResult: Kết quả xử lý nghiệp vụ</returns>
        /// createdby Dvanh 31/8/2021
        ServiceResult GetById(Guid entityId);

        /// <summary>
        /// sửa thông tin đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>ServiceResult: Kết quả xử lý nghiệp vụ</returns>
        /// createdby Dvanh 31/8/2021
        ServiceResult Edit(MISAEntity entity, Guid entityId);
        #endregion

    }
}

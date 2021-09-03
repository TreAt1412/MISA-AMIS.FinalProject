using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Core.Entities;

namespace MISA.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity>
    {   
        /// <summary>
        /// Hàm lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created By: Dvanh 15/8/2021
        List<MISAEntity> GetAll();

        /// <summary>
        /// Hàm lấy dữ liệu theo id
        /// </summary>
        /// <returns></returns>
        /// Created By: Dvanh 15/8/2021
        MISAEntity GetById(Guid entityId);

        /// <summary>
        /// Hàm thêm mới dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created By: Dvanh 15/8/2021
        int Add(MISAEntity entity);

        /// <summary>
        /// Hàm sửa dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created By: Dvanh 15/8/2021
        int Edit(MISAEntity entity, Guid entityId);

        /// <summary>
        /// Hàm xóa dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created By: Dvanh 15/8/2021
        int Delete(Guid entityId);

    

        /// <summary>
        /// Hàm check trùng mã
        /// </summary>
        /// <returns></returns>
        /// Created By: Dvanh 18/8/2021
        bool checkedCodeExist(string entityCode, Guid entityId);
    }
}

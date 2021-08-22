using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IBaseService<MISAEntity>
    {
        #region Methods
        /// <summary>
        /// them moi duw lieu cua entity
        /// Create: PTDuyen(28/07/2021)
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>so luong hang bi sua doi</returns>
        ServiceResult AddObj(MISAEntity entity);

        /// <summary>
        /// sua du lieu cua entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>so luong ban ghi bi sua</returns>
        ServiceResult UpdateObj(MISAEntity entity);

        /// <summary>
        /// xoa du lieu theo id
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// Create: PTDuyen(28/07/2021)
        /// <returns>so ban ghi bi xoa</returns>
        ServiceResult DeleteObj(string entityId);

        /// <summary>
        /// xoa nhieu ban gi theo mang id
        /// Create: PTDuyen(03/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang cac id truyen vao</param>
        /// <returns>so ban ghi bi xoa</returns>
        ServiceResult DeleteMultiObj(List<String> listEntityId);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<MISAEntity>
    {
        #region Methods
        /// <summary>
        /// lay tat ca cac object
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <returns>Danh sach object</returns>
        List<MISAEntity> GetAllObj();

        /// <summary>
        /// lay du lieu object theo id
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// <returns>object</returns>
        MISAEntity GetObjById(Guid entityId);

        /// <summary>
        /// them moi duw lieu cua object
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>so luong hang bi sua doi</returns>
        string AddObj(MISAEntity entity);

        /// <summary>
        /// sua du lieu cua object
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>so luong ban ghi bi sua</returns>
        string UpdateObj(MISAEntity entity);

        /// <summary>
        /// xoa du lieu theo id
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// Create: PTDuyen(17/08/2021)
        /// <returns>so ban ghi bi xoa</returns>
        string DeleteObj(Guid entityId);

        /// <summary>
        /// xoa nhieu ban gi theo mang id
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang cac id truyen vao</param>
        /// <returns>so ban ghi bi xoa</returns>
        string DeleteMultiObj(List<Guid> listEntityId);

        /// <summary>
        /// lay du lieu theo mot property nao do
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity">entity truyen vao</param>
        /// <param name="property">property truyen vao</param>
        /// <returns>entity</returns>
        MISAEntity GetObjByProperty(MISAEntity entity, PropertyInfo property);
        #endregion
    }
}

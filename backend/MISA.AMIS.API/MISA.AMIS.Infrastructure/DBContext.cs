using Dapper;
using MISA.AMIS.Core.Consts;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure
{
    /// <summary>
    /// tuong tac voi databse
    /// Create: PTDuyen(28/07/2021)
    /// </summary>
    public class DBContext<MISAEntity>:IBaseRepository<MISAEntity>, IDisposable where MISAEntity : BaseEntity
    {
        #region Fields
        protected IDbConnection DBConnection;
        protected string _connectionString;
        #endregion
        public DBContext()
        {
            //thông tin ket noi Db
            _connectionString = "" 
                                + "Host = 47.241.69.179;" 
                                + "Port = 3306;" 
                                + "Database = Web06.TEST.MF917.PTDuyen;"
                                + "User Id = dev;" + "Password = 12345678";
            //Khoi tao ket noi
            DBConnection = new MySqlConnection(_connectionString);
        }

        #region Methods
        /// <summary>
        /// lay tat ca cac object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>Danh sach object</returns>
        public List<MISAEntity> GetAllObj()
        {
            //lay ten class entity
            var className = typeof(MISAEntity).Name;
            var entities = DBConnection.Query<MISAEntity>($"Proc_Get{className}", commandType: CommandType.StoredProcedure).ToList();
            return entities;
        }


        /// <summary>
        /// lay du lieu object theo id
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// <returns>object</returns>
        public MISAEntity GetObjById(Guid entityId)
        {
            DynamicParameters parameters = new DynamicParameters();
            //lay ten class entity
            var className = typeof(MISAEntity).Name;
            parameters.Add($"{className}Id", entityId);
            var entity = DBConnection.QueryFirstOrDefault<MISAEntity>($"Proc_Get{className}ById", param: parameters, commandType: CommandType.StoredProcedure);
            return entity;
        }

        /// <summary>
        /// them moi duw lieu cua object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>so luong hang bi sua doi</returns>
        public string AddObj(MISAEntity entity)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    //lay ten class entity
                    var className = typeof(MISAEntity).Name;
                    rowEffects = DBConnection.Execute($"Proc_Post{className}", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    rowEffects = 0;
                    return e.Message;
                }

            }; 
        }

        /// <summary>
        /// sua du lieu cua object
        /// create:PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>so luong ban ghi bi sua</returns>
        public string UpdateObj(MISAEntity entity)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using(var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    var className = typeof(MISAEntity).Name;
                    rowEffects = DBConnection.Execute($"Proc_Update{className}", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    rowEffects = 0;
                    return e.Message;
                }
            }
        }
        /// <summary>
        /// xoa du lieu theo id
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// Create: PTDuyen(18/08/2021)
        /// <returns>so ban ghi bi xoa</returns>
        public string DeleteObj(Guid entityId)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using(var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    //lay ten class entity
                    var className = typeof(MISAEntity).Name;
                    parameter.Add($"{className}Id", entityId);
                    rowEffects += DBConnection.Execute($"Proc_Delete{className}ById", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    rowEffects = 0;
                    return e.Message;
                }
            }
        }

        /// <summary>
        /// xoa nhieu ban ghi theo list id truyen vao
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="listEntityId"></param>
        /// <returns>so ban ghi bi xoa</returns>
        public string DeleteMultiObj(List<Guid> listEntityId)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using(var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    //lay ten class entity
                    var className = typeof(MISAEntity).Name;
                    foreach (var entityId in listEntityId)
                    {
                        //thêm tham số 
                        DynamicParameters parameter = new DynamicParameters();
                        parameter.Add($"{className}Id", entityId);
                        //thực hiện xóa
                        rowEffects = DBConnection.Execute($"Proc_Delete{className}ById", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
                    }
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    rowEffects = 0;
                    return e.Message;
                }
            }
        }

        /// <summary>
        /// xoa nhieu ban gi theo mang id
        /// Create: PTDuyen(03/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang cac id truyen vao</param>
        /// <returns>so ban ghi bi xoa</returns>
        public MISAEntity GetObjByCode(string entityCode)
        {
            //lay ten class entity
            var className = typeof(MISAEntity).Name;
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add($"{className}Code", entityCode);
            var entity = DBConnection.QueryFirstOrDefault<MISAEntity>($"Proc_Get{className}ByCode", param: parameter, commandType: CommandType.StoredProcedure);
            return entity;
        }

        /// <summary>
        /// lay du lieu obj theo property nao do tru chinh entity truyen vao
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">entity truyen vao</param>
        /// <param name="property">property truyen vao</param>
        /// <returns>entity</returns>
        public MISAEntity GetObjByProperty(MISAEntity entity, PropertyInfo property)
        {
            var sqlCommand = string.Empty;
            // lấy tên class
            var className = typeof(MISAEntity).Name;
            // lấy value property chứa Id của class
            var entityId = entity.GetType().GetProperty($"{className}Id").GetValue(entity);
            // tham số động
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PropertyValue", property.GetValue(entity));
            // nếu là thêm mới thì tìm entity có property giống property truyền vào
            if (entity.EntityState == EntityState.Add)
            {
                if (className == "Employee")
                {
                    sqlCommand = $"SELECT * FROM View_{className} WHERE {property.Name} = @PropertyValue";
                }
                else
                {
                    sqlCommand = $"SELECT * FROM {className} WHERE {property.Name} = @PropertyValue";
                }
            }
            // nếu update thì tìm entity có property giống property truyền vào trừ chính nó ra
            else if (entity.EntityState == EntityState.Update)
            {
                if (className=="Employee")
                {
                    parameter.Add("@EntityId", entityId);
                    sqlCommand = $"SELECT * FROM View_{className} WHERE {property.Name} = @PropertyValue AND {className}Id <> @EntityId";
                }
                else
                {
                    parameter.Add("@EntityId", entityId);
                    sqlCommand = $"SELECT * FROM {className} WHERE {property.Name} = @PropertyValue AND {className}Id <> @EntityId";
                }
            }
            // nếu không phải thêm mới hay update thì return null
            else return null;

            var entityGet = DBConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand,param: parameter, commandType: CommandType.Text);
            return entityGet;
        }

        /// <summary>
        /// dong ket noi khi ket noi dang mo ma khong duoc su dung
        /// create: PTDuyen(18/08/2021)
        /// </summary>
        public void Dispose()
        {
            if(DBConnection.State == ConnectionState.Open)
            {
                DBConnection.Close();
            }
        }
        #endregion
    }
}

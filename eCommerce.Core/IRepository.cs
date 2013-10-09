using System.Linq;
using System.Runtime.Serialization;

namespace eCommerce.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>TO-DO: consider to change to interface in order to avoid to inheriting from class</remarks>
    [DataContract]
    public abstract class EntityBase
    {
        [DataMember]
        public virtual long Id { get; set; }
    }

    /// <summary>
    /// CRUD of entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys">Primary keys</param>
        /// <returns></returns>
        T GetByKeys(params object[] keys);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IQueryable<T> Table { get; }
    }
}

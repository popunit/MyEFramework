using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    public abstract class EntityBase
    {
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

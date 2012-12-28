using eCommerce.Core;
using eCommerce.Extensions.Data.MongoRepository;
using eCommerce.Extensions.Data.MongoRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.NoSql.Repositories
{
    public class MongoDbRepository<T> : eCommerce.Core.IRepository<T>
        where T : EntityBase, IEntity, new()
    {
        private MongoRepository<T> repository;
        public MongoDbRepository()
        {
            repository = new MongoRepository<T>();
        }

        public T GetByKeys(params object[] keys)
        {
            if(null == keys || keys.Count() != 1)
                return null; // only support objectId
            return this.repository.GetById(keys[0].ToString());
        }

        public bool Insert(T entity)
        {
            try
            {
                this.repository.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                this.repository.Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                this.repository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<T> Table
        {
            get { return this.repository.All(); }
        }

        /// <summary>
        /// this function is only available in concrete repository but interface because
        /// the clear function should not be called in production environment. Clear a
        /// repository is not reasonable if it isn't running on a test environment. 
        /// </summary>
        public void Clear()
        {
            repository.DeleteAll();
        }
    }
}

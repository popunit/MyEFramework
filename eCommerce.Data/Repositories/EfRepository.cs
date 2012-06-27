using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Core.Infrastructure.NoAOP;

namespace eCommerce.Data.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly IDatabase db;
        private IDbSet<T> store;

        public EfRepository(IDatabase db)
        {
            this.db = db;
        }

        public T GetByKeys(params object[] keys)
        {
            return store.Find(keys);
        }

        public void Insert(T entity)
        {
            //AspectF.Define.MustBeNonNull(entity)
            //    .
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Table
        {
            get { throw new NotImplementedException(); }
        }
    }
}

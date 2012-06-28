using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Core;
using eCommerce.Core.Infrastructure.NoAOP;

namespace eCommerce.Data.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : EntityBase, new()
    {
        private readonly IDatabase db;
        private IDbSet<T> store;
        private IDbSet<T> Store
        {
            get 
            {
                if (null == store)
                    store = db.Store<T>();
                return store;
            }
        }

        public EfRepository(IDatabase db)
        {
            this.db = db;
        }

        public T GetByKeys(params object[] keys)
        {
            //return store.Find(keys);
            return AspectF.Define.MustBeNonNull(keys).
                Return<T>(() => 
                {
                    return store.Find(keys) ?? db.GetEmptyEntity<T>();
                });
        }

        public void Insert(T entity)
        {
            AspectF.Define.MustBeNonNull(entity).
                HandleException(typeof(DbEntityValidationException)).
                Do(() =>
                {
                    var state = this.db.GetEntityState<T>(entity);
                    this.Store.Add(entity);
                    this.db.SaveChanges();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>Reference to http://blogs.msdn.com/b/adonet/archive/2011/01/29/using-dbcontext-in-ef-feature-ctp5-part-4-add-attach-and-entity-states.aspx
        /// </remarks>
        public void Update(T entity)
        {
            AspectF.Define.MustBeNonNull(entity).
                HandleException(typeof(DbEntityValidationException)).
                Do(() => 
                {
                    var state = this.db.GetEntityState<T>(entity);
                    if (state == EntityState.Detached)
                    {
                        var existedObject = 
                            db.Store<T>().Local.Where(x => x.Id == entity.Id).FirstOrDefault(); // check if has the same object in the context
                        if (null != existedObject)
                        {
                            // copy value of properties to attched object
                            Mapper.Map(entity, existedObject);
                        }
                        else
                        {
                            this.db.SetEntityState<T>(entity, EntityState.Modified);
                        }
                    }

                    this.db.SaveChanges();
                });
        }

        public void Delete(T entity)
        {
            AspectF.Define.MustBeNonNull(entity).
                HandleException(typeof(DbEntityValidationException)).
                Do(() => 
                {
                    this.Store.Remove(entity);
                    this.db.SaveChanges();
                });
        }

        public IQueryable<T> Table
        {
            get { return this.Store; }
        }
    }
}

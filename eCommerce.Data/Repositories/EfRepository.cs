using AutoMapper;
using eCommerce.Core;
using eCommerce.Core.Infrastructure.NoAOP;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace eCommerce.Data.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : EntityBase, new()
    {
        private readonly IDatabase _db;
        //private IDbSet<T> _store;

        /// <summary>
        /// Represent DbSet
        /// </summary>
        private IDbSet<T> Store
        {
            get 
            {
                return _db.Store<T>();
            }
        }

        public EfRepository(IDatabase db)
        {
            this._db = db;
        }

        public T GetByKeys(params object[] keys)
        {
            //return store.Find(keys);
            return AspectF.Define.MustBeNonNull(keys).
                Return<T>(() => this.Store.Find(keys) ?? _db.GetEmptyEntity<T>());
        }

        /// <summary>
        /// Insert an item into database
        /// </summary>
        /// <param name="entity">An entity, its state should be detached, if not, please notice if the entity should be added</param>
        /// <returns></returns>
        public bool Insert(T entity)
        {
            return AspectF.Define.MustBeNonNull(entity).
                HandleException(typeof(DbEntityValidationException))
                .Return<bool>(() =>
                {
                    //var state = this._db.GetEntityState<T>(entity);
                    this.Store.Add(entity);
                    // commit transaction
                    return this._db.SaveChanges() > 0; // not successful or no item needs to be saved
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>Reference to http://blogs.msdn.com/b/adonet/archive/2011/01/29/using-dbcontext-in-ef-feature-ctp5-part-4-add-attach-and-entity-states.aspx
        /// </remarks>
        public bool Update(T entity)
        {
            return AspectF.Define.MustBeNonNull(entity).
                HandleException(typeof(DbEntityValidationException)).
                Return<bool>(() => 
                {
                    var state = this._db.GetEntityState<T>(entity);
                    if (state == EntityState.Detached)
                    {
                        //var existedObject = 
                        //    _db.Store<T>().Local.Where(x => x.Id == entity.Id).FirstOrDefault(); // check if has the same object in the context
                        //if (null != existedObject)
                        //{
                        //    // copy value of properties to attched object
                        //    Mapper.Map(entity, existedObject);
                        //}
                        //else
                        //{
                        //    this._db.SetEntityState<T>(entity, EntityState.Modified);
                        //}
                        if (entity.Id == 0)
                            return false;
                        this._db.SetEntityState<T>(entity, EntityState.Modified);
                    }

                    // commit transaction
                    return this._db.SaveChanges() > 0; // not successful or no item needs to be saved
                });
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="entity">An entity, its state should be attached</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            return AspectF.Define.MustBeNonNull(entity).
                HandleException(typeof(DbEntityValidationException)).
                Return<bool>(() =>
                {
                    this.Store.Remove(entity);

                    // commit transaction
                    return this._db.SaveChanges() > 0; // not successful or no item needs to be deleted
                });
        }

        public IQueryable<T> Table
        {
            get { return this.Store; }
        }
    }
}

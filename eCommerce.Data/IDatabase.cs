using eCommerce.Core;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;

namespace eCommerce.Data
{
    /// <summary>
    /// EF database interface
    /// </summary>
    public interface IDatabase
    {
        // TO-DO: change IDbSet to common IQuery in order to avoid to using Entity framework in future
        IDbSet<TEntity> Store<TEntity>() where TEntity : EntityBase, new();

        EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : EntityBase, new();

        void SetEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : EntityBase, new();

        int SaveChanges();

        /// <summary>
        /// execute stored procedure and other sql statement
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<TEntity> ExecuteCommand<TEntity>(string command, params DbParameter[] parameters)
            where TEntity : EntityBase, new();

        /// <summary>
        /// Return an entity as empty in order to avoid using null
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        TEntity GetEmptyEntity<TEntity>() where TEntity : EntityBase, new();
    }
}

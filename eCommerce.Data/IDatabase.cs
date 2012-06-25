using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;

namespace eCommerce.Data
{
    /// <summary>
    /// EF database interface
    /// </summary>
    public interface IDatabase
    {
        IDbSet<TEntity> Store<TEntity>() where TEntity : EntityBase, new();
        int SaveChanges();
        IList<TEntity> ExecuteCommand<TEntity>(string command, params DbParameter[] parameters)
            where TEntity : EntityBase, new();
    }
}

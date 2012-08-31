using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace eCommerce.Data.DatabaseInitializers
{
    /// <summary>
    /// Create table if it is not exist
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <remarks>implement IDatabaseInitializer interface</remarks>
    public class CreateTablesIfNotExist<TContext> : IDatabaseInitializer<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <remarks>Transactional operations. Cannot find system.transactions.dll
        /// in visual studio 2012 beta : http://connect.microsoft.com/VisualStudio/feedback/details/736571/vs11-system-transaction-namespace-is-missing
        /// </remarks>
        public void InitializeDatabase(TContext context)
        {
            try
            {
                if (context.Database.Exists())
                {
                    // if host in IIS, should config IIS user permission to database. For sql server,
                    // one approach is setting ApplicationPoolIdentity account into sql server security
                    // and another approach is using LocalSystem directly for the app pool
                    if (!context.Database.CompatibleWithModel(false))
                        context.Database.Delete(); // TO-DO: for debug, if release, should not delete directly
                }
                context.Database.CreateIfNotExists();
            }
            catch(System.Exception e)
            {
                throw e;
            }

        }
    }
}

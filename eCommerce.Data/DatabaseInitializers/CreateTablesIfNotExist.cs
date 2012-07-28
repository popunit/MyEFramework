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
            // check if database exists
            var hasExisted = context.Database.Exists();
            if (hasExisted)
            {
                var existedTableNames = context.Database.SqlQuery<string>(DbConstants.Query_Table_Names);

                if (null != existedTableNames && existedTableNames.Count() > 0) // check if there are tables existed
                {
                    using (TransactionScope scope =
                        new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        try
                        {
                            // force to create table and run IDatabaseInitializer multiple times
                            // http://social.msdn.microsoft.com/Forums/en/adodotnetentityframework/thread/e753187a-c86a-4952-802d-6ab46ee55a8d
                            //context.Database.Initialize(true); 
                            var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                            context.Database.ExecuteSqlCommand(dbCreationScript);

                            //Seed(context);
                            context.SaveChanges();
                            scope.Complete();
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using System.Data.Entity.Core.Objects;

namespace eCommerce.Data.Common
{
    public static class TypeHelper
    {
        public static Type GetProxyType(this EntityBase entity)
        {
            return entity.GetType();
        }

        public static Type GetUnproxyType(this EntityBase entity)
        {
            return ObjectContext.GetObjectType(entity.GetType());
        }

        public static void EnableProxyCreation(this DbContext context)
        {
            context.Configuration.ProxyCreationEnabled = true;
        }

        public static void DisableProxyCreation(this DbContext context)
        {
            context.Configuration.ProxyCreationEnabled = false;
        }

        public static ObjectContext ToObjectContext(this DbContext context)
        {
            return ((IObjectContextAdapter)context).ObjectContext;
        }

        public static string CreateDatabaseScript(this DbContext context)
        {
            return context.ToObjectContext().CreateDatabaseScript();
        }
    }
}

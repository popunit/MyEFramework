using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Events
{
    public static class EntityEventExtension
    {
        public static EntityEvent<T> Mark<T>(this T entity, EntityStatus status)
            where T : EntityBase, new()
        {
            if (null == entity)
                return null;
            return new EntityEvent<T>(entity, status);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Core.Common;

namespace eCommerce.Data.Common
{
    internal abstract class EmptyEntityMap<T>
        where T : EntityBase
    {
        private readonly Type entityType = typeof(T);
        public Type EntityType
        {
            get { return entityType; }
        }
        public abstract T Get();
        public bool IsValid()
        {
            Type t = Get().GetType();
            if (t == typeof(T) || !t.IsInherit(typeof(T)))
                return false;
            return true;
        }
    }
}

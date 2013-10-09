using eCommerce.Core;
using eCommerce.Core.Common;
using System;

namespace eCommerce.Data.Common
{
    public interface IEmptyEntityMap<out T>
        where T : EntityBase, new()
    {
        Type EntityType { get; }
        T Get();
        bool IsValid();
    }

    public abstract class EmptyEntityMap<T> : IEmptyEntityMap<T>
        where T : EntityBase, new()
    {
        private readonly Type _entityType = typeof(T);
        public Type EntityType
        {
            get { return _entityType; }
        }
        public abstract T Get();
        public bool IsValid()
        {
            Type t = Get().GetType();
            if (t == typeof(T) || !t.IsInheritFrom(typeof(T)))
                return false;
            return true;
        }
    }
}

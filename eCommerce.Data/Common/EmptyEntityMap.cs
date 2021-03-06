﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Core.Common;

namespace eCommerce.Data.Common
{
    public interface IEmptyEntityMap<T>
        where T : EntityBase
    {
        Type EntityType { get; }
        T Get();
        bool IsValid();
    }

    public abstract class EmptyEntityMap<T> : IEmptyEntityMap<T>
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
            if (t == typeof(T) || !t.IsInheritFrom(typeof(T)))
                return false;
            return true;
        }
    }
}

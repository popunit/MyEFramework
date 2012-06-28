using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;

namespace eCommerce.Data.Common
{
    internal interface IEmptyEntityMap
    { }

    internal abstract class EmptyEntityMap<T, K> : IEmptyEntityMap
        where T : EntityBase
        where K : T, new()
    {
        public K Get()
        {
            return new K();
        }
    }
}

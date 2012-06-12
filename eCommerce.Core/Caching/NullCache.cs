using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Caching
{
    public class NullCache : ICacheManager
    {
        public T Get<T>(string key)
        {
            return default(T);
        }

        public bool Set(string key, object data, int cacheTime)
        {
            return false;
        }

        public bool Contains(string key)
        {
            return false;
        }

        public bool Remove(string key)
        {
            return false;
        }

        public void Flush()
        {
            // do nothing
        }
    }
}

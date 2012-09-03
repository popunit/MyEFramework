using eCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Extensions.Caching.ServerAppFabric
{
    /// <summary>
    /// Server AppFabric Cache
    /// </summary>
    public class AppFabricCache : ICacheManager
    {
        //private readonly DataCacheFactory

        //public AppFabricCache()
        //{
 
        //}

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, object data, int cacheTime)
        {
            throw new NotImplementedException();
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveByPattern(string patternOfKeys)
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }
    }
}

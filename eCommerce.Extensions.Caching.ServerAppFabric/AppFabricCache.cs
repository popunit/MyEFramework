using eCommerce.Core;
using eCommerce.Core.Common;
using Microsoft.ApplicationServer.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eCommerce.Extensions.Caching.ServerAppFabric
{
    /// <summary>
    /// Server AppFabric Cache
    /// </summary>
    public class AppFabricCache : ICacheManager
    {
        private readonly DataCacheFactory factory;
        private readonly DataCache cache;
        private readonly string cacheName = "eCommerce"; // [TO-DO] to use config
        private readonly string regionName = "DistributedCache"; // [TO-DO] to use config

        public AppFabricCache()
        {
            factory = new DataCacheFactory();
            cache = factory.GetCache(cacheName);

            try
            {
                cache.CreateRegion(regionName);
            }
            catch
            {
                // if the region already exists, this will throw an exception.
                // Velocity has no API to check if a region exists or not.
            }
        }

        public T Get<T>(string key)
        {
            try
            {
                return (T)cache.Get(key,regionName);
            }
            catch
            {
                return default(T);
            }
        }

        public bool Set(string key, object data, TimeSpan timeout)
        {
            var version = cache.Put(key, data, timeout, regionName);
            return null != version;
        }

        public bool Contains(string key)
        {
            return null != cache.Get(key, regionName);
        }

        public bool Remove(string key)
        {
            if (this.Contains(key))
                cache.Remove(key, regionName);
            return true;
        }

        public void RemoveByPattern(string patternOfKeys)
        {
            var regex = new Regex(patternOfKeys, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            cache.GetObjectsInRegion(regionName).ForEach(keyValuePair => 
            {
                if (regex.IsMatch(keyValuePair.Key))
                    keysToRemove.Add(keyValuePair.Key);
            });

            keysToRemove.ForEach(key => 
            {
                this.Remove(key);
            });
        }

        public void Flush()
        {
            cache.ClearRegion(regionName);
        }
    }
}

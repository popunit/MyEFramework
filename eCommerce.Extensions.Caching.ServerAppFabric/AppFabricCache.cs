using eCommerce.Core;
using eCommerce.Core.Common;
using Microsoft.ApplicationServer.Caching;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace eCommerce.Extensions.Caching.ServerAppFabric
{
    /// <summary>
    /// Server AppFabric Cache
    /// </summary>
    public class AppFabricCache : ICacheManager, IDisposable
    {
        private readonly DataCacheFactory _factory;
        private readonly DataCache _cache;
        private const string CacheName = "eCommerce"; // [TO-DO] to use config
        private const string RegionName = "DistributedCache"; // [TO-DO] to use config

        public AppFabricCache()
        {
            _factory = new DataCacheFactory();
            _cache = _factory.GetCache(CacheName);

            try
            {
                _cache.CreateRegion(RegionName);
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
                return (T)_cache.Get(key,RegionName);
            }
            catch
            {
                return default(T);
            }
        }

        public bool Set(string key, object data, TimeSpan timeout)
        {
            var version = _cache.Put(key, data, timeout, RegionName);
            return null != version;
        }

        public bool Contains(string key)
        {
            return null != _cache.Get(key, RegionName);
        }

        public bool Remove(string key)
        {
            if (this.Contains(key))
                _cache.Remove(key, RegionName);
            return true;
        }

        public void RemoveByPattern(string patternOfKeys)
        {
            var regex = new Regex(patternOfKeys, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            _cache.GetObjectsInRegion(RegionName).ForEach(keyValuePair => 
            {
                if (regex.IsMatch(keyValuePair.Key))
                    keysToRemove.Add(keyValuePair.Key);
            });

            keysToRemove.ForEach(key => this.Remove(key));
        }

        public void Flush()
        {
            _cache.ClearRegion(RegionName);
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace eCommerce.Core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public bool Set(string key, object data, TimeSpan timeout)
        {
            if (null == data)
                return false;
            var policy = new CacheItemPolicy {AbsoluteExpiration = DateTime.Now + timeout};
            return Cache.Add(new CacheItem(key, data), policy);
        }

        public bool Contains(string key)
        {
            return Cache.Contains(key);
        }

        public bool Remove(string key)
        {
            try
            {
                Cache.Remove(key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Flush()
        {
            foreach (var item in Cache)
            {
                this.Remove(item.Key);
            }
        }

        public void RemoveByPattern(string patternOfKeys)
        {
            var regex = new Regex(patternOfKeys, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = (from item in Cache where regex.IsMatch(item.Key) select item.Key).ToList();

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }
    }
}

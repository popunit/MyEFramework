using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + timeout;
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
            var keysToRemove = new List<String>();

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }
    }
}

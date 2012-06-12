using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
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

        public bool Set(string key, object data, int cacheMinutes)
        {
            if (null == data)
                return false;
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheMinutes);
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
    }
}

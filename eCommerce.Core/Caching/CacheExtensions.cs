using System;

namespace eCommerce.Core.Caching
{
    public static class CacheExtensions
    {
        public static T GetOrAdd<T>(this ICacheManager cacheManager, string key, Func<T> callbackIfNotFound)
        {
            return GetOrAdd(cacheManager, key, TimeSpan.FromHours(1), callbackIfNotFound);
        }

        public static T GetOrAdd<T>(this ICacheManager cacheManager, string key, TimeSpan timeout, Func<T> callbackIfNotFound)
        {
            if (cacheManager.Contains(key))
                return cacheManager.Get<T>(key);

            var result = callbackIfNotFound();
            cacheManager.Set(key, result, timeout);
            return result;
        }
    }
}

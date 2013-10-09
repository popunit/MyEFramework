using System;

namespace eCommerce.Core
{
    /// <summary>
    /// GRUD for cache manager framework
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Get data according to key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Insert/update target data into cache container
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool Set(string key, object data, TimeSpan timeout);

        /// <summary>
        /// Check if the current key has been stored
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// Remove target data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// Removes target data by pattern
        /// </summary>
        /// <param name="patternOfKeys">pattern of keys</param>
        /// <remarks>I don't want to return bool type because the code cannot be rollback if one of item cannot be removed</remarks>
        void RemoveByPattern(string patternOfKeys);

        /// <summary>
        /// Clear all the data
        /// </summary>
        void Flush();
    }
}

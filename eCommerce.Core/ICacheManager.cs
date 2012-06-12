using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        bool Set(string key, object data, int cacheTime);

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
        /// Clear all the data
        /// </summary>
        void Flush();
    }
}

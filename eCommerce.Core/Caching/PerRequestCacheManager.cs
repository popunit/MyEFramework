using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace eCommerce.Core.Caching
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Still cache data in memory, use http context base so that it can be tested</remarks>
    public class PerRequestCacheManager : ICacheManager
    {
        private readonly HttpContextBase context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">get the core registered data from container</param>
        public PerRequestCacheManager(HttpContextBase context)
        {
            this.context = context;
        }

        protected virtual IDictionary GetItems()
        {
            if (null != context)
                return context.Items;
            return null;
        }

        public T Get<T>(string key)
        {
            var items = GetItems();
            if (items == null)
                return default(T);

            return (T)items[key];
        }

        public bool Set(string key, object data, int cacheTime)
        {
            var items = GetItems();
            if (items == null)
                return false;

            if (data != null)
            {
                if (items.Contains(key))
                    items[key] = data;
                else
                    items.Add(key, data);

                return true;
            }

            return false;
        }

        public bool Contains(string key)
        {
            var items = GetItems();
            if (items == null)
                return false;

            return (items[key] != null);
        }

        public bool Remove(string key)
        {
            var items = GetItems();
            if (items == null)
                return false;

            items.Remove(key);
            return true;
        }

        public void RemoveByPattern(string patternOfKeys)
        {
            var items = GetItems();
            if (items == null)
                return;

            var enumerator = items.GetEnumerator();
            var regex = new Regex(patternOfKeys, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    keysToRemove.Add(enumerator.Key.ToString());
                }
            }

            foreach (string key in keysToRemove)
            {
                items.Remove(key);
            }
        }

        public void Flush()
        {
            var items = GetItems();
            if (items == null)
                return;

            var enumerator = items.GetEnumerator();
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                keysToRemove.Add(enumerator.Key.ToString());
            }

            foreach (string key in keysToRemove)
            {
                items.Remove(key);
            }
        }
    }
}

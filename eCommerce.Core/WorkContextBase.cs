using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>I don't want to reference to concreted Entity object in core project</remarks>
    public abstract class WorkContextBase
    {
        public ConcurrentDictionary<string, EntityBase> Items 
        { 
            get; 
            protected set; 
        }

        protected virtual T GetData<T>(string name = "") where T : EntityBase, new()
        {
            try
            {
                string keyName = string.Empty;
                if (string.IsNullOrEmpty(name))
                    keyName = typeof(T).FullName;
                else
                    keyName = name;
                if (Items.ContainsKey(keyName))
                    return Items[keyName] as T;
                else
                    return default(T);
            }
            catch
            {
                return default(T);
            }
        }

        protected virtual bool SetData<T>(T value, string name = "") where T : EntityBase, new()
        {
            try
            {
                string keyName = string.Empty;
                if (string.IsNullOrEmpty(name))
                    keyName = typeof(T).FullName;
                else
                    keyName = name;
                Items.AddOrUpdate(keyName, value, (key, val) => val);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

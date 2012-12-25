using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Services.WcfClient.Entities;
using System.Collections.Concurrent;

namespace eCommerce.Services
{
    public abstract class WebWorkContextBase : IWorkContext
    {
        public virtual User CurrentUser 
        {
            get
            {
                return GetData<User>();
            }
            set
            {
                SetData<User>(value);
            }
        }

        public ConcurrentDictionary<string, EntityBase> Items
        {
            get;
            protected set; 
        }

        public virtual T GetData<T>(string name = "") where T : EntityBase, new()
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

        public virtual bool SetData<T>(T value, string name = "") where T : EntityBase, new()
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

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    public abstract class WorkContextBase
    {
        public ConcurrentDictionary<string, object> Items 
        { 
            get; 
            protected set; 
        }
        protected abstract T GetData<T>(string name) where T : EntityBase, new();
        protected abstract bool SetData<T>(string name, T value) where T : EntityBase, new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Common
{
    internal class LookUp
    {
        private static readonly IDictionary<Type, object> dict
            = new Dictionary<Type, object>();

        private LookUp()
        {
            // do nothing, avoid to instantiate it
        }

        internal static IDictionary<Type, object> Objects
        {
            get { return dict; }
        }
    }

    public class Singleton<T>
    {
        private static T instance; // store the data with type T, avoid to re-look up into dictionary
        public static T Instance
        {
            get 
            { 
                return instance; 
            }
            set 
            {
                instance = value;
                LookUp.Objects[typeof(T)] = value;
            }
        }
    }

}

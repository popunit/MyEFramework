using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Common
{
    public static class TypeHelper
    {
        public static bool IsInherit(this Type child, Type parent)
        {
            try
            {
                if (parent.IsAssignableFrom(child))
                    return true;

                // IsAssignableFrom doesn't work for generic type
                if (parent.IsGenericTypeDefinition)
                {
                    var genericTypeDefinition = parent.GetGenericTypeDefinition();
                    foreach (var implementedInterface in child.FindInterfaces((objType, objCriteria) => true, null))
                    {
                        if (!implementedInterface.IsGenericType)
                            continue;

                        var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                        return isMatch;
                    }
                    return false;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collections, Action<T> callback)
        {
            if (null == collections || null == callback)
                return;
            foreach (var item in collections)
            {
                callback(item);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

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

    public static class EnumHelper
    {
        public static string Name(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string desc = value.ToString();

            FieldInfo info = value.GetType().GetField(desc);
            var attrs = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                desc = attrs[0].Description;
            }

            return desc;
        }
    }

    /// <summary>
    /// Store temp data throughout request
    /// </summary>
    public class RequstTempData
    {
        // TO-DO: Check how many items do it have in multi threads
        private static ThreadLocal<RequstTempData> __inc = new ThreadLocal<RequstTempData>();

        public RequstTempData()
        {
            try
            {
                DataBag = new ExpandoObject();
                __inc.Value = this;
            }
            catch
            {
                throw new System.Exception("Unknown exception");
            }
        }

        public dynamic DataBag
        {
            get;
            set;
        }

        public static RequstTempData Local
        {
            get { return __inc.Value; }
        }

        //public void Dispose()
        //{
        //    __inc.Dispose();
        //    GC.SuppressFinalize(this);
        //}
    } 
}

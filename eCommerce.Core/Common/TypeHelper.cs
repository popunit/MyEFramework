using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="childConcreted">Type not abstract</param>
        /// <param name="parentInterface">Type is interface</param>
        /// <returns></returns>
        public static bool IsInherit(this Type childConcreted, Type parentInterface)
        {
            try
            {
                if (parentInterface.IsAssignableFrom(childConcreted) && parentInterface != childConcreted)
                    return true;

                if (childConcreted.IsAbstract)
                    return false;

                // IsAssignableFrom doesn't work for generic type
                if (parentInterface.IsGenericTypeDefinition)
                {
                    var genericTypeDefinition = parentInterface.GetGenericTypeDefinition();
                    foreach (var implementedInterface in childConcreted.FindInterfaces((objType, objCriteria) => true, null))
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

        public static string[] Trims(this IEnumerable<string> collections)
        {
            string[] items = collections.ToArray();
            Array.ForEach(items, item => item.Trim());
            return items;
        }

        public static TypeConverter GetTypeConverter(this Type type)
        {
            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();

            return TypeDescriptor.GetConverter(type);
        }

        public static string ToSafeString(this object obj)
        {
            if (null != obj)
                return obj.ToString();
            else
                return string.Empty;
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

    public class GenericListTypeConverter<T> : TypeConverter
    {
        protected readonly TypeConverter typeConverter;

        public GenericListTypeConverter()
        {
            typeConverter = TypeDescriptor.GetConverter(typeof(T));
            if (typeConverter == null)
                throw new InvalidOperationException("No type converter exists for type " + typeof(T).FullName);
        }

        protected virtual string[] GetStringArray(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                string[] lines = input.Split(',');
                return lines.Trims();
            }
            else
                return new string[0];
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                string[] items = this.GetStringArray(sourceType.ToString());
                return items.Count() > 0;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] items = GetStringArray((string)value);
                var list = new List<T>();
                items.ForEach(item => 
                {
                    object obj = typeConverter.ConvertFromInvariantString(item);
                    if (null != obj)
                        list.Add((T)obj);
                });

                return list;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string line = string.Empty;
                var list = value as IEnumerable<T>;
                if (null != list)
                {
                    list.ForEach(item => 
                    {
                        var str = Convert.ToString(item, CultureInfo.InvariantCulture);
                        line += str;
                        line += ",";
                    });
                    line.Trim(','); // remove the last comma
                }

                return line;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

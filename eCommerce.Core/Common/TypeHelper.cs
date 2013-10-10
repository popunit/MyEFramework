using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using eCommerce.Exception;

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
        public static bool IsInheritFrom(this Type childConcreted, Type parentInterface)
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

        public static bool HasCustomAttribute(this Type type, Type attributeType)
        {
            var attribute = type.GetCustomAttribute(attributeType);
            return !attribute.IsNull();
        }

        public static bool HasCustomAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return HasCustomAttribute(type, typeof(TAttribute));
        }

        public static Type[] GetTypesFromCurrentAssembly(this Type type)
        {
            return Assembly.GetAssembly(type).GetTypes();
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> callback)
        {
            if (null == enumeration || callback.IsNull())
                return;
            foreach (var item in enumeration)
            {
                callback(item);
            }
        }

        /// <summary>
        /// IEnumerable doesn't represent Collection type, so in actual we should not 
        /// use add like a collection. Should use IList or ICollection instead.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> enumeration, T item)
        {
            if (null == enumeration)
                throw new NullReferenceException("Target Object cannot be null!");
            foreach (var i in enumeration)
                yield return i;
            yield return item;
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumeration)
        {
            if (null == enumeration || collection.IsNull())
                return null;
            enumeration.ForEach(collection.Add);

            return collection;
        }

        public static T GetItem<T>(this IList<T> list, Predicate<T> predicate)
        {
            //if (null == list)
            //    throw new NullReferenceException("Target Object cannot be null!");

            //T result = default(T);
            //if (list.Count == 0)
            //    return result;
            
            //for (var i = 0; i < list.Count; i++)
            //{
            //    var item = list[i];
            //    if (predicate(item))
            //    {
            //        result = item;
            //        break;
            //    }
            //}
            //return result;

            return list.SingleOrDefault(i => predicate(i));
        }

        /// <summary>
        /// Predicate_T and Func_T_Bool have the same signiture but they still are different types. 
        /// Moreover, the usage of predicate is narrower than that of Func. Only use Predicate when
        /// we just need to do judgement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Predicate<T> ToPredicate<T>(this Func<T, bool> source)
        {
            var result = new Predicate<T>(source);
            return result;
        }   

        public static string[] Trims(this IEnumerable<string> collection)
        {
            string[] items = collection.ToArray();
            Array.ForEach(items, item => item = item.Trim());
            return items;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumeration)
        {
            if (null == enumeration || !enumeration.Any())
                return true;
            return false;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <remarks>[TO-DO] object type is not correct sometimes</remarks>
        public static string ToSafeString(this object obj)
        {
            if (null != obj)
                return obj.ToString();
            else
                return string.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull<T>(this T obj) where T : class
        {
            return null == obj;
        }

        public static bool IsDataContract(this Type type)
        {
            var attr = type.GetCustomAttribute<DataContractAttribute>(false);
            return null != attr;
        }

        public static Uri EnsureWsdl(this Uri uri)
        {
            string absoluteUri = uri.AbsoluteUri;
            if (absoluteUri.EndsWith("?wsdl", true, CultureInfo.InvariantCulture) == false)
            {
                if (absoluteUri.Contains(".svc"))
                {
                    var reg = new Regex(@"([\w|\.]+://\S+\.svc)\S+");
                    Match match = reg.Match(absoluteUri);
                    if (!match.IsNull() && match.Groups.Count > 1)
                    {
                        string link = match.Groups[1].Value;
                        return new Uri(link + "?wsdl");
                    }
                }

                return new Uri(absoluteUri.TrimEnd('/','?') + "?wsdl");
            }

            return uri;
        }
    }

    public static class EnumHelper
    {
        public static string Name(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static T Index<T>(this Enum value) where T : struct
        {
            return (T)Enum.Parse(value.GetType(), Name(value));
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string desc = value.ToString();

            FieldInfo info = value.GetType().GetField(desc);
            var attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

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
        private readonly static ThreadLocal<RequstTempData> Inc = new ThreadLocal<RequstTempData>();

        public RequstTempData()
        {
            try
            {
                DataBag = new ExpandoObject();
                Inc.Value = this;
            }
            catch
            {
                throw new CommonException("Unknown exception");
            }
        }

        public dynamic DataBag
        {
            get;
            set;
        }

        public static RequstTempData Local
        {
            get { return Inc.Value; }
        }

        //public void Dispose()
        //{
        //    __inc.Dispose();
        //    GC.SuppressFinalize(this);
        //}
    }

    public class GenericListTypeConverter<T> : TypeConverter
    {
        protected readonly TypeConverter Converter;

        public GenericListTypeConverter()
        {
            Converter = TypeDescriptor.GetConverter(typeof(T));
            if (Converter == null)
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
                return items.Any();
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var str = value as string;
            if (str != null)
            {
                string[] items = GetStringArray(str);
                var list = new List<T>();
                items.ForEach(item => 
                {
                    object obj = Converter.ConvertFromInvariantString(item);
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
                    line = line.Trim(','); // remove the last comma
                }

                return line;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

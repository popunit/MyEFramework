using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Common
{
    public class DataBuilder
    {
        public static Action<T, TValue> Set<T, TValue>(string property)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            ParameterExpression valArg = Expression.Parameter(typeof(TValue), "val");
            Expression expr = arg;
            foreach (string prop in props.Take(props.Length - 1))
            {
                // use reflection (not ComponentModel) to mirror LINQ 
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            // final property set...
            PropertyInfo finalProp = type.GetProperty(props.Last());
            MethodInfo setter = finalProp.GetSetMethod();
            expr = Expression.Call(expr, setter, valArg);
            return Expression.Lambda<Action<T, TValue>>(expr, arg, valArg).Compile();

        }

        public static Func<T, TValue> Get<T, TValue>(string property)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ 
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            return Expression.Lambda<Func<T, TValue>>(expr, arg).Compile();
        }
    }
}

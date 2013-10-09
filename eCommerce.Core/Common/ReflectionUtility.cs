using System;
using System.Linq.Expressions;

namespace eCommerce.Core.Common
{
    public static class ReflectionUtility
    {
        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            var body = (MemberExpression)expression.Body;
            return body.Member.Name;
        }
    }

}

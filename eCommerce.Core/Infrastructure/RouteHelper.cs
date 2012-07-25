using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;
using eCommerce.Exception;

namespace eCommerce.Core.Infrastructure
{
    public static class RouteHelper
    {
        internal static void RoutingToExecute<T>(
            IRoute routing,
            Action<T> executing) where T : IOrderable
        {
            //var routing = containerManager.Resolve<IRoute>(typeof(WebsiteRoute).Name);
            var types = routing.FindType<T>();
            var instances = new List<T>();
            types.ForEach(t =>
            {
                instances.Add((T)Activator.CreateInstance(t));
            });

            // register object in order
            instances.OrderBy(t => t.Order).ForEach(i => executing(i));
        }

        public static void RoutingToType<T>(
            IRoute routing,
            Action<Type> executing)
        {
            var types = routing.FindType<T>();
            types.ForEach(t => executing(t));
        }

        public static void RoutingToGenericType(
            IRoute routing, 
            Type genericType, 
            Action<Type> executing)
        {
            var types = routing.FindType(genericType); // IsGenericTypeDefinition?
            types.ForEach(t => executing(t));
        }

        internal static void FindExceptionToHandle(
            IRoute routing,
            Type targetExceptionType,
            System.Exception ex,
            bool throwIfNotFound = false)
        {
            // TO-DO: should cache here because here will be performed multi times.
            var types = routing.FindType<IHandler>();
            var instances = new List<IHandler>();
            IEnumerable<Type> filters;
            if (targetExceptionType == null)
                filters = types.Where(t => t == targetExceptionType);
            else
                filters = types;
            foreach (var t in filters)
            {
                Type arg;
                if (t.IsGenericType)
                    arg = t.GetGenericArguments()[0];
                else
                    arg = t.BaseType.GetGenericArguments()[0];

                object obj;
                try
                { obj = Convert.ChangeType(ex, arg); }
                catch
                { obj = null; }
                if (null != obj)
                {
                    instances.Add((IHandler)Activator.CreateInstance(t));
                    break;
                }
            }

            if (instances.Count == 0)
            {
                if (throwIfNotFound)
                {
                    throw new System.Exception(
                        string.Format("Can not find the Exception type {0} registered", ex.GetType().Name),
                        ex);
                }
            }
            else
                instances[0].Handle(ex);
        }
    }
}

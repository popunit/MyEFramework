using eCommerce.Core.Common;
using eCommerce.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Core.Infrastructure
{
    public static class RouteHelper
    {
        /// <summary>
        /// Find instance of T and execute for the instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searcher"></param>
        /// <param name="executing"></param>
        public static void RoutingToExecute<T>(
            this ISearcher searcher,
            Action<T> executing) where T : IOrderable
        {
            RoutingToExecute(searcher, Order.ASC, executing);
        }

        public static void RoutingToExecute<T>(
            this ISearcher searcher,
            Order orderBy,
            Action<T> executing) where T : IOrderable
        {
            if (null == searcher)
                throw new NullReferenceException();
            var types = searcher.FindType<T>();
            var instances = new List<T>();
            types.ForEach(t => instances.Add((T)EmitHelper.FastGetInstance(t)()));

            switch (orderBy)
            {
                case Order.ASC:
                    instances.OrderBy(t => t.Order).ForEach(executing);
                    break;
                case Order.DESC:
                    instances.OrderByDescending(t => t.Order).ForEach(executing);
                    break;
            }
            
        }

        /// <summary>
        /// Find type of T and execute for type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searcher"></param>
        /// <param name="executing"></param>
        public static void RoutingToTypeExecute<T>(
            this ISearcher searcher,
            Action<Type> executing)
        {
            var types = searcher.FindType<T>();
            types.ForEach(executing);
        }

        /// <summary>
        /// Find type of T and execute for type
        /// </summary>
        /// <param name="searcher"></param>
        /// <param name="genericType"></param>
        /// <param name="executing"></param>
        public static void RoutingToTypeExecute(
            this ISearcher searcher, 
            Type genericType, 
            Action<Type> executing)
        {
            var types = searcher.FindType(genericType); // IsGenericTypeDefinition?
            types.ForEach(executing);
        }

        internal static void FindExceptionToHandle(
            this ISearcher searcher,
            Type targetExceptionType,
            System.Exception ex,
            bool throwIfNotFound = false)
        {
            // TO-DO: should cache here because here will be performed multi times.
            var types = searcher.FindType<IHandler>();
            var instances = new List<IHandler>();
            IEnumerable<Type> filters = 
                targetExceptionType != null ? types.Where(t => t == targetExceptionType) : types;
            foreach (var t in filters)
            {
                Type arg;
                if (t.IsGenericType)
                    arg = t.GetGenericArguments()[0];
                else
                    arg = t.BaseType.GetGenericArguments()[0]; // [TO-DO] check if base type exists

                object obj;
                try
                { obj = Convert.ChangeType(ex, arg); }
                catch
                { obj = null; }
                if (null != obj)
                {
                    //instances.Add((IHandler)Activator.CreateInstance(t));
                    instances.Add((IHandler)EmitHelper.FastGetInstance(t)());
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

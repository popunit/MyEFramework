using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;

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
    }
}

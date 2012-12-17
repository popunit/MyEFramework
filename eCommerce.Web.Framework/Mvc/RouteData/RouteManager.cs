using eCommerce.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace eCommerce.Web.Framework.Mvc.RouteData
{
    /// <summary>
    /// Find all the MVC route config and register
    /// </summary>
    public class RouteManager : IRouteManager
    {
        private readonly ISearcher routing;
        public RouteManager(ISearcher routing)
        {
            this.routing = routing;
        }

        public void RegisterAllRoutes(RouteCollection routes)
        {
            routing.RoutingToExecute<IRouteRegistrar>(type => type.RegisterRoutes(routes));
        }
    }
}

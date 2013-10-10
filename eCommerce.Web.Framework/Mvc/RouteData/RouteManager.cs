using eCommerce.Core.Infrastructure;
using System.Web.Routing;

namespace eCommerce.Web.Framework.Mvc.RouteData
{
    /// <summary>
    /// Find all the MVC route config and register
    /// </summary>
    public class RouteManager : IRouteManager
    {
        private readonly ISearcher _routing;
        public RouteManager(ISearcher routing)
        {
            this._routing = routing;
        }

        public void RegisterAllRoutes(RouteCollection routes)
        {
            _routing.RoutingToExecute<IRouteRegistrar>(type => type.RegisterRoutes(routes));
        }
    }
}

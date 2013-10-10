using System.Web.Routing;

namespace eCommerce.Web.Framework.Mvc.RouteData
{
    public interface IRouteManager
    {
        /// <summary>
        /// Register all the routes from registrars
        /// </summary>
        /// <param name="routes"></param>
        void RegisterAllRoutes(RouteCollection routes);
    }
}

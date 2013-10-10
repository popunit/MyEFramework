using eCommerce.Core;
using System.Web.Routing;

namespace eCommerce.Web.Framework.Mvc.RouteData
{
    public interface IRouteRegistrar : IOrderable
    {
        void RegisterRoutes(RouteCollection routes);
    }
}

using eCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace eCommerce.Web.Framework.Mvc.RouteData
{
    public interface IRouteRegistrar : IOrderable
    {
        void RegisterRoutes(RouteCollection routes);
    }
}

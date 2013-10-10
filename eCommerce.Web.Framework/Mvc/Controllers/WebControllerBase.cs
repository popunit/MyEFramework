using eCommerce.Web.Framework.Mvc.Filters.ActionFilters;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.Controllers
{
    [StoreLastVisitedPage]
    public abstract class WebControllerBase : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            //return base.CreateActionInvoker();
            // use AsyncControllerActionInvoker?
            return new WebControllerActionInvoker();
        }
    }
}

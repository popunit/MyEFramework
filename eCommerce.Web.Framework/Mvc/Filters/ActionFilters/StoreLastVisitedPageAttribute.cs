using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Web.Framework.Mvc.Extensions;

namespace eCommerce.Web.Framework.Mvc.Filters.ActionFilters
{
    [NotAllowChildAction]
    [HttpMethodFilter(DisableDelete = true, DisablePut = true, DisablePost = true)]
    public class StoreLastVisitedPageAttribute : FilterAttribute, IActionFilter
    {
        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.DBIsInstalled())
                return;
            if (!filterContext.HasRequest())
                return;
            var httpHelper = DependencyResolver.Current.GetService<IHttpHelper>();
            var requestUrl = httpHelper.GetCurrentRequestUrl(true);
            if (!string.IsNullOrEmpty(requestUrl)) // if has request url, store it
            { 
            }
            
            // TO-DO: store last visited page
            throw new NotImplementedException();
        }
    }
}

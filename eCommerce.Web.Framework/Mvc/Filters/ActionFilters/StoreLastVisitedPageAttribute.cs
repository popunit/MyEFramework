using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
            if (!filterContext.IsValid())
                return;
            //var settings = DependencyResolver.Current.GetService<UserSettings>();
            
            // TO-DO: store last visited page
            throw new NotImplementedException();
        }
    }
}

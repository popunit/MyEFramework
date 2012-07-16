using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Web.Framework.Mvc.Extensions;

namespace eCommerce.Web.Framework.Mvc.Filters.ActionFilters
{
    [NotAllowChildController]
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
            
        }
    }
}

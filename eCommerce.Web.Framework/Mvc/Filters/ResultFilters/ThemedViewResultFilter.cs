using System;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.Filters.ResultFilters
{
    public class ThemedViewResultFilter : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}

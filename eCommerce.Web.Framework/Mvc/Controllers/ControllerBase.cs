using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return base.CreateActionInvoker();
        }
    }

    public class WebControllerActionInvoker : ControllerActionInvoker
    {
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            FilterInfo info = base.GetFilters(controllerContext, actionDescriptor);
            //foreach (var toExclude in info.ActionFilters.OfType<ExcludeFilterAttribute>().Select(f => f.FilterToExclude).ToArray())
            //{
            //    RemoveWhere(filterInfo.ActionFilters, filter => toExclude.IsAssignableFrom(filter.GetType()));
            //    RemoveWhere(filterInfo.AuthorizationFilters, filter => toExclude.IsAssignableFrom(filter.GetType()));
            //    RemoveWhere(filterInfo.ExceptionFilters, filter => toExclude.IsAssignableFrom(filter.GetType()));
            //    RemoveWhere(filterInfo.ResultFilters, filter => toExclude.IsAssignableFrom(filter.GetType()));
            //}
            //return filterInfo;
            if (controllerContext.IsChildAction)
            {
                RemoveWhere(info.ActionFilters,
                    filter =>
                    {
                        var attributes = filter.GetType().GetCustomAttributes(typeof(NotAllowChildControllerAttribute), false);
                        if (null == attributes || attributes.Length == 0)
                            return false;
                        else
                            return true;
                    });
            }
        }

        private static IList<T> RemoveWhere<T>(IList<T> list, Predicate<T> predicate)
        {

            if (list == null || list.Count == 0)
                return list;
            //note: didn't use foreach because an exception will be thrown when you remove items during enumeration
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            return list;
        }
    }
}

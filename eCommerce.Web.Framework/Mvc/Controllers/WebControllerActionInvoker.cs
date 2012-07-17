using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Core.Common;
using eCommerce.Core.Data;
using eCommerce.Web.Framework.Mvc.Filters.ActionFilters;

namespace eCommerce.Web.Framework.Mvc.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>http://stackoverflow.com/questions/4684150/a-way-to-exclude-action-filters-in-asp-net-mvc</remarks>
    public class WebControllerActionInvoker : ControllerActionInvoker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        /// <remarks>TO-DO: performance check in future</remarks>
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            FilterInfo info = base.GetFilters(controllerContext, actionDescriptor);

            //// check not allow child action attribute
            //if (controllerContext.IsChildAction)
            //{
            //    RemoveWhere(info.ActionFilters,
            //        filter =>
            //        {
            //            var attributes = filter.GetType().GetCustomAttributes(typeof(NotAllowChildActionAttribute), false);
            //            if (null == attributes || attributes.Length == 0)
            //                return false;
            //            else
            //                return true;
            //        });
            //}

            // HttpMethodFilter
            RemoveWhere(info.ActionFilters,
                filter =>
                {
                    if (filter.GetType() == typeof(StoreLastVisitedPageAttribute))
                    {
                        var userSettings = DependencyResolver.Current.GetService<UserSettings>();
                        if (!userSettings.StoreLastVisitedPage)
                            return true;
                    }

                    if (controllerContext.IsChildAction)
                    {
                        var attr = filter.GetType().GetCustomAttributes(typeof(NotAllowChildActionAttribute), false);
                        if (null != attr && attr.Length != 0)
                            return true;
                    }

                    var attributes = filter.GetType().GetCustomAttributes(typeof(HttpMethodFilterAttribute), false);
                    if (null != attributes && attributes.Length == 0)
                    {
                        foreach(var attr in attributes)
                        {
                            var attribute = attr as HttpMethodFilterAttribute;
                            if (null != attribute)
                            {
                                if (String.Equals(controllerContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                                    return attribute.DisableGet;
                                if (String.Equals(controllerContext.HttpContext.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
                                    return attribute.DisablePost;
                                if (String.Equals(controllerContext.HttpContext.Request.HttpMethod, "PUT", StringComparison.OrdinalIgnoreCase))
                                    return attribute.DisablePut;
                                if (String.Equals(controllerContext.HttpContext.Request.HttpMethod, "DELETE", StringComparison.OrdinalIgnoreCase))
                                    return attribute.DisableDelete;
                                break;
                            }
                        }
                    }
                    return false;
                });

            return info;
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

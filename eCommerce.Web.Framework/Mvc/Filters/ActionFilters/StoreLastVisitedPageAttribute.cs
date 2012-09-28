using eCommerce.Core;
using eCommerce.Services;
using eCommerce.Services.Extensions;
using eCommerce.Services.Users;
using eCommerce.Web.Framework.Mvc.Extensions;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.Filters.ActionFilters
{
    [NotAllowChildAction]
    [HttpMethodFilter(DisableDelete = true, DisablePut = true, DisablePost = true)]
    public class StoreLastVisitedPageAttribute : FilterAttribute, IActionFilter
    {
        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // doing nothing
        }

        /// <summary>
        /// Save current visited page url to store for using in future
        /// </summary>
        /// <param name="filterContext"></param>
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
                var context = DependencyResolver.Current.GetService<WorkContextServiceBase>();
                var userService = DependencyResolver.Current.GetService<IUserDataService>();
                
                var storedUrl = context.CurrentUser.GetCharacteristic<string>(UserCharacteristicResource.LastVisitedPage);
                if (requestUrl != storedUrl)
                {
                    userService.SaveUserCharacteristic(context.CurrentUser, UserCharacteristicResource.LastVisitedPage, requestUrl); // update stored url
                }
            }
        }
    }
}

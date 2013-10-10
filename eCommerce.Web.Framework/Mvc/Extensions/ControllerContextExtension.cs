using eCommerce.Services.Common;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.Extensions
{
    public static class ControllerContextExtension
    {
        public static bool DbIsInstalled(this ControllerContext controllerContext)
        {
            //return DatabaseSettingHelper.FindDatabaseSettings;
            return DependencyResolver.Current.GetService<IDataInfoDataService>().DatabaseIsInstalled();
        }

        public static bool IsValid(this ControllerContext controllerContext)
        {
            return null != controllerContext &&
                null != controllerContext.HttpContext;
        }

        public static bool HasRequest(this ControllerContext controllerContext)
        {
            if (controllerContext.IsValid())
                if (null != controllerContext.HttpContext.Request)
                    return true;

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Core.Data;

namespace eCommerce.Web.Framework.Mvc.Extensions
{
    public static class ControllerContextExtension
    {
        public static bool DBIsInstalled(this ControllerContext controllerContext)
        {
            return DatabaseSettingHelper.FindDatabaseSettings;
        }

        public static bool IsValid(this ControllerContext controllerContext)
        {
            return null != controllerContext &&
                null != controllerContext.HttpContext &&
                null != controllerContext.HttpContext.Request;
        }
    }
}

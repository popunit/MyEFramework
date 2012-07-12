using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.RouteData
{
    public static class RouteDataExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identifierForController">the string should be in accordence with setting for route data in global file</param>
        /// <returns></returns>
        /// <remarks>http://www.cnblogs.com/szw/archive/2011/03/08/1977548.html</remarks>
        public static string GetControllerName(this ControllerContext context, string identifierForController = "controller")
        {
            try
            {
                if (null == context)
                    return null;
                return context.RouteData.GetRequiredString(identifierForController);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public static string GetActionName(this ControllerContext context, string identifierForAction = "action")
        {
            try
            {
                if (null == context)
                    return null;
                return context.RouteData.GetRequiredString(identifierForAction);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}

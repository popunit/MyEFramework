using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/6862403/get-area-name-from-actionexecutingcontext</remarks>
        public static string GetAreaName(this ControllerContext context)
        {
            if (null == context)
                return string.Empty;

            var routeData = context.RouteData;
            object obj2;
            if (routeData.DataTokens.TryGetValue("area", out obj2)) // get area from default route
            {
                return (obj2 as string);
            }
            return GetAreaName(routeData.Route);
        }

        public static string GetAreaName(RouteBase route)
        {
            // get area name from all the route including registered custom routes
            var area = route as IRouteWithArea;
            if (area != null)
            {
                return area.Area;
            }
            var route2 = route as Route;
            if ((route2 != null) && (route2.DataTokens != null))
            {
                return (route2.DataTokens["area"] as string);
            }
            return null;
        }
    }
}

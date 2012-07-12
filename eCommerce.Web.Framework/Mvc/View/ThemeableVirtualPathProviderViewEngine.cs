using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using eCommerce.Core.Infrastructure;
using eCommerce.Core;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Core.Enums;
using eCommerce.Web.Framework.Mvc.RouteData;

namespace eCommerce.Web.Framework.Mvc.View
{
    public abstract class ThemeableVirtualPathProviderViewEngine : VirtualPathProviderViewEngine
    {
        // to change search location should override (properties):
        // ViewLocationFormats
        // MasterLocationFormats
        // PartialViewLocationFormats
        // AreaViewLocationFormats
        // AreaMasterLocationFormats
        // AreaPartialViewLocationFormats

        protected ThemeableVirtualPathProviderViewEngine()
        {
            
        }

        /// <summary>
        /// Core function
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="viewName"></param>
        /// <param name="masterName"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var mobileDeviceChecker = DependencyResolver.Current.GetService<IMobileDeviceCheck>();
            bool useMobileDevice = mobileDeviceChecker.MobileDeviceIsAvailable(controllerContext.HttpContext);
            var workType = useMobileDevice ? WorkType.Mobile : WorkType.Desktop; // extend it if more work type


            // TO-DO: try to return New View

            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        protected virtual ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache, WorkType workType)
        {
            return AspectF.Define.MustBeNonNull(controllerContext).MustBeNonNullOrEmpty(viewName).Return<ViewEngineResult>(() => 
            {
                var theme = MvcHelper.GetCurrentTheme(workType);
                var controllerName = controllerContext.GetControllerName();

                // TO-DO
                throw new NotImplementedException();
            });
        }

        /// <summary>
        /// Core function
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="partialViewName"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        protected virtual string GetPath(ControllerContext controllerContext, string[] locations, string[] areaLocations, string locationsPropertyName, string name, string controllerName, string theme, string cacheKeyPrefix, bool useCache, WorkType workType, out string[] searchedLocations)
        {
            throw new NotImplementedException("GetPath");
        }
    }
}

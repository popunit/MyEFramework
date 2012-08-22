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
using System.Globalization;
using System.Web.Routing;

namespace eCommerce.Web.Framework.Mvc.View.ViewEngines
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// ViewEngine is used to search view's locations
    /// Implement IViewEngine, Find ViewEngineResult (includes IView and IViewEngine)
    /// </remarks>
    public abstract class ThemeableVirtualPathProviderViewEngine : VirtualPathProviderViewEngine
    {
        // To change search location should override (properties):
        // ViewLocationFormats
        // MasterLocationFormats
        // PartialViewLocationFormats
        // AreaViewLocationFormats
        // AreaMasterLocationFormats
        // AreaPartialViewLocationFormats

        // To be supported view page file:
        // FileExtensions

        // To cache location API:
        // ViewLocationCache

        private readonly string mobileViewSuffix = "_Mobile";  // consider configuring it

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

            string adjustedViewName = useMobileDevice ? string.Format("{0}{1}", viewName, mobileViewSuffix) : viewName; // it's very important
            var result = this.FindThemeView(controllerContext, adjustedViewName, masterName, useCache, workType);

            // hack here. if cannot find mobile view for mobile mode, try again to find desktop view to render instead
            if (useMobileDevice && (result == null || result.View == null))
            {
                result = this.FindThemeView(controllerContext, viewName, masterName, useCache, WorkType.Desktop);
            }

            return result;
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
            var mobileDeviceChecker = DependencyResolver.Current.GetService<IMobileDeviceCheck>();
            bool useMobileDevice = mobileDeviceChecker.MobileDeviceIsAvailable(controllerContext.HttpContext);
            var workType = useMobileDevice ? WorkType.Mobile : WorkType.Desktop; // extend it if more work type

            string adjustedPartialViewName = useMobileDevice ? string.Format("{0}{1}", partialViewName, mobileViewSuffix) : partialViewName; // it's very important
            var result = this.FindThemePartialView(controllerContext, adjustedPartialViewName, useCache, workType);

            // hack here. if cannot find mobile view for mobile mode, try again to find desktop view to render instead
            if (useMobileDevice && (result == null || result.View == null))
            {
                result = this.FindThemePartialView(controllerContext, partialViewName, useCache, WorkType.Desktop);
            }

            return result;
        }

        protected virtual ViewEngineResult FindThemeView(ControllerContext controllerContext, string viewName, string masterName, bool useCache, WorkType workType)
        {
            return AspectF.Define.MustBeNonNull(controllerContext).MustBeNonNullOrEmpty(viewName).Return<ViewEngineResult>(() => 
            {
                var theme = MvcHelper.GetCurrentTheme(workType);
                var controllerName = controllerContext.GetControllerName();

                string[] viewSearchedLocations;
                string[] masterSearchedLocations;
                string viewPath = this.GetPath(controllerContext, this.ViewLocationFormats, this.AreaViewLocationFormats, "ViewLocationFormats", viewName, controllerName, theme, "View", useCache, workType, out viewSearchedLocations);
                string masterPath = this.GetPath(controllerContext, this.MasterLocationFormats, this.AreaMasterLocationFormats, "MasterLocationFormats", masterName, controllerName, theme, "Master", useCache, workType, out masterSearchedLocations);

                if (!string.IsNullOrEmpty(viewPath) && (!string.IsNullOrEmpty(masterPath) || string.IsNullOrEmpty(masterName)))
                {
                    return new ViewEngineResult(this.CreateView(controllerContext, viewPath, masterPath), this);
                }

                if (null == viewSearchedLocations)
                    viewSearchedLocations = new string[0];
                if (null == masterSearchedLocations)
                    masterSearchedLocations = new string[0];
                return new ViewEngineResult(viewSearchedLocations.Union(masterSearchedLocations));
            });
        }

        protected virtual ViewEngineResult FindThemePartialView(ControllerContext controllerContext, string partialViewName, bool useCache, WorkType workType)
        {
            return AspectF.Define.MustBeNonNull(controllerContext).MustBeNonNullOrEmpty(partialViewName).Return<ViewEngineResult>(() => 
            {
                var theme = MvcHelper.GetCurrentTheme(workType);
                string controllerName = controllerContext.GetControllerName();

                string[] partialViewSearchedLocations;
                string partialViewPath = this.GetPath(controllerContext, this.PartialViewLocationFormats, this.AreaPartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, controllerName, theme, "PartialView", useCache, workType, out partialViewSearchedLocations);

                if (!string.IsNullOrEmpty(partialViewPath))
                    return new ViewEngineResult(this.CreatePartialView(controllerContext, partialViewPath), this);
                else
                    return new ViewEngineResult(partialViewSearchedLocations);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext">Current Controller Context</param>
        /// <param name="locationFormats">View location format or master location format</param>
        /// <param name="areaLocationFormats">Area view location format or area master location format</param>
        /// <param name="locationFormatsPropertyName">location format name as return argument in information</param>
        /// <param name="viewName">Target View Name which is be finding</param>
        /// <param name="controllerName">Current Controller Name</param>
        /// <param name="theme">Target Theme Name which is be finding</param>
        /// <param name="cacheKeyPrefix">Prefix for parts of cache key name</param>
        /// <param name="useCache">If using cache</param>
        /// <param name="workType">Current Work Type (PC or mobile)</param>
        /// <param name="searchedLocations">Get the locations have been searched where don't have view pages</param>
        /// <returns>The path of target view</returns>
        protected virtual string GetPath(
            ControllerContext controllerContext, 
            string[] locationFormats, 
            string[] areaLocationFormats, 
            string locationFormatsPropertyName, 
            string viewName, 
            string controllerName, 
            string theme, 
            string cacheKeyPrefix, 
            bool useCache, 
            WorkType workType, 
            out string[] searchedLocations)
        {
            searchedLocations = null; // initialize searched locations

            // if view name is empty, return empty
            if (string.IsNullOrEmpty(viewName))
                return string.Empty;

            // get current area name
            string areaName = controllerContext.GetAreaName();

            // handle admin area
            if (!string.IsNullOrEmpty(areaName) && areaName.Equals("admin", StringComparison.InvariantCultureIgnoreCase))
            {
                // don't support mobile for admin
                if (workType == WorkType.Mobile)
                { 
                    searchedLocations = new string[0]; // empty because program doesn't search any location
                    return string.Empty;
                }

                var formats = areaLocationFormats.ToList();
                formats.InsertRange(0, LocationSettings.AdminLocationFormat); // make sure admin location is the default searching location
                areaLocationFormats = formats.ToArray();
            }

            var viewLocations = GetViewLocations(locationFormats, !string.IsNullOrEmpty(areaName) ? areaLocationFormats : null); // pass location format and area location format (if area is not in url, pass null)
            if (viewLocations.Count == 0)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Properties cannot be null or empty.", new object[] { locationFormatsPropertyName }));
            }

            string keyName = this.CreateCacheKey(
                cacheKeyPrefix, 
                viewName, 
                this.CheckIfRelativePath(viewName) ? string.Empty : controllerName,  // if the view name includes controller name, will not pass controller name
                areaName, 
                theme);

            if (useCache)
            {
                // use API to cache view location
                var cachedLocation = this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, keyName);
                if (null != cachedLocation)
                    return cachedLocation; // if location has been cached, return;
            }

            // if useCache is false and view location hasn't been cached.
            // No matter whether the useCache is true or not, location always inserts into cache
            // the view name doesn't includes path
            if (!this.CheckIfRelativePath(viewName))
            {
                return this.GetPathFromGeneralName(controllerContext, viewLocations, keyName, controllerName, areaName, theme, keyName, ref searchedLocations);
            }
            else
            {
                return this.GetPathFromSpecificName(controllerContext, viewName, keyName, ref searchedLocations);
            }
        }

        ///// <summary>
        ///// Get current area name from route data
        ///// </summary>
        ///// <param name="routeData">route data</param>
        ///// <returns></returns>
        //protected virtual string GetAreaName(System.Web.Routing.RouteData routeData)
        //{
        //    object obj2;
        //    if (routeData.DataTokens.TryGetValue("area", out obj2))
        //    {
        //        return (obj2 as string);
        //    }
        //    return GetAreaName(routeData.Route);
        //}

        //protected virtual string GetAreaName(System.Web.Routing.RouteBase route)
        //{
        //    var area = route as IRouteWithArea;
        //    if (area != null)
        //    {
        //        return area.Area;
        //    }
        //    var route2 = route as Route;
        //    if ((route2 != null) && (route2.DataTokens != null))
        //    {
        //        return (route2.DataTokens["area"] as string);
        //    }
        //    return null;
        //}

        protected virtual List<ViewLocation> GetViewLocations(string[] viewLocationFormats, string[] areaLocationFormats)
        {
            var list = new List<ViewLocation>(); // create empty list
            if (null != areaLocationFormats)
                list.AddRange(areaLocationFormats.Select(format => new AreaAwareViewLocation(format)).Cast<ViewLocation>());
            if(null != viewLocationFormats)
                list.AddRange(viewLocationFormats.Select(format => new ViewLocation(format)));
            return list;
        }

        protected virtual bool CheckIfRelativePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            return path.StartsWith(@"~/") ||  // relative path specifying root
                path.StartsWith(@"/"); // relative path
        }

        protected virtual string CreateCacheKey(string prefix, string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:{5}", new object[] { base.GetType().AssemblyQualifiedName, prefix, viewName, controllerName, areaName, theme });
        }

        /// <summary>
        /// If view name includes path, use this function
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="viewName"></param>
        /// <param name="cacheKey"></param>
        /// <param name="searchedLocations"></param>
        /// <returns></returns>
        protected virtual string GetPathFromSpecificName(ControllerContext controllerContext, string viewName, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = viewName;
            if (!this.SupportedFileExtension(virtualPath) ||
                !this.FileExists(controllerContext, virtualPath))
            {
                // cannot find view page
                virtualPath = string.Empty; // clear path
                searchedLocations = new string[] { virtualPath }; // set searched location, at this moment, program only searches one location
            }

            this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
            return virtualPath;
        }

        /// <summary>
        /// If view name doesn't include path, use this function
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="locations"></param>
        /// <param name="viewName"></param>
        /// <param name="controllerName"></param>
        /// <param name="areaName"></param>
        /// <param name="theme"></param>
        /// <param name="cacheKey"></param>
        /// <param name="searchedLocations"></param>
        /// <returns></returns>
        /// <remarks>TO-DO: update the logic according to MVC source code</remarks>
        protected virtual string GetPathFromGeneralName(ControllerContext controllerContext, List<ViewLocation> locations, string viewName, string controllerName, string areaName, string theme, string cacheKey, ref string[] searchedLocations)
        {
            var searchedLocationList = new List<string>();
            if (null != searchedLocations)
                searchedLocationList.AddRange(searchedLocations);
            foreach(var location in locations)
            {
                string virtualPath = location.Format(viewName, controllerName, areaName, theme); // get the entire location string
                if (this.FileExists(controllerContext, virtualPath))
                {
                    this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
                    searchedLocations = searchedLocationList.ToArray();
                    return virtualPath;
                }
                else
                {
                    searchedLocationList.Add(virtualPath); // add searched location
                }
            };

            // if cannot find view page
            searchedLocations = searchedLocationList.ToArray();
            return string.Empty;
        }

        /// <summary>
        /// Check if the file's extension is supported
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        protected virtual bool SupportedFileExtension(string virtualPath)
        {
            if (null == this.FileExtensions)
                return true; // if file extension is null, any file's type is supported

            string extensionOfFile = VirtualPathUtility.GetExtension(virtualPath).TrimStart('.');
            return this.FileExtensions.Contains(extensionOfFile, StringComparer.OrdinalIgnoreCase);
        }
    }
}

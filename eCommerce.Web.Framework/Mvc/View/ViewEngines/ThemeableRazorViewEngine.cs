using System.Collections.Generic;
using System.Web.Compilation;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.View.ViewEngines
{
    public class ThemeableRazorViewEngine : ThemeableVirtualPathProviderViewEngine
    {
        /// <summary>
        /// Initalize location formats
        /// </summary>
        public ThemeableRazorViewEngine()
        {
            this.ViewLocationFormats = new[] 
            { 
                //themes
                "~/Themes/{2}/Views/{1}/{0}.cshtml", 
                "~/Themes/{2}/Views/{1}/{0}.vbhtml", 
                "~/Themes/{2}/Views/Shared/{0}.cshtml",
                "~/Themes/{2}/Views/Shared/{0}.vbhtml",

                //default
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml", 
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml",

                //Admin
                "~/Administration/Views/{1}/{0}.cshtml",
                "~/Administration/Views/{1}/{0}.vbhtml",
                "~/Administration/Views/Shared/{0}.cshtml",
                "~/Administration/Views/Shared/{0}.vbhtml",
            };

            this.AreaViewLocationFormats = new[] 
            {
                //themes
                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml", 
                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.vbhtml", 
                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml", 
                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.vbhtml",
                                           
                //default
                "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"

                // Admin
                // will insert later
            };

            this.MasterLocationFormats = new[] 
            {
                //themes
                "~/Themes/{2}/Views/{1}/{0}.cshtml", 
                "~/Themes/{2}/Views/{1}/{0}.vbhtml", 
                "~/Themes/{2}/Views/Shared/{0}.cshtml", 
                "~/Themes/{2}/Views/Shared/{0}.vbhtml",

                //default
                "~/Views/{1}/{0}.cshtml", 
                "~/Views/{1}/{0}.vbhtml", 
                "~/Views/Shared/{0}.cshtml", 
                "~/Views/Shared/{0}.vbhtml"
            };

            this.AreaMasterLocationFormats = new[] 
            {
                //themes
                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml", 
                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.vbhtml", 
                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml", 
                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.vbhtml",

                //default
                "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                "~/Areas/{2}/Views/{1}/{0}.vbhtml", 
                "~/Areas/{2}/Views/Shared/{0}.cshtml", 
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            this.PartialViewLocationFormats = new[] 
            { 
                 //themes
                 "~/Themes/{2}/Views/{1}/{0}.cshtml",
                 "~/Themes/{2}/Views/{1}/{0}.vbhtml", 
                 "~/Themes/{2}/Views/Shared/{0}.cshtml", 
                 "~/Themes/{2}/Views/Shared/{0}.vbhtml",

                 //default
                 "~/Views/{1}/{0}.cshtml", 
                 "~/Views/{1}/{0}.vbhtml", 
                 "~/Views/Shared/{0}.cshtml", 
                 "~/Views/Shared/{0}.vbhtml",

                 //Admin
                 "~/Administration/Views/{1}/{0}.cshtml",
                 "~/Administration/Views/{1}/{0}.vbhtml",
                 "~/Administration/Views/Shared/{0}.cshtml",
                 "~/Administration/Views/Shared/{0}.vbhtml",
            };

            this.AreaPartialViewLocationFormats = new[] 
            { 
                 //themes
                 "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
                 "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.vbhtml",
                 "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml", 
                 "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.vbhtml",
                                                 
                 //default
                 "~/Areas/{2}/Views/{1}/{0}.cshtml",
                 "~/Areas/{2}/Views/{1}/{0}.vbhtml", 
                 "~/Areas/{2}/Views/Shared/{0}.cshtml", 
                 "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            // only support mvc view page currently
            this.FileExtensions = new[] { "cshtml", "vbhtml" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        /// <remarks>Write logic according to MVC source code. About BuildManager.GetObjectFactory,
        /// reference to : http://msdn.microsoft.com/en-us/library/system.web.compilation.buildmanager.getobjectfactory.aspx</remarks>
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return null != BuildManager.GetObjectFactory(virtualPath, false); // find if there is an object can represent virtual path
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string layoutPath = null;
            var runViewStartPages = false;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, layoutPath, runViewStartPages, fileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            string layoutPath = masterPath;
            var runViewStartPages = true;
            IEnumerable<string> fileExtensions = this.FileExtensions;
            return new RazorView(controllerContext, viewPath, layoutPath, runViewStartPages, fileExtensions);
        }
    }
}

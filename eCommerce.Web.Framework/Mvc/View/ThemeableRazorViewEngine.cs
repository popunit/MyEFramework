using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.View
{
    public class ThemeableRazorViewEngine : ThemeableVirtualPathProviderViewEngine
    {
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
            throw new NotImplementedException();
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            throw new NotImplementedException();
        }
    }
}

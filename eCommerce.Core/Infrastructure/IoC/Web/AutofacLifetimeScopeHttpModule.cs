using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using eCommerce.Core.Common;

namespace eCommerce.Core.Infrastructure.IoC.Web
{
    /// <summary>
    /// Autofac scope pre-request
    /// </summary>
    public class AutofacLifetimeScopeHttpModule : IHttpModule
    {
        private static readonly object tag = "AutofacWebRequest"; // it is fixed, required by autofac MVC pre-request instance


        private static ILifetimeScope lifetimeScope
        {
            get
            {
                return HttpContext.Current.Items[typeof(ILifetimeScope)] as ILifetimeScope;
            }
            set 
            {
                HttpContext.Current.Items[typeof(ILifetimeScope)] = value;
            }
        }

        /// <summary>
        /// [TO-DO] Try to figure out how to use it and test the performance
        /// </summary>
        private static RequstTempData tempData
        {
            get
            {
                return HttpContext.Current.Items[typeof(RequstTempData)] as RequstTempData;
            }
            set
            {
                HttpContext.Current.Items[typeof(RequstTempData)] = value;
            }
        }

        public static ILifetimeScope GetLifetimeScope(IContainer container)
        {
            if (IsValid())
            {
                if (null == lifetimeScope)
                    lifetimeScope = container.BeginLifetimeScope(tag);
                return lifetimeScope;
            }
            else
                return null;
        }
        
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            tempData = new RequstTempData();
            context.EndRequest += (o, e) => 
            {
                if (lifetimeScope != null)
                    lifetimeScope.Dispose();
            };
        }       

        public static bool IsValid()
        {
            return HttpContext.Current != null;
        }
    }
}

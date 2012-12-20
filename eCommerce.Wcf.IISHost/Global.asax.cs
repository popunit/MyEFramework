using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;
using Autofac.Integration.Mvc;
using eCommerce.Core;
using eCommerce.Core.Configuration;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;
using eCommerce.Data;
using eCommerce.Data.DataProvider;
using eCommerce.Data.Repositories;
using eCommerce.Wcf.Services.Users;
using eCommerce.Core.Caching;
using eCommerce.Data.Domain.Common;
using eCommerce.Wcf.Services.Common;

namespace eCommerce.Wcf.IISHost
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>http://www.eidias.com/Blog/2012/2/13/simple-wcf-hosting-wcf-service-by-autofac-in-aspnet-mvc-3</remarks>
        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
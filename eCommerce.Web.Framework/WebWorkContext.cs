using eCommerce.Services;
using eCommerce.Services.Authentication;
using eCommerce.Services.WcfClient;
using eCommerce.Services.WcfClient.Entities;
using eCommerce.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eCommerce.Services.Extensions;
using eCommerce.Core.Common;
using eCommerce.Services.Users;
using eCommerce.Core;
using eCommerce.Core.Common.Web;

namespace eCommerce.Web.Framework
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Web work context should not include Settings. Settings should be included into
    /// StoreStateSettings</remarks>
    public class WebWorkContext : WebWorkContextBase
    {
        private readonly HttpContextBase httpContext;
        private readonly IHttpHelper httpHelper;
        private readonly IUserDataService userService;
        private readonly IAuthenticationService authenticationService;

        public WebWorkContext(
            HttpContextBase httpContext,
            IHttpHelper httpHelper,
            IUserDataService userService,
            IAuthenticationService authenticationService)
        {
            this.httpContext = httpContext;
            this.httpHelper = httpHelper;
            this.userService = userService;
            this.authenticationService = authenticationService;
        }

        public override User CurrentUser
        {
            get
            {
                if (base.CurrentUser.IsNull())
                {
                    var user = GetCurrentUser();
                    base.CurrentUser = user;
                    return user;
                }
                else
                    return base.CurrentUser;
            }
            set
            {
                base.CurrentUser = value;
            }
        }

        protected User GetCurrentUser()
        {
            //if (base.CurrentUser != null)
            //    return this.CurrentUser;

            User currentUser = null;
            if (httpContext.HasRequest())
            { 
                // TO-DO: to handle differnet situation
                currentUser = authenticationService.GetAuthenticatedUser();
                if (currentUser.IsValid())
                {
                    // TO-DO
                }
                else
                {
                    var userCookie = httpHelper.GetCookie(Const.USER_COOKIE);
                    if (userCookie.IsValid())
                    {
                        Guid guid;
                        if (Guid.TryParse(userCookie.Value, out guid)) // get user guid
                        {

                        }
                    }

                    if (!currentUser.IsValid())
                    {
                        currentUser = userService.CreateGuest();
                    }
                }
            }

            // TO-DO
            return currentUser;
        }
    }
}

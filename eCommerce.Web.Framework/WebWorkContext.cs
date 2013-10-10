using eCommerce.Core;
using eCommerce.Core.Common;
using eCommerce.Core.Common.Web;
using eCommerce.Services;
using eCommerce.Services.Authentication;
using eCommerce.Services.Extensions;
using eCommerce.Services.Users;
using eCommerce.Services.WcfClient.Entities;
using eCommerce.Web.Framework.Mvc;
using System;
using System.Web;

namespace eCommerce.Web.Framework
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Web work context should not include Settings. Settings should be included into
    /// StoreStateSettings</remarks>
    public class WebWorkContext : WebWorkContextBase
    {
        private readonly HttpContextBase _httpContext;
        private readonly IHttpHelper _httpHelper;
        private readonly IUserDataService _userService;
        private readonly IAuthenticationService _authenticationService;

        public WebWorkContext(
            HttpContextBase httpContext,
            IHttpHelper httpHelper,
            IUserDataService userService,
            IAuthenticationService authenticationService)
        {
            this._httpContext = httpContext;
            this._httpHelper = httpHelper;
            this._userService = userService;
            this._authenticationService = authenticationService;
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
            if (_httpContext.HasRequest())
            { 
                // TO-DO: to handle differnet situation
                currentUser = _authenticationService.GetAuthenticatedUser();
                if (currentUser.IsValid())
                {
                    // TO-DO
                }
                else
                {
                    var userCookie = _httpHelper.GetCookie(Const.UserCookie);
                    if (userCookie.IsValid())
                    {
                        Guid guid;
                        if (Guid.TryParse(userCookie.Value, out guid)) // get user guid
                        {

                        }
                    }

                    if (!currentUser.IsValid())
                    {
                        currentUser = _userService.CreateGuest();
                    }
                }
            }

            // TO-DO
            return currentUser;
        }
    }
}

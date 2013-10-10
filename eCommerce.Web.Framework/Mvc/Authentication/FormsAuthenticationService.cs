using eCommerce.Core.Data;
using eCommerce.Services.Authentication;
using eCommerce.Services.Extensions;
using eCommerce.Services.Users;
using eCommerce.Services.WcfClient.Entities;
using System;
using System.Web;
using System.Web.Security;

namespace eCommerce.Web.Framework.Mvc.Authentication
{
    /// <summary>
    /// MVC Service for authenticated user
    /// </summary>
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly IUserDataService _userService;
        private readonly UserSettings _settings;
        private readonly TimeSpan _expiration;

        private User _targetUser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext">httpContext from Dependency resolver</param>
        /// <param name="userService">user service from Dependency resolver</param>
        /// <param name="settings">user characteristic from Dependency resolver</param>
        public FormsAuthenticationService(
            HttpContextBase httpContext,
            IUserDataService userService,
            UserSettings settings)
        {
            this._httpContext = httpContext;
            this._userService = userService;
            this._settings = settings;
            this._expiration = FormsAuthentication.Timeout; // require Minimal AspNetHostingPermission
        }

        /// <summary>
        /// Save encrypted cookie by authentication ticket
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistentCookie"></param>
        public void SignIn(User user, bool isPersistentCookie)
        {
            var localTime = DateTime.UtcNow.ToLocalTime();
            var ticket = new FormsAuthenticationTicket(
                1, // version
                user.GetUserNameOrEmail(), // cookie's name
                localTime,
                localTime.Add(this._expiration),
                isPersistentCookie,
                user.GetUserNameOrEmail(), // cookie's content
                FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                HttpOnly = true
            }; // create encrypted cookie
            if (ticket.IsPersistent) // the cookie's expiration will not later than form authentication timeout even if the ticket is persistent
            {
                cookie.Expires = ticket.Expiration; // in actual, the cookie's expires will be set as form authentication timeout default. Reference: http://stackoverflow.com/questions/10345817/what-is-the-purpose-of-formsauthenticationticket-ispersistent-property
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
                cookie.Domain = FormsAuthentication.CookieDomain;
            _httpContext.Response.Cookies.Add(cookie);
            _targetUser = user;
        }

        public void SignOut()
        {
            _targetUser = null; // dispose user
            FormsAuthentication.SignOut();
        }

        public User GetAuthenticatedUser()
        {
            if (null != _targetUser)
                return _targetUser;
            if (!_httpContext.HasRequest() ||                        // httpContext is not existed
                !_httpContext.Request.IsAuthenticated ||             // request is not authenticated
                !(_httpContext.User.Identity is FormsIdentity))      // identity is not forms identity (user other authentication mechanism)
                return null;

            var identity = _httpContext.User.Identity as FormsIdentity;
            var user = GetAuthenticatedUserFromTicket(identity.Ticket);

            if (user.IsValid() && user.HasRegisteredRole())
                _targetUser = user;
            return _targetUser;
        }

        private User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (null == ticket)
                throw new ArgumentNullException("ticket");

            if (string.IsNullOrWhiteSpace(ticket.UserData))
            {
                return null;
            }
            else
            {
                return _userService.GetUserByNameOrEmail(ticket.UserData);
            }
        }
    }
}

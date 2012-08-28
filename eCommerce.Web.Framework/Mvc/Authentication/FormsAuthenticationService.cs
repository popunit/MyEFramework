using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using eCommerce.Core.Data;
using eCommerce.Services.Authentication;
using eCommerce.Services.WcfClient.Entities;
using eCommerce.Services.Users;

namespace eCommerce.Web.Framework.Mvc.Authentication
{
    /// <summary>
    /// MVC Service for authenticated user
    /// </summary>
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase httpContext;
        private readonly IUserDataService userService;
        private readonly UserSettings settings;
        private readonly TimeSpan expiration;

        private User targetUser;

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
            this.httpContext = httpContext;
            this.userService = userService;
            this.settings = settings;
            this.expiration = FormsAuthentication.Timeout; // require Minimal AspNetHostingPermission
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
                localTime.Add(this.expiration),
                isPersistentCookie,
                user.GetUserNameOrEmail(), // cookie's content
                FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)); // create encrypted cookie
            cookie.HttpOnly = true;
            if (ticket.IsPersistent) // the cookie's expiration will not later than form authentication timeout even if the ticket is persistent
            {
                cookie.Expires = ticket.Expiration; // in actual, the cookie's expires will be set as form authentication timeout default. Reference: http://stackoverflow.com/questions/10345817/what-is-the-purpose-of-formsauthenticationticket-ispersistent-property
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
                cookie.Domain = FormsAuthentication.CookieDomain;
            httpContext.Response.Cookies.Add(cookie);
            targetUser = user;
        }

        public void SignOut()
        {
            targetUser = null; // dispose user
            FormsAuthentication.SignOut();
        }

        public User GetAuthenticatedUser()
        {
            if (null != targetUser)
                return targetUser;
            if (!httpContext.HasRequest() ||                        // httpContext is not existed
                !httpContext.Request.IsAuthenticated ||             // request is not authenticated
                !(httpContext.User.Identity is FormsIdentity))      // identity is not forms identity (user other authentication mechanism)
                return null;

            var identity = httpContext.User.Identity as FormsIdentity;
            var user = GetAuthenticatedUserFromTicket(identity.Ticket);

            if (user.IsValid() && user.HasRegisteredRole())
                targetUser = user;
            return targetUser;
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
                return userService.GetUserByNameOrEmail(ticket.UserData);
            }
        }
    }
}

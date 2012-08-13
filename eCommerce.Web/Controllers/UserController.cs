using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Core.Data;
using eCommerce.Web.Framework.Mvc.Controllers;
using eCommerce.Web.Models.User;

namespace eCommerce.Web.Controllers
{
    public class UserController : WebControllerBase
    {
        private readonly UserSettings userSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>The controller must be registered into container, otherwise
        /// we cannot call the non-default constructor because we cannot get the 
        /// implement for these arguments</remarks>
        public UserController(UserSettings userSettings)
        {
            this.userSettings = userSettings;
        }

        /// <summary>
        /// Login for user
        /// </summary>
        /// <param name="isGuest">gust login or not</param>
        /// <returns></returns>
        /// <remarks>Two login ways: anonymous guest login and authenticated login</remarks>
        public ActionResult Login(bool isGuest = false)
        {
            // create new model without information to wait for customer to input
            var model = new LoginViewModel 
            {
                IsLoginAsGuest = isGuest,
                UsingUserEmail = userSettings.UsingUserEmail,
                // TO-DO: Set Captcha or not 
            };

            return View(model);
        }

    }
}

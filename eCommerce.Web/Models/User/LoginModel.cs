using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerce.Web.Framework.Mvc;

namespace eCommerce.Web.Models.User
{
    public class LoginModel : WebModelBase
    {
        public bool IsLoginAsGuest { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public bool UsingUserEmail { get; set; } // enable username or email
        public string Password { get; set; }

        public bool RememberMe { get; set; } // checkbox for remember me
        public bool DisplayCaptcha { get; set; } // Captcha for avoiding sign in/out too many times
    }
}
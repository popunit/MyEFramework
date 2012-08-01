using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Services.WcfClient.Entities;

namespace eCommerce.Services.Authentication
{
    public interface IAuthenticationService
    {
        void SignIn(User user, bool createPersistentCookie);
        void SignOut();
        User GetAuthenticatedUser();
    }
}

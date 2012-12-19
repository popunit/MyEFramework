using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Services.WcfClient.Entities;

namespace eCommerce.Services.Users
{
    public interface IUserDataService
    {
        User GetUserByName(string userName);

        User GetUserByEmail(string email);

        User GetUserByNameOrEmail(string userNameOrEmail);

        User GetUserByGuid(Guid guid);

        bool AddUserRole(UserRole userRole);

        User CreateGuest();

        bool UpdateCustomerRole(UserRole userRole);
    }
}

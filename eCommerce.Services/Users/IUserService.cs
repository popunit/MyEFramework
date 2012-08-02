using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Services.WcfClient.Entities;

namespace eCommerce.Services.Users
{
    public interface IUserService
    {
        bool SaveUserCharacteristic(User user, string key, string value);

        User GetUserByName(string userName);

        User GetUserByEmail(string email);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Services.WcfClient;
using eCommerce.Services.WcfClient.Entities;

namespace eCommerce.Services.Users
{
    public class UserService : IUserService
    {
        public bool SaveUserCharacteristic(User user, string key, string value)
        {
            return AspectF.Define.MustBeNonNull(user).Return<bool>(() => 
            {
                bool isSucceed = false;
                using (UserServiceClient proxy = new UserServiceClient("BasicHttpBinding_IUserService"))
                {
                    isSucceed = proxy.SaveUserCharacteristic(user.Id, key, value);
                }
                return isSucceed;
            });
        }


        public User GetUserByName(string userName)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}

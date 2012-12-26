using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Services.WcfClient;
using eCommerce.Services.WcfClient.Entities;
using System.ServiceModel;
using eCommerce.Exception;
using eCommerce.Services.Extensions.NoAOP;

namespace eCommerce.Services.Users
{
    public class UserDataService : IUserDataService
    {
        public User GetUserByName(string userName)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(userName).
                WcfClient<IUserService>().Return<User>((aspect) =>
            {
                return aspect.Proxy.GetUserByName(userName);
            });
        }

        public User GetUserByEmail(string email)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(email).
                WcfClient<IUserService>().Return<User>((aspect) =>
            {
                return aspect.Proxy.GetUserByEmail(email);
            });
        }

        public User GetUserByNameOrEmail(string userNameOrEmail)
        {
            var userSettings = EngineContext.Current.Resolve<UserSettings>();
            return userSettings.UsingUserEmail ?
                GetUserByEmail(userNameOrEmail) :
                GetUserByName(userNameOrEmail);
        }

        public User GetUserByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public bool AddUserRole(UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public User CreateGuest()
        {
            return AspectF.Define.WcfClient<IUserService>().Return<User>((aspect) => 
            {
                return aspect.Proxy.CreateGuest();
            });
        }

        public bool UpdateCustomerRole(UserRole userRole)
        {
            throw new NotImplementedException();
        }
    }
}

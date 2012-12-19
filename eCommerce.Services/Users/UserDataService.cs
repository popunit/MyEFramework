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

namespace eCommerce.Services.Users
{
    public class UserDataService : IUserDataService
    {
        public User GetUserByName(string userName)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(userName).Return<User>(() =>
            {
                var proxy = ProxyFactory.Create<IUserService, BasicHttpBinding>();
                try
                {
                    return proxy.GetUserByName(userName);
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }

        public User GetUserByEmail(string email)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(email).Return<User>(() =>
            {
                var proxy = ProxyFactory.Create<IUserService, BasicHttpBinding>();
                try
                {
                    return proxy.GetUserByEmail(email);
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
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
            return AspectF.Define.Return<User>(() =>
            {
                var proxy = ProxyFactory.Create<IUserService, BasicHttpBinding>();
                try
                {
                    return proxy.CreateGuest();
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }

        public bool UpdateCustomerRole(UserRole userRole)
        {
            throw new NotImplementedException();
        }
    }
}

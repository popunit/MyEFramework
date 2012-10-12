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
        public bool SaveUserCharacteristic(User user, string key, string value)
        {
            return AspectF.Define.MustBeNonNull(user).Return<bool>(() => 
            {
                //bool isSucceed = false;
                //using (UserServiceClient proxy = new UserServiceClient("BasicHttpBinding_IUserService"))
                //{
                //    isSucceed = proxy.SaveUserCharacteristic(user.Id, key, value);
                //}
                //return isSucceed;

                bool isSucceed = false;
                var proxy = ProxyFactory.Create<IUserService, BasicHttpBinding>();
                try
                {
                    isSucceed = proxy.SaveUserCharacteristic(user.Id, key, value);
                    return isSucceed;
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }


        public User GetUserByName(string userName)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(userName).Return<User>(() =>
            {
                //using (UserServiceClient proxy = new UserServiceClient("BasicHttpBinding_IUserService"))
                //{
                //    return proxy.GetUserByName(userName);
                //}

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
                //using (UserServiceClient proxy = new UserServiceClient("BasicHttpBinding_IUserService"))
                //{
                //    return proxy.GetUserByEmail(email);
                //}

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using eCommerce.Core;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Data;
using eCommerce.Data.Domain.Users.Entities;
using eCommerce.Wcf.Services.Contracts.Users;

namespace eCommerce.Wcf.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserCharacteristic> userCharacteristicRepository;
        private readonly ILifetimeScope container;

        public UserService()
        {
            container = AutofacHostFactory.Container;
            this.userRepository = container.Resolve<IRepository<User>>();
            this.userCharacteristicRepository = container.Resolve<IRepository<UserCharacteristic>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>TO-DO: I'd like to build the operation as async</remarks>
        public bool SaveUserCharacteristic(long userId, string key, string value)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(key, value).Return<bool>(()=>
            {
                var user = userRepository.GetByKeys(userId);
                if (null == user) // user cannot be null
                    return false;
                var characteristic = user.UserCharacteristics.FirstOrDefault(
                    uc => uc.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
                if (null != characteristic) // update
                {
                    characteristic.Value = value; // TO-DO: value should be check
                    return UpdateUserCharacteristic(characteristic);
                }
                else // insert
                {
                    UserCharacteristic uc = new UserCharacteristic();
                    uc.User = user;
                    uc.Key = key;
                    uc.Value = value;
                    return InsertUserCharacteristic(uc);
                }
            });
        }

        public User GetUserByName(string userName)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(userName).Return<User>(() => 
            {
                //return userRepository.Table.Where(u => u.UserName == userName)
                //    .OrderBy(u => u.Id).FirstOrDefault();
                return (from u in userRepository.Table
                        where u.UserName == userName
                        orderby u.Id
                        select u).FirstOrDefault();
            });
        }

        public User GetUserByEmail(string email)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(email).Return<User>(() => 
            {
                return (from u in userRepository.Table
                        where u.Email == email
                        orderby u.Id
                        select u).FirstOrDefault();
            });
        }

        private bool UpdateUserCharacteristic(UserCharacteristic userCharacteristic)
        {
            return AspectF.Define.MustBeNonNull(userCharacteristic).Return<bool>(()=>
            {
                //TO-DO: EventEmit to send change notification
                return this.userCharacteristicRepository.Update(userCharacteristic);
            });
        }

        private bool InsertUserCharacteristic(UserCharacteristic userCharacteristic)
        {
            return AspectF.Define.MustBeNonNull(userCharacteristic).Return<bool>(() =>
            {
                //TO-DO: EventEmit to send change notification
                return this.userCharacteristicRepository.Insert(userCharacteristic);
            });
        }
    }
}

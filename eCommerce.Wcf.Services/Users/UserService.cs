using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public UserService(
            IRepository<User> repository,
            IRepository<UserCharacteristic> userCharacteristicRepository)
        {
            this.userRepository = repository;
            this.userCharacteristicRepository = userCharacteristicRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>TO-DO: I'd like to build the operation as async</remarks>
        public bool UpdateUserCharacteristic(string userId, string key, string value)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(userId, key, value).Return<bool>(()=>
            {
                var user = userRepository.GetByKeys(userId);
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

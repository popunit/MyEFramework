using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Data.Domain.Users.Entities;

namespace eCommerce.Wcf.Services.Contracts.Users
{
    /// <summary>
    /// GIUD for User entity
    /// </summary>
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// Update or insert user characteristic to database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        bool SaveUserCharacteristic(long userId, string key, string value);
    }
}

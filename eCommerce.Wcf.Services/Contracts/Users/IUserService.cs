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
        [OperationContract]
        User GetUserByName(string userName);

        [OperationContract]
        User GetUserByEmail(string email);

        [OperationContract]
        User GetUserByGuid(Guid guid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemRoleName">Role Group Name</param>
        /// <returns></returns>
        [OperationContract]
        UserRole[] GetUserRolesBySystemName(string systemRoleName);

        [OperationContract]
        bool AddUserRole(UserRole userRole);

        [OperationContract]
        User CreateGuest();

        [OperationContract]
        bool UpdateCustomerRole(UserRole userRole);
    }
}

using eCommerce.Core;
using eCommerce.Core.Caching;
using eCommerce.Core.Common;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Data.Domain.Users.Entities;
using eCommerce.Data.Resources;
using eCommerce.Exception;
using eCommerce.Wcf.Services.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;

namespace eCommerce.Wcf.Services.Users
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true|false)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserRole> userRoleRepository;
        //private readonly ILifetimeScope container;
        private readonly ICacheManager cacheManager;

        public UserService()
        {
            //container = AutofacHostFactory.Container;
            this.userRepository = EngineContext.Current.Resolve<IRepository<User>>();
            this.userRoleRepository = EngineContext.Current.Resolve<IRepository<UserRole>>();
            this.cacheManager = EngineContext.Current.Resolve<ICacheManager>();
        }

        #region GET
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

        public User GetUserByGuid(Guid guid)
        {
            return AspectF.Define.MustBeNonDefault<Guid>(guid).Return<User>(() =>
            {
                return (from u in userRepository.Table
                        where u.UserGuid == guid
                        orderby u.Id
                        select u).FirstOrDefault();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemRoleName"></param>
        /// <returns></returns>
        /// <remarks>Should cache here because the data won't be changed frequently</remarks>
        public UserRole[] GetUserRolesBySystemName(string systemRoleName)
        {
            return AspectF.Define.MustBeNonNullOrEmpty(systemRoleName).Return<UserRole[]>(() => 
            {
                string key = string.Format(Constants.CACHE_USERROLE_FORMAT,systemRoleName);
                return cacheManager.GetOrAdd<UserRole[]>(key, () => 
                {
                    return (from ur in userRoleRepository.Table
                           where ur.SystemName.Equals(systemRoleName, StringComparison.InvariantCultureIgnoreCase)
                           orderby ur.Id
                           select ur).ToArray();
                });
            });
        }

        #endregion

        #region INSERT
        public bool AddUserRole(UserRole userRole)
        {
            return AspectF.Define.MustBeNonNull(userRole).Return<bool>(() =>
                {
                    bool succeed = userRoleRepository.Insert(userRole);
                    if (succeed)
                        cacheManager.RemoveByPattern(Constants.CACHE_USERROLE_PATTERN);

                    // event notification

                    return succeed;
                });
        }

        public User CreateGuest()
        {
            return AspectF.Define.Return<User>(() => 
            {
                var user = new User 
                {
                    Actived = true,
                    CreateTime = DateTime.UtcNow,
                    ActiveTime = DateTime.UtcNow,
                    UserRoles = new List<UserRole>() // initial user role
                };

                var roles = GetUserRolesBySystemName(SystemUserRoleNameResource.Guest);
                if (roles.IsNullOrEmpty())
                {
                    throw new CommonException(
                        string.Format("Cannot find role according to system name '{0}'",
                        SystemUserRoleNameResource.Guest));
                }

                user.UserRoles.AddRange(roles);
                userRepository.Insert(user);
                return user;
            });
        }

        #endregion

        #region UPDATE
        public bool UpdateCustomerRole(UserRole userRole)
        {
            return AspectF.Define.MustBeNonNull(userRole).Return<bool>(() =>
                {
                    bool succeed = userRoleRepository.Update(userRole);
                    if (succeed)
                        cacheManager.RemoveByPattern(Constants.CACHE_USERROLE_PATTERN);

                    // event notification

                    return succeed;
                });
        }

        #endregion

    }
}

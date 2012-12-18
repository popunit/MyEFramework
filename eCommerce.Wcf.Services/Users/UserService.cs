using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using eCommerce.Core;
using eCommerce.Core.Common;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Data;
using eCommerce.Data.Domain.Users.Entities;
using eCommerce.Wcf.Services.Contracts.Users;

namespace eCommerce.Wcf.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserRole> userRoleRepository;
        private readonly ILifetimeScope container;
        private readonly ICacheManager cacheManager;

        public UserService()
        {
            container = AutofacHostFactory.Container;
            this.userRepository = container.Resolve<IRepository<User>>();
            this.userRoleRepository = container.Resolve<IRepository<UserRole>>();
            this.cacheManager = container.Resolve<ICacheManager>();
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
    }
}

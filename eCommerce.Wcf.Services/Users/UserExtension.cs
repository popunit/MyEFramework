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
using eCommerce.Data.Repositories;
using eCommerce.Wcf.Services.Contracts.Users;

namespace eCommerce.Wcf.Services.Users
{
    /// <summary>
    /// Supplement for User service
    /// </summary>
    /// <remarks>New Async pattern: http://blog.vuscode.com/malovicn/archive/2012/01/21/what-is-new-in-wcf-in-net-4-5-taskt-and-async.aspx</remarks>
    public class UserExtension : IUserExtension
    {
        private readonly IRepository<User> repository;
        private readonly ILifetimeScope container;

        public UserExtension()
        {
            container = AutofacHostFactory.Container;
            this.repository = container.Resolve<IRepository<User>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //public UserCharacteristic GetCharacteristic(int userId, string key)
        public string GetCharacteristicValue(long userId, string key)
        {
            return AspectF.Define.MustBeNonNull(userId).Return<string>(() => 
            {
                var characteristic = repository.GetByKeys(userId).UserCharacteristics.FirstOrDefault(
                    uc => uc.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
                if (null == characteristic)
                    return null;
                return characteristic.Value;
            });
        }
    }
}

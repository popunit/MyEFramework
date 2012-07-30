using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Data.Domain.Users.Entities;

namespace eCommerce.Wcf.Services.Contracts.Users
{
    [ServiceContract]
    public interface IUserExtension
    {
        [OperationContract]
        string GetCharacteristicValue(long userId, string key);
    }
}

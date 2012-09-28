using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Wcf.Services.Contracts.Common
{
    [ServiceContract]
    public interface IDataInfoService
    {
        [OperationContract]
        bool DatabaseIsInstalled();
    }
}

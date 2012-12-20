using eCommerce.Services.WcfClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Common
{
    public class DataInfoDataService : IDataInfoDataService
    {
        public bool DatabaseIsInstalled()
        {
            var proxy = ProxyFactory.Create<IDataInfoService>();
            try
            {
                return proxy.DatabaseIsInstalled();
            }
            finally
            {
                if (null != proxy)
                    (proxy as ICommunicationObject).Close();
            }
        }
    }
}

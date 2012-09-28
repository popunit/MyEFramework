using eCommerce.Services.WcfClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Common
{
    public class DataInfoDataService : IDataInfoDataService
    {
        public bool DatabaseIsInstalled()
        {
            using (DataInfoServiceClient proxy = new DataInfoServiceClient())
            {
                return proxy.DatabaseIsInstalled();
            }
        }
    }
}

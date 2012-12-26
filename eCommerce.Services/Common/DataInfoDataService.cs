using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Exception;
using eCommerce.Services.WcfClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Services.Extensions.NoAOP;

namespace eCommerce.Services.Common
{
    public class DataInfoDataService : IDataInfoDataService
    {
        public bool DatabaseIsInstalled()
        {
            return AspectF.Define.WcfClient<IDataInfoService>().Return<bool>((aspect) =>
            {
                return aspect.Proxy.DatabaseIsInstalled();
            });
        }
    }
}

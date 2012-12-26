using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Extensions.NoAOP
{
    public static class AspectFExtensions
    {
        public static AspectF WcfClient<T>(this AspectF aspect)
        {
            return aspect.Combine((work) =>
            {
                var proxy = ProxyFactory.Create<T>();
                aspect.Proxy = proxy;
                try
                {
                    work();
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
                catch (FaultException<ExceptionDetail> ex)
                {
                    (proxy as ICommunicationObject).Abort();
                    throw new CommonException(ex.Detail.Message);
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                catch (CommonException ex)
                {
                    throw ex;
                }
            });
        }
    }
}

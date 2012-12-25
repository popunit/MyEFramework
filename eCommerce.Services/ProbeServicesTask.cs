using eCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;
using System.ServiceModel;
using eCommerce.Exception;

namespace eCommerce.Services
{
    public class ProbeServicesTask : IWarmupTask
    {
        public void Execute()
        {
            var types = typeof(ProbeServicesTask).GetTypesFromCurrentAssembly();
            List<Task<bool>> tasks = new List<Task<bool>>();
            types.Where(t => t.HasCustomAttribute<ServiceContractAttribute>()).ForEach(t => 
            {
                var currentType = t;
                tasks.Add(ProxyFactory.FindAsync(t));
            });

            var continueResult = Task.Factory.ContinueWhenAll(tasks.ToArray(), ts => 
            {
                foreach (var t in ts)
                {
                    if (!t.Result)
                        throw new CommonException("Cannot find all the services"); // TO-DO: throw specific service info
                }
            });

            try
            {
                continueResult.Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.Flatten().InnerExceptions)
                {
                    throw;
                }
            }
        }

        public int Order
        {
            get { return 1; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceModelEx;
using System.ServiceModel.Discovery;
using System.ServiceModel;
using System.ServiceModel.Channels;
using eCommerce.Core.Common;

namespace eCommerce.Services
{
    public class ProxyFactory
    {
        public static TContract Create<TContract, TBinding>()
            where TBinding : Binding, new()
        {
            var dict = SingletonDictionary<Type, EndpointAddress>.Instance;
            var key = typeof(TContract);

            EndpointAddress address = null;
            Binding binding = new TBinding();
            uint retry = 0;
            if (dict.Keys.Contains(key))
            {
                address = dict[key];
                try
                {
                    TContract proxy = ChannelFactory<TContract>.CreateChannel(binding, address);
                    if (null != proxy)
                        return proxy;
                }
                catch
                {
                    // do nothing
                }
                finally
                {
                    retry = 1;
                }
            }

            DiscoveryClient client = new DiscoveryClient(new UdpDiscoveryEndpoint());
            var criteria = new FindCriteria(typeof(TContract));
            criteria.Duration = new TimeSpan(0, 0, 0, 30);
            criteria.MaxResults = 1; // only get one matched service
            var discovered = client.Find(criteria);
            client.Close();

            if (discovered.Endpoints.Count == 0)
                return default(TContract); // cannot find services
            address = discovered.Endpoints[0].Address;

            if (retry == 1)
                dict[key] = address;
            else
                dict.Add(key, address);

            try
            {
                return ChannelFactory<TContract>.CreateChannel(binding, address);
            }
            catch
            {
                return default(TContract);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Discovery;
using System.ServiceModel;
using System.ServiceModel.Channels;
using eCommerce.Core.Common;
using System.ServiceModel.Description;
using eCommerce.Wcf.Framework.Metadata;
using eCommerce.Exception;

namespace eCommerce.Services
{
    public class ProxyFactory
    {
        public static TContract Create<TContract>()
        {
            var dict = SingletonDictionary<Type, KeyValuePair<EndpointAddress, Binding>>.Instance; // TO-DO, one type has more than one endpointaddress according to contracts
            var key = typeof(TContract);

            EndpointAddress address = null;
            if (dict.Keys.Contains(key))
            {
                try
                {
                    TContract proxy = ChannelFactory<TContract>.CreateChannel(dict[key].Value, dict[key].Key);
                    if (null != proxy)
                        return proxy;
                }
                catch
                {
                    // do nothing
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

            try
            {
                ServiceEndpoint[] serviceEndpoint = address.GetEndpoints();
                if (serviceEndpoint.IsNull() || serviceEndpoint.Length != 1)
                    throw new CommonException("More than one endpoint be found!");
                var binding = serviceEndpoint[0].Binding;
                var keyValuePair = new KeyValuePair<EndpointAddress, Binding>(address, binding);
                if (dict.ContainsKey(key))
                    dict[key] = keyValuePair;
                else
                    dict.Add(key, keyValuePair);

                return ChannelFactory<TContract>.CreateChannel(binding, address);
            }
            catch
            {
                return default(TContract);
            }
        }
    }
}

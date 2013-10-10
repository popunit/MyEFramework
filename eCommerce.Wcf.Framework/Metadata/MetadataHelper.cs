using eCommerce.Core.Common;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace eCommerce.Wcf.Framework.Metadata
{
    public static class MetadataHelper
    {
        public static ServiceEndpoint[] GetEndpoints(this EndpointAddress endpointAddress)
        {
            if (endpointAddress.IsNull() || endpointAddress.Uri == default(Uri))
                return null;
            Uri address = endpointAddress.Uri;

            // make sure url is end with '?wsdl'
            //if (address.AbsoluteUri.EndsWith("?wsdl", true, CultureInfo.InvariantCulture) == false)
            //{
            //    string wsdlAddress = address.AbsoluteUri + "?wsdl";
            //    address = new Uri(wsdlAddress);
            //}
            address = address.EnsureWsdl();
            ServiceEndpointCollection serviceEndpoints = null;
            BindingElement bindingElement = null;

            // address is http/https
            if (address.Scheme == Uri.UriSchemeHttp || address.Scheme == Uri.UriSchemeHttps)
            {
                if (address.Scheme == Uri.UriSchemeHttp)
                {
                    var httpBindingElement = new HttpTransportBindingElement();
                    httpBindingElement.MaxReceivedMessageSize *= 5; // default set
                    bindingElement = httpBindingElement;
                }
                else
                {
                    var httpsBindingElement = new HttpsTransportBindingElement();
                    httpsBindingElement.MaxReceivedMessageSize *= 5; // default set
                    bindingElement = httpsBindingElement;
                }

                try
                {
                    var binding = new CustomBinding(bindingElement);
                    var mexClient = new MetadataExchangeClient(binding);
                    var metadata = mexClient.GetMetadata(address, MetadataExchangeClientMode.HttpGet);
                    serviceEndpoints = (new WsdlImporter(metadata)).ImportAllEndpoints();
                    return serviceEndpoints.ToArray();
                }
                catch
                {
                    // do nothing, try another metadata exchange to get metadata
                }
            }

            if (address.Scheme == Uri.UriSchemeHttp)
            {
                bindingElement = new HttpTransportBindingElement();
            }
            if (address.Scheme == Uri.UriSchemeHttps)
            {
                bindingElement = new HttpsTransportBindingElement();
            }
            if (address.Scheme == Uri.UriSchemeNetTcp)
            {
                bindingElement = new TcpTransportBindingElement();
            }
            if (address.Scheme == Uri.UriSchemeNetPipe)
            {
                bindingElement = new NamedPipeTransportBindingElement();
            }

            serviceEndpoints = QueryMexEndpoints(address.AbsoluteUri, bindingElement);
            return serviceEndpoints.ToArray();
        }

        /// <summary>
        /// Download and query metadata via http-get or metadata exchange
        /// </summary>
        /// <param name="mexAddress"></param>
        /// <param name="bindingElement"></param>
        /// <returns></returns>
        private static ServiceEndpointCollection QueryMexEndpoints(string mexAddress, BindingElement bindingElement)
        {
            dynamic element = bindingElement;
            element.MaxReceivedMessageSize *= 5;

            var binding = new CustomBinding(element);

            var mexClient = new MetadataExchangeClient(binding);
            MetadataSet metadata = mexClient.GetMetadata(new EndpointAddress(mexAddress));
            MetadataImporter importer = new WsdlImporter(metadata);
            return importer.ImportAllEndpoints();
        }
    }
}

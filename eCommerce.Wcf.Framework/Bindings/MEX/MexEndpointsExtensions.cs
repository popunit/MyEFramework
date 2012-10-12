using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Wcf.Framework.Bindings.MEX
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/system.servicemodel.description.servicemetadatabehavior.aspx
    /// </remarks>
    public static class MexEndpointsExtensions
    {
        public static void EnableAllMexEndpoints(this ServiceHost host)
        {
            // Add a ServiceMetadataBehavior object to the ServiceDescription.Behaviors collection (or the <serviceMetadata> element in an application configuration file) to enable or disable the publication of service metadata. However, adding the behavior to a service is not sufficient to enable metadata publication:
            //      To enable WS-Transfer GET metadata retrieval, you must also add an endpoint to your service in which the contract is IMetadataExchange. For an example, see How To: Publish Metadata for a Service Using Code. The IMetadataExchange endpoint can be configured as can any other endpoint.
            //      To enable HTTP GET metadata retrieval, set the HttpGetEnabled property to true. For more information about the address of HTTP GET metadata, see HttpGetEnabled.
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());

            foreach (Uri baseAddress in host.BaseAddresses)
            {
                Binding binding = null;

                if (baseAddress.Scheme == "net.tcp")
                {
                    binding = MetadataExchangeBindings.CreateMexTcpBinding();
                }
                if (baseAddress.Scheme == "net.pipe")
                {
                    binding = MetadataExchangeBindings.CreateMexNamedPipeBinding();
                }
                if (baseAddress.Scheme == "http")
                {
                    binding = MetadataExchangeBindings.CreateMexHttpBinding();
                }
                if (baseAddress.Scheme == "https")
                {
                    binding = MetadataExchangeBindings.CreateMexHttpsBinding();
                }

                if (binding != null)
                {
                    host.AddServiceEndpoint(typeof(IMetadataExchange), binding, "MEX");
                }
            }
        }
    }
}

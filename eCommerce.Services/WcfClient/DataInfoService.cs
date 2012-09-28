using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.WcfClient
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IDataInfoService")]
    public interface IDataInfoService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDataInfoService/DatabaseIsInstalled", ReplyAction = "http://tempuri.org/IDataInfoService/DatabaseIsInstalledResponse")]
        bool DatabaseIsInstalled();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDataInfoService/DatabaseIsInstalled", ReplyAction = "http://tempuri.org/IDataInfoService/DatabaseIsInstalledResponse")]
        System.Threading.Tasks.Task<bool> DatabaseIsInstalledAsync();
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataInfoServiceChannel : IDataInfoService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataInfoServiceClient : System.ServiceModel.ClientBase<IDataInfoService>, IDataInfoService
    {

        public DataInfoServiceClient()
        {
        }

        public DataInfoServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public DataInfoServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public DataInfoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public DataInfoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public bool DatabaseIsInstalled()
        {
            return base.Channel.DatabaseIsInstalled();
        }

        public System.Threading.Tasks.Task<bool> DatabaseIsInstalledAsync()
        {
            return base.Channel.DatabaseIsInstalledAsync();
        }
    }
}

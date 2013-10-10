using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace eCommerce.Wcf.Framework.ServiceBehaviors.ErrorHandlers
{
    /// <summary>
    /// A generic error handler provider. In actual, how to handle error that is up to
    /// passed parameter error hanlder.
    /// </summary>
    /// <remarks>http://msdn.microsoft.com/en-us/library/system.servicemodel.description.iservicebehavior.aspx</remarks>
    public class GenericErrorHandlerBehavior : IServiceBehavior, IErrorHandler
    {
        private readonly IErrorHandler errorHandler;
        public GenericErrorHandlerBehavior(IErrorHandler errorHandler)
        {
            this.errorHandler = errorHandler;
        }

        #region IServiceBehavior Members
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            return; // modifies no binding parameters
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            // This behavior is an IErrorHandler implementation and  
            // must be applied to each ChannelDispatcher.
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(this);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            return; // do nothing, if we should specify error type or others, please implement here
        }
        #endregion

        #region IErrorHandler Members
        public bool HandleError(Exception error)
        {
            return errorHandler.HandleError(error);
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            errorHandler.ProvideFault(error, version, ref fault);
        }
        #endregion
    }
}

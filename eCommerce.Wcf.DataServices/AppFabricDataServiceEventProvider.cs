using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace eCommerce.Wcf.DataServices
{
    public class AppFabricDataServiceEventProvider
    {
        private const string DiagnosticsConfigSectionName = "system.serviceModel/diagnostics";

        private const int ErrorEventId = 219;
        private const int WarningEventId = 302;
        private const int InfoEventId = 303;

        private const int Version = 0;
        private const int Task = 0;
        private const int Opcode = 0;
        private const long Keywords = 0x20000000001e0004;
        private const byte Channel = 0x12;

        private const int ErrorLevel = 0x2;
        private const int WarningLevel = 0x3;
        private const int InfoLevel = 0x4;
        private readonly EventProvider _innerEventProvider;

        private EventDescriptor _errorDescriptor;
        private string _hostReference;
        private bool _hostReferenceIsComplete;
        private EventDescriptor _infoDescriptor;
        private EventDescriptor _warningDescriptor;

        public AppFabricDataServiceEventProvider()
        {
            Guid providerId;
            if (HostingEnvironment.IsHosted)
            {
                var config = (DiagnosticSection)WebConfigurationManager.GetSection(DiagnosticsConfigSectionName);
                providerId = new Guid(config.EtwProviderId);
                _hostReferenceIsComplete = false;
            }
            else
            {
                var config = (DiagnosticSection)ConfigurationManager.GetSection(DiagnosticsConfigSectionName);
                providerId = new Guid(config.EtwProviderId);
                _hostReference = string.Empty;
                _hostReferenceIsComplete = true;
            }

            _innerEventProvider = new EventProvider(providerId);

            _errorDescriptor = new EventDescriptor(ErrorEventId, Version, Channel, ErrorLevel, Opcode, Task, Keywords);
            _warningDescriptor = new EventDescriptor(WarningEventId, Version, Channel, WarningLevel, Opcode, Task,
                                                     Keywords);
            _infoDescriptor = new EventDescriptor(InfoEventId, Version, Channel, InfoLevel, Opcode, Task, Keywords);
        }

        private string HostReference
        {
            get
            {
                if (_hostReferenceIsComplete == false)
                {
                    CreateHostReference();
                }
                return _hostReference;
            }
        }

        public bool WriteErrorEvent(string name, string format, params object[] args)
        {
            return WriteErrorEvent(name, string.Format(format, args));
        }

        public bool WriteErrorEvent(string name, string payload)
        {
            if (!_innerEventProvider.IsEnabled(_errorDescriptor.Level, _errorDescriptor.Keywords))
            {
                return true;
            }
            return _innerEventProvider.WriteEvent(ref _errorDescriptor, name, HostReference, payload);
        }

        public bool WriteErrorEvent(Exception ex)
        {
            if (!_innerEventProvider.IsEnabled(_errorDescriptor.Level, _errorDescriptor.Keywords))
            {
                return true;
            }
            return _innerEventProvider.WriteEvent(ref _errorDescriptor, ex.ToString(), ex.GetType().FullName,
                                                 HostReference, AppDomain.CurrentDomain.FriendlyName);
        }

        public bool WriteWarningEvent(string name, string format, params object[] args)
        {
            return WriteWarningEvent(name, string.Format(format, args));
        }

        public bool WriteWarningEvent(string name, string payload)
        {
            if (!_innerEventProvider.IsEnabled(_warningDescriptor.Level, _warningDescriptor.Keywords))
            {
                return true;
            }
            return _innerEventProvider.WriteEvent(ref _warningDescriptor, name, HostReference, payload);
        }

        public bool WriteInformationEvent(string name, string format, params object[] args)
        {
            return WriteInformationEvent(name, string.Format(format, args));
        }

        public bool WriteInformationEvent(string name, string payload)
        {
            if (!_innerEventProvider.IsEnabled(_infoDescriptor.Level, _infoDescriptor.Keywords))
            {
                return true;
            }
            return _innerEventProvider.WriteEvent(ref _infoDescriptor, name, HostReference, payload);
        }

        private void CreateHostReference()
        {
            if (OperationContext.Current != null)
            {
                var serviceHostBase = OperationContext.Current.Host;

                var virtualPathExtension = serviceHostBase.Extensions.Find<VirtualPathExtension>();
                if (virtualPathExtension != null && virtualPathExtension.VirtualPath != null)
                {
                    //     HostReference Format
                    //     <SiteName><ApplicationVirtualPath>|<ServiceVirtualPath>|<ServiceName> 

                    string serviceName = serviceHostBase.Description.Name;
                    string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
                    string serviceVirtualPath = virtualPathExtension.VirtualPath.Replace("~", string.Empty);

                    _hostReference = string.Format("{0}{1}|{2}|{3}", HostingEnvironment.SiteName, applicationVirtualPath,
                                                   serviceVirtualPath, serviceName);

                    _hostReferenceIsComplete = true;
                    return;
                }
            }

            // If the entire host reference is not available, fall back to site name and app virtual path.  This will happen
            // if you try to emit a trace from outside an operation (e.g. startup) before an in operation trace has been emitted.
            _hostReference = string.Format("{0}{1}", HostingEnvironment.SiteName,
                                           HostingEnvironment.ApplicationVirtualPath);
        }
    }
}
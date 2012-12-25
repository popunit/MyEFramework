using Autofac.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using eCommerce.Core.Caching;
using eCommerce.Core;
using eCommerce.Wcf.Services.Users;
using eCommerce.Wcf.Services.Common;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Data;
using eCommerce.Data.DataProvider;
using eCommerce.Data;
using eCommerce.Data.Repositories;
using Autofac;
using System.IO;
using System.Web.Routing;
using System.ServiceModel.Activation;
using eCommerce.Core.Diagnosis;
using System.ServiceModel.Description;

namespace eCommerce.Wcf.IISHost
{
    public class AutofacServiceHostDiscoveryFactory : AutofacServiceHostFactory
    {
        /// <summary>
        /// Initial container at the beginning of server activation
        /// </summary>
        /// <remarks>http://www.eidias.com/Blog/2012/2/13/simple-wcf-hosting-wcf-service-by-autofac-in-aspnet-mvc-3</remarks>
        static AutofacServiceHostDiscoveryFactory()
        {
            DependencyRegistrar.Register();
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);

            bool debugEnabled = AutofacHostFactory.Container.Resolve<IDebugHelper>().DebugEnabled;
            if (debugEnabled)
            {
                ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();

                // if not found - add behavior with setting turned on 
                if (debug == null)
                {
                    host.Description.Behaviors.Add(
                         new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                }
                else
                {
                    // make sure setting is turned ON
                    if (!debug.IncludeExceptionDetailInFaults)
                    {
                        debug.IncludeExceptionDetailInFaults = true;
                    }
                }
            }

            return host;
        }
    }
}
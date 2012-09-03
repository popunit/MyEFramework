//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using eCommerce.Data;
using Autofac.Integration.Wcf;
using Autofac;
using eCommerce.Data.Common;

namespace eCommerce.Wcf.DataServices
{
    public partial class DataService : DataService<CommerceDbContext>
    {
        /// <summary>
        /// For writing event log into AppFabric instead of default written logic
        /// </summary>
        private static readonly AppFabricDataServiceEventProvider appFabricEventProvider = 
            new AppFabricDataServiceEventProvider();

        public DataService()
        {
            ProcessingPipeline.ProcessingRequest += (o, args) => appFabricEventProvider.WriteInformationEvent(
                "DataServiceRequest",
                "Processing HTTP {0} request for URI {1}",
                args.OperationContext.RequestMethod,
                args.OperationContext.AbsoluteRequestUri);
        }

        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        protected override CommerceDbContext CreateDataSource()
        {
            var context = AutofacHostFactory.Container.Resolve<IDatabase>() as CommerceDbContext;
            context.DisableProxyCreation(); // disable proxy creation
            return context;
        }

        /// <summary>
        /// Override Handle Exception
        /// </summary>
        /// <param name="args"></param>
        protected override void HandleException(HandleExceptionArgs args)
        {
            appFabricEventProvider.WriteErrorEvent(args.Exception);
        }
    }
}

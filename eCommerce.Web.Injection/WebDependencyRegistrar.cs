using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using eCommerce.Core;
using eCommerce.Core.Events;
using eCommerce.Core.Infrastructure;
using eCommerce.Services;
using eCommerce.Services.WcfClient.Entities;
using eCommerce.Web.Framework;
using eCommerce.Web.Framework.Mvc;
using eCommerce.Web.Framework.Mvc.UI.TitleParts;
using eCommerce.Web.Framework.Subscribers;

namespace eCommerce.Web.Injection
{
    public class WebDependencyRegistrar : RegistrarBase<ContainerBuilder>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="route"></param>
        /// <remarks>HttpContext is always available here</remarks>
        public override void Register(ContainerBuilder builder, IRoute route)
        {
            #region Http Context
            // register call back action (main for construction of type)
            // if httpcontext == null 
            // reason: 1.async request (hack)
            //         2. not web application (test)
            builder.Register(context =>
                null != HttpContext.Current ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                MvcMocker.FakeHttpContext()).As<HttpContextBase>().InstancePerHttpRequest();

            builder.Register(context => context.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>().InstancePerHttpRequest();
            builder.Register(context => context.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>().InstancePerHttpRequest();
            builder.Register(context => context.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>().InstancePerHttpRequest();
            builder.Register(context => context.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>().InstancePerHttpRequest();

            #endregion

            builder.RegisterType<HttpHelper>().As<IHttpHelper>().InstancePerHttpRequest();

            builder.RegisterType<MobileDeviceCheck>().As<IMobileDeviceCheck>().InstancePerHttpRequest(); // Keyed<IMobileDeviceCheck>(typeof(MobileDeviceCheck));

            builder.RegisterType<WebWorkContext>().As<WorkContextServiceBase>().InstancePerHttpRequest();

            builder.RegisterType<ViewPageHeaderBuilder>().As<IViewPageHeaderBuilder>().InstancePerHttpRequest();

            #region Events
            // register subscribers
            RouteHelper.RoutingToGenericType(
                route,
                typeof(ISubscriber<>),
                i => 
                {
                    builder.RegisterType(i).As(i.FindInterfaces((type, criteria) =>
                    {
                        var isMatch = ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());  // check?
                        return isMatch;
                    }, typeof(ISubscriber<>))).InstancePerHttpRequest(); // InstancePerLifetimeScope();
                });

            builder.RegisterType<ObserverService>().As<IObserverService>().InstancePerDependency(); // get new every time

            //var test = builder.Build();
            //User user = new User();
            //test.Resolve<IObserverService>().GetSubscriptionCenter<EntityEvent<User>>().Subscribe(e =>
            //{
            //    e.Handle(user.Mark(EntityStatus.Update));
            //});

            #endregion
        }

        public override int Order
        {
            get { return 0; }
        }
    }
}

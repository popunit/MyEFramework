using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac.Integration.Mvc;
using Autofac.Builder;

namespace eCommerce.Core.Infrastructure.IoC
{
    /// <summary>
    /// Extension functions for Autofac
    /// </summary>
    public static class AutofacContainerManagerExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> PerLifeStyle<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder, LifeStyle lifeStyle)
        {
            switch (lifeStyle)
            {
                case LifeStyle.Scope:
                    // [TO-DO] Need to test instance per http request
                    return HttpContext.Current != null ? builder.InstancePerHttpRequest() : builder.InstancePerLifetimeScope();
                case LifeStyle.Transient:
                    return builder.InstancePerDependency();
                case LifeStyle.Singleton:
                    // Signleton is used for all the requests
                    return builder.SingleInstance();
                default:
                    return builder.SingleInstance();
            }
        }
    }
}

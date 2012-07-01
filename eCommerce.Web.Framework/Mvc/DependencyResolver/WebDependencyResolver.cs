using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.NoAOP;

namespace eCommerce.Web.Framework.Mvc.DependencyResolver
{
    /// <summary>
    /// When the MVC Framework needs to create an instance of a class, it calls the static methods of the
    /// System.Web.Mvc.DependencyResolver class. We can add DI throughout an MVC application by
    /// implementing the IDependencyResolver interface and registering our implementation with
    /// DependencyResolver. That way, whenever the framework needs to create an instance of a class, it will call
    /// our class
    /// </summary>
    public class WebDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            try
            {
                return EngineContext.Current.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                var type = typeof(IEnumerable<>).MakeGenericType(serviceType); // change T to IEnumerable<T>
                return (IEnumerable<object>)EngineContext.Current.Resolve(type);
            }
            catch
            {
                return null;
            }
        }
    }
}

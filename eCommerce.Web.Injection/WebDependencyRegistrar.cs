using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using eCommerce.Core;

namespace eCommerce.Web.Injection
{
    public class WebDependencyRegistrar : RegistrarBase<ContainerBuilder>
    {
        public override void Register(ContainerBuilder builder, Core.Infrastructure.IRoute route)
        {

        }

        public override int Order
        {
            get { return 0; }
        }
    }
}

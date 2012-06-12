using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.IoC;
using NUnit.Framework;

namespace eCommerce.Core.Tests.Infrastructure
{
    [TestFixture]
    public class EngineTest
    {
        public class TestRegistrar1 : RegistrarBase<ContainerBuilder>
        {
            private int order;

            public override void Register(ContainerBuilder builder, IRoute route)
            {
            }

            public override int Order
            {
                get { return this.order; }
            }
        }

        public class TestRegistrar2 : IRegistrar
        {
            private int order;

            public void Register(dynamic builder, IRoute route)
            {
            }

            public int Order
            {
                get { return this.order; }
            }
        }

        [Test]
        public void Initialize_Engine()
        {
            Engine engine = new Engine();
            IContainer builder = (new ContainerBuilder()).Build();
            engine.Init(new AutofacContainerManager(builder), new ContainerConfig()); 
        }
    }
}

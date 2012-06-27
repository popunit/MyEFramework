using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.IoC;
using eCommerce.Core.Infrastructure.NoAOP;
using NUnit.Framework;

namespace eCommerce.Core.Tests.Infrastructure.NoAOP
{
    [TestFixture]
    public class AspectFTests
    {
        [SetUp]
        public void SetUp()
        {
            IContainer builder = (new ContainerBuilder()).Build();
            EngineContext.Initialize(new AutofacContainerManager(builder), new ContainerConfig(), false);
        }

        [Test]
        public void Can_Catch_Exception_Specified()
        {
            Assert.Throws<System.Exception>(() => 
            {
                AspectF.Define.HandleException(typeof(DbEntityValidationException)).Do(() =>
                {
                    throw new DbEntityValidationException();
                });
            });
        }
    }
}

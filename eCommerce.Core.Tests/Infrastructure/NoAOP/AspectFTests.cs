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
using eCommerce.Exception;
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
            Assert.Throws<CommonException>(() => 
            {
                AspectF.Define.HandleException(typeof(DbEntityValidationException)).Do(() =>
                {
                    throw new DbEntityValidationException();
                });
            });
        }

        [Test]
        public void Can_Catch_Exception_ReturnValue()
        {
            bool isException = true;
            var obj = AspectF.Define.HandleException().Return(() => 
            {
                if (isException)
                    throw new System.Exception();
                return new object();
            });
        }

        [Test]
        public void Can_Catch_SubException_Custom()
        {
            Assert.Throws<System.Exception>(() =>
            {
                bool isException = true;
                var obj = AspectF.Define.HandleException<System.Exception>((ex) => { throw new System.Exception(); }).Return(() =>
                {
                    if (isException)
                        throw new CommonException();
                    return 0;
                });
            });
        }
    }
}

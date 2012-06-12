using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using eCommerce.Core.Infrastructure.IoC;
using NUnit.Framework;

namespace eCommerce.Core.Tests.Infrastructure.IoC
{
    [TestFixture]
    public class AutofacContainerManagerTest
    {
        private IContainerManager manager;

        interface ITestClass
        { 
        }

        class TestClass : ITestClass
        {
            public int Property1 { get; set; }
            public string Property2 { get; set; }
            public TestClass()
            {
                Property1 = 1;
                Property2 = "Property";
            }
        }

        class TestClass2 : TestClass 
        {
            public double Property3 = 10.01;
        }

        [SetUp]
        public void Setup()
        {
            var build = new ContainerBuilder();
            manager = new AutofacContainerManager(build.Build());
        }

        [Test]
        public void Register_Can_Concrete_Type_Into_Autofac_Container()
        {
            manager.AddComponent(typeof(TestClass), "Singleton_TestClass_1", LifeStyle.Singleton);

            // test 1
            var result = manager.Resolve<TestClass>("Singleton_TestClass_1");
            Assert.That(result.Property1, Is.EqualTo(1));
            Assert.That(result.Property2, Is.EqualTo("Property"));

            // test 2
            result.Property1 = 2;
            result.Property2 = "MyProperty";
            result = manager.Resolve<TestClass>("Singleton_TestClass_1");
            Assert.That(result.Property1, Is.EqualTo(2));
            Assert.That(result.Property2, Is.EqualTo("MyProperty"));

            // test 3
            manager.AddComponent(typeof(TestClass), "Singleton_TestClass_2", LifeStyle.Singleton);
            result = manager.Resolve<TestClass>("Singleton_TestClass_2");
            Assert.That(result.Property1, Is.EqualTo(1));
            Assert.That(result.Property2, Is.EqualTo("Property"));

            // test 4
            var results = manager.ResolveAll<TestClass>();
            Assert.That(results.Count(), Is.EqualTo(2));

            // test 5 set another type into the same key
            manager.AddComponent(typeof(TestClass2), "Singleton_TestClass_1", LifeStyle.Singleton);
            var result2 = manager.Resolve<TestClass2>("Singleton_TestClass_1");
            Assert.That(result.Property1, Is.EqualTo(1));
            Assert.That(result.Property2, Is.EqualTo("Property"));
            result = manager.Resolve<TestClass>("Singleton_TestClass_1");
            Assert.That(result.Property1, Is.EqualTo(2));
            Assert.That(result.Property2, Is.EqualTo("MyProperty"));

            //// test 
            //result = manager.Resolve<TestClass>("The key is not existed");
            //Assert.That(result, Is.Null);

            // test 5
            manager.AddComponent<TestClass, TestClass2>("Inherit_TestClass_1", LifeStyle.Singleton);
            result = manager.Resolve<TestClass>("Inherit_TestClass_1");
            Assert.That(result, Is.Not.Null);
            //result = manager.Resolve<TestClass2>("Inherit_TestClass_1");
            //Assert.That(result, Is.Not.Null);
            //var iResult = manager.Resolve<ITestClass>("Inherit_TestClass_1");
            //Assert.That(iResult, Is.Not.Null);

            // TO-DO: Per Thread/Transit
        }
    }
}

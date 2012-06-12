using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;
using NUnit.Framework;

namespace eCommerce.Core.Tests.Common
{
    [TestFixture]
    public class SingletonTest
    {
        class TestClass
        {
            public int Property1 { get; set; }
            public string Property2 { get; set; }
        }

        [Test]
        public void Singleton_Value_IsNull()
        {
            var instance = Singleton<SingletonTest>.Instance;
            Assert.That(instance, Is.Null);
        }

        [Test]
        public void Singleton_StructType_WithSameValue()
        {
            Singleton<int>.Instance = 1;
            var instance = Singleton<int>.Instance;
            Assert.AreEqual(instance, 1);
        }

        [Test]
        public void Singleton_ReferenceType_WithSameValue()
        {
            Singleton<TestClass>.Instance = new TestClass { Property1 = 1, Property2 = "Property" };
            var instance = Singleton<TestClass>.Instance;
            Assert.That(instance, Is.Not.Null);
        }
    }
}

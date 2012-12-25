using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using eCommerce.Core.Common;

namespace eCommerce.Core.Tests.Common
{
    [TestFixture]
    public class TypeHelperTest
    {
        internal interface ITestClass { };

        internal class TestClass : ITestClass { };

        [Test]
        public void Type_Is_Inherit_From_Interface()
        {
            Assert.IsTrue(typeof(TestClass).IsInheritFrom(typeof(ITestClass)));
        }

        [Test]
        public void Type_Is_Inherit_From_GenericType()
        {
            Assert.IsTrue(typeof(List<int>).IsInheritFrom(typeof(IList<>)));
        }
    }
}

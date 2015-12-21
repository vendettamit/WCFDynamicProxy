using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DummyService.Contracts;
using WcfDynamicProxy.Tests.Helper;
using NUnit.Framework;

namespace WcfDynamicProxy.Tests.Tests
{
    /// <summary>
    /// Implements the tests for CustomClientProxyFactory class. The tests have falvour of integration
    /// as we're testing it on dummy service client.
    /// </summary>
    [TestFixture]
    public class CustomClientProxyFactoryTest
    {
        /// <summary>
        /// Setup required before the tests of the fixture will run.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            ServiceHostProcessInvoker.InvokeDummyService();
        }

        /// <summary>
        /// Tear down to perform clean when the execution is finished.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            ServiceHostProcessInvoker.KillDummyService();
        }

        /// <summary>
        /// Case when the method GetDefaultErrorLoggingInterfaceProxy was invoked to get the real 
        /// proxy of a test service, succeeds.
        /// </summary>
        [Test]
        public void GetDefaultWCFClientProxy_RequestingProxyClientGeneration_Succeeds()
        {
            var proxy = DynamicProxyWrapperFactory.GetProxyInstanceByInterface<ITestService>("TestService");

            var result = proxy.SayHello();

            Assert.IsNotNull(result, "The result was null.");
            Assert.IsNotEmpty(result, "The result was empty.");
        }
    }
}

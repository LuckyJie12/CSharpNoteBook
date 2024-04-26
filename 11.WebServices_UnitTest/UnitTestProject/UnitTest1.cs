using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// WebServices
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            ServiceReference.WebService1SoapClient soapClient = new ServiceReference.WebService1SoapClient();
            var Result = soapClient.GetUser(new ServiceReference.CustomSoapHeader() { UserName = "Lucky", Password = "123" });
        }
    }
}

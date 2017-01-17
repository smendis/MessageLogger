using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Core.Tests.mocks;
using MessageLogger.Core.Interfaces;
using System.Web;
using Moq;

namespace MessageLogger.Core.Tests
{
    [TestClass]
    public class SessionHandlerTests
    {
        ISessionHandler handler;

        [TestInitialize]
        public void Setup()
        {
            ISettingsRepository repo = new MockSettingsRepository();

            handler = new SessionHandler(repo);

            //mock the application cache
            var mockedHttpContextBase = new Mock<HttpContextBase>();
            mockedHttpContextBase.Setup(m => m.Cache).Returns(HttpRuntime.Cache);
        }

        [TestMethod]
        public void Test_GetSessionAliveKey_Returns()
        {
            var result = handler.GetSessionAliveKey("bc2491ac20454fbcbd7d0d05d56a3a15");
            Assert.AreEqual("ALIVE-bc2491ac20454fbcbd7d0d05d56a3a15", result);
        }


        [TestMethod]
        public void Test_GetSessionTokenKey_Returns()
        {
            var result = handler.GetSessionTokenKey("13df603d-8d31-4624-8607-0248f5e43a52");
            Assert.AreEqual("TOKEN-13df603d-8d31-4624-8607-0248f5e43a52", result);
        }

        [TestMethod]
        public void Test_CreateOrExtendSession_Updated_Cache()
        {
            string key = "test";
            handler.CreateOrExtendSession(key);

            Assert.IsNotNull(HttpRuntime.Cache[key]);
        }

        [TestMethod]
        public void Test_IsSessionExists_Returns_True()
        {
            string key = "test";
            HttpRuntime.Cache[key] = true;

            bool isPass = handler.IsSessionExists(key);

            Assert.IsTrue(isPass);
        }

        [TestMethod]
        public void Test_IsSessionExists_Returns_False()
        {
            string key = "test123";
            bool isPass = handler.IsSessionExists(key);

            Assert.IsFalse(isPass);
        }
    }
}

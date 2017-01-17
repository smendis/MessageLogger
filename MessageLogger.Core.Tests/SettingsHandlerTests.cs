using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Core.Tests.mocks;

namespace MessageLogger.Core.Tests
{
    [TestClass]
    public class SettingsHandlerTests
    {
        SettingsHandler handler;

        [TestInitialize]
        public void Setup()
        {
            ISettingsRepository repo = new MockSettingsRepository();

            handler = new SettingsHandler(repo);
        }

        [TestMethod]
        public void Test_GetSessionLifetime_Success()
        {
            var result = handler.GetSessionLifetime();
            Assert.AreEqual(1200, result);
        }
    }
}

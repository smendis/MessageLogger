using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Core.Tests.mocks;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Core.Interfaces;
using MessageLogger.Core.Dto;
using System.Threading.Tasks;
using MessageLogger.Data;

namespace MessageLogger.Core.Tests
{
    [TestClass]
    public class ApplicationHandlerTests
    {
        private IApplicationHandler handler;

        [TestInitialize]
        public void Setup()
        {
            IApplicationRepository appRepo = new MockApplicationRepository();
            ILogRepository logRepo = new MockLogRepository();

            handler = new ApplicationHandler(appRepo, logRepo);
        }

        [TestMethod]
        public void Test_CheckIfAppExists_Returns_True()
        {
            var result = handler.CheckIfAppExists("Test");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_RegisterApp_Returns_Application()
        {
            var result = handler.RegisterApp("Test");
            Assert.IsInstanceOfType(result, typeof(RegistrationDto));
            Assert.AreEqual("Test", result.display_name);
            Assert.IsNotNull(result.application_id);
            Assert.IsNotNull(result.secret);
        }

        [TestMethod]
        public void Test_ValidateCredentials_Returns_True()
        {
            var result = handler.ValidateCredentials("bc2491ac20454fbcbd7d0d05d56a3a15", "206a73bc84554b84b004fd5aa");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ValidateCredentials_Returns_False()
        {
            var result = handler.ValidateCredentials("bc2491ac20454fbcbd7d0d05d56a3a15", "4c395f0b7ce8e482b76d5df05");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_LogAsync_Returns_Success()
        {
            int response = 0;
            Task.Run(async () =>
            {
                response = await handler.LogAsync(new Log() { application_id = "bc2491ac20454fbcbd7d0d05d56a3a15", logger = "Test.MethodC", level = "Error", message = "This is a test message" });

            }).GetAwaiter().GetResult();
            
            Assert.AreEqual(1, response);
        }
    }
}

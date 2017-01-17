using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Repository.Repositories;
using System.Data.Entity;

namespace MessageLogger.Repository.Tests
{
    [TestClass]
    public class ApplicationRepositoryTests
    {
        private IApplicationRepository repository;

        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new LoggerDbInitializer());
            LoggerDbContext context = new LoggerDbContext();
            context.Database.Initialize(true);
            repository = new ApplicationRepository(context);
        }

        [TestMethod]
        public void Test_CheckIfAppExists_Returns_True()
        {
            bool isExist = repository.CheckIfAppExists("Test");
            Assert.IsTrue(isExist);
        }
        
        [TestMethod]
        public void Test_CheckIfAppExists_Returns_False()
        {
            bool isExist = repository.CheckIfAppExists("Test2");
            Assert.IsFalse(isExist);
        }
        
        [TestMethod]
        public void Test_GetApplicationIfExists_Returns_Application()
        {
            var application = repository.GetApplicationIfExists("bc2491ac20454fbcbd7d0d05d56a3a15");
            Assert.IsNotNull(application);
            Assert.AreEqual("bc2491ac20454fbcbd7d0d05d56a3a15", application.application_id);
            Assert.AreEqual("Test", application.display_name);
            Assert.AreEqual("4c395f0b7ce8e482b76d5df05", application.secret);

        }
        
        [TestMethod]
        public void Test_GetApplicationIfExists_Returns_Null()
        {
            var application = repository.GetApplicationIfExists("4c395f0b7ce8e482b76d5df05");
            Assert.IsNull(application);
        }
    }
}

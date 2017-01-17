using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Repository.Repositories;
using System.Data.Entity;

namespace MessageLogger.Repository.Tests
{
    [TestClass]
    public class SettingsRepositoryTests
    {
        private ISettingsRepository repository;

        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new LoggerDbInitializer());
            LoggerDbContext context = new LoggerDbContext();
            context.Database.Initialize(true);
            repository = new SettingsRepository(context);
        }

        [TestMethod]
        public void Test_GetSessionLifetime_Succeed()
        {
            int lifetime = repository.GetSessionLifetime();
            Assert.AreEqual(1200, lifetime);
        }


    }
}

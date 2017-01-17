using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Repository.Repositories;
using System.Data.Entity;
using System.Collections.Generic;
using MessageLogger.Data;
using System.Threading.Tasks;

namespace MessageLogger.Repository.Tests
{
    [TestClass]
    public class LogRepositoryTests
    {
        private ILogRepository repository;

        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new LoggerDbInitializer());
            LoggerDbContext context = new LoggerDbContext();
            context.Database.Initialize(true);
            repository = new LogRepository(context);
        }

        [TestMethod]
        public void Test_AddAsync_Succeed()
        {
            Task.Run(async () =>
            {
                var response = await repository.AddAsync(new Log() { application_id = "bc2491ac20454fbcbd7d0d05d56a3a15", logger = "Test.MethodC", level = "Error", message = "This is a test message" });
                
            }).GetAwaiter().GetResult();

            var list = (List<Log>)repository.Find(i => i.application_id != null);
            Assert.AreEqual(3, list.Count);
        }
    }
}

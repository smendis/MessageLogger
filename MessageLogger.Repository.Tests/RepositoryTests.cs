using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Data;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Repository.Repositories;
using System.Data.Entity;
using MessageLogger.Repository.Tests;
using System.Collections.Generic;

namespace MessageLogger.Repository.Tests
{

    [TestClass]
    public class RepositoryTests
    {
        private IRepository<Settings> repository;

        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new LoggerDbInitializer());
            LoggerDbContext context = new LoggerDbContext();
            context.Database.Initialize(true);
            repository = new Repository<Settings>(context);
        }

        [TestMethod]
        public void Test_Repository_Find_Existing_Item()
        {
            var list = (List<Settings>)repository.Find(i=>i.setting_name == "session_lifetime");

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Test_Repository_Find_None_Existing_Item()
        {
            var list = (List<Settings>)repository.Find(i => i.setting_name == "rate_limit");

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Test_Repository_Add()
        {
            repository.Add(new Settings() { setting_name="rate_limit", setting_value=60});

            var list = (List<Settings>)repository.Find(i => i.setting_name != null);

            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void Test_Repository_Remove_Existing_Item()
        {
            var setting = ((List<Settings>)repository.Find(i => i.setting_name == "session_lifetime"))[0];
            repository.Remove(setting);

            var list = (List<Settings>)repository.Find(i => i.setting_name != null);

            Assert.AreEqual(0, list.Count);
        }
    }
}

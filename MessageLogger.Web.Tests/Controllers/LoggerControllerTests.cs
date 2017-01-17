using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Web.Controllers;
using System.Web.Mvc;

namespace MessageLogger.Web.Tests.Controllers
{
    [TestClass]
    public class LoggerControllerTests
    {
        [TestMethod]
        public void Test_Logger_Index_View_Not_Null()
        {
            // Arrange
            LoggerController controller = new LoggerController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Logger_Authenticate_View_Not_Null()
        {
            // Arrange
            LoggerController controller = new LoggerController();

            // Act
            ViewResult result = controller.Authenticate() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

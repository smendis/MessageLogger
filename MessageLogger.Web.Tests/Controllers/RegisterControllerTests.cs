using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Web.Controllers;
using System.Web.Mvc;

namespace MessageLogger.Web.Tests.Controllers
{
    [TestClass]
    public class RegisterControllerTests
    {
        [TestMethod]
        public void Test_Register_Index_View_Not_Null()
        {
            // Arrange
            RegisterController controller = new RegisterController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

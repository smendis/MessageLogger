using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Web.Controllers;
using System.Web.Mvc;
using MessageLogger.Web.Models;
using System.Web;
using System.Threading.Tasks;

namespace MessageLogger.Web.Tests.Controllers
{
    [TestClass]
    public class RegisterControllerTests
    {
        RegisterController controller;

        [TestInitialize]
        public void Setup()
        {
            controller = new RegisterController(new Mocks.MockRegisterWebService());
        }

        [TestMethod]
        public void Test_Register_Get_Index_View_Not_Null()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Register_Post_Index_Returns_And_Redirects()
        {
            ActionResult actionResult = null;
            // Act
            Task.Run(async () =>
            {
                actionResult = await controller.Index(new RegisterRequestModel { display_name = "Test1" });

            }).GetAwaiter().GetResult();
            
            
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));

            var result = actionResult as RedirectToRouteResult;
            Assert.AreEqual("Result", result.RouteValues["action"]);


            Assert.IsTrue(controller.TempData.ContainsKey("model"));
            Assert.IsInstanceOfType(controller.TempData["model"], typeof(RegisterViewModel));

            var model = controller.TempData["model"] as RegisterViewModel;
            Assert.IsNotNull(model.Result);
            Assert.IsInstanceOfType(model.Result, typeof(RegisterResultModel));
            Assert.AreEqual("Test1", model.Result.display_name);
        }
    }
}

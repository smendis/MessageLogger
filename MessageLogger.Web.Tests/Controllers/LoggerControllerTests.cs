using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Web.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;
using MessageLogger.Web.Models;
using System.Web;
using System.IO;
using System.Web.SessionState;
using Moq;

namespace MessageLogger.Web.Tests.Controllers
{
    [TestClass]
    public class LoggerControllerTests
    {
        LoggerController controller;

        [TestInitialize]
        public void Setup()
        {
            controller = new LoggerController(new Mocks.MockLoggerWebService());
        }

        [TestMethod]
        public void Test_Authenticate_Get_Index_View_Not_Null()
        {
            // Act
            var result = controller.Authenticate();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Authenticate_Post_Index_Returns_And_Redirects()
        {
            // Act
            ActionResult actionResult = null;
            Task.Run(async () =>
            {
                actionResult = await controller.Authenticate(new AuthRequestModel());

            }).GetAwaiter().GetResult();

            //assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));

            var result = actionResult as RedirectToRouteResult;
            Assert.AreEqual("AuthenticateResult", result.RouteValues["action"]);
            
            Assert.IsTrue(controller.TempData.ContainsKey("model"));
            Assert.IsInstanceOfType(controller.TempData["model"], typeof(AuthViewModel));

            var model = controller.TempData["model"] as AuthViewModel;
            Assert.IsNotNull(model.Result);
            Assert.IsInstanceOfType(model.Result, typeof(AuthResultModel));
            Assert.AreEqual("token", model.Result.access_token);
        }

        [TestMethod]
        public void Test_Log_Get_Index_View_Not_Null()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_Log_Post_Index_Without_Authenticatio_Returns_Error()
        {
            //act
            ActionResult actionResult = null;
            Task.Run(async () =>
            {
                actionResult = await controller.Index(new LogRequestModel());

            }).GetAwaiter().GetResult();

            //assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));

            var result = actionResult as RedirectToRouteResult;
            Assert.AreEqual("Result", result.RouteValues["action"]);
            
            Assert.IsTrue(controller.TempData.ContainsKey("model"));
            Assert.IsInstanceOfType(controller.TempData["model"], typeof(LogViewModel));

            var model = controller.TempData["model"] as LogViewModel;
            Assert.IsNotNull(model.Error);
        }

        [TestMethod]
        public void Test_Log_Post_Index_Returns_And_Redirects()
        {
            //setup session
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(m => m.HttpContext.Session["id"]).Returns(true);
            controller.ControllerContext = mockControllerContext.Object;
            
            //act
            ActionResult actionResult = null;
            Task.Run(async () =>
            {
                actionResult = await controller.Index(new LogRequestModel() {  application_id = "id"});

            }).GetAwaiter().GetResult();

            //assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));

            var result = actionResult as RedirectToRouteResult;
            Assert.AreEqual("Result", result.RouteValues["action"]);


            Assert.IsTrue(controller.TempData.ContainsKey("model"));
            Assert.IsInstanceOfType(controller.TempData["model"], typeof(LogViewModel));

            var model = controller.TempData["model"] as LogViewModel;
            Assert.IsNotNull(model.Result);
            Assert.IsInstanceOfType(model.Result, typeof(LogResultModel));
            Assert.AreEqual("true", model.Result.success);
        }
    }
}

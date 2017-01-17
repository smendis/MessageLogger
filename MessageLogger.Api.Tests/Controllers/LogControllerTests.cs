using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Api.Controllers;
using MessageLogger.Api.Tests.mocks;
using System.Web;
using Moq;
using MessageLogger.Api.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace MessageLogger.Api.Tests.Controllers
{
    [TestClass]
    public class LogControllerTests
    {
        LogController controller;

        [TestInitialize]
        public void Setup()
        {
            controller = new LogController(new MockHandlerProvider());

            //setup cache
            var mockedHttpContextBase = new Mock<HttpContextBase>();
            mockedHttpContextBase.Setup(m => m.Cache).Returns(HttpRuntime.Cache);
        }

        [TestMethod]
        public void Test_Log_With_Invalid_Model()
        {
            IHttpActionResult actionResult = null;

            controller.ModelState.AddModelError("", "force error");
            var result = controller.Log(new LogRequestModel());
            Task.Run(async () =>
            {
                actionResult = await controller.Log(new LogRequestModel());

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }


        [TestMethod]
        public void Test_Log_With_Valid_Model()
        {
            IHttpActionResult actionResult = null;

            controller.ModelState.Clear();
            var result = controller.Log(new LogRequestModel());
            Task.Run(async () =>
            {
                actionResult = await controller.Log(new LogRequestModel());

            }).GetAwaiter().GetResult();

            var contentResult = actionResult as OkNegotiatedContentResult<LogResponseModel>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(contentResult.Content.success);
        }
    }
}

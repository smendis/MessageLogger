using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Api.Controllers;
using MessageLogger.Api.Tests.mocks;
using MessageLogger.Api.Models;
using System.Web.Http.Results;
using System.Web.Http;
using MessageLogger.Core.Dto;

namespace MessageLogger.Api.Tests.Controllers
{
    [TestClass]
    public class RegisterControllerTests
    {
        RegisterController controller;

        [TestInitialize]
        public void Setup()
        {
            controller = new RegisterController(new MockHandlerProvider());
        }

        [TestMethod]
        public void Test_Register_With_Invalid_Model()
        {
            RegisterRequestModel model = new RegisterRequestModel();
            controller.ModelState.AddModelError("", "force error");
            IHttpActionResult actionResult = controller.Register(model);

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Test_Register_With_Valid_Model()
        {
            RegisterRequestModel model = new RegisterRequestModel()
            {
                display_name = "Test1"
            };
            controller.ModelState.Clear();
            IHttpActionResult actionResult = controller.Register(model);
            var result = actionResult as OkNegotiatedContentResult<RegistrationDto>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual("Test1", result.Content.display_name);
        }
    }
}

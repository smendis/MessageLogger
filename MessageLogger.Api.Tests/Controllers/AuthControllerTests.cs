using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLogger.Api.Controllers;
using System.Net.Http;
using System.Web.Http;
using MessageLogger.Api.Tests.mocks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http.Headers;
using System.Web;
using Moq;
using System.Web.Http.Results;
using MessageLogger.Api.Models;

namespace MessageLogger.Api.Tests.Controllers
{
    /// <summary>
    /// Summary description for AuthControllerUnitTest
    /// </summary>
    [TestClass]
    public class AuthControllerTests
    {
        AuthController controller;

        [TestInitialize]
        public void Setup()
        {
            controller = new AuthController(new MockHandlerProvider());

            //setup cache
            var mockedHttpContextBase = new Mock<HttpContextBase>();
            mockedHttpContextBase.Setup(m => m.Cache).Returns(HttpRuntime.Cache);
        }

        [TestMethod]
        public void Test_Authenticate_Valid_Token()
        {
            //create request with authorizatioin header
            AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Basic", "YmMyNDkxYWMyMDQ1NGZiY2JkN2QwZDA1ZDU2YTNhMTU6MjA2YTczYmM4NDU1NGI4NGIwMDRmZDVhYQ==");
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRequestHeaders headers = request.Headers;
            headers.Authorization = authorization;
            controller.Request = request;
            HttpControllerContext controllerContext = new HttpControllerContext();
            HttpActionContext actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;
            controller.ControllerContext = controllerContext;
            controller.ControllerContext.Request = request;

            
            IHttpActionResult actionResult = controller.Authenticate();
            var result = actionResult as OkNegotiatedContentResult<AuthResponseModel>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsNotNull(result.Content.access_token);
        }


        [TestMethod]
        public void Test_Authenticate_Invalid_Token()
        {
            //create request with authorizatioin header
            AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Basic", "YmMyNDkxYWMyMDQ1NGZiY2JkN2QwZDA1ZDU2YTNhMTUyMDZhNzNiYzg0NTU0Yjg0YjAwNGZkNWFh");
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRequestHeaders headers = request.Headers;
            headers.Authorization = authorization;
            controller.Request = request;
            HttpControllerContext controllerContext = new HttpControllerContext();
            HttpActionContext actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;
            controller.ControllerContext = controllerContext;
            controller.ControllerContext.Request = request;


            IHttpActionResult actionResult = controller.Authenticate();

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Test_Authenticate_With_Existing_Session()
        {
            controller = new AuthController(new MockHandlerProvider2());

            //create request with authorizatioin header
            AuthenticationHeaderValue authorization = new AuthenticationHeaderValue("Basic", "YmMyNDkxYWMyMDQ1NGZiY2JkN2QwZDA1ZDU2YTNhMTU6MjA2YTczYmM4NDU1NGI4NGIwMDRmZDVhYQ==");
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRequestHeaders headers = request.Headers;
            headers.Authorization = authorization;
            controller.Request = request;
            HttpControllerContext controllerContext = new HttpControllerContext();
            HttpActionContext actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;
            controller.ControllerContext = controllerContext;
            controller.ControllerContext.Request = request;


            IHttpActionResult actionResult = controller.Authenticate();

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(UnauthorizedResult));
        }
    }
}

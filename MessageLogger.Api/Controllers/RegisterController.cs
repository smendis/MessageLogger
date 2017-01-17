using MessageLogger.Api.Models;
using MessageLogger.Core;
using MessageLogger.Core.Dto;
using MessageLogger.Core.Interfaces;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MessageLogger.Api.Controllers
{
    [ApiVersion("1.0")]
    public class RegisterController : ApiController
    {
        IApplicationHandler appHandler = null;

        public RegisterController(IHandlerProvider provider)
        {
            appHandler = provider.GetApplicationHandler();
        }


        /// <summary>
        /// Register applications
        /// </summary>
        /// <param name="model">The RegisterRequestModel model.</param>
        [HttpPost]
        [Route("v{version:apiVersion}/register")]
        [Route("register")]
        [ResponseType(typeof(RegistrationDto))]
        public IHttpActionResult Register([FromBody]RegisterRequestModel model)
        {
            var display_name = model.display_name;

            if (!ModelState.IsValid)
                return BadRequest();

            if (appHandler.CheckIfAppExists(display_name))
                return BadRequest();

            var registeredApp = appHandler.RegisterApp(display_name);
            return Ok(registeredApp);
        }
    }
}

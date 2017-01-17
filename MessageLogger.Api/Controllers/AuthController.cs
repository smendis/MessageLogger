using MessageLogger.Api.Helpers;
using MessageLogger.Api.Models;
using MessageLogger.Core.Interfaces;
using MessageLogger.Data;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Description;

namespace MessageLogger.Api.Controllers
{

    [ApiVersion("1.0")]
    public class AuthController : ApiController
    {
        IApplicationHandler appHandler;
        ISessionHandler sessionHandler;

        public AuthController(IHandlerProvider provider)
        {
            appHandler = provider.GetApplicationHandler();
            sessionHandler = provider.GetSessionHandler();
        }

        /// <summary>
        /// Authenticates the client.
        /// </summary>
        [Route("v{version:apiVersion}/auth")]
        [Route("auth")]
        [HttpPost]
        [ResponseType(typeof(AuthResponseModel))]
        public IHttpActionResult Authenticate()
        {
            string token = GetToken();
            if (token == null || token.IndexOf(":") == -1)
            {
                Trace.TraceError("Authentication failed. Authorization header token error. Token:"+token);
                return BadRequest();
            }

            string application_id = token.Substring(0, token.IndexOf(":"));
            string application_secret = token.Substring(token.IndexOf(":") + 1);
            
            bool isValid = appHandler.ValidateCredentials(application_id, application_secret);
            if (!isValid)
            {
                Trace.TraceError("Authentication failed. Incorrect credentials for application_id: " + application_id);
                return Unauthorized();
            }

            //ckeck for active sessions
            var aliveKey = sessionHandler.GetSessionAliveKey(application_id);
            if (!sessionHandler.IsSessionExists(aliveKey))//has no valid session 
            {
                //create access token
                var access_token = Guid.NewGuid().ToString();

                //create session
                var tokenKey = sessionHandler.GetSessionTokenKey(access_token);
                sessionHandler.CreateOrExtendSession(tokenKey);
                sessionHandler.CreateOrExtendSession(aliveKey);

                Trace.TraceInformation("New session created for application_id:" + application_id);
                
                //send access_token
                var response = new AuthResponseModel(access_token);
                return Ok(response);
            }
            else
            {
                Trace.TraceError("Authentication failed due to the rule, 'Only one active session per application' for application_id:" + application_id);
                return Unauthorized();
            }
        }

        private string GetToken()
        {
            if (Request.Headers.Authorization != null &&
                Request.Headers.Authorization.Parameter != null)
            {
                string authToken = Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                return decodedToken;
            }
            else
            {
                Trace.TraceError("Authentication failed. Authorization header does not exists.");
            }

            return null;
        }
    }
}

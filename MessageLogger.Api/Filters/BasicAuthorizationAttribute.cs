using MessageLogger.Api.Helpers;
using MessageLogger.Core.Interfaces;
using MessageLogger.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MessageLogger.Api.Filters
{
    public class BasicAuthorizationAttribute : AuthorizationFilterAttribute
    {
        [Inject]
        public IHandlerProvider provider { get; set; }

        public BasicAuthorizationAttribute()
        {
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            ISessionHandler sessionHandler = provider.GetSessionHandler();
            string accessToken = GetAccessToken(actionContext);
            if(accessToken != null)
            {
                if (!sessionHandler.TryExtendSession(accessToken))
                {
                    //no valid session found
                    Forbidden(actionContext);
                }
            }
            else
            {
                BadRequest(actionContext);
            }

            base.OnAuthorization(actionContext);
        }

        private string GetAccessToken(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null &&
                actionContext.Request.Headers.Authorization.Parameter != null)
            {
                string encryptedToken = actionContext.Request.Headers.Authorization.Parameter;
                string access_token = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedToken));
                return access_token;
            }
            else
            {
                Trace.TraceError("Authorization header is not present");
            }

            return null;
        }
        
        private void Forbidden(HttpActionContext actionContext)
        {
            Trace.TraceWarning("Basic Authorization Attribute sending error response: 403. No valid session for the token.");
            actionContext.Response = ClientErrors.InvalidToken(actionContext);
        }

        private void BadRequest(HttpActionContext actionContext)
        {
            Trace.TraceWarning("Basic Authorization Attribute sending error response: 400. Access token not present");
            actionContext.Response = ClientErrors.InvalidJson(actionContext);
        }
    }
}
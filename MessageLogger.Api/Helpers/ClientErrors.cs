using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace MessageLogger.Api.Helpers
{
    public class ClientErrors
    {
        public static HttpResponseMessage InvalidJson(HttpActionContext actionContext)
        {
            return actionContext.Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public static HttpResponseMessage Unauthorized(HttpActionContext actionContext)
        {
            return actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        public static HttpResponseMessage RateLimitExceeded(HttpActionContext actionContext)
        {
            return actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Application Rate Limit Exceeded");
        }

        public static HttpResponseMessage InvalidToken(HttpActionContext actionContext)
        {
            return actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid access token");
        }
    }
}
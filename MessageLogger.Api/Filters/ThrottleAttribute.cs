using MessageLogger.Api.Helpers;
using MessageLogger.Core.Helper;
using MessageLogger.Core.Interfaces;
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
    public class ThrottleAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The maximum number of requests the application accepts per minute from a authenticated client.
        /// </summary>
        private int RateLimit = 60;


        /// <summary>
        /// The time in minutes to wait after exceeding the rate limit before accepting client's requests again.
        /// </summary>
        private int RateLimitWait = 5;

        private readonly object syncLock = new object();

        [Inject]
        public IHandlerProvider provider { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var access_token = GetAccessToken(actionContext);
            if (access_token != null)
            {
                var key = GetCacheKey(access_token);

                lock (syncLock)
                {
                    //add a listner for request count
                    if (HttpRuntime.Cache[key] == null)
                    {
                        HttpRuntime.Cache.Add(key,
                            1,
                            null,
                            DateTime.Now.AddMinutes(1),
                            Cache.NoSlidingExpiration,
                            CacheItemPriority.High,
                            null);
                    }
                    else
                    {
                        //listner exists for request count
                        var current_requests = (int)HttpRuntime.Cache[key];
                        if (current_requests < RateLimit)
                        {
                            HttpRuntime.Cache.Insert(key,
                           current_requests + 1,
                           null,
                           DateTime.Now.AddMinutes(1),
                           Cache.NoSlidingExpiration,
                           CacheItemPriority.High,
                           null);
                        }
                        else //hit rate limit, wait for another 5 minutes 
                        {
                            HttpRuntime.Cache.Insert(key,
                           current_requests,
                           null,
                           DateTime.Now.AddMinutes(RateLimitWait),
                           Cache.NoSlidingExpiration,
                           CacheItemPriority.High,
                           null);

                            Forbidden(actionContext);
                        }
                    }
                }
            }
            else
            {
                BadRequest(actionContext);
            }

            base.OnActionExecuting(actionContext);
        }

        private void BadRequest(HttpActionContext actionContext)
        {
            Trace.TraceError("Trottle Attribute sending error response: 400. Bad request, since the access_token is not found in the request header");
            actionContext.Response = ClientErrors.InvalidJson(actionContext);
        }

        private void Forbidden(HttpActionContext actionContext)
        {
            Trace.TraceError("Trottle Attribute sending error response: 403. Application Rate Limit Exceeded.");
            actionContext.Response = ClientErrors.RateLimitExceeded(actionContext);
        }

        private string GetAccessToken(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null &&
                actionContext.Request.Headers.Authorization.Parameter != null)
            {
                string encryptedToken = actionContext.Request.Headers.Authorization.Parameter;
                string accessToken = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedToken));

                return accessToken;
            }
            return null;
        }

        private string GetCacheKey(string access_token)
        {
            return string.Concat(ApplicationConstants.THROTTLE_IDENTIFIER, "-", access_token);
        }

    }
}
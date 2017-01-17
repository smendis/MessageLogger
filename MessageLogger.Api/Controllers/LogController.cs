using MessageLogger.Api.Filters;
using MessageLogger.Api.Helpers;
using MessageLogger.Api.Models;
using MessageLogger.Core;
using MessageLogger.Core.Interfaces;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace MessageLogger.Api.Controllers
{

    [ApiVersion("1.0")]
    [BasicAuthorization]
    public class LogController : ApiController
    {
        IApplicationHandler applicationHandler = null;

        public LogController(IHandlerProvider provider)
        {
            applicationHandler = provider.GetApplicationHandler();
        }


        /// <summary>
        /// Persist the client log message asynchronously.
        /// </summary>
        /// <param name="model">Log request model object.</param>
        [Route("v{version:apiVersion}/log")]
        [Route("log")]
        [HttpPost]
        [Throttle]
        [ResponseType(typeof(LogResponseModel))]
        public async Task<IHttpActionResult> Log([FromBody]LogRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var log = Mapper.LogRequestModelToLog(model);
            int result = await applicationHandler.LogAsync(log);
            
            var response = new LogResponseModel(result > 0);//success or failure of the save to database operation
            return Ok(response);
        }
    }
}

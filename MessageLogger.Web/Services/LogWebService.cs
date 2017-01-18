using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MessageLogger.Web.Models;
using MessageLogger.Web.Helpers;

namespace MessageLogger.Web.Services
{
    public class LogWebService : WebService, ILogWebService
    {
        public async Task<AuthResultModel> Authenticate(string token)
        {
            var encodedVallue = Base64Utility.Encode(token.ToString());
            AuthResultModel result = await base.Post<AuthResultModel>("auth", encodedVallue, null);
            return result;
        }

        public async Task<LogResultModel> Log(string token, object data)
        {
            var encodedVallue = Base64Utility.Encode(token.ToString());
            LogResultModel result = await base.Post<LogResultModel>("log", encodedVallue, data);
            return result;
        }
    }
}
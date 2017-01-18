using MessageLogger.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageLogger.Web.Models;

namespace MessageLogger.Web.Tests.Mocks
{
    public class MockLoggerWebService : ILogWebService
    {
        public Task<AuthResultModel> Authenticate(string authorizationHeader)
        {
            return Task.Run(() => new AuthResultModel { access_token = "token" });
        }

        public Task<LogResultModel> Log(string authorizationHeader, object data)
        {
            return Task.Run(() => new LogResultModel { success = "true" });
        }
    }
}

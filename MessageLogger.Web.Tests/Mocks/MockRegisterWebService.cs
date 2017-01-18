using MessageLogger.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageLogger.Web.Models;

namespace MessageLogger.Web.Tests.Mocks
{
    public class MockRegisterWebService : IRegisterWebService
    {
        public Task<RegisterResultModel> Register(RegisterRequestModel data)
        {
            return Task.Run(() => new RegisterResultModel { display_name = data.display_name });
        }
    }
}

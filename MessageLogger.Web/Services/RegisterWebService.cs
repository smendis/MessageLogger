using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MessageLogger.Web.Models;

namespace MessageLogger.Web.Services
{
    public class RegisterWebService : WebService, IRegisterWebService
    {
        public async Task<RegisterResultModel> Register(RegisterRequestModel data)
        {
            RegisterResultModel app = await base.Post<RegisterResultModel>("register", null, data);
            return app;
        }
    }
}
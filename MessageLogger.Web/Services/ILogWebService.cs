using MessageLogger.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Web.Services
{
    public interface ILogWebService
    {
        Task<AuthResultModel> Authenticate(string authorizationHeader);
        Task<LogResultModel> Log(string authorizationHeader, object data);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Web.Services
{
    interface IWebService
    {
        Task<T> Post<T>(string relativeUri, string authorizationHeader, object data);
    }
}

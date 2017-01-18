using MessageLogger.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Web.Services
{
    public interface IRegisterWebService
    {
        Task<RegisterResultModel> Register(RegisterRequestModel data);
    }
}

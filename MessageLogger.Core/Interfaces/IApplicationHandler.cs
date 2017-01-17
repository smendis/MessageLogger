using MessageLogger.Core.Dto;
using MessageLogger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Core.Interfaces
{
    public interface IApplicationHandler
    {
        bool CheckIfAppExists(string display_name);
        RegistrationDto RegisterApp(string display_name);
        bool ValidateCredentials(string application_id, string application_secret);
        Task<int> LogAsync(Log log);
    }
}

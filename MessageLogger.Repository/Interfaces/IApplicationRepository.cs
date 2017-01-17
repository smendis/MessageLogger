using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageLogger.Data;

namespace MessageLogger.Repository.Interfaces
{
    public interface IApplicationRepository : IRepository<Application>
    {
        bool CheckIfAppExists(string display_name);
        Application GetApplicationIfExists(string application_id);
    }
}

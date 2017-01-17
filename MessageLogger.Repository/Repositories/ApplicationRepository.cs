using MessageLogger.Data;
using MessageLogger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MessageLogger.Repository.Repositories
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(LoggerDbContext context):base(context)
        {

        }

        public bool CheckIfAppExists(string display_name)
        {
            return (Context as LoggerDbContext).Applications.Any(i => i.display_name == display_name);
        }

        public Application GetApplicationIfExists(string application_id)
        {
            return (Context as LoggerDbContext).Applications.FirstOrDefault(i => i.application_id == application_id);
        }
    }
}

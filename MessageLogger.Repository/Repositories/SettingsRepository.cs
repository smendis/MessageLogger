using MessageLogger.Data;
using MessageLogger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Repository.Repositories
{
    public class SettingsRepository : Repository<Settings>, ISettingsRepository
    {
        public const string SESSION_LIFETIME = "session_lifetime";

        public SettingsRepository(DbContext context):base(context)
        {

        }
        
        public int GetSessionLifetime()
        {
            if ((Context as LoggerDbContext).Settings.Any(i => i.setting_name == SESSION_LIFETIME))
                return (Context as LoggerDbContext).Settings.SingleOrDefault(i => i.setting_name == SESSION_LIFETIME).setting_value;
            else
                return 1200;
        }
    }
}

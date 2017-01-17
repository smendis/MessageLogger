using MessageLogger.Core.Interfaces;
using MessageLogger.Repository;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Core
{
    public class HandlerProvider : IHandlerProvider
    {
        LoggerDbContext dbContext;

        IApplicationRepository appRepository;
        ILogRepository logRepository;
        ISettingsRepository settingsRepository;

        public HandlerProvider()
        {
            dbContext = new LoggerDbContext();
            appRepository = new ApplicationRepository(dbContext);
            logRepository = new LogRepository(dbContext);
            settingsRepository = new SettingsRepository(dbContext);
        }

        public IApplicationHandler GetApplicationHandler()
        {  
            return new ApplicationHandler(appRepository, logRepository);
        }
        
        public ISessionHandler GetSessionHandler()
        {
            return new SessionHandler(settingsRepository);
        }
    }
}

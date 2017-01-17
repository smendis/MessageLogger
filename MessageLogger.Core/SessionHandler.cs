using MessageLogger.Core.Helper;
using MessageLogger.Core.Interfaces;
using MessageLogger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace MessageLogger.Core
{
    public class SessionHandler : ISessionHandler
    {
        ISettingsRepository settingsRepository;

        public SessionHandler(ISettingsRepository settings_repository)
        {
            settingsRepository = settings_repository;
        }

        public void CreateOrExtendSession(string key)
        {
            if (HttpRuntime.Cache[key] == null)//create session
            {
                HttpRuntime.Cache.Add(key,
               true,
               null,
               DateTime.Now.AddMinutes(SessionLifetime),
               Cache.NoSlidingExpiration,
               CacheItemPriority.High,
               null);
            }
            else //extend session
            {
                HttpRuntime.Cache.Insert(key,
               true,
               null,
               DateTime.Now.AddMinutes(SessionLifetime),
               Cache.NoSlidingExpiration,
               CacheItemPriority.High,
               null);
            }
        }

        public string GetSessionAliveKey(string application_id)
        {
            return string.Concat(ApplicationConstants.ALIVE_TOKEN_IDENTIFIER, "-", application_id);
        }

        public string GetSessionTokenKey(string access_token)
        {
            return string.Concat(ApplicationConstants.SESSION_TOKEN_IDENTIFIER, "-", access_token);
        }

        public bool IsSessionExists(string key)
        {
            return (HttpRuntime.Cache[key] != null);
        }

        private int SessionLifetime
        {
            get
            {
                if (HttpRuntime.Cache[ApplicationConstants.SESSION_LIFETIME] == null)
                {
                    HttpRuntime.Cache[ApplicationConstants.SESSION_LIFETIME] = settingsRepository.GetSessionLifetime();
                }
                return (int)HttpRuntime.Cache[ApplicationConstants.SESSION_LIFETIME];
            }
        }
        
    }
}

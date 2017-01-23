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
        readonly object syncLock = new object();

        public SessionHandler(ISettingsRepository settings_repository)
        {
            settingsRepository = settings_repository;
        }
        public void CreateOrExtendSession(string key)
        {
            lock (syncLock)
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
        }

        public bool TryCreateSession(string application_id, out string token)
        {
            bool success = false;

            token = Guid.NewGuid().ToString();
            var aliveToken = GetSessionAliveKey(application_id);
            var sessionToken = GetSessionTokenKey(token);

            lock (syncLock)
            {
                if(HttpRuntime.Cache[aliveToken] == null)
                {
                    //register access token
                    AddOrUpdateCache(sessionToken, application_id, true);
                    //register application_id as alive
                    AddOrUpdateCache(aliveToken, true, true);

                    success = true;
                }
                else
                {
                    token = null;
                }               
            }

            return success;
        }

        public bool TryExtendSession(string access_token)
        {
            bool success = false;
            var sessionToken = GetSessionTokenKey(access_token);
            lock (syncLock)
            {
                if(HttpRuntime.Cache[sessionToken] != null)
                {
                    string application_id = HttpRuntime.Cache[sessionToken].ToString();
                    string aliveToken = GetSessionAliveKey(application_id);
                    //extend access token
                    AddOrUpdateCache(sessionToken, application_id, false);
                    //extend application_id as alive
                    AddOrUpdateCache(aliveToken, true, false);
                    success = true;
                }
            }
            return true;
        }
        public void AddOrUpdateCache(string key, object value, bool isNew)
        {
            if (isNew)
            {
                HttpRuntime.Cache.Add(key,
                   value,
                   null,
                   DateTime.Now.AddMinutes(SessionLifetime),
                   Cache.NoSlidingExpiration,
                   CacheItemPriority.High,
                   null);
            }
            else
            {
                HttpRuntime.Cache.Insert(key,
                   value,
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
                int lifetime = 0;
                lock (syncLock)
                {
                    if (HttpRuntime.Cache[ApplicationConstants.SESSION_LIFETIME] == null)
                    {
                        HttpRuntime.Cache[ApplicationConstants.SESSION_LIFETIME] = settingsRepository.GetSessionLifetime();
                    }
                    lifetime = (int)HttpRuntime.Cache[ApplicationConstants.SESSION_LIFETIME];
                }
                return lifetime;
            }
        }
        
    }
}

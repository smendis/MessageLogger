using MessageLogger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MessageLogger.Api.Tests.mocks
{
    class MockSessionHandler2 : ISessionHandler
    {
        public void CreateOrExtendSession(string key)
        {
            HttpRuntime.Cache[key] = true;
        }

        public string GetSessionAliveKey(string application_id)
        {
            return application_id;
        }

        public string GetSessionTokenKey(string access_token)
        {
            return access_token;
        }

        public bool IsSessionExists(string key)
        {
            return true;
        }

        public bool TryCreateSession(string application_id, out string token)
        {
            HttpRuntime.Cache[application_id] = true;
            token = "";
            return true;
        }

        public bool TryExtendSession(string token)
        {
            HttpRuntime.Cache[token] = true;
            return true;
        }
    }
}

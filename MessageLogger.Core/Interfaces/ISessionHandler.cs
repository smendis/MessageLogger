using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Core.Interfaces
{
    public interface ISessionHandler
    {
        string GetSessionAliveKey(string application_id);
        string GetSessionTokenKey(string access_token);
        bool IsSessionExists(string key);
        void CreateOrExtendSession(string key);
    }
}

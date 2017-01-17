using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Core.Interfaces
{
    public interface ISettingsHandler
    {
        int GetSessionLifetime();
    }
}

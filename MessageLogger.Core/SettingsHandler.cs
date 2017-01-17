using MessageLogger.Core.Interfaces;
using MessageLogger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Core
{
    public class SettingsHandler : ISettingsHandler
    {
        ISettingsRepository settingsRepository;

        public SettingsHandler(ISettingsRepository repository)
        {
            settingsRepository = repository;
        }

        public int GetSessionLifetime()
        {
            return settingsRepository.GetSessionLifetime();
        }
    }
}

using MessageLogger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageLogger.Repository.Interfaces;
using MessageLogger.Data;
using MessageLogger.Core.Dto;
using MessageLogger.Core.Helper;

namespace MessageLogger.Core
{
    public class ApplicationHandler : IApplicationHandler
    {
        private IApplicationRepository appRepository;
        private ILogRepository logRepository;

        public ApplicationHandler(IApplicationRepository app_repository, ILogRepository log_repository)
        {
            appRepository = app_repository;
            logRepository = log_repository;
        }

        public bool CheckIfAppExists(string display_name)
        {
            return appRepository.CheckIfAppExists(display_name);
        }

        public RegistrationDto RegisterApp(string display_name)
        {
            var dto = new RegistrationDto(display_name);
            var app = new Application(dto.application_id, display_name, Encryptor.Encrypt(dto.secret));
            appRepository.Add(app);

            return dto;
        }

        public bool ValidateCredentials(string application_id, string application_secret)
        {
            var app = appRepository.GetApplicationIfExists(application_id);
            if (app == null)
                return false;

            var db_app_secret = app.secret;
            var client_encrypted_secret = Encryptor.Encrypt(application_secret);

            return client_encrypted_secret.Equals(db_app_secret);
        }
        
        public async Task<int> LogAsync(Log log)
        {
            return await logRepository.AddAsync(log);
        }
    }
}

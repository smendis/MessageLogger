using MessageLogger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageLogger.Core.Dto;
using MessageLogger.Data;

namespace MessageLogger.Api.Tests.mocks
{
    class MockApplicationHandler : IApplicationHandler
    {
        public bool CheckIfAppExists(string display_name)
        {
            return false;
        }

        public Task<int> LogAsync(Log log)
        {
            Task<int> task = Task<int>.Factory.StartNew(() =>
            {
                return 1;
            });

            return task;
        }

        public RegistrationDto RegisterApp(string display_name)
        {
            return new RegistrationDto(display_name);
        }

        public bool ValidateCredentials(string application_id, string application_secret)
        {
            return true;
        }
    }
}

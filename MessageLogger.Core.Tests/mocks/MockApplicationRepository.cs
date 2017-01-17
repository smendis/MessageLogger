using MessageLogger.Data;
using MessageLogger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Core.Tests.mocks
{
    class MockApplicationRepository : IApplicationRepository
    {
        public void Add(MessageLogger.Data.Application entity)
        {
      
        }

        public bool CheckIfAppExists(string display_name)
        {
           return true;
        }

        public IEnumerable<MessageLogger.Data.Application> Find(System.Linq.Expressions.Expression<Func<MessageLogger.Data.Application, bool>> predicate)
        {
            return new List<Application>() { new Application() { application_id = "bc2491ac20454fbcbd7d0d05d56a3a15" , display_name = "Test" , secret = "4c395f0b7ce8e482b76d5df05" } };
        }
        
        public MessageLogger.Data.Application GetApplicationIfExists(string application_id)
        {
            var app = new Application() { application_id = "bc2491ac20454fbcbd7d0d05d56a3a15", display_name = "Test", secret = "4c395f0b7ce8e482b76d5df05" };
            return app;
        }

        public void Remove(MessageLogger.Data.Application entity)
        {
  
        }

        public bool ValidateCredentials(string app_id, string secret)
        {
            return true;
        }
    }
}

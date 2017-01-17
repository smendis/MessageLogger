using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Repository.Tests
{
    class LoggerDbInitializer : DropCreateDatabaseAlways<LoggerDbContext>
    {
        protected override void Seed(LoggerDbContext context)
        {
            context.Applications.Add(new Data.Application("bc2491ac20454fbcbd7d0d05d56a3a15", "Test", "4c395f0b7ce8e482b76d5df05"));
            context.Logs.Add(new Data.Log() { application_id= "bc2491ac20454fbcbd7d0d05d56a3a15", logger= "Test.MethodA", level="Error", message= "This is a test message" });
            context.Logs.Add(new Data.Log() { application_id = "bc2491ac20454fbcbd7d0d05d56a3a15", logger = "Test.MethodB", level = "Info", message = "This is a test Method" });
            context.Settings.Add(new Data.Settings() { setting_name = "session_lifetime", setting_value=1200});

            base.Seed(context);
        }
    }
}

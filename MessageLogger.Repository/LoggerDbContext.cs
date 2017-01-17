using MessageLogger.Data;
using System.Data.Entity;

namespace MessageLogger.Repository
{
    public class LoggerDbContext : DbContext
    {
        public LoggerDbContext()
            : base("name=MessageLoggerEntities")
        {

        }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
    }
}

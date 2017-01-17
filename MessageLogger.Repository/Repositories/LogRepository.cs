using MessageLogger.Data;
using MessageLogger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Repository.Repositories
{
    public class LogRepository: Repository<Log>, ILogRepository 
    {
        public LogRepository(LoggerDbContext context):base(context)
        {

        }
        
        public async Task<int> AddAsync(Log log)
        {
            (Context as LoggerDbContext).Logs.Add(log);
            return await(Context as LoggerDbContext).SaveChangesAsync();
        }
    }
}

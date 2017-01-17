using MessageLogger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Repository.Interfaces
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<int> AddAsync(Log log);
    }
}

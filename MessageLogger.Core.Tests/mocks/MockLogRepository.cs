using MessageLogger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageLogger.Data;
using System.Linq.Expressions;

namespace MessageLogger.Core.Tests.mocks
{
    class MockLogRepository : ILogRepository
    {
        public void Add(Log entity)
        {
            
        }

        public Task<int> AddAsync(Log log)
        {
            Task<int> task = Task<int>.Factory.StartNew(() =>
            {
                return 1;
            });

            return task;
        }

        public IEnumerable<Log> Find(Expression<Func<Log, bool>> predicate)
        {
            return new List<Log>();
        }

        public Log Get(int id)
        {
            return new Log();
        }

        public void Remove(Log entity)
        {

        }
    }
}

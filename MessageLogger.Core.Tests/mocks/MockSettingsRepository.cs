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
    class MockSettingsRepository : ISettingsRepository
    {
        public void Add(Settings entity)
        {
        }

        public IEnumerable<Settings> Find(Expression<Func<Settings, bool>> predicate)
        {
            return new List<Settings>();
        }

        public int GetSessionLifetime()
        {
            return 1200;
        }

        public void Remove(Settings entity)
        {
        }
    }
}

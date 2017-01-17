using MessageLogger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Api.Tests.mocks
{
    class MockHandlerProvider2 : IHandlerProvider
    {
        public IApplicationHandler GetApplicationHandler()
        {
            return new MockApplicationHandler();
        }

        public ISessionHandler GetSessionHandler()
        {
            return new MockSessionHandler2();
        }
    }
}

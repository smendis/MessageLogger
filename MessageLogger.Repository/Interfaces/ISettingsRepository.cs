﻿using MessageLogger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Repository.Interfaces
{
    public interface ISettingsRepository : IRepository<Settings>
    {
        int GetSessionLifetime();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;

namespace LCMSMSWebApi.Services
{
    public interface ISyncDatabasesService
    {
        Task UpdateLastUpdatedTimeStamp();
    }
}

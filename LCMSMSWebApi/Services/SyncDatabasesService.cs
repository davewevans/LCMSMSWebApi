using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LCMSMSWebApi.Services
{
    /// <summary>
    /// Provides implementation to keep local (client-side)
    /// databases in sync with central (server-side) database. 
    /// </summary>
    public class SyncDatabasesService: ISyncDatabasesService
    {
        private readonly ApplicationDbContext _dbContext;

        public SyncDatabasesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateLastUpdatedTimeStamp()
        {
            //
            // Only need one record for this entity.
            //

            var entity = await _dbContext.DbUpdates.FirstOrDefaultAsync();
            
            if (entity == null)
            {
                await _dbContext.DbUpdates.AddAsync(new DbUpdate
                {
                    DateTimeStamp = DateTime.Now
                });
                await _dbContext.SaveChangesAsync();
                return;
            }

            entity.DateTimeStamp = DateTime.Now;
            await _dbContext.SaveChangesAsync();

        }

    }
}

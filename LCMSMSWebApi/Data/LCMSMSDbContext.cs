using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LCMSMSWebApi.Data
{
    public class LCMSMSDbContext : DbContext
    {
        public LCMSMSDbContext(DbContextOptions<LCMSMSDbContext> options) : base(options)
        {
                
        }

        public DbSet<Orphan> Orphans { get; set; }
    }
}

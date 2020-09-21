using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LCMSMSWebApi.Data
{
    public class PaulDbContext : DbContext
    {
        public PaulDbContext(DbContextOptions<PaulDbContext> options) : base(options)
        {
                
        }

        public DbSet<OrpansRealData> Orphans { get; set; }
        public DbSet<Academic> Academics { get; set; }
        public DbSet<Narration> Narrations { get; set; }

        public DbSet<GuardianRealData> Guardians { get; set; }
    }
}

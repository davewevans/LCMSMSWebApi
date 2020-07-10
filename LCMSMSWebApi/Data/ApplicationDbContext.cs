using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LCMSMSWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrphanPicture>().HasKey(x => new { x.OrphanID, x.PictureID });
            modelBuilder.Entity<OrphanSponsor>().HasKey(x => new { x.OrphanID, x.SponsorID });

            // SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Orphan> Orphans { get; set; }
        public DbSet<Academic> Academics { get; set; }
        public DbSet<Narration> Narrations { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<DbUpdate> DbUpdates { get; set; }
        public DbSet<OrphanPicture> OrphanPictures { get; set; }
        public DbSet<OrphanSponsor> OrphanSponsors { get; set; }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var dataSeeder = new DummyDataSeeder(modelBuilder);
        }
    }
}

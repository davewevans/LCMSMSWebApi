﻿using LCMSMSWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LCMSMSWebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrphanSponsor>().HasKey(x => new { x.OrphanID, x.SponsorID });

            base.OnModelCreating(modelBuilder);
        }
       
        public DbSet<Orphan> Orphans { get; set; }
        public DbSet<Academic> Academics { get; set; }
        public DbSet<Narration> Narrations { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<DbUpdate> DbUpdates { get; set; }
        public DbSet<OrphanSponsor> OrphanSponsors { get; set; }
        public DbSet<OrphanProfilePic> OrphanProfilePics { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using LCMSMSWebApi.Models;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDbController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PaulDbContext _paulDbContext;
        private readonly TempDbContext _tempDbContext;

        private readonly IWebHostEnvironment _env;

       

        //private PaulDbContext _paulDbContext;
        public SeedDbController(ApplicationDbContext dbContext, 
            TempDbContext tempDbContext, 
            PaulDbContext paulDbContext, 
            IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _tempDbContext = tempDbContext;
            _paulDbContext = paulDbContext;

            // _paulDbContext = paulDbContext;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            // PopulateRealData();

            // var seeder = new DummyDataSeeder(_dbContext, _env);
            //seeder.SeedAllDummyData();

            // seeder.SeedGuardians();
            // seeder.SeedSponsors();
            // await seeder.SeedNarrations();
            // seeder.SeedPictures();
            // seeder.SeedOrphanSponsors();

            // seeder.SeedAcademicsForeignKey();

            // BuildLocalOrphanDB();

            // BuildAzureOrphanDB();


            //var orphanPics = _dbContext.Pictures;
            //foreach (var pic in orphanPics)
            //{
            //    _dbContext.Pictures.Remove(pic);
            //}
            //await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private void PopulateRealData()
        {
            var orphans = _paulDbContext.Orphans.ToList();
            var academics = _paulDbContext.Academics.ToList();
            var narrations = _paulDbContext.Narrations.ToList();
            var guardians = _paulDbContext.Guardians.ToList();

            foreach (var orphan in orphans)
            {
                _tempDbContext.Orphans.Add(new Orphan
                {
                    OrphanID = orphan.OrphanID,
                    FirstName = orphan.FirstName,
                    MiddleName = orphan.MiddleName,
                    LastName = orphan.LastName,
                    Gender = orphan.Gender,
                    LCMStatus = orphan.LCMStatus,
                    ProfileNumber = orphan.ProfileNumber,
                    DateOfBirth = orphan.DateOfBirth,
                    EntryDate = orphan.EntryDate,
                    GuardianID = orphan.GuardianID
                });
            }
            foreach (var academic in academics)
            {
                _tempDbContext.Academics.Add(new Academic {
                    Grade = academic.Grade,
                    KCPE = academic.KCPE,
                    KCSE = academic.KCSE,
                    School = academic.School,
                    EntryDate = academic.EntryDate,
                    OrphanID = academic.OrphanID
                });
            }
            foreach (var narration in narrations)
            {
                if (narration.OrphanID == null) continue;

                _tempDbContext.Narrations.Add(new Narration { 
                    Subject = narration.Subject,
                    Note = narration.Note,
                    EntryDate = narration.EntryDate,
                    OrphanID = narration.OrphanID,               
                });
            }

            foreach (var guardian in guardians)
            {
                _tempDbContext.Guardians.Add(new Guardian
                {
                    FirstName = guardian.FirstName,
                    LastName = guardian.LastName,
                    EntryDate = guardian.EntryDate,
                    Location = guardian.Location,
                });
            }

            try
            {
                _tempDbContext.Database.OpenConnection();
                _tempDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orphans ON");
                _tempDbContext.SaveChanges();
                _tempDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orphans OFF");
            }
            finally
            {
                _tempDbContext.Database.CloseConnection();
            }
           
        }

        public void BuildAzureOrphanDB()
        {
            // Orphans
            // Sponsors
            // Guardians
            // Narrations
            // Pictures
            // OrphanSponsors
            // Academics

            var inContext = _dbContext;
            var outContext = _dbContext;

            var inRecs = (from r in inContext.Academics select r);
            foreach (var r in inRecs)
            {
                outContext.Academics.Add(r);
            }
            outContext.Database.OpenConnection();
            try
            {
                outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Academics ON");
                outContext.SaveChanges();
                outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Academics OFF");
            }
            finally
            {
                outContext.Database.CloseConnection();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDbController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AzureDevDbContext _azureDevDbContext;
        // private readonly PaulDbContext _paulDbContext;
        private readonly IWebHostEnvironment _env;

        //private PaulDbContext _paulDbContext;
        public SeedDbController(ApplicationDbContext dbContext, AzureDevDbContext azureDevDbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _azureDevDbContext = azureDevDbContext;
           // _paulDbContext = paulDbContext;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

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
            var outContext = _azureDevDbContext;

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

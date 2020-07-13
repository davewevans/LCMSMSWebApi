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
        private readonly IWebHostEnvironment _env;

        //private PaulDbContext _paulDbContext;
        public SeedDbController(ApplicationDbContext dbContext, AzureDevDbContext azureDevDbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _azureDevDbContext = azureDevDbContext;
            _env = env;
        }

        [HttpGet]
        public IActionResult Get()
        {

            // var seeder = new DummyDataSeeder(_dbContext, _env);
            // seeder.SeedGuardians();
            // seeder.SeedSponsors();
            // seeder.SeedNarrations();
            // seeder.SeedPictures();
            // seeder.SeedOrphanPictures();
            // seeder.SeedOrphanSponsors();

            // BuildLocalOrphanDB();

            return Ok();
        }


        public void BuildLocalOrphanDB()
        {
            var inContext = _dbContext;
            var outContext = _azureDevDbContext;

            var inRecs = (from r in inContext.Sponsors select r);
            foreach (var r in inRecs)
            {
                outContext.Sponsors.Add(r);
            }
            outContext.Database.OpenConnection();
            try
            {
                outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Sponsors ON");
                outContext.SaveChanges();
                outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Sponsors OFF");
            }
            finally
            {
                outContext.Database.CloseConnection();
            }
        }
    }
}

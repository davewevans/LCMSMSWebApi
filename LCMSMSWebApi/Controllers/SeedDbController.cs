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
        private readonly IWebHostEnvironment _env;

        //private PaulDbContext _paulDbContext;
        public SeedDbController(ApplicationDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
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

            return Ok();
        }


        //public void BuildLocalOrphanDB()
        //{
        //    var outContext = _dbContext;
        //    var inContext = _paulDbContext;

        //    var inRecs = (from r in inContext.Narrations select r);
        //    foreach (var r in inRecs)
        //    {
        //        outContext.Narrations.Add(r);
        //    }
        //    outContext.Database.OpenConnection();
        //    try
        //    {
        //        outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Narrations ON");
        //        outContext.SaveChanges();
        //        outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Narrations OFF");
        //    }
        //    finally
        //    {
        //        outContext.Database.CloseConnection();
        //    }
        //}
    }
}

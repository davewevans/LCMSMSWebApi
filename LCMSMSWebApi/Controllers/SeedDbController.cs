using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDbController : ControllerBase
    {
        private AppDbContext _dbContext;
        private PaulDbContext _paulDbContext;
        public SeedDbController(AppDbContext dbContext, PaulDbContext paulDbContext)
        {
            _dbContext = dbContext;
            _paulDbContext = paulDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            bool canConnect = _paulDbContext.Database.CanConnect();

            //return Ok(canConnect);

            //BuildLocalOrphanDB();

            var records = _dbContext.Narrations.ToList();
            return Ok(records);
        }


        public void BuildLocalOrphanDB()
        {
            var outContext = _dbContext;
            var inContext = _paulDbContext;

            var inRecs = (from r in inContext.Narrations select r);
            foreach (var r in inRecs)
            {
                outContext.Narrations.Add(r);
            }
            outContext.Database.OpenConnection();
            try
            {
                outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Narrations ON");
                outContext.SaveChanges();
                outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Narrations OFF");
            }
            finally
            {
                outContext.Database.CloseConnection();
            }
        }
    }
}

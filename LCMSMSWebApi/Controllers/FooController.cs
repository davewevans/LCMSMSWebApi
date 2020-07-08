using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooController : ControllerBase
    {
        private LCMSMSDbContext _dbContext;
        private PaulDbContext _paulDbContext;
        public FooController(LCMSMSDbContext dbContext, PaulDbContext paulDbContext)
        {
            _dbContext = dbContext;
            _paulDbContext = paulDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //bool canConnect = _paulDbContext.Database.CanConnect();


            return Ok(_paulDbContext.Orphans);
        }
        //static void BuildLocalOrphanDB()
        //{
        //    using (SMSContext inContext = new SMSContext())
        //    using (LocalContext outContext = new LocalContext())
        //    {
        //        var inOrphanRecs = (from r in inContext.Orphans select r);
        //        foreach (var r in inOrphanRecs)
        //        {
        //            outContext.Orphans.Add(r);
        //        }
        //        outContext.Database.OpenConnection();
        //        try
        //        {
        //            outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orphans ON");
        //            outContext.SaveChanges();
        //            outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orphans OFF");
        //        }
        //        finally
        //        {
        //            outContext.Database.CloseConnection();
        //        }



        //    }
        //}
    }
}

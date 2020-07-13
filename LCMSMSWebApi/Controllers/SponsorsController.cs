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
    public class SponsorsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SponsorsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // TODO map to dto

            return Ok(_dbContext.Sponsors.ToList());
        }
    }
}

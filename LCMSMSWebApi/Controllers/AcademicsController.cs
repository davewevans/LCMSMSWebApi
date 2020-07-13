using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AcademicsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // TODO map to dto

            return Ok(_dbContext.Academics.ToList());
        }
    }
}

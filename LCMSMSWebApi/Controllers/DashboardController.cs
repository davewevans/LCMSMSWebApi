using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public DashboardController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("orphanStats")]
        public async Task<IActionResult> GetOrphanStats()
        {
            var orphanStatsDto = new OrphanStatisticsDto
            {
                ActiveCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus == "Active"),
                InactiveCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus == "Not Active"),
                TotalCount = await _dbContext.Orphans.CountAsync(),
            };

            return Ok(orphanStatsDto);
        }
    }
}

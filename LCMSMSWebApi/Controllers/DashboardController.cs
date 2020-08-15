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

        [HttpGet("narrationStats")]
        public async Task<IActionResult> GetNarrationStats()
        {
            var narrations = await _dbContext.Narrations.ToListAsync();
            var narrationStats = new NarrationStatisticsDto();

            narrationStats.TotalNarrationCount = narrations.Count();
            narrationStats.OrphanNarrationCount = (from x in narrations where x.OrphanID != 0 && x.OrphanID != null select x).Count();
            narrationStats.GuardianNarrationCount = (from x in narrations where x.GuardianID != 0 && x.GuardianID != null select x).Count();
            narrationStats.OrphanLast6MoCount = narrations.Where(x => x.OrphanID != 0 && x.OrphanID != null &&
                DateTime.Compare(x.EntryDate, DateTime.Today.AddMonths(-6)) >= 0).Count();
            narrationStats.GuardianLast6MoCount = narrations.Where(x => x.GuardianID != 0 && x.GuardianID != null &&
                DateTime.Compare(x.EntryDate, DateTime.Today.AddMonths(-6)) >= 0).Count();

            narrationStats.OrphanLastContact = narrations.Where(x => x.OrphanID != 0 && x.OrphanID != null).OrderByDescending(d => d.EntryDate).FirstOrDefault().EntryDate;
            narrationStats.GuardianLastContact = narrations.Where(x => x.OrphanID != 0 && x.OrphanID != null).OrderByDescending(d => d.EntryDate).FirstOrDefault().EntryDate;


            return Ok(narrationStats);
        }
    }
}

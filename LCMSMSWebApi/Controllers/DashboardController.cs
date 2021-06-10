using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LCMSMSWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/dashboard")]
    [ApiController]   
    public class DashboardController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public DashboardController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("totalCounts")]
        public async Task<IActionResult> GetTotalCounts()
        {

            var totalCounts = new TotalCountsDTO
            {
                TotalOrphans = await _dbContext.Orphans.CountAsync(),
                TotalGuardians = await _dbContext.Guardians.CountAsync(),
                TotalSponsors = await _dbContext.Sponsors.CountAsync(),
                TotalVulnerables = await _dbContext.Orphans.CountAsync(x => x.Condition.Equals("Vulnerable"))
            };

            return Ok(totalCounts);
        }

        [HttpGet("orphanStats")]
        public async Task<IActionResult> GetOrphanStats()
        {
            var orphanStatsDto = new OrphanStatisticsDTO
            {
                ActiveCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus.Equals("Active")),
                InactiveCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus.Equals("Not Active") || x.LCMStatus.Equals("Inactive")),
                ActiveInSchoolCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus.Equals("Active-In School")),
                ActiveNotInSchoolCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus.Equals("Active-Not In School")),
                InactiveMarriedCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus.Equals("Inactive-Married")),
                InactiveWorkingCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus.Equals("Inactive-Working")),
                InactiveDeceasedCount = await _dbContext.Orphans.CountAsync(x => x.LCMStatus.Equals("Inactive-Deceased")),                
                TotalCount = await _dbContext.Orphans.CountAsync(),
            };

            return Ok(orphanStatsDto);
        }

        [HttpGet("narrationStats")]
        public async Task<IActionResult> GetNarrationStats()
        {
            var narrations = await _dbContext.Narrations.ToListAsync();
            var narrationStats = new NarrationStatisticsDTO();

            narrationStats.TotalNarrationCount = narrations.Count();
            narrationStats.OrphanNarrationCount = (from x in narrations where x.OrphanID != 0 && x.OrphanID != null select x).Count();
            narrationStats.GuardianNarrationCount = (from x in narrations where x.GuardianID != 0 && x.GuardianID != null select x).Count();
            narrationStats.OrphanLast6MoCount = narrations.Where(x => x.OrphanID != 0 && x.OrphanID != null &&
                DateTime.Compare(x.EntryDate, DateTime.Today.AddMonths(-6)) >= 0).Count();
            narrationStats.GuardianLast6MoCount = narrations.Where(x => x.GuardianID != 0 && x.GuardianID != null &&
                DateTime.Compare(x.EntryDate, DateTime.Today.AddMonths(-6)) >= 0).Count();

            try
            {
               //narrationStats.OrphanLastContact = narrations.Where(x => x.OrphanID != 0 && x.OrphanID != null).OrderByDescending(d => d.EntryDate).FirstOrDefault()?.EntryDate;
               //narrationStats.GuardianLastContact = narrations.Where(x => x.GuardianID != 0 && x.GuardianID != null).OrderByDescending(d => d.EntryDate).FirstOrDefault()?.EntryDate;
            }
            catch (Exception ex)
            {
                // TODO log
            }

            return Ok(narrationStats);
        }
    }
}

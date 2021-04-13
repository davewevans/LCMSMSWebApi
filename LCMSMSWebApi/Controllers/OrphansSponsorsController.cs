using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/orphanssponsors")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    public class OrphansSponsorsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISyncDatabasesService _syncDatabasesService;

        public OrphansSponsorsController(ApplicationDbContext dbContext, ISyncDatabasesService syncDatabasesService)
        {
            _dbContext = dbContext;
            _syncDatabasesService = syncDatabasesService;
        }

        [HttpPost("assignSponsor")]
        public async Task<ActionResult> PostAssignment([FromBody] OrphanSponsorDTO orphanSponsorDto)
        {

            var newAssignment = new OrphanSponsor
            {
                OrphanID = orphanSponsorDto.OrphanID,
                SponsorID = orphanSponsorDto.SponsorID,
                EntryDate = orphanSponsorDto.EntryDate
            };

            bool exists = await _dbContext.OrphanSponsors.AnyAsync(x => x.OrphanID == orphanSponsorDto.OrphanID && x.SponsorID == orphanSponsorDto.SponsorID);
            if (exists) return Ok();

            await _dbContext.OrphanSponsors.AddAsync(newAssignment);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return Ok();
        }

        [HttpPost("removeSponsor")]
        public async Task<ActionResult> PostRemove([FromBody] OrphanSponsorDTO orphanSponsorDto)
        {
            var recordToRemove = await _dbContext.OrphanSponsors
                .FirstOrDefaultAsync(x => x.OrphanID == orphanSponsorDto.OrphanID && x.SponsorID == orphanSponsorDto.SponsorID);


            var orphanHistory = new OrphanHistory
            {
                OrphanID = orphanSponsorDto.OrphanID,
                SponsorID = orphanSponsorDto.SponsorID,
                EntryDate = DateTime.UtcNow,
                UnassignedAt = DateTime.UtcNow
            };

            // Add to orphan history
            _dbContext.OrphanHistory.Add(orphanHistory);
            await _dbContext.SaveChangesAsync();

            if (recordToRemove == null)
            {
                return BadRequest();
            }

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            _dbContext.OrphanSponsors.Remove(recordToRemove);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

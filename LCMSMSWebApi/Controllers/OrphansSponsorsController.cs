using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult> PostAssignment([FromBody] OrphanSponsorDto orphanSponsorDto)
        {

            var newAssignment = new OrphanSponsor
            {
                OrphanID = orphanSponsorDto.OrphanID,
                SponsorID = orphanSponsorDto.SponsorID,
                EntryDate = orphanSponsorDto.EntryDate
            };

            await _dbContext.OrphanSponsors.AddAsync(newAssignment);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return Ok();
        }

        [HttpPost("removeSponsor")]
        public async Task<ActionResult> PostRemove([FromBody] OrphanSponsorDto orphanSponsorDto)
        {
            var recordToRemove = await _dbContext.OrphanSponsors.FirstOrDefaultAsync(x => x.OrphanID == orphanSponsorDto.OrphanID && x.SponsorID == orphanSponsorDto.SponsorID);

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

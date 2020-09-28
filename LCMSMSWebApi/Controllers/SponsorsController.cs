using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    public class SponsorsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;

        public SponsorsController(ApplicationDbContext dbContext, IMapper mapper, ISyncDatabasesService syncDatabasesService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var sponsors = _dbContext.Sponsors.ToList();
            var sponsorsDto = _mapper.Map<List<SponsorDto>>(sponsors);

            return Ok(sponsorsDto);
        }

        [HttpGet("{id}", Name = "getSponsor")]
        public async Task<ActionResult<SponsorDto>> Get(int id)
        {
            var sponsor = await _dbContext.Sponsors.FirstOrDefaultAsync(x => x.SponsorID == id);

            if (sponsor == null)
            {
                return NotFound();
            }

            return _mapper.Map<SponsorDto>(sponsor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SponsorDto sponsorDto)
        {
            var sponsor = _mapper.Map<Sponsor>(sponsorDto);

            await _dbContext.Sponsors.AddAsync(sponsor);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            sponsorDto = _mapper.Map<SponsorDto>(sponsor);

            return new CreatedAtRouteResult("getSponsor", new { id = sponsorDto.SponsorID }, sponsorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SponsorUpdateDto sponsorUpdateDtoDto)
        {
            //
            // TODO
            // Make sure client sends complete object.
            // Put request can be error prone. For example,
            // if the dto is sent by the client and a property is null,
            // this null value will overwrite the value in the db.
            // This may or may not be what we want!
            //

            var sponsor = await _dbContext.Sponsors.FirstOrDefaultAsync(x => x.SponsorID == id);

            if (sponsor == null)
            {
                return NotFound();
            }

            sponsor = _mapper.Map(sponsorUpdateDtoDto, sponsor);

            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _dbContext.Sponsors.AnyAsync(x => x.SponsorID == id);

            if (!exists)
            {
                return NotFound();
            }

            var sponsorToDelete = await _dbContext.Sponsors.FirstOrDefaultAsync(x => x.SponsorID == id);
            _dbContext.Sponsors.Remove(sponsorToDelete);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }
    }
}

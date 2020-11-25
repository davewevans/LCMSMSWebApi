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
    public class GuardiansController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;

        public GuardiansController(ApplicationDbContext dbContext, IMapper mapper, ISyncDatabasesService syncDatabasesService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var guardians = _dbContext.Guardians.ToList();
            var guradiansDto = _mapper.Map<List<GuardianDto>>(guardians);

            return Ok(guradiansDto);
        }

        [HttpGet("{id}", Name = "getGuardian")]
        public async Task<ActionResult<GuardianDto>> Get(int id)
        {
            var guardian = await _dbContext.Guardians.FirstOrDefaultAsync(x => x.GuardianID == id);

            if (guardian == null)
            {
                return NotFound();
            }

            return _mapper.Map<GuardianDto>(guardian);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GuardianDto guardianDto)
        {
            var guardian = _mapper.Map<Guardian>(guardianDto);

            await _dbContext.Guardians.AddAsync(guardian);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            guardianDto = _mapper.Map<GuardianDto>(guardian);

            return new CreatedAtRouteResult("getGuardian", new { id = guardianDto.GuardianID }, guardianDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GuardianUpdateDTO guardianUpdateDto)
        {
            //
            // TODO
            // Make sure client sends complete object.
            // Put request can be error prone. For example,
            // if the dto is sent by the client and a property is null,
            // this null value will overwrite the value in the db.
            // This may or may not be what we want!
            //

            var guardian = await _dbContext.Guardians.FirstOrDefaultAsync(x => x.GuardianID == id);

            if (guardian == null)
            {
                return NotFound();
            }

            guardian = _mapper.Map(guardianUpdateDto, guardian);

            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _dbContext.Guardians.AnyAsync(x => x.GuardianID == id);

            if (!exists)
            {
                return NotFound();
            }

            var guardianToDelete = await _dbContext.Guardians.FirstOrDefaultAsync(x => x.GuardianID == id);
            _dbContext.Guardians.Remove(guardianToDelete);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }
    }
}

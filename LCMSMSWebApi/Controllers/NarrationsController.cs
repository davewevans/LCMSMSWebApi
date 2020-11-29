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
    public class NarrationsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;

        public NarrationsController(ApplicationDbContext dbContext, IMapper mapper, ISyncDatabasesService syncDatabasesService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var narrations = _dbContext.Narrations.ToList();
            var narrationsDto = _mapper.Map<List<NarrationDTO>>(narrations);

            return Ok(narrationsDto);
        }

        [HttpGet("{id}", Name = "GetNarration")]
        public async Task<ActionResult<NarrationDTO>> Get(int id)
        {
            var narration = await _dbContext.Narrations.FirstOrDefaultAsync(x => x.NarrationID == id);

            if (narration == null)
            {
                return NotFound();
            }

            return _mapper.Map<NarrationDTO>(narration);
        }

        [HttpGet("orphan/{orphanId}", Name = "GetOrphanNarrations")]
        public IActionResult GetOrphanNarrations(int orphanId)
        {
            var narrations = _dbContext.Narrations.Where(x => x.OrphanID == orphanId).ToList();
            var narrationsDto = _mapper.Map<List<NarrationDTO>>(narrations);

            return Ok(narrationsDto);
        }
      
        [HttpGet("guardian/{guardianId}", Name = "GetGuardianNarrations")]
        public IActionResult GetGuardianNarrations(int guardianId)
        {
            var narrations = _dbContext.Narrations.Where(x => x.GuardianID == guardianId).ToList();
            var narrationsDto = _mapper.Map<List<NarrationDTO>>(narrations);

            return Ok(narrationsDto);
        }      

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NarrationDTO narrationDto)
        {
            var narration = _mapper.Map<Narration>(narrationDto);

            await _dbContext.Narrations.AddAsync(narration);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            narrationDto = _mapper.Map<NarrationDTO>(narration);

            return new CreatedAtRouteResult("GetNarration", new { id = narrationDto.NarrationID }, narrationDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] NarrationUpdateDTO narrationUpdateDtoDto)
        {
            //
            // TODO
            // Make sure client sends complete object.
            // Put request can be error prone. For example,
            // if the dto is sent by the client and a property is null,
            // this null value will overwrite the value in the db.
            // This may or may not be what we want!
            //

            var narration = await _dbContext.Narrations.FirstOrDefaultAsync(x => x.NarrationID == id);

            if (narration == null)
            {
                return NotFound();
            }

            narration = _mapper.Map(narrationUpdateDtoDto, narration);

            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _dbContext.Narrations.AnyAsync(x => x.NarrationID == id);

            if (!exists)
            {
                return NotFound();
            }

            var narrationToDelete = await _dbContext.Narrations.FirstOrDefaultAsync(x => x.NarrationID == id);
            _dbContext.Narrations.Remove(narrationToDelete);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }
    }
}

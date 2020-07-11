using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrphansController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;

        public OrphansController(ApplicationDbContext dbContext, IMapper mapper, ISyncDatabasesService syncDatabasesService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // TODO map to dto
            var orphans = _dbContext.Orphans.ToList();
            var orphansDto = _mapper.Map<List<OrphanDto>>(orphans);

            return Ok(orphansDto);
        }

        [HttpGet("{id}", Name = "getOrphan")]
        public async Task<ActionResult<OrphanDto>> Get(int id)
        {
            var orphan = await _dbContext.Orphans.FirstOrDefaultAsync(x => x.OrphanID == id);

            if (orphan == null)
            {
                return NotFound();
            }

            return _mapper.Map<OrphanDto>(orphan);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrphanDto orphanDto)
        {
            var orphan = _mapper.Map<Orphan>(orphanDto);

            await _dbContext.Orphans.AddAsync(orphan);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            orphanDto = _mapper.Map<OrphanDto>(orphan);

            return new CreatedAtRouteResult("getOrphan", new {id = orphanDto.OrphanID }, orphanDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrphanUpdateDto orphanUpdateDtoDto)
        {
            //
            // TODO
            // Make sure client sends complete object.
            // Put request can be error prone. For example,
            // if the dto is sent by the client and a property is null,
            // this null value will overwrite the value in the db.
            // This may or may not be what we want!
            //

            var orphan = await _dbContext.Orphans.FirstOrDefaultAsync(x => x.OrphanID == id);

            if (orphan == null)
            {
                return NotFound();
            }

            orphan = _mapper.Map(orphanUpdateDtoDto, orphan);

            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _dbContext.Orphans.AnyAsync(x => x.OrphanID == id);

            if (!exists)
            {
                return NotFound();
            }

            var orphanToDelete = await _dbContext.Orphans.FirstOrDefaultAsync(x => x.OrphanID == id);
            _dbContext.Orphans.Remove(orphanToDelete);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }
    }
}

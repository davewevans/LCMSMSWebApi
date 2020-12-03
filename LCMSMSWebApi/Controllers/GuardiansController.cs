using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.enums;
using LCMSMSWebApi.Helpers;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

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
            var guradiansDto = _mapper.Map<List<GuardianDTO>>(guardians);

            return Ok(guradiansDto);
        }

        /// <summary>
        /// Get guardian records for the Syncfusion DataGrid
        /// ref: https://www.syncfusion.com/forums/154196/databind-to-paging-rest-api
        /// </summary>
        /// <returns></returns>
        [HttpGet("guardiansSFDataGrid")]
        public object GetGuardiansSFDataGrid()
        {
            var data = _dbContext.Guardians.AsQueryable();
            var count = data.Count();
            var queryString = Request.Query;

            StringValues Skip, Take, SearchTerm, ColumnName, SortDirection;

            int skip = 0;
            int top = 20;
            int sortDirection = 0;

            string searchTerm = "";
            string columnName = "";

            bool descending = false;

            // Parse query string sent from Syncfusion DataGrid
            if (queryString.Keys.Contains("$inlinecount"))
            {
                skip = queryString.TryGetValue("$skip", out Skip) ? Convert.ToInt32(Skip[0]) : 0;
                top = queryString.TryGetValue("$top", out Take) ? Convert.ToInt32(Take[0]) : data.Count();
                searchTerm = queryString.TryGetValue("SearchTerm", out SearchTerm) ? SearchTerm[0] : "";
                columnName = queryString.TryGetValue("ColumnName", out ColumnName) ? ColumnName[0] : "";
                sortDirection = queryString.TryGetValue("SortDirection", out SortDirection) ? Convert.ToInt32(SortDirection[0]) : 0;
                descending = (SortingDirection)sortDirection == SortingDirection.Descending ? true : false;
            }

            List<Guardian> guardians = new List<Guardian>();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                guardians = (from guardian in data
                           where guardian.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                           guardian.LastName.ToLower().Contains(searchTerm.ToLower())
                           select guardian)
                           .Skip(skip)
                           .Take(top)
                           .OrderByDynamic(columnName, descending)
                           .ToList();
            }
            else // No search term 
            {
                guardians = data
                    .Skip(skip)
                    .Take(top)
                    .OrderByDynamic(columnName, descending)
                    .ToList();
            }

            var guardiansDto = _mapper.Map<List<GuardianDTO>>(guardians);

            return new { Items = guardiansDto, Count = count };
        }

        [HttpGet("{id}", Name = "getGuardian")]
        public async Task<ActionResult<GuardianDTO>> Get(int id)
        {
            var guardian = await _dbContext.Guardians.FirstOrDefaultAsync(x => x.GuardianID == id);

            if (guardian == null)
            {
                return NotFound();
            }

            return _mapper.Map<GuardianDTO>(guardian);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GuardianDTO guardianDto)
        {
            var guardian = _mapper.Map<Guardian>(guardianDto);

            await _dbContext.Guardians.AddAsync(guardian);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            guardianDto = _mapper.Map<GuardianDTO>(guardian);

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

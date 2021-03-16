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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/guardians")]
    [ApiController]
    [AllowAnonymous]
    public class GuardiansController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;
        private readonly OrphanService _orphanService;
        private readonly IPictureStorageService _pictureStorageService;
        private readonly PictureService _pictureService;
        private readonly ILogger<GuardiansController> _logger;

        public GuardiansController(ApplicationDbContext dbContext, 
            IMapper mapper, 
            ISyncDatabasesService syncDatabasesService,
            OrphanService orphanService,
            IPictureStorageService pictureStorageService,
            PictureService pictureService,
            ILogger<GuardiansController> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
            _orphanService = orphanService;
            _pictureStorageService = pictureStorageService;
            _pictureService = pictureService;
            _logger = logger;
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("guardianDetails/{id}", Name = "getGuardian")]
        public async Task<ActionResult<GuardianDTO>> Get(int id)
        {
            var guardian = await _dbContext
                .Guardians
                .Include("Narrations")
                .Include("Orphans")
                .FirstOrDefaultAsync(x => x.GuardianID == id);

            if (guardian == null)
            {
                return NotFound();
            }

            guardian.Narrations = guardian.Narrations.OrderByDescending(n => n.EntryDate).ToList();

            return _mapper.Map<GuardianDTO>(guardian);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("guardianOrphans/{id}")]
        public async Task<ActionResult> GetGuardianOrphans(int id)
        {
            var guardian = await _dbContext.Guardians.FirstOrDefaultAsync(x => x.GuardianID == id);
            if (guardian is null) return NotFound();
            var orphans =  _dbContext.Orphans.Where(x => x.GuardianID == guardian.GuardianID).ToList();
            var orphansDto = _mapper.Map<List<OrphanDTO>>(orphans);

            // Set profile pic url or placeholder url for each orphan
            orphansDto.ForEach(orphan =>
            {
                orphan.ProfilePicUrl = string.IsNullOrWhiteSpace(orphan.ProfilePicFileName)
                ? $"{ _pictureStorageService.BaseUrl }{ _pictureService.PlaceholderPic }"
                : $"{ _pictureStorageService.BaseUrl }{ orphan.ProfilePicFileName }";
            });

            // Append location to profile number
            orphansDto
                .ForEach(x => x.ProfileNumber = _orphanService.AppendLocationToProfileNumber(x.ProfileNumber, x.Location));

            return Ok(orphansDto);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GuardianUpdateDTO guardianUpdateDto)
        {
            try
            {
                var guardian = await _dbContext.Guardians.FirstOrDefaultAsync(x => x.GuardianID == id);

                if (guardian == null)
                {
                    return NotFound();
                }

                guardian.FirstName = guardianUpdateDto.FirstName;
                guardian.LastName = guardianUpdateDto.LastName;
                guardian.Location = guardianUpdateDto.Location;
                guardian.MainPhone = guardianUpdateDto.MainPhone;
                guardian.AltPhone1 = guardianUpdateDto.AltPhone1;
                guardian.AltPhone2 = guardianUpdateDto.AltPhone2;
                guardian.AltPhone3 = guardianUpdateDto.AltPhone3;


                await _dbContext.SaveChangesAsync();

                await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }           
            
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

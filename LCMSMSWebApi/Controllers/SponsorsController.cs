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
    [Route("api/sponsors")]
    [ApiController]    
    [AllowAnonymous]
    public class SponsorsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;
        private readonly OrphanService _orphanService;
        private readonly IPictureStorageService _pictureStorageService;
        private readonly PictureService _pictureService;

        public SponsorsController(ApplicationDbContext dbContext, 
            IMapper mapper, 
            ISyncDatabasesService syncDatabasesService,
             OrphanService orphanService,
            IPictureStorageService pictureStorageService,
            PictureService pictureService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
            _orphanService = orphanService;
            _pictureStorageService = pictureStorageService;
            _pictureService = pictureService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var sponsors = _dbContext.Sponsors.ToList();
            var sponsorsDto = _mapper.Map<List<SponsorDTO>>(sponsors);

            return Ok(sponsorsDto);
        }

        /// <summary>
        /// Get sponsor records for the Syncfusion DataGrid
        /// ref: https://www.syncfusion.com/forums/154196/databind-to-paging-rest-api
        /// </summary>
        /// <returns></returns>
        [HttpGet("sponsorsSFDataGrid")]
        public object GetSponsorsSFDataGrid()
        {
            var data = _dbContext.Sponsors.AsQueryable();
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

            List<Sponsor> sponsors = new List<Sponsor>();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sponsors = (from sponsor in data
                           where sponsor.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                           sponsor.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                           sponsor.Email.ToLower().Contains(searchTerm.ToLower())
                           select sponsor)
                           .Skip(skip)
                           .Take(top)
                           .OrderByDynamic(columnName, descending)
                           .ToList();
            }
            else // No search term 
            {
                sponsors = data
                    .Skip(skip)
                    .Take(top)
                    .OrderByDynamic(columnName, descending)
                    .ToList();
            }

            var sponsorsDto = _mapper.Map<List<SponsorDTO>>(sponsors);

            return new { Items = sponsorsDto, Count = count };
        }

        [HttpGet("sponsorDetails/{id}", Name = "getSponsor")]
        public async Task<ActionResult<SponsorDTO>> Get(int id)
        {
            var sponsor = await _dbContext
                .Sponsors
                .FirstOrDefaultAsync(x => x.SponsorID == id);

            if (sponsor == null)
            {
                return NotFound();
            }

            var sponsorDto = _mapper.Map<SponsorDTO>(sponsor);

            // Include orphans
            var orphans = from os in _dbContext.OrphanSponsors
                          where os.SponsorID == sponsorDto.SponsorID
                          select os.Orphan;

            if (orphans != null)
                sponsorDto.Orphans = _mapper.Map<List<OrphanDTO>>(orphans.ToList());

            return sponsorDto;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("sponsorOrphans/{id}")]
        public async Task<ActionResult> GetSponsorOrphans(int id)
        {
            var sponsor = await _dbContext.Sponsors.FirstOrDefaultAsync(x => x.SponsorID == id);
            if (sponsor is null) return NotFound();            

            // Include orphans
            var orphans = from os in _dbContext.OrphanSponsors
                          where os.SponsorID == sponsor.SponsorID
                          select os.Orphan;

            if (orphans == null) return null;

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
        public async Task<ActionResult> Post([FromBody] SponsorDTO sponsorDto)
        {
            var sponsor = _mapper.Map<Sponsor>(sponsorDto);

            await _dbContext.Sponsors.AddAsync(sponsor);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            sponsorDto = _mapper.Map<SponsorDTO>(sponsor);

            return new CreatedAtRouteResult("getSponsor", new { id = sponsorDto.SponsorID }, sponsorDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SponsorUpdateDTO sponsorUpdateDto)
        {
            var sponsor = await _dbContext.Sponsors.FirstOrDefaultAsync(x => x.SponsorID == id);

            if (sponsor == null)
            {
                return NotFound();
            }

            sponsor.FirstName = sponsorUpdateDto.FirstName;
            sponsor.LastName = sponsorUpdateDto.LastName;
            sponsor.Address = sponsorUpdateDto.Address;
            sponsor.City = sponsorUpdateDto.City;
            sponsor.State = sponsorUpdateDto.State;
            sponsor.ZipCode = sponsorUpdateDto.ZipCode;
            sponsor.MainPhone = sponsorUpdateDto.MainPhone;
            sponsor.Email = sponsorUpdateDto.Email;
            sponsor.Status = sponsorUpdateDto.Status;
            sponsor.LastDonationDate = sponsorUpdateDto.LastDonationDate;

            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

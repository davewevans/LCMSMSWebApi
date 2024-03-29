﻿using System;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/narrations")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    public class NarrationsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;
        private readonly UserManager<ApplicationUser> _userManager;

        public NarrationsController(ApplicationDbContext dbContext, 
            IMapper mapper, 
            ISyncDatabasesService syncDatabasesService, 
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var narrations = _dbContext.Narrations.ToList();
            var narrationsDto = _mapper.Map<List<NarrationDTO>>(narrations);

            return Ok(narrationsDto);
        }

        /// <summary>
        /// Get narrations records for the Syncfusion DataGrid
        /// ref: https://www.syncfusion.com/forums/154196/databind-to-paging-rest-api
        /// </summary>
        /// <returns></returns>
        [HttpGet("narrationsSFDataGrid/{filter}")]
        public object GetNarrationsSFDataGrid(string filter)
        {

            // filter:
            // all
            // approved
            // rejected

            IQueryable<Narration> data;
            switch(filter)
            {
                case "all":
                    data = _dbContext.Narrations.AsQueryable();
                    break;
                case "approved":
                    data = _dbContext.Narrations.Where(x => x.Approved).AsQueryable();
                    break;
                case "rejected":
                    data = _dbContext.Narrations.Where(x => x.Rejected).AsQueryable();
                    break;
                default:
                    data = _dbContext.Narrations.AsQueryable();
                    break;
            }
            

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

            List<Narration> narrations = new List<Narration>();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                narrations = (from narration in data
                             where narration.Subject.ToLower().Contains(searchTerm.ToLower()) ||
                             narration.Note.ToLower().Contains(searchTerm.ToLower())
                             select narration)
                          .Skip(skip)
                          .Take(top)
                          .OrderByDynamic(columnName, descending)
                          .ToList();

                count = narrations.Count();
            }
            else // No search term 
            {
                narrations = data
                    .Skip(skip)
                    .Take(top)
                    .OrderByDynamic(columnName, descending)
                    .ToList();
            }

            var narrationsDto = _mapper.Map<List<NarrationDTO>>(narrations);

            return new { Items = narrationsDto, Count = count };
        }



        [HttpGet("{id}", Name = "GetNarration")]
        public async Task<ActionResult<NarrationDTO>> Get(int id)
        {
            var narration = await _dbContext.Narrations.FirstOrDefaultAsync(x => x.NarrationID == id);

            if (narration == null)
            {
                return NotFound();
            }

            var narrationDto = _mapper.Map<NarrationDTO>(narration);
            
            if (narrationDto.OrphanID != null)
            {
                var orphan = _dbContext.Orphans.FirstOrDefault(x => x.OrphanID == narration.OrphanID);
                narrationDto.OrphanName = $"{orphan?.FirstName} {orphan?.LastName}";
            }

            if (narrationDto.GuardianID != null)
            {
                var guardian = _dbContext.Guardians.FirstOrDefault(x => x.GuardianID == narration.GuardianID);
                narrationDto.GuardianName = $"{guardian?.FirstName} {guardian?.LastName}";
            }
            if (narrationDto.SubmittedByID != null)
            {
                var submittedByuser = _dbContext.Users.FirstOrDefault(x => x.Id == narration.SubmittedByID);
                narrationDto.SubmittedByName = $"{submittedByuser?.FirstName} {submittedByuser?.LastName}";
            }
            if (narrationDto.Approved && narrationDto.ApprovedByID != null)
            {
                var approvedByuser = _dbContext.Users.FirstOrDefault(x => x.Id == narrationDto.ApprovedByID);
                narrationDto.ApprovedByName = $"{approvedByuser?.FirstName} {approvedByuser?.LastName}";
            }
            if (narrationDto.Rejected && narrationDto.RejectedByID != null)
            {
                var rejectedByuser = _dbContext.Users.FirstOrDefault(x => x.Id == narrationDto.RejectedByID);
                narrationDto.RejectedByName = $"{rejectedByuser?.FirstName} {rejectedByuser?.LastName}";
            }

            return narrationDto;
        }

        //[HttpGet("orphanNarrations/{orphanId}", Name = "GetOrphanNarrations")]
        //public async Task<IActionResult> GetOrphanNarrations(int orphanId)
        //{
        //    var narrations = _dbContext.Narrations.Where(x => x.OrphanID == orphanId).ToList();
        //    var narrationsDto = _mapper.Map<List<NarrationDTO>>(narrations);
        //    narrationsDto = narrationsDto.OrderByDescending(n => n.EntryDate).ToList();
        //    return Ok(narrationsDto);
        //}
      
        //[HttpGet("guardianNarrations/{guardianId}", Name = "GetGuardianNarrations")]
        //public async Task<IActionResult> GetGuardianNarrations(int guardianId)
        //{          
        //    var narrations = _dbContext.Narrations.Where(x => x.GuardianID == guardianId).ToList();
        //    var narrationsDto = _mapper.Map<List<NarrationDTO>>(narrations);
        //    narrationsDto = narrationsDto.OrderByDescending(n => n.EntryDate).ToList();
        //    return Ok(narrationsDto);
        //}


        [HttpGet("pendingNarrationsCount")]
        public IActionResult GetPendingNarrationsCount()
        {
            var narrations = _dbContext.Narrations.Where(x => x.Approved == false && x.Rejected == false).ToList();           
            return Ok(narrations.Count());
        }


        [HttpGet("pendingApprovalNarrations")]
        public async Task<IActionResult> GetPendingApprovalNarrations([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _dbContext.Narrations.Where(x => x.Approved == false).AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDTO.RecordsPerPage);
            var narrations = await queryable.Paginate(paginationDTO)
                .Select(x => new NarrationDTO
                {
                    NarrationID = x.NarrationID,
                    Subject = x.Subject,
                    Note = x.Note,
                    EntryDate = x.EntryDate,
                    Approved = x.Approved,                   
                    SubmittedAt = x.SubmittedAt,
                    SubmittedByID = x.SubmittedByID,
                    ApprovedAt = x.ApprovedAt, 
                    ApprovedByID = x.ApprovedByID,                   
                    OrphanID = x.OrphanID,
                    GuardianID = x.GuardianID,
                    Comments = x.Comments

                }).ToListAsync();


            // var narrations = _dbContext.Narrations.Where(x => x.Approved == false).ToList();

            // populate with names of submitted by and orphan/guardian
            //var narrationsDto = _mapper.Map<List<NarrationDTO>>(narrations);

            foreach (var narration in narrations)
            {
                if (narration.OrphanID != null)
                {
                    var orphan = _dbContext.Orphans.FirstOrDefault(x => x.OrphanID == narration.OrphanID);
                    narration.OrphanName = $"{orphan?.FirstName} {orphan?.LastName}";
                }

                if (narration.GuardianID != null)
                {
                    var guardian = _dbContext.Guardians.FirstOrDefault(x => x.GuardianID == narration.GuardianID);
                    narration.GuardianName = $"{guardian?.FirstName} {guardian?.LastName}";
                }
                if (narration.SubmittedByID != null)
                {
                    var submittedByuser = _dbContext.Users.FirstOrDefault(x => x.Id == narration.SubmittedByID);
                    narration.SubmittedByName = $"{submittedByuser?.FirstName} {submittedByuser?.LastName}";
                }
            }

            narrations = narrations.OrderByDescending(n => n.EntryDate).ToList();
            return Ok(narrations);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NarrationDTO narrationDto)
        {
            var narration = _mapper.Map<Narration>(narrationDto);
            narration.EntryDate = DateTime.UtcNow;
            narration.SubmittedAt = DateTime.UtcNow;

            // find user by email to get id
            var identityUser = await _userManager.FindByEmailAsync(narrationDto.SubmittedByEmail);
            narration.SubmittedByID = identityUser != null ? identityUser.Id : "";

            await _dbContext.Narrations.AddAsync(narration);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            narrationDto = _mapper.Map<NarrationDTO>(narration);

            return new CreatedAtRouteResult("GetNarration", new { id = narrationDto.NarrationID }, narrationDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] NarrationUpdateDTO narrationUpdateDtoDto)
        {
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

        [HttpPut("ApprovedNarration/{id}")]
        public async Task<ActionResult> ApprovedNarration(int id, [FromBody] NarrationDTO narrationDto)
        {
            var narration = await _dbContext.Narrations.FirstOrDefaultAsync(x => x.NarrationID == id);

            if (narration == null)
            {
                return NotFound();
            }

            // find user by email to get id
            var identityUser = await _userManager.FindByEmailAsync(narrationDto.ApprovedByEmail);
            narration.ApprovedByID = identityUser != null ? identityUser.Id : "";
            narration.ApprovedAt = DateTime.UtcNow;
            narration.Approved = true;
            narration.Rejected = false;
            narration.Comments = narrationDto.Comments;

            await _dbContext.SaveChangesAsync();

            //await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }

        [HttpPut("RejectedNarration/{id}")]
        public async Task<ActionResult> RejectedNarration(int id, [FromBody] NarrationDTO narrationDto)
        {
            var narration = await _dbContext.Narrations.FirstOrDefaultAsync(x => x.NarrationID == id);

            if (narration == null)
            {
                return NotFound();
            }

            // find user by email to get id
            var identityUser = await _userManager.FindByEmailAsync(narrationDto.RejectedByEmail);
            narration.RejectedByID = identityUser != null ? identityUser.Id : "";
            narration.RejectedAt = DateTime.UtcNow;
            narration.Rejected = true;
            narration.Approved = false;
            narration.Comments = narrationDto.Comments;

            await _dbContext.SaveChangesAsync();

            //await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

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

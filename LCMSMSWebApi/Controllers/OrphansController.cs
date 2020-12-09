using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Helpers;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.enums;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/orphans")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    public class OrphansController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;
        private readonly IPictureStorageService _pictureStorageService;
        private readonly IDocumentStorageService _documentStorageService;
        private readonly PictureService _pictureService;
        private readonly OrphanService _orphanService;
        private readonly ILogger<OrphansController> _logger;
        private readonly string _placeholderPic = "no_image_found_300x300.jpg";
        

        public OrphansController(ApplicationDbContext dbContext,
            IMapper mapper,
            ISyncDatabasesService syncDatabasesService,
            IPictureStorageService pictureStorageService,
            IDocumentStorageService documentStorageService,
            PictureService pictureService,
            OrphanService orphanService,
            ILogger<OrphansController> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
            _pictureStorageService = pictureStorageService;
            _documentStorageService = documentStorageService;
            _pictureService = pictureService;
            _orphanService = orphanService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all orphan records.
        /// </summary>
        /// <returns></returns>
        [HttpGet("allOrphans")]
        public async Task<IActionResult> GetAll()
        {
            List<OrphanDTO> orphansDto = new List<OrphanDTO>();

            var orphans = await _dbContext.Orphans
               .AsNoTracking()
               .Include("Guardian")
               .Include("Narrations")
               .Include("Academics")
               .OrderBy(o => o.LastName)
               .ToListAsync();

            orphansDto = _mapper.Map<List<OrphanDTO>>(orphans);

            // Set profile pic url or placeholder url for each orphan
            orphansDto.ForEach(orphan =>
            {
                orphan.ProfilePicUrl = string.IsNullOrWhiteSpace(orphan.ProfilePicFileName)
                ? $"{ _pictureStorageService.BaseUrl }{ _placeholderPic }"
                : $"{ _pictureStorageService.BaseUrl }{ orphan.ProfilePicFileName }";
            });

            // Append location to profile number
            orphansDto
                .ForEach(x => x.ProfileNumber = _orphanService.AppendLocationToProfileNumber(x.ProfileNumber, x.Location));

            return Ok(orphansDto);
        }

        /// <summary>
        /// Gets orphan records with pagination, filtering, and sorting.
        /// </summary>
        /// <param name="orphanParameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] OrphanParameters orphanParameters = null)
        {
            // Find orphans based on parameters in query string (pagination, filter, sort, etc.)
            var orphans = await _orphanService.GetOrphansPagedListAsync(orphanParameters);

            // Pagination meta data for response
            var metaData = _orphanService.GetMetaData(orphans);

            // Add pagination meta data to response header
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            var orphansDto = _mapper.Map<List<OrphanDTO>>(orphans);

            // Set profile or placeholder pic for each orphan
            _orphanService.SetProfilePicUrlForOrphans(orphansDto);

            // Append location to profile number
            orphansDto
                .ForEach(x => x.ProfileNumber = _orphanService.AppendLocationToProfileNumber(x.ProfileNumber, x.Location));

            return Ok(orphansDto);
        }

        /// <summary>
        /// Get orphan records for the Syncfusion DataGrid
        /// ref: https://www.syncfusion.com/forums/154196/databind-to-paging-rest-api
        /// </summary>
        /// <returns></returns>
        [HttpGet("orphansSFDataGrid")]
        public object GetOrphansSFDataGrid()
        {
            var data = _dbContext.Orphans.AsQueryable();
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

            List<Orphan> orphans = new List<Orphan>();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                orphans = (from orphan in data
                          where orphan.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                          orphan.MiddleName.ToLower().Contains(searchTerm.ToLower()) ||
                          orphan.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                          orphan.ProfileNumber.ToLower().Contains(searchTerm.ToLower())
                           select orphan)
                           .Skip(skip)
                           .Take(top)
                           .OrderByDynamic(columnName, descending)
                           .ToList();
            }
            else // No search term 
            {
                orphans = data
                    .Skip(skip)
                    .Take(top)
                    .OrderByDynamic(columnName, descending)
                    .ToList();
            }
            
            var orphansDto = _mapper.Map<List<OrphanDTO>>(orphans);

            // Set profile or placeholder pic for each orphan
            _orphanService.SetProfilePicUrlForOrphans(orphansDto);

            // Append location to profile number
            orphansDto
                .ForEach(x => x.ProfileNumber = _orphanService.AppendLocationToProfileNumber(x.ProfileNumber, x.Location));

            return new { Items = orphansDto, Count = count };
        }

        /// <summary>
        /// Gets orphan details by orphan ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orphanDetails/{id}", Name = "orphanDetails")]
        public async Task<ActionResult<OrphanDetailsDTO>> GetOrphan(int id)
        {
            var orphan = await _dbContext.Orphans
                .AsNoTracking()
                .Include("Guardian")
                .Include("Narrations")
                .Include("Academics")
                .FirstOrDefaultAsync(x => x.OrphanID == id);

            if (orphan == null)
            {
                return NotFound();
            }

            var orphanDto = _mapper.Map<OrphanDetailsDTO>(orphan);

            // Assign the profile or placeholder pic
            orphanDto.ProfilePicUrl = string.IsNullOrWhiteSpace(orphan.ProfilePicFileName)
                ? $"{ _pictureStorageService.BaseUrl }{ _placeholderPic }"
                : $"{ _pictureStorageService.BaseUrl }{ orphan.ProfilePicFileName }";

            // Picture album for orphan
            orphanDto.Pictures = await _pictureService.FindOrphanPicsByIdAsync(id);            

            // Include sponsors
            var sponsors = from os in _dbContext.OrphanSponsors
                           where os.OrphanID == orphanDto.OrphanID
                           select os.Sponsor;
            orphanDto.Sponsors = _mapper.Map<List<SponsorDTO>>(sponsors.ToList());

            // Sort Academics
            orphanDto.Academics = orphanDto.Academics.OrderByDescending(x => x.EntryDate).ToList();

            // Append location to profile # (ex: LCM 105-W)
            orphanDto.ProfileNumber = _orphanService.AppendLocationToProfileNumber(orphanDto.ProfileNumber, orphanDto.Location);

            return orphanDto;
        }
       
        /// <summary>
        /// Gets the orphan's guardian.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orphanGuardian/{id}")]
        public async Task<ActionResult<Guardian>> GetOrphanGuardian(int id)
        {
            var orphan = await _dbContext.Orphans.FirstOrDefaultAsync(o => o.OrphanID == id);
            if (orphan == null) return BadRequest();
            if (orphan.GuardianID == null) return NotFound();

            var guardian = await _dbContext.Guardians.FirstOrDefaultAsync(g => g.GuardianID == orphan.GuardianID);
            if (guardian == null) return NotFound();
            return guardian;
        }

        /// <summary>
        /// Gets the orphan's sponsors.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orphanSponsors/{id}")]
        public async Task<ActionResult<List<SponsorDTO>>> GetOrphanSponsors(int id)
        {
            var orphan = await _dbContext.Orphans
                .FirstOrDefaultAsync(o => o.OrphanID == id);

            if (orphan == null) return BadRequest();

            // Include sponsors
            var sponsors = from os in _dbContext.OrphanSponsors
                           where os.OrphanID == orphan.OrphanID
                           select os.Sponsor;

            return _mapper.Map<List<SponsorDTO>>(sponsors.ToList());
        }

        /// <summary>
        /// Gets the picture album of the orphan.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orphanPictures/{id}")]
        public async Task<ActionResult<List<PictureDTO>>> GetOrphanPictures(int id)
        {
            var orphan = await _dbContext.Orphans
                .FirstOrDefaultAsync(o => o.OrphanID == id);

            if (orphan == null) return BadRequest("No orphan found.");

            var picDtos = await _pictureService.FindOrphanPicsByIdAsync(id);

            return Ok(picDtos);
        }

        /// <summary>
        /// Gets documents submitted by the orphan. Typcally, this will be PDF files.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orphanDocuments/{id}")]
        public async Task<ActionResult<List<PictureDTO>>> GetOrphanDocuments(int id)
        {
            var orphan = await _dbContext.Orphans
                .FirstOrDefaultAsync(o => o.OrphanID == id);

            if (orphan == null) return BadRequest();

            var documents = _dbContext.Documents
                .Include("Sponsor")
                .Where(x => x.OrphanID == id)
                .ToList();

            var docsDtos = _mapper.Map<List<DocumentDTO>>(documents);

            // Sets the base url for each pic
            docsDtos.ForEach(p =>
            {
                p.BaseUrl = _documentStorageService.BaseUrl;
            });

            return Ok(docsDtos);
        }

        /// <summary>
        /// Create new orphan record.
        /// </summary>
        /// <param name="orphanDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrphanDTO orphanDto)
        {
            var orphan = _mapper.Map<Orphan>(orphanDto);
            orphan.EntryDate = DateTime.UtcNow;
            await _dbContext.Orphans.AddAsync(orphan);
            await _dbContext.SaveChangesAsync();

            //
            // TODO not sure if I'll use this or not.
            //
            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            orphanDto = _mapper.Map<OrphanDTO>(orphan);

            return new CreatedAtRouteResult("orphanDetails", new { id = orphanDto.OrphanID }, orphanDto);
        }

        /// <summary>
        /// Edit exising orphan record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orphanEditDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrphanEditDTO orphanEditDto)
        {
            var orphan = await _dbContext.Orphans.FirstOrDefaultAsync(x => x.OrphanID == id);

            if (orphan == null)
            {
                return NotFound();
            }

            orphan = _mapper.Map(orphanEditDto, orphan);

            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }

        /// <summary>
        /// Edit a signle property in an existing orphan record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchOrphan"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Orphan> patchOrphan)
        {
            var recordToPatch = await _dbContext.Orphans.FirstOrDefaultAsync(o => o.OrphanID == id);
            if (recordToPatch == null)
            {
                return NotFound("Record not found.");
            }

            patchOrphan.ApplyTo(recordToPatch, ModelState);
            await _dbContext.SaveChangesAsync();
            return Ok(recordToPatch);

        }

        /// <summary>
        /// Delete an orphan record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

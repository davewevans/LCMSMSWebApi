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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
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
        private readonly string _placeholderPic = "no_image_found_300x300.jpg";
        

        public OrphansController(ApplicationDbContext dbContext,
            IMapper mapper,
            ISyncDatabasesService syncDatabasesService,
            IPictureStorageService pictureStorageService,
            IDocumentStorageService documentStorageService,
            PictureService pictureService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
            _pictureStorageService = pictureStorageService;
            _documentStorageService = documentStorageService;
            _pictureService = pictureService;
        }

        /// <summary>
        /// Gets all orphan records with no pagination, sorting, or filtering.
        /// </summary>
        /// <returns></returns>
        [HttpGet("allOrphans")]
        public async Task<IActionResult> GetAll()
        {
            List<OrphanDetailsDTO> orphansDto = new List<OrphanDetailsDTO>();

            var orphans = await _dbContext.Orphans
               .AsNoTracking()
               .Include("Guardian")
               .Include("Narrations")
               .Include("Academics")
               .OrderBy(o => o.LastName)
               .ToListAsync();

            orphansDto = _mapper.Map<List<OrphanDetailsDTO>>(orphans);

            // Set profile pic url or placeholder url for each orphan
            orphansDto.ForEach(orphan =>
            {
                orphan.ProfilePicUrl = string.IsNullOrWhiteSpace(orphan.ProfilePicFileName)
                ? $"{ _pictureStorageService.BaseUrl }{ _placeholderPic }"
                : $"{ _pictureStorageService.BaseUrl }{ orphan.ProfilePicFileName }";
            });
           
            return Ok(orphansDto);
        }

        /// <summary>
        /// Gets orphan records with pagination, filtering, and sorting.
        /// </summary>
        /// <param name="orphanParameters"></param>
        /// <returns></returns>
        [HttpGet("orphans")]
        public async Task<IActionResult> Get([FromQuery] OrphanParameters orphanParameters = null)
        {
            List<OrphanDetailsDTO> orphansDto = new List<OrphanDetailsDTO>();

            var orphans = await PagedList<Orphan>
                .ToPagedListAsync(_dbContext.Orphans
                .AsNoTracking()                
                .Include("Guardian")
                .Include("Narrations")
                .Include("Academics")
                .OrderBy(o => o.LastName),
                orphanParameters.PageNumber, orphanParameters.PageSize);

            var metaData = new
            {
                orphans.TotalCount,
                orphans.PageSize,
                orphans.PageNumber,
                orphans.HasNext,
                orphans.HasPrevious
            };

            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            orphansDto = _mapper.Map<List<OrphanDetailsDTO>>(orphans);

            orphansDto.ForEach(orphan =>
            {
                orphan.ProfilePicUrl = string.IsNullOrWhiteSpace(orphan.ProfilePicFileName)
              ? $"{ _pictureStorageService.BaseUrl }{ _placeholderPic }"
              : $"{ _pictureStorageService.BaseUrl }{ orphan.ProfilePicFileName }";
            });

            return Ok(orphansDto);
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
                .Include("Documents")
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

            var picDtos = _pictureService.FindOrphanPicsByIdAsync(id);

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

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
    public class AcademicsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;

        public AcademicsController(ApplicationDbContext dbContext, IMapper mapper, ISyncDatabasesService syncDatabasesService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var academics = _dbContext.Academics.ToList();
            var academicsDto = _mapper.Map<List<AcademicDTO>>(academics);

            return Ok(academicsDto);
        }

        [HttpGet("{id}", Name = "getAcademic")]
        public async Task<ActionResult<AcademicDTO>> Get(int id)
        {
            var academic = await _dbContext.Academics.FirstOrDefaultAsync(x => x.AcademicID == id);

            if (academic == null)
            {
                return NotFound();
            }

            return _mapper.Map<AcademicDTO>(academic);
        }

        [HttpGet("orphan/{orphanId}", Name = "GetOrphanAcademics")]
        public IActionResult GetOrphanAcademics(int orphanId)
        {
            var academics = _dbContext.Academics.Where(x => x.OrphanID == orphanId).ToList();
            var academicsDto = _mapper.Map<List<AcademicDTO>>(academics);
            academicsDto = academicsDto.OrderByDescending(x => x.EntryDate).ToList();
            return Ok(academicsDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AcademicDTO academicDto)
        {
            var academic = _mapper.Map<Academic>(academicDto);

            await _dbContext.Academics.AddAsync(academic);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            academicDto = _mapper.Map<AcademicDTO>(academic);

            return new CreatedAtRouteResult("getAcademic", new { id = academicDto.AcademicID }, academicDto);
        }

        [HttpPost("postinlineedit")]
        public async Task<ActionResult> PostInlineEdit([FromBody] AcademicDTO academicDto)
        {
            var academic = _dbContext.Academics.FirstOrDefault(a => a.AcademicID == academicDto.AcademicID);

            if (academic == null) return BadRequest("No Academic record found.");

            academic.Grade = academicDto.Grade;
            academic.KCPE = academicDto.KCPE;
            academic.KCSE = academicDto.KCSE;
            academic.School = academic.School;
            
            await _dbContext.SaveChangesAsync();
            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();          

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AcademicUpdateDTO academicUpdateDto)
        {
            //
            // TODO
            // Make sure client sends complete object.
            // Put request can be error prone. For example,
            // if the dto is sent by the client and a property is null,
            // this null value will overwrite the value in the db.
            // This may or may not be what we want!
            //

            var academic = await _dbContext.Academics.FirstOrDefaultAsync(x => x.AcademicID == id);

            if (academic == null)
            {
                return NotFound();
            }

            academic = _mapper.Map(academicUpdateDto, academic);

            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _dbContext.Academics.AnyAsync(x => x.AcademicID == id);

            if (!exists)
            {
                return NotFound();
            }

            var academicToDelete = await _dbContext.Academics.FirstOrDefaultAsync(x => x.AcademicID == id);
            _dbContext.Academics.Remove(academicToDelete);
            await _dbContext.SaveChangesAsync();

            await _syncDatabasesService.UpdateLastUpdatedTimeStamp();

            return NoContent();
        }
    }
}

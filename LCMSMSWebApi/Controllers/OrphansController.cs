﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Helpers;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrphansController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncDatabasesService _syncDatabasesService;
        private readonly IFileStorageService _fileStorageService;
        private readonly string _placeholderPic = "no_image_found_300x300.jpg";

        public OrphansController(ApplicationDbContext dbContext, 
            IMapper mapper, 
            ISyncDatabasesService syncDatabasesService,
            IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncDatabasesService = syncDatabasesService;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] OrphanParameters orphanParameters=null)
        {
            if (orphanParameters == null)
            {
                orphanParameters = new OrphanParameters();
            }

            var orphans = PagedList<Orphan>
                .ToPagedList(_dbContext.Orphans
                .Include("Pictures")
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

            var orphansDto = _mapper.Map<List<OrphanDetailsDto>>(orphans);

            orphansDto.ForEach(orphan =>
            {                
                var pic = _dbContext.Pictures.SingleOrDefault(p => p.PictureID == orphan.ProfilePictureID);
                orphan.ProfilePic = pic == null ?
                new PictureDto { BaseUri = _fileStorageService.BaseUri, PictureFileName = _placeholderPic } 
                : new PictureDto
                {
                    PictureID = pic.PictureID,
                    PictureFileName = pic.PictureFileName,
                    BaseUri = _fileStorageService.BaseUri,
                    SetAsProfilePic = true,
                    Caption = pic.Caption
                };

                orphan.ProfilePicUri = Path.Combine(orphan.ProfilePic.BaseUri, orphan.ProfilePic.PictureFileName);
            });            
            return Ok(orphansDto);
        }

        [HttpGet("{id}", Name = "getOrphan")]
        public async Task<ActionResult<OrphanDetailsDto>> Get(int id)
        {
            var orphan = await _dbContext.Orphans
                .AsNoTracking()
                .Include("Pictures")
                .Include("Guardian")
                .Include("Narrations")
                .Include("Academics")
                .FirstOrDefaultAsync(x => x.OrphanID == id);

            if (orphan == null)
            {
                return NotFound();
            }

            var orphanDto = _mapper.Map<OrphanDetailsDto>(orphan);

            // Assign the profile pic
            orphanDto.ProfilePic = orphanDto.Pictures
                .SingleOrDefault(p => p.PictureID == orphanDto.ProfilePictureID);

            if (orphanDto.ProfilePic == null)
            {
                orphanDto.ProfilePic = new PictureDto
                {
                    BaseUri = _fileStorageService.BaseUri,
                    PictureFileName = _placeholderPic
                };
            }

            // Sets the base uri for each pic
            orphanDto.Pictures.ForEach(p =>
            {
                p.BaseUri = _fileStorageService.BaseUri;
                p.SetAsProfilePic = p.PictureID == orphanDto.ProfilePictureID;
            });

            // Include sponsors
            var sponsors = from os in _dbContext.OrphanSponsors
                           where os.OrphanID == orphanDto.OrphanID
                           select os.Sponsor;

            orphanDto.Sponsors = _mapper.Map<List<SponsorDto>>(sponsors.ToList());

            return orphanDto;
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
        public async Task<ActionResult> Put(int id, [FromBody] OrphanEditDto orphanEditDto)
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

            orphan = _mapper.Map(orphanEditDto, orphan);

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

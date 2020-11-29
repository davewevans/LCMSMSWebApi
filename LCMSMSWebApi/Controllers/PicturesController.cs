using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    public class PicturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPictureStorageService _pictureStorageService;
        private readonly PictureService _pictureService;
        private readonly IHostEnvironment _environment;
        private readonly ILogger _logger;
        private readonly string _containerName = "lcmsmsblobdemo";        
        private const int _imageSizeMaxWidth = 800;
        private const int _profilePicMaxWidth = 300;

        public PicturesController(ApplicationDbContext context,
            IMapper mapper,
            IPictureStorageService pictureStorageService,
            PictureService pictureService,
            IHostEnvironment environment,
            ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _pictureStorageService = pictureStorageService;
            _pictureService = pictureService;
            _environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var pictures = _context.Pictures;
            return Ok(_mapper.Map<List<PictureDTO>>(pictures));
        }
      

        [HttpGet("picture/{id}")]
        public async Task<ActionResult> GetPicture(int id)
        {
            var picture = await _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == id);
            var pictureDto = _mapper.Map<PictureDTO>(picture);
            pictureDto.BaseUrl = _pictureStorageService.BaseUrl;
            return Ok(pictureDto);
        }

        [HttpGet("profilePictureUrl/{id}")]
        public async Task<ActionResult> GetOrphanProfilePicUrl(int id)
        {
            var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == id);
            if (orphan == null) return NotFound("No orphan found with that id.");
            var profilePicDto = new ProfilePicDTO
            {
                PictureURL = $"{_pictureStorageService.BaseUrl}/{orphan.ProfilePicFileName}"
            };
            return Ok(profilePicDto);
        }

        [HttpGet("orphanAlbumPictures/{id}")]
        public async Task<ActionResult> GetOrphanAlbumPictures(int id)
        {
            var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == id);
            if (orphan == null) return NotFound("No orphan found with that id.");  
            
            var pics = await _pictureService.FindOrphanPicsByIdAsync(id);

            return Ok(pics);
        }    

        [HttpPost("uploadPicture")]
        public async Task<ActionResult> UploadPicture([FromForm] PictureUploadDTO dto)
        {
            // Validation
            if (dto.File == null || dto.File.Length == 0) return BadRequest("No image file found.");
            if (dto.OrphanID == 0) return BadRequest("No Orphan ID found.");   

            // Resize if too big. If not too big, then returns null and gets bytes.           
            var pictureBytes = _pictureService.ResizeFileIfTooBig(dto.File, _imageSizeMaxWidth)
                               ?? await _pictureService.GetImageBytesAsync(dto.File);

            // Optimize with TinyPNG API
            pictureBytes = await _pictureService.OptimizeWithTinyPngAsync(pictureBytes);            

            // File storage service can use multiple connection strings (Photo & PDFs). This sets it.
            // _pictureStorageService.SetConnectionString(StorageConnectionType.Photo);          

            try
            {
                // Save file to blob storage
                var extension = Path.GetExtension(dto.File.FileName).ToLower();
                string picUrl =
                    await _pictureStorageService.SaveFile(pictureBytes, extension, _containerName,
                        dto.File.ContentType);

                var newPic = new Picture
                {
                    PictureFileName = Path.GetFileName(picUrl),
                    Caption = dto.Caption,
                    EntryDate = DateTime.Now
                };

                await _context.Pictures.AddAsync(newPic);
                await _context.SaveChangesAsync();

                // For the many-to-many relationship
                var orphan = await _context.Orphans.FirstOrDefaultAsync(x => x.OrphanID == dto.OrphanID);                
                var orphanPicObj = new OrphanPicture
                {
                    PictureID = newPic.PictureID,
                    OrphanID = orphan.OrphanID,
                    EntryDate = DateTime.UtcNow
                };
                await _context.OrphanPictures.AddAsync(orphanPicObj);
                await _context.SaveChangesAsync();

                return Ok(picUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Pictures.UploadPicture: {ex.Message}");
                return BadRequest("Not a valid request.");
            }          
        }

        [HttpPost("uploadProfilePicture")]
        public async Task<ActionResult> UploadProfilePicture([FromForm] PictureUploadDTO dto)
        {
            // Validation
            if (dto.File == null || dto.File.Length == 0) return BadRequest("No image file found.");
            if (dto.OrphanID == 0) return BadRequest("No Orphan ID found.");

            // Resize if too big. If not too big, then returns null and gets bytes.           
            var pictureBytes = _pictureService.ResizeFileIfTooBig(dto.File, _profilePicMaxWidth)
                               ?? await _pictureService.GetImageBytesAsync(dto.File);

            // Optimize with TinyPNG API
            pictureBytes = await _pictureService.OptimizeWithTinyPngAsync(pictureBytes);

            // File storage service can use multiple connection strings (Photo & PDFs). This sets it.
            //_pictureStorageService.SetConnectionString(StorageConnectionType.Photo);               

            try
            {
                // Save file to blob storage
                var extension = Path.GetExtension(dto.File.FileName);
                string picUrl =
                    await _pictureStorageService.SaveFile(pictureBytes, extension, _containerName,
                        dto.File.ContentType);

                // Save file name to Orphan entity
                var orphan = await _context.Orphans.FirstOrDefaultAsync(x => x.OrphanID == dto.OrphanID);
                orphan.ProfilePicFileName = Path.GetFileName(picUrl);
                await _context.SaveChangesAsync();

                return Ok(picUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Pictures.UploadProfilePicture: {ex.Message}");
                return BadRequest("Not a valid request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _context.Pictures.AnyAsync(x => x.PictureID == id);
            if (!exists)
            {
                return NotFound();
            }

            var picToDelete = _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == id);
            _context.Remove(picToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}

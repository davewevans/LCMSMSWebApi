﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LCMSMSWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        private readonly IFileStorageService _fileStorageService;
        private readonly ImageService _imageService;
        private readonly IHostEnvironment _environment;
        private readonly string _containerName = "lcmsmsblobdemo";
        private const int ProfilePicMaxWidth = 300;
        private const int ImageSizeMaxWidth = 800;

        public PicturesController(ApplicationDbContext context,
            IMapper mapper,
            IFileStorageService fileStorageService,
            ImageService imageService,
            IHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _imageService = imageService;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var pictures = _context.Pictures;
            return Ok(_mapper.Map<List<PictureDto>>(pictures));
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetOrphanPhotos(int id)
        //{
        //    string blobBaseUri = _fileStorageService.BaseUri;
        //    return Ok();
        //}

        [HttpGet("profilePicture/{id}")]
        public async Task<ActionResult> GetOrphanProfilePicUri(int id)
        {
            string blobBaseUri = _fileStorageService.BaseUrl;
            var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == id);
            if (orphan == null) return NotFound("No orphan found with that id.");
            var picture = await _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == orphan.ProfilePictureID);
            if (picture == null) return NotFound("No profile picture for this orphan.");
            var pictureDto = _mapper.Map<PictureDto>(picture);
            pictureDto.BaseUrl = _fileStorageService.BaseUrl;
            pictureDto.SetAsProfilePic = true;
            return Ok(pictureDto);
        }

        [HttpGet("picture/{id}")]
        public async Task<ActionResult> GetPicture(int id)
        {
            var picture = await _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == id);
            var pictureDto = _mapper.Map<PictureDto>(picture);
            pictureDto.BaseUrl = _fileStorageService.BaseUrl;
            return Ok(pictureDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No image file found.");

            PictureCreationDto picCreation;
            try
            {
                string dataStr = Request.Form[""].ToString();
                picCreation = Newtonsoft.Json.JsonConvert.DeserializeObject<PictureCreationDto>(dataStr);
                picCreation.Picture = file; 
            }
            catch (Exception ex)
            {
                // TODO log exception
                return BadRequest("Not a valid request.");
            }       

            int maxSize = picCreation.SetAsProfilePic ? ProfilePicMaxWidth : ImageSizeMaxWidth;

            var pictureBytes = _imageService.ResizeIfTooBig(picCreation.Picture, maxSize)
                               ?? await _imageService.GetImageBytesAsync(picCreation.Picture);

            pictureBytes = await _imageService.OptimizeWithTinyPngAsync(pictureBytes);

            var extension = Path.GetExtension(picCreation.Picture.FileName);

            _fileStorageService.SetConnectionString(StorageConnectionType.Photo);
            string picUri =
                await _fileStorageService.SaveFile(pictureBytes, extension, _containerName,
                    picCreation.Picture.ContentType);

            var newPic = new Picture
            {
                PictureFileName = Path.GetFileName(picUri),
                Caption = picCreation.Caption,
                CreatedAt = DateTime.Now,
                OrphanID = picCreation.OrphanID,                
            };

            try
            {
                await _context.Pictures.AddAsync(newPic);
                await _context.SaveChangesAsync();

                if (picCreation.SetAsProfilePic)
                {
                    var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == picCreation.OrphanID);
                    orphan.ProfilePictureID = newPic.PictureID;
                }

                await _context.SaveChangesAsync();
                var pictureDto = _mapper.Map<PictureDto>(newPic);
                pictureDto.BaseUrl = _fileStorageService.BaseUrl;
                pictureDto.SetAsProfilePic = picCreation.SetAsProfilePic;

                return Ok(picUri);
            }
            catch (Exception ex)
            {
                // TODO log exception
                return BadRequest("Not a valid request.");
            }
          
        }


        [HttpPut]
        public async Task<ActionResult> Put(int id, UpdateProfilePicture updateProfilePicture)
        {
            //
            // Picture has already been uploaded to storage
            // User has selected a different image to be the profile pic
            //

            var picture = await _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == id);
            if (picture == null) return NotFound();

            byte[] pictureBytes =
                await _fileStorageService.DownloadAsync(picture.PictureFileName, _containerName);

            IFormFile imageFile;
            await using (var stream = new MemoryStream(pictureBytes))
            {
                imageFile = new FormFile(stream, 0, pictureBytes.Length, picture.PictureFileName, picture.PictureFileName);
            }

            byte[] pictureBytesResized = _imageService.ResizeIfTooBig(imageFile, ProfilePicMaxWidth) ?? pictureBytes;

            var extension = Path.GetExtension(picture.PictureFileName);

            if (pictureBytes.Length > pictureBytesResized.Length)
            {
                string picUri =
                    await _fileStorageService.SaveFile(pictureBytes, extension, _containerName,
                        imageFile.ContentType);

                var newPic = new Picture
                {
                    PictureFileName = Path.GetFileName(picUri),
                    Caption = updateProfilePicture.Caption,
                    CreatedAt = DateTime.Now,
                    OrphanID = updateProfilePicture.OrphanID
                };

                await _context.Pictures.AddAsync(newPic);
                await _context.SaveChangesAsync();

                var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == newPic.OrphanID);
                orphan.ProfilePictureID = newPic.PictureID;
                await _context.SaveChangesAsync();

            }
            else // pic did not need resizing
            {
                var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == picture.OrphanID);

                // Sets the pic as the profile pic
                orphan.ProfilePictureID = picture.PictureID;
                await _context.SaveChangesAsync();
            }

            return NoContent();
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

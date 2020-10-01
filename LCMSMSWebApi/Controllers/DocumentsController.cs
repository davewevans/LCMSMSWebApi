﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly ImageService _imageService;
        private readonly IHostEnvironment _environment;
        private readonly string _containerName = "lcmsmspdfs";
        private const int ProfilePicMaxWidth = 300;
        private const int ImageSizeMaxWidth = 800;

        public DocumentsController(ApplicationDbContext context,
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
            //string blobBaseUri = _fileStorageService.BaseUri;
            //var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == id);
            //if (orphan == null) return NotFound("No orphan found with that id.");
            //var picture = await _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == orphan.ProfilePictureID);
            //if (picture == null) return NotFound("No profile picture for this orphan.");
            //var pictureDto = _mapper.Map<PictureDto>(picture);
            //pictureDto.BaseUri = _fileStorageService.BaseUri;
            //pictureDto.SetAsProfilePic = true;
            //return Ok(pictureDto);

            return Ok();
        }

        [HttpGet("picture/{id}")]
        public async Task<ActionResult> GetPicture(int id)
        {
            //var picture = await _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == id);
            //var pictureDto = _mapper.Map<PictureDto>(picture);
            //pictureDto.BaseUri = _fileStorageService.BaseUri;
            //return Ok(pictureDto);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile file)
        {
            //if (file == null || file.Length == 0) return BadRequest("No image file found.");

            //PictureCreationDto picCreation;
            //try
            //{
            //    string dataStr = Request.Form[""].ToString();
            //    picCreation = Newtonsoft.Json.JsonConvert.DeserializeObject<PictureCreationDto>(dataStr);
            //    picCreation.Picture = file;
            //}
            //catch (Exception ex)
            //{
            //    // TODO log exception
            //    return BadRequest("Not a valid request.");
            //}

            //int maxSize = picCreation.SetAsProfilePic ? ProfilePicMaxWidth : ImageSizeMaxWidth;

            //var pictureBytes = _imageService.ResizeIfTooBig(picCreation.Picture, maxSize)
            //                   ?? await _imageService.GetImageBytesAsync(picCreation.Picture);

            //pictureBytes = await _imageService.OptimizeWithTinyPngAsync(pictureBytes);

            //var extension = Path.GetExtension(picCreation.Picture.FileName);

            //string picUri =
            //    await _fileStorageService.SaveFile(pictureBytes, extension, _containerName,
            //        picCreation.Picture.ContentType);

            //var newPic = new Picture
            //{
            //    PictureFileName = Path.GetFileName(picUri),
            //    Caption = picCreation.Caption,
            //    EntryDate = DateTime.Now,
            //    OrphanID = picCreation.OrphanID,
            //};

            //try
            //{
            //    await _context.Pictures.AddAsync(newPic);
            //    await _context.SaveChangesAsync();

            //    if (picCreation.SetAsProfilePic)
            //    {
            //        var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == picCreation.OrphanID);
            //        orphan.ProfilePictureID = newPic.PictureID;
            //    }

            //    await _context.SaveChangesAsync();
            //    var pictureDto = _mapper.Map<PictureDto>(newPic);
            //    pictureDto.BaseUri = _fileStorageService.BaseUri;
            //    pictureDto.SetAsProfilePic = picCreation.SetAsProfilePic;

            //    return Ok(picUri);
            //}
            //catch (Exception ex)
            //{
            //    // TODO log exception
            //    return BadRequest("Not a valid request.");
            //}

            return Ok();
        }


        //[HttpPost]
        //public async Task<IActionResult> Post([FromForm] IFormFile image)
        //{
        //    string dataStr = Request.Form[""].ToString();
        //    var picCreation = Newtonsoft.Json.JsonConvert.DeserializeObject<PictureCreationDto>(dataStr);


        //    if (image == null || image.Length == 0)
        //        return BadRequest("Upload a file");

        //    string fileName = image.FileName;
        //    string extension = Path.GetExtension(fileName);

        //    var allowedExtensions = new List<string> { ".jpg", ".png", ".bmp" };

        //    if (!allowedExtensions.Contains(extension))
        //        return BadRequest("File is not a valid image");

        //    string newFileName = $"{Guid.NewGuid()}{extension}";
        //    string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot", "Images", newFileName);

        //    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //    {
        //        await image.CopyToAsync(fileStream);
        //    }

        //    return Ok($"Images/{newFileName}");
        //}


        [HttpPut]
        public async Task<ActionResult> Put(int id, UpdateProfilePicture updateProfilePicture)
        {
            //
            // Picture has already been uploaded to storage
            // User has selected a different image to be the profile pic
            //

            //var picture = await _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == id);
            //if (picture == null) return NotFound();

            //byte[] pictureBytes =
            //    await _fileStorageService.DownloadAsync(picture.PictureFileName, _containerName);

            //IFormFile imageFile;
            //await using (var stream = new MemoryStream(pictureBytes))
            //{
            //    imageFile = new FormFile(stream, 0, pictureBytes.Length, picture.PictureFileName, picture.PictureFileName);
            //}

            //byte[] pictureBytesResized = _imageService.ResizeIfTooBig(imageFile, ProfilePicMaxWidth) ?? pictureBytes;

            //var extension = Path.GetExtension(picture.PictureFileName);

            //if (pictureBytes.Length > pictureBytesResized.Length)
            //{
            //    string picUri =
            //        await _fileStorageService.SaveFile(pictureBytes, extension, _containerName,
            //            imageFile.ContentType);

            //    var newPic = new Picture
            //    {
            //        PictureFileName = Path.GetFileName(picUri),
            //        Caption = updateProfilePicture.Caption,
            //        EntryDate = DateTime.Now,
            //        OrphanID = updateProfilePicture.OrphanID
            //    };

            //    await _context.Pictures.AddAsync(newPic);
            //    await _context.SaveChangesAsync();

            //    var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == newPic.OrphanID);
            //    orphan.ProfilePictureID = newPic.PictureID;
            //    await _context.SaveChangesAsync();

            //}
            //else // pic did not need resizing
            //{
            //    var orphan = await _context.Orphans.SingleOrDefaultAsync(x => x.OrphanID == picture.OrphanID);

            //    // Sets the pic as the profile pic
            //    orphan.ProfilePictureID = picture.PictureID;
            //    await _context.SaveChangesAsync();
            //}

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var exists = await _context.Pictures.AnyAsync(x => x.PictureID == id);
            //if (!exists)
            //{
            //    return NotFound();
            //}

            //var picToDelete = _context.Pictures.SingleOrDefaultAsync(x => x.PictureID == id);
            //_context.Remove(picToDelete);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}


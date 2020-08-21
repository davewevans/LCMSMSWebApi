using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UWPPicturesDemo : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly ImageService _imageService;
        private readonly string _containerName = "lcmsmsblobdemo";
        private const int ImageSizeMaxWidth = 800;

        public UWPPicturesDemo(ApplicationDbContext context,
            IFileStorageService fileStorageService,
            ImageService imageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pics = _context.OrphanProfilePics;
            var dtos = new List<OrphanProfilePicDto>();
            foreach (var pic in pics)
            {
                dtos.Add(new OrphanProfilePicDto
                {
                    OrphanID = pic.OrphanID,
                    PicUrl = pic.PicUrl
                });
            }
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get (int id)
        {
            var orphanPic = await _context.OrphanProfilePics.FirstOrDefaultAsync(x => x.OrphanID == id);
            string defaultUrl = "https://lcmsmsphotostorage.blob.core.windows.net/lcmsmsblobdemo/no_image_found_300x300.jpg";

            string url = orphanPic == null ? defaultUrl : orphanPic.PicUrl;
          
            return Ok(url);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile image)
        {
            string dataStr = Request.Form[""].ToString();
            var picCreation = Newtonsoft.Json.JsonConvert.DeserializeObject<PictureCreationDto>(dataStr);
            picCreation.Picture = image;

            if (picCreation.Picture == null) return NotFound();

            var pictureBytes = _imageService.ResizeIfTooBig(picCreation.Picture, ImageSizeMaxWidth)
                               ?? await _imageService.GetImageBytesAsync(picCreation.Picture);

            pictureBytes = await _imageService.OptimizeWithTinyPngAsync(pictureBytes);

            var extension = Path.GetExtension(picCreation.Picture.FileName);

            string picUri =
                await _fileStorageService.SaveFile(pictureBytes, extension, _containerName,
                    picCreation.Picture.ContentType);

            var newPic = new OrphanProfilePic
            {
                OrphanID = picCreation.OrphanID,
                PicUrl = picUri
            };

            var existingPics = _context.OrphanProfilePics.Where(p => p.OrphanID == picCreation.OrphanID);
            foreach (var pic in existingPics)
            {
                _context.OrphanProfilePics.Remove(pic);
            }
            await _context.SaveChangesAsync();

            await _context.OrphanProfilePics.AddAsync(newPic);
            await _context.SaveChangesAsync();          

            return Ok(picUri);
        }
    }
}

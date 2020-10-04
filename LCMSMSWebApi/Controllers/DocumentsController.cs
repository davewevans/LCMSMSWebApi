using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IHostEnvironment _environment;
        private readonly string _containerName = "lcmsmspdfs";

        public DocumentsController(ApplicationDbContext context,
            IMapper mapper,
            IFileStorageService fileStorageService,
            IHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var docs = _context.Documents;
            return Ok(_mapper.Map<List<DocumentDTO>>(docs));
        }        


        [HttpGet("pdf/{id}")]
        public async Task<ActionResult> GetPdf(int id)
        {
            var doc = await _context.Documents.Include("Sponsor").FirstOrDefaultAsync(x => x.DocumentID == id);
            var docDto = _mapper.Map<DocumentDTO>(doc);
            docDto.BaseUrl = _fileStorageService.BaseUrl;
            return Ok(docDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile file)
        {

            if (file == null || file.Length == 0) return BadRequest("No image file found.");

            DocumentCreationDTO docCreation;
            try
            {
                string dataStr = Request.Form[""].ToString();
                docCreation = Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentCreationDTO>(dataStr);
                docCreation.Document = file;
            }
            catch (Exception ex)
            {
                // TODO log exception
                return BadRequest("Not a valid request.");
            }  

            var extension = Path.GetExtension(docCreation.Document.FileName);

            await using var memoryStream = new MemoryStream();
            await docCreation.Document.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            string contentType = "application/pdf";

            _fileStorageService.SetConnectionString(StorageConnectionType.Document);
            string documentUrl =
                await _fileStorageService.SaveFile(fileBytes, extension, _containerName,
                    contentType);

            var newDoc = new Document
            {
                CreatedAt = DateTime.Now,
                FileName = Path.GetFileName(documentUrl),
                OrphanID = docCreation.OrphanID,
                SponsorID = docCreation.SponsorID
            };

            try
            {
                await _context.Documents.AddAsync(newDoc);
                await _context.SaveChangesAsync();              

                //var documentDto = _mapper.Map<DocumentDTO>(newDoc);
                //documentDto.BaseUrl = _fileStorageService.BaseUri;

                return Ok(documentUrl);
            }
            catch (Exception ex)
            {
                // TODO log exception
                return BadRequest("Not a valid request.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _context.Documents.AnyAsync(x => x.DocumentID == id);
            if (!exists)
            {
                return NotFound();
            }

            var docToDelete = await _context.Documents.FirstOrDefaultAsync(x => x.DocumentID == id);
            _context.Remove(docToDelete);
            await _context.SaveChangesAsync();          

            return NoContent();
        }

    }
}


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
    [Route("api/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDocumentStorageService _documentsStorageService;
        private readonly IHostEnvironment _environment;
        private readonly string _containerName = "lcmsmspdfs";

        public DocumentsController(ApplicationDbContext context,
            IMapper mapper,
            IDocumentStorageService documentsStorageService,
            IHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _documentsStorageService = documentsStorageService;
            _environment = environment;
        }

        /// <summary>
        /// Gets all documents.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            var docs = _context.Documents;
            return Ok(_mapper.Map<List<DocumentDTO>>(docs));
        }

        /// <summary>
        /// Gets all PDFs by orphan ID.
        /// </summary>
        /// <param name="orphanId"></param>
        /// <returns></returns>
        [HttpGet("pdfs/{orphanId}")]
        public ActionResult GetPDFsByOrphanId(int orphanId)
        {
            var pdfs = _context.Documents
                .Where(x => x.OrphanID == orphanId)
                .Include("Sponsor");
            var pdfDtos = _mapper.Map<List<DocumentDTO>>(pdfs);
            pdfDtos.ForEach(x => x.BaseUrl = _documentsStorageService.BaseUrl);
            return Ok(pdfDtos);
        }

        /// <summary>
        /// Gets single PDF including Sponsor by PDF's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("pdf/{id}")]
        public async Task<ActionResult> GetPdf(int id)
        {
            var pdf = await _context.Documents.Include("Sponsor").FirstOrDefaultAsync(x => x.DocumentID == id);
            var pdfDto = _mapper.Map<DocumentDTO>(pdf);
            pdfDto.BaseUrl = _documentsStorageService.BaseUrl;
            return Ok(pdfDto);
        }       
        
        /// <summary>
        /// Upload a new PDF document.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("uploadPDF")]
        public async Task<ActionResult> UploadPDF([FromForm] UploadPDFDTO dto)
        {
            // Validation
            if (dto.File == null || dto.File.Length == 0) return BadRequest("No file found.");
            if (dto.OrphanID == 0) return BadRequest("No Orphan ID found.");


            // Save file to blob storage
            var extension = Path.GetExtension(dto.File.FileName).ToLower();
            string contentType = "application/pdf";
            await using var memoryStream = new MemoryStream();
            await dto.File.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            string documentUrl =
                await _documentsStorageService.SaveFile(fileBytes, extension, _containerName,
                    contentType);

            var newDoc = new Document
            {
                EntryDate = DateTime.UtcNow,
                FileName = Path.GetFileName(documentUrl),
                OrphanID = dto.OrphanID,
                SponsorID = dto.SponsorID,
                AllSponsors = dto.AllSponsors
            };

            try
            {
                await _context.Documents.AddAsync(newDoc);
                await _context.SaveChangesAsync();

                return Ok(documentUrl);
            }
            catch (Exception ex)
            {
                // TODO log exception
                return BadRequest("Not a valid request.");
            }
        }           

        /// <summary>
        /// Delete document.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


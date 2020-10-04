using LCMSMSWebApi.Validations;
using Microsoft.AspNetCore.Http;
using System;

namespace LCMSMSWebApi.DTOs
{
    public class DocumentCreationDTO
    {
        [FileSizeValidator(MaxFileSizeInMbs: 10)]
        [ContentTypeValidator(ContentType.PDF)]
        public IFormFile Document { get; set; }

        public string FileName { get; set; }

        public string BaseUri { get; set; }

        public DateTime EntryDate { get; set; }

        public int OrphanID { get; set; }

        public int SponsorID { get; set; }
    }
}

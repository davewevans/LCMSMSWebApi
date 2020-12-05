using LCMSMSWebApi.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.DTOs
{
    public class UploadPDFDTO
    {
        [FileSizeValidator(MaxFileSizeInMbs: 50)]
        [ContentTypeValidator(Validations.ContentType.PDF)]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string BaseUrl { get; set; }

        public string ContentType { get; set; }
       
        public int OrphanID { get; set; }

        public int SponsorID { get; set; }

        public bool AllSponsors { get; set; }
    }
}

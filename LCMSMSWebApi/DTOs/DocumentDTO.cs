using LCMSMSWebApi.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class DocumentDTO
    {
        public int DocumentID { get; set; }

        [FileSizeValidator(MaxFileSizeInMbs: 20)]
        [ContentTypeValidator(Validations.ContentType.PDF)]
        public IFormFile Document { get; set; }

        public string FileName { get; set; }

        public string BaseUrl { get; set; }

        public string ContentType { get; set; }

        public SponsorDto Sponsor { get; set; }

        public DateTime EntryDate { get; set; }

        public int OrphanID { get; set; }

        public int SponsorID { get; set; }
    }
}

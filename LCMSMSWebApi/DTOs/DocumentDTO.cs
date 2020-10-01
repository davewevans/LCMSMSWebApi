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

        [FileSizeValidator(MaxFileSizeInMbs: 10)]
        [ContentTypeValidator(ContentType.PDF)]
        public IFormFile Document { get; set; }

        public string DocumentFileName { get; set; }

        public string BaseUri { get; set; }

        public DateTime EntryDate { get; set; }

        public int OrphanID { get; set; }

        public int SponsorID { get; set; }
    }
}

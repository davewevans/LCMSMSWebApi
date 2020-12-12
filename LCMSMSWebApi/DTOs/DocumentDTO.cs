using Microsoft.AspNetCore.Http;
using System;

namespace LCMSMSWebApi.DTOs
{
    public class DocumentDTO
    {
        public int DocumentID { get; set; }
      
        public IFormFile Document { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public string BaseUrl { get; set; }

        public string ContentType { get; set; }

        public SponsorDTO Sponsor { get; set; }

        public bool AllSponsors { get; set; }

        public DateTime EntryDate { get; set; }

        public int OrphanID { get; set; }

        public int? SponsorID { get; set; }
    }
}

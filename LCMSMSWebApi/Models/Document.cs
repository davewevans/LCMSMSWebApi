using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Models
{
    public class Document
    {
        public int DocumentID { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public bool AllSponsors { get; set; }

        public int OrphanID { get; set; }

        public int? SponsorID { get; set; }

        public DateTime EntryDate { get; set; }

        public Orphan Orphan { get; set; }

        public Sponsor Sponsor { get; set; }

        
    }
}

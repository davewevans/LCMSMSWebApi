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

        public string ContentType { get; set; }

        public bool SendToAll { get; set; }

        public int OrphanID { get; set; }

        public int? SponsorID { get; set; }
    }
}

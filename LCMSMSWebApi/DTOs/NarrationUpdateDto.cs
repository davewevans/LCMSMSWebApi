using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class NarrationUpdateDto
    {
        public string Subject { get; set; }

        public string Note { get; set; }

        public DateTime EntryDate { get; set; }

        public int? OrphanID { get; set; }

        public int? GuardianID { get; set; }
    }
}

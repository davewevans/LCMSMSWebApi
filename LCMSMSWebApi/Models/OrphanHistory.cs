using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Models
{
    public class OrphanHistory
    {
        public int OrphanHistoryID { get; set; }

        public int OrphanID { get; set; }

        public int? GuardianID { get; set; }

        public string RelationshipToGuardian { get; set; }

        public int? SponsorID { get; set; }

        public DateTime UnassignedAt { get; set; }

        public DateTime EntryDate { get; set; }
    }
}

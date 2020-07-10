using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Models
{
    public class OrphanSponsor
    {
        public int OrphanID { get; set; }
        public int SponsorID { get; set; }
        public Orphan Orphan { get; set; }
        public Sponsor Sponsor { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
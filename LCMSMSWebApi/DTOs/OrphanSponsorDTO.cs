using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class OrphanSponsorDto
    {
        public int OrphanID { get; set; }
        public int SponsorID { get; set; }
        public OrphanDto Orphan { get; set; }
        public SponsorDto Sponsor { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
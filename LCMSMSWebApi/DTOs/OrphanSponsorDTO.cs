using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class OrphanSponsorDTO
    {
        public int OrphanID { get; set; }
        public int SponsorID { get; set; }
        public OrphanDTO Orphan { get; set; }
        public SponsorDTO Sponsor { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
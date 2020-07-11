using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Models
{
    public class Sponsor
    {
        // TODO: What about sponsors in foreign countries?
        // We need to allow for entering addresses with no state.


        public int SponsorID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string MainPhone { get; set; }
        public DateTime EntryDate { get; set; }
        public List<OrphanSponsor> OrphanSponsors { get; set; }
    }
}
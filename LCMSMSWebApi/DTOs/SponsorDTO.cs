using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LCMSMSWebApi.DTOs
{
    public class SponsorDTO
    {
        // TODO: What about sponsors in foreign countries?
        // We need to allow for entering addresses with no state.

        [JsonIgnore]
        public int SponsorID { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonIgnore]    
        public string FullName => $"{ FirstName } { LastName }";
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }

        //[Email(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        //[Phone(ErrorMessage = "Invalid phone number.")]
        [JsonProperty("main_phone")]
        public string MainPhone { get; set; }

        [JsonProperty("entry_date")]
        public DateTime EntryDate { get; set; }
        [JsonIgnore]
        public List<OrphanSponsorDTO> OrphanSponsors { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LCMSMSWebApi.DTOs
{
    public class GuardianDTO
    {
        [JsonIgnore]
        public int GuardianID { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonIgnore]
        public string FullName => $"{ FirstName } { LastName }";

        [JsonProperty("entry_date")]
        public DateTime EntryDate { get; set; }

        public string Location { get; set; }
    }
}

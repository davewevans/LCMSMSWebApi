using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LCMSMSWebApi.DTOs
{
    public class GuardianDTO
    {
        public int GuardianID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{ FirstName } { LastName }";

        public DateTime EntryDate { get; set; }

        public string Location { get; set; }
    }
}

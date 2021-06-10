using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Models;
using Newtonsoft.Json;

namespace LCMSMSWebApi.DTOs
{
    public class GuardianDTO
    {
        public int GuardianID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public bool IsDeceased { get; set; }

        public int NumberOfDependents { get; set; }

        public string FullName => $"{ FirstName } { LastName }";

        public DateTime EntryDate { get; set; }

        public string Location { get; set; }

        public string MainPhone { get; set; }

        public string AltPhone1 { get; set; }

        public string AltPhone2 { get; set; }

        public string AltPhone3 { get; set; }

        public List<OrphanDTO> Orphans { get; set; }

        public List<NarrationDTO> Narrations { get; set; }
    }
}

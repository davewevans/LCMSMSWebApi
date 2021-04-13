using System;
using System.Collections.Generic;

namespace LCMSMSWebApi.DTOs
{
    public class GuardianUpdateDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public bool IsDeceased { get; set; }

        public DateTime EntryDate { get; set; }

        public string Location { get; set; }

        public string MainPhone { get; set; }

        public string AltPhone1 { get; set; }

        public string AltPhone2 { get; set; }

        public string AltPhone3 { get; set; }
    }
}

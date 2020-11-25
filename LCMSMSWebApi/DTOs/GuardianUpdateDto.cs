using System;

namespace LCMSMSWebApi.DTOs
{
    public class GuardianUpdateDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime EntryDate { get; set; }

        public string Location { get; set; }
    }
}

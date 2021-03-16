using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LCMSMSWebApi.Models
{
    public class Guardian
    {
        public int GuardianID { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "An entry date is required.")]
        public DateTime EntryDate { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }

        [Phone(ErrorMessage = "Phone number is invalid.")]
        public string MainPhone { get; set; }

        [Phone(ErrorMessage = "Phone number is invalid.")]
        public string AltPhone1 { get; set; }

        [Phone(ErrorMessage = "Phone number is invalid.")]
        public string AltPhone2 { get; set; }

        [Phone(ErrorMessage = "Phone number is invalid.")]
        public string AltPhone3 { get; set; }

        public List<Orphan> Orphans { get; set; }

        public List<Narration> Narrations { get; set; }

    }
}

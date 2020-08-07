using System;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.Models
{
    public class Narration
    {
        public int NarrationID { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Subject { get; set; }

        [MaxLength(1000)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Note { get; set; }

        [Required(ErrorMessage = "An entry date is required.")]
        public DateTime EntryDate { get; set; }

        public int? OrphanID { get; set; }

        public int? GuardianID { get; set; }
    }
}
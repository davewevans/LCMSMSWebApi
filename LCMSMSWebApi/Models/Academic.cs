using System;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.Models
{
    public class Academic
    {
        public int AcademicID { get; set; }

        [MaxLength(30)]
        public string Grade { get; set; }

        [MaxLength(30)]
        public string KCPE { get; set; }

        [MaxLength(30)]
        public string KCSE { get; set; }

        [MaxLength(255)]
        public string School { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public DateTime EntryDate { get; set; }

        public int OrphanID { get; set; }
    }
}



using LCMSMSWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LCMSMSWebApi.Models
{
    public class Orphan
    {
        public int OrphanID { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "{0} is required.")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string MiddleName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(30)]
        public string LCMStatus { get; set; }

        [MaxLength(30)]
        public string ProfileNumber { get; set; }

        [Required(ErrorMessage = "An entry date is required.")]
        public DateTime EntryDate { get; set; }

        public int? GuardianID { get; set; }

        //public int? ProfilePictureID { get; set; }

        public string ProfilePicFileName { get; set; }

        public Guardian Guardian { get; set; }

        public List<Narration> Narrations { get; set; }

        // public List<Picture> Pictures { get; set; }

        public List<Academic> Academics { get; set; }

        public List<OrphanSponsor> OrphanSponsors { get; set; }

        public List<OrphanPicture> OrphanPictures { get; set; }

        public List<Document> Documents { get; set; }

    }
}


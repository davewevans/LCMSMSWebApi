using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace LCMSMSWebApi.Models
{
    public class Orphan
    {
        public int OrphanID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string LCMStatus { get; set; }

        public string ProfileNumber { get; set; }

        public DateTime EntryDate { get; set; }

        public int? GuardianID { get; set; }

        public List<Narration> Narrations { get; set; }

        public List<OrphanPicture> OrphanPictures { get; set; }

        public List<OrphanSponsor> OrphanSponsors { get; set; }
    }
}


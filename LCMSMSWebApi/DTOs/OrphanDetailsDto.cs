using LCMSMSWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class OrphanDetailsDTO
    {
        public int OrphanID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string LCMStatus { get; set; }

        public string ExitStatus { get; set; }

        public string ProfileNumber { get; set; }

        public DateTime? YearOfAdmission { get; set; }

        public string Condition { get; set; }

        public string Location { get; set; }

        public DateTime EntryDate { get; set; }

        public string RelationshipToGuardian { get; set; }

        public int? GuardianID { get; set; }

        public string ProfilePicFileName { get; set; }

        public string ProfilePicUrl { get; set; }        

        public List<PictureDTO> Pictures { get; set; }

        public List<OrphanSponsorDTO> OrphanSponsors { get; set; }

        public List<SponsorDTO> Sponsors { get; set; }

        public GuardianDTO Guardian { get; set; }

        public List<NarrationDTO> Narrations { get; set; }

        public List<AcademicDTO> Academics { get; set; }

        public List<DocumentDTO> Documents { get; set; }
    }
}

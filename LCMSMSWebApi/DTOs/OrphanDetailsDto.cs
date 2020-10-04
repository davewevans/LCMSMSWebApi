using LCMSMSWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class OrphanDetailsDto
    {
        public int OrphanID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string LCMStatus { get; set; }

        public string ProfileNumber { get; set; }

        public DateTime EntryDate { get; set; }

        public int? GuardianID { get; set; }

        public int? ProfilePictureID { get; set; }

        public string ProfilePicUrl { get; set; }

        public PictureDto ProfilePic { get; set; }

        public List<PictureDto> Pictures { get; set; }

        public List<OrphanSponsorDto> OrphanSponsors { get; set; }

        public List<SponsorDto> Sponsors { get; set; }

        public GuardianDto Guardian { get; set; }

        public List<NarrationDto> Narrations { get; set; }

        public List<AcademicDto> Academics { get; set; }

        public List<DocumentDTO> Documents { get; set; }
    }
}

using LCMSMSWebApi.Models;
using System;
using System.Collections.Generic;


namespace LCMSMSWebApi.DTOs
{
    public class OrphanDTO
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

        public List<Narration> Narrations { get; set; }

        public List<Sponsor> Sponsors { get; set; }

        public Guardian Guardian { get; set; }

        public List<AcademicDTO> Academics { get; set; }

        public override string ToString()
        {
            return $"{ FirstName } { MiddleName } { LastName } { Gender } { DateOfBirth:d} { LCMStatus } { ProfileNumber }";
        }
    }
}


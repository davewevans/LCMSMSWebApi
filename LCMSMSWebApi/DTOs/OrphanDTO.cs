using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Models;
using Newtonsoft.Json;


namespace LCMSMSWebApi.DTOs
{
    public class OrphanDto
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

        public PictureDto ProfilePic { get; set; }

        public List<PictureDto> Pictures { get; set; }

        public List<Narration> Narrations { get; set; }

        public List<Sponsor> Sponsors { get; set; }

        public Guardian Guardian { get; set; }

        public List<AcademicDto> Academics { get; set; }

        public override string ToString()
        {
            return $"{ FirstName } { MiddleName } { LastName } { Gender } { DateOfBirth:d} { LCMStatus } { ProfileNumber }";
        }
    }
}


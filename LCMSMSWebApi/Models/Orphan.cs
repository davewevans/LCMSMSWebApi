using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.enums;

namespace LCMSMSWebApi.Models
{
    public class Orphan
    {
        public int OrphanId { get; set; }
        //[Required] public string FirstName { get; set; } = string.Empty;
        //public string MiddleName { get; set; } = string.Empty;
        //[Required] public string LastName { get; set; } = string.Empty;

        //[NotMapped]
        //public string FullName => $"{FirstName} {MiddleName} {LastName}";
        //public Gender Gender { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string LCMStatus { get; set; }
        //public string ProfileNumber { get; set; }
        //public DateTime EntryDate { get; set; }
        //public int GuardianId { get; set; }
        
    }
}


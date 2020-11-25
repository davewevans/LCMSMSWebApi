using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }       

        public DateTime EntryDate { get; set; }

    }
}

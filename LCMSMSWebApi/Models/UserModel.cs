using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public byte[] Salt { get; set; }      

        public DateTime CreatedAt { get; set; }

    }
}

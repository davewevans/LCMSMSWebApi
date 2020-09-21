using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Models
{
    public class GuardianRealData
    {
        [Key]
        public int GuardianID { get; set; }
   
        public string FirstName { get; set; } = string.Empty;

    
        public string LastName { get; set; } = string.Empty;

   
        public DateTime EntryDate { get; set; }

        public string Location { get; set; }
    }
}

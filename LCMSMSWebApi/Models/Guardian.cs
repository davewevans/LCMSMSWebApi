using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace LCMSMSWebApi.Models
{
    public class Guardian
    {
        public int GuardianID { get; set; }
       
        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
      
        public DateTime EntryDate { get; set; }

        public string Location { get; set; }

        public List<Orphan> Orphans { get; set; }
    }
}

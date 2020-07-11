using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.Models
{
    public class Narration
    {
        public int NarrationID { get; set; }

        public string Subject { get; set; }

        public string Note { get; set; }

        public DateTime EntryDate { get; set; }

        public int? OrphanID { get; set; }

        public int? GuardianID { get; set; }
    }
}
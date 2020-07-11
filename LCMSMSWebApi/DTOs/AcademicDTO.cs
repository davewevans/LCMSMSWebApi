using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class AcademicDto
    {
        public int AcademicID { get; set; }
        public string Grade { get; set; }
        public string KCPE { get; set; }
        public string KCSE { get; set; }
        public string School { get; set; }
        public DateTime EntryDate { get; set; }
        public int OrphanID { get; set; }
    }
}



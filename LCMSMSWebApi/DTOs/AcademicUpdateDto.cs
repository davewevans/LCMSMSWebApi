using System;

namespace LCMSMSWebApi.DTOs
{
    public class AcademicUpdateDTO
    {
        public string Grade { get; set; }
        public string KCPE { get; set; }
        public string KCSE { get; set; }
        public string School { get; set; }
        public DateTime EntryDate { get; set; }
        public int OrphanID { get; set; }
    }
}

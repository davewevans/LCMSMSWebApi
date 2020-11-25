using System;

namespace LCMSMSWebApi.DTOs
{
    public class NarrationStatisticsDTO
    {    
        public int TotalNarrationCount { get; set; }

        public int OrphanNarrationCount { get; set; }

        public int GuardianNarrationCount { get; set; }

        public int OrphanLast6MoCount { get; set; }

        public int GuardianLast6MoCount { get; set; }

        public DateTime? OrphanLastContact { get; set; }

        public DateTime? GuardianLastContact { get; set; }        
    }
}

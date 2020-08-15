using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class OrphanStatisticsDto
    {
        public int ActiveCount { get; set; } 

        public int InactiveCount { get; set; }

        public int UnknownCount
        {
            get
            {
                return TotalCount - (ActiveCount + InactiveCount);
            }
        }

        public int TotalCount { get; set; }
    }
}

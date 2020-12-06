namespace LCMSMSWebApi.DTOs
{
    public class OrphanStatisticsDTO
    {
        public int ActiveCount { get; set; } 

        public int InactiveCount { get; set; }

        public int ActiveInSchoolCount { get; set; }

        public int ActiveNotInSchoolCount { get; set; }

        public int InactiveMarriedCount { get; set; }

        public int InactiveWorkingCount { get; set; }

        public int InactiveDeceasedCount { get; set; }

        public int UnknownCount
        {
            get
            {
                return TotalCount - (ActiveCount + InactiveCount + ActiveInSchoolCount + ActiveNotInSchoolCount + InactiveMarriedCount + InactiveWorkingCount + InactiveDeceasedCount);
            }
        }

        public int TotalCount { get; set; }
    }
}

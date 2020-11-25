namespace LCMSMSWebApi.DTOs
{
    public class OrphanStatisticsDTO
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

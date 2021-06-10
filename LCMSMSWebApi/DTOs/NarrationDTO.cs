using System;

namespace LCMSMSWebApi.DTOs
{
    public class NarrationDTO
    {
        public int NarrationID { get; set; }

        public string Subject { get; set; }

        public string Note { get; set; }

        public DateTime EntryDate { get; set; }

        public bool Approved { get; set; }

        public bool Rejected { get; set; }

        public DateTime? SubmittedAt { get; set; }

        public string SubmittedByID { get; set; }

        public string SubmittedByEmail { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public string ApprovedByID { get; set; }

        public DateTime? RejectedAt { get; set; }

        public string RejectedByID { get; set; }

        public string ApprovedByEmail { get; set; }

        public string ApprovedByName { get; set; }

        public string RejectedByEmail { get; set; }

        public string RejectedByName { get; set; }

        public string Comments { get; set; }

        public string SubmittedByName { get; set; }        

        public string OrphanName { get; set; }

        public string GuardianName { get; set; }

        public int? OrphanID { get; set; }

        public int? GuardianID { get; set; }

    }
}
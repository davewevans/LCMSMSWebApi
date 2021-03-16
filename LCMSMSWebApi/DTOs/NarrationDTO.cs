using System;

namespace LCMSMSWebApi.DTOs
{
    public class NarrationDTO
    {
        public int NarrationID { get; set; }

        public string Subject { get; set; }

        public string SubjectPendingApproval { get; set; }

        public string Note { get; set; }

        public string NotePendingApproval { get; set; }

        public DateTime EntryDate { get; set; }

        public bool Approved { get; set; }

        public DateTime? SubmittedAt { get; set; }

        public string SubmittedByID { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public string ApprovedByID { get; set; }

        public string SubmittedByName { get; set; }

        public string ApprovedByName { get; set; }

        public int? OrphanID { get; set; }

        public int? GuardianID { get; set; }

    }
}
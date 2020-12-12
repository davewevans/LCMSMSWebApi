using System;

namespace LCMSMSWebApi.DTOs
{
    public class PictureDTO
    {       
        public int PictureID { get; set; }     

        public string PictureFileName { get; set; }

        public string BaseUrl { get; set; }

        public string Caption { get; set; }

        public DateTime? TakenDate { get; set; }

        public DateTime EntryDate { get; set; }

        public int OrphanID { get; set; }
    }
}
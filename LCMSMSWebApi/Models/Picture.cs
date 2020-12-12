using System;
using System.Collections.Generic;

namespace LCMSMSWebApi.Models
{
    public class Picture
    {
        public int PictureID { get; set; }

        public string PictureFileName { get; set; }

        public string Caption { get; set; }

        public DateTime? TakenDate { get; set; }

        public DateTime EntryDate { get; set; }

        public List<OrphanPicture> OrphanPictures { get; set; }
    }
}
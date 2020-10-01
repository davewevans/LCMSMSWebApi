using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.Models
{
    public class Picture
    {
        public int PictureID { get; set; }
        public string PictureFileName { get; set; }
        public string Caption { get; set; }
        public DateTime EntryDate { get; set; }
        public int OrphanID { get; set; }
        public Orphan Orphan { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
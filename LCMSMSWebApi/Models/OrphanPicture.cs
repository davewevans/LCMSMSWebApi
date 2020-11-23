using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Models
{
    public class OrphanPicture
    {
        public int OrphanID { get; set; }

        public int PictureID { get; set; }

        public DateTime EntryDate { get; set; }

        public Orphan Orphan { get; set; }

        public Picture Picture { get; set; }
        
    }
}

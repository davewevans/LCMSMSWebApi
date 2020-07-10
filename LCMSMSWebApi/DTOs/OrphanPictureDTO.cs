using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class OrphanPictureDTO
    {
        public int OrphanID { get; set; }
        public int PictureID { get; set; }
        public DateTime EntryDate { get; set; }
        public OrphanDTO Orphan { get; set; }
        public PictureDTO Picture { get; set; }
    }
}
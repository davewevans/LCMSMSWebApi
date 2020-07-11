using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class OrphanPictureDto
    {
        public int OrphanID { get; set; }
        public int PictureID { get; set; }
        public DateTime EntryDate { get; set; }
        public OrphanDto Orphan { get; set; }
        public PictureDto Picture { get; set; }
    }
}
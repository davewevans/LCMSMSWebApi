using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class PictureDTO
    {
        public int PictureID { get; set; }
        public string PictureUri { get; set; }
        public string Caption { get; set; }
        public DateTime EntryDate { get; set; }
        public List<OrphanPictureDTO> OrphanPictures { get; set; }
    }
}
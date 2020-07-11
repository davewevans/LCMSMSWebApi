using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LCMSMSWebApi.DTOs
{
    public class PictureDto
    {
        [JsonIgnore]
        public int PictureID { get; set; }

        public string PictureUri { get; set; }

        public string Caption { get; set; }

        public DateTime EntryDate { get; set; }

        public List<OrphanPictureDto> OrphanPictures { get; set; }
    }
}
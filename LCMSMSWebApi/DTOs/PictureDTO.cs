using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LCMSMSWebApi.DTOs
{
    public class PictureDTO
    {
        [JsonIgnore]
        public int PictureID { get; set; }

        public string PictureUri { get; set; }

        public string Caption { get; set; }

        [JsonProperty("entry_date")]
        public DateTime EntryDate { get; set; }

        [JsonIgnore]
        public List<OrphanPictureDTO> OrphanPictures { get; set; }
    }
}
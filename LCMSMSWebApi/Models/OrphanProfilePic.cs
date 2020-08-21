using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Models
{
    //
    // Temporary for demo purposes only
    //
    public class OrphanProfilePic
    {
        public int OrphanProfilePicID { get; set; }

        public int OrphanID { get; set; }

        public string PicUrl { get; set; }
    }
}

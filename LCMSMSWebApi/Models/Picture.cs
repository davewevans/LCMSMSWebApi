﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.Models
{
    public class Picture
    {
        public int PictureID { get; set; }
        public string PictureUri { get; set; }
        public string Caption { get; set; }
        public DateTime EntryDate { get; set; }
        public List<OrphanPicture> OrphanPictures { get; set; }
    }
}
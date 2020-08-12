﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace LCMSMSWebApi.DTOs
{
    public class PictureDto
    {       
        public int PictureID { get; set; }

        [FileSizeValidator(MaxFileSizeInMbs: 4)]
        [ContentTypeValidator(ContentType.Image)]
        public IFormFile Picture { get; set; }

        public string PictureFileName { get; set; }

        public string BaseUri { get; set; }

        public bool SetAsProfilePic { get; set; }

        public string Caption { get; set; }

        public DateTime EntryDate { get; set; }

        public int OrphanID { get; set; }
    }
}
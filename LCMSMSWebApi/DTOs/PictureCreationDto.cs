using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LCMSMSWebApi.Validations;
using Microsoft.AspNetCore.Http;

namespace LCMSMSWebApi.DTOs
{
    public class PictureCreationDto
    {
        [FileSizeValidator(MaxFileSizeInMbs: 4)]
        [ContentTypeValidator(ContentType.Image)]
        public IFormFile Picture { get; set; }

        public string PictureFileName { get; set; }

        public bool SetAsProfilePic { get; set; }

        public string Caption { get; set; }

        public int OrphanID { get; set; }
    }
}

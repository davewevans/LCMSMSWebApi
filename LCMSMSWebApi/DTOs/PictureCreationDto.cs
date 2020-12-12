using LCMSMSWebApi.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.DTOs
{
    public class PictureCreationDTO
    {
        //[FileSizeValidator(MaxFileSizeInMbs: 10)]
        //[ContentTypeValidator(Validations.ContentType.Image)]
        public IFormFile Picture { get; set; }

        public string PictureFileName { get; set; }

        public bool SetAsProfilePic { get; set; }

        [StringLength(256)]
        public string Caption { get; set; }

        public DateTime? TakenDate { get; set; }

        public string ContentType { get; set; }

        public int OrphanID { get; set; }
    }
}

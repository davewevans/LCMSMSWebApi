using LCMSMSWebApi.Validations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LCMSMSWebApi.DTOs
{
    public class PictureUploadDTO
    {
        [FileSizeValidator(MaxFileSizeInMbs: 20)]
        [ContentTypeValidator(ContentType.Image)]
        public IFormFile File { get; set; }

        public string Caption { get; set; }

        [Required(ErrorMessage = "No orphan ID found.")]
        public int OrphanID { get; set; }
    }
}

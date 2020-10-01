using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LCMSMSWebApi.Validations
{
    public class ContentTypeValidator : ValidationAttribute
    {
        private readonly string[] validContentTypes;

        private readonly string[] imageContentTypes = new string[] { "image/jpeg", "image/png", "image/gif" };

        private readonly string[] documentContentTypes = new string[] { "application/pdf", "application/x-pdf" };

        public ContentTypeValidator(string[] ValidContentTypes)
        {
            validContentTypes = ValidContentTypes;
        }

        public ContentTypeValidator(ContentType contentTypeGroup)
        {
            switch (contentTypeGroup)
            {
                case ContentType.Image:
                    validContentTypes = imageContentTypes;
                    break;
                case ContentType.PDF:
                    validContentTypes = documentContentTypes;
                    break;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (!validContentTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Content-Type should be one of the following: {string.Join(", ", validContentTypes)}");
            }

            return ValidationResult.Success;

        }

    }

    public enum ContentType
    {
        Image,
        PDF
    }
}

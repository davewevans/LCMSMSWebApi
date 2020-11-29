using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TinifyAPI;
using Exception = TinifyAPI.Exception;

namespace LCMSMSWebApi.Services
{
    public class PictureService
    {
        private readonly ApplicationDbContext context;
        private readonly IPictureStorageService pictureStorageService;

        public PictureService(ApplicationDbContext context, IPictureStorageService pictureStorageService)
        {
            this.context = context;
            this.pictureStorageService = pictureStorageService;
        }

        public PictureService(IConfiguration configuration)
        {
            Tinify.Key = configuration.GetValue<string>("TinyPngApiKey");
        }       

        public byte[] ResizeFileIfTooBig(IFormFile imageFile, int maxWidth)
        {
            // Checks if image size (not bytes) is too big. If so, the resizes per max width.
            // I didn't refactor into two methods to prevent loading to stream multiple times.

            try
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var encoder = GetEncoder(extension?.ToLower());
                using var stream = imageFile.OpenReadStream();
                using var output = new MemoryStream();
                using var image = Image.Load(stream);
                if (image.Width <= maxWidth) return null;
                var divisor = image.Width / maxWidth;
                var height = Convert.ToInt32(Math.Round((decimal)(image.Height / divisor)));

                image.Mutate(x => x.Resize(maxWidth, height));
                image.Save(output, encoder);
                output.Position = 0;
                return output.ToArray();
                
            }
            catch (Exception ex)
            {
                // TODO log exception
                return null;
            }
        }

        public async Task<byte[]> GetImageBytesAsync(IFormFile imageFile)
        {
            await using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        private static IImageEncoder GetEncoder(string extension)
        {
            IImageEncoder encoder = null;

            extension = extension.Replace(".", "");

            var isSupported = Regex.IsMatch(extension, "gif|png|jpe?g", RegexOptions.IgnoreCase);

            if (isSupported)
            {
                switch (extension)
                {
                    case "png":
                        encoder = new PngEncoder();
                        break;
                    case "jpg":
                        encoder = new JpegEncoder();
                        break;
                    case "jpeg":
                        encoder = new JpegEncoder();
                        break;
                    case "gif":
                        encoder = new GifEncoder();
                        break;
                    default:
                        encoder = new PngEncoder();
                        break;
                }
            }

            return encoder;
        }

        private static IImageDecoder GetDecoder(string extension)
        {
            IImageDecoder decoder = null;

            extension = extension.Replace(".", "");

            var isSupported = Regex.IsMatch(extension, "gif|png|jpe?g", RegexOptions.IgnoreCase);

            if (isSupported)
            {
                switch (extension)
                {
                    case "png":
                        decoder = new PngDecoder();
                        break;
                    case "jpg":
                        decoder = new JpegDecoder();
                        break;
                    case "jpeg":
                        decoder = new JpegDecoder();
                        break;
                    case "gif":
                        decoder = new GifDecoder();
                        break;
                    default:
                        decoder = new PngDecoder();
                        break;
                }
            }

            return decoder;
        }

        public void ResizeForProfile(int width)
        {

        }

        public async Task<byte[]> OptimizeWithTinyPngAsync(byte[] pictureBytes)
        {
            try
            {
                //
                // Optimize image with TinyPng API
                // https://tinypng.com/developers/reference/dotnet
                //
                return await Tinify.FromBuffer(pictureBytes).ToBuffer();
            }
            catch (Exception ex)
            {
                // TODO log exception
                return pictureBytes;
            }
        }

        public async Task<List<PictureDTO>> FindOrphanPicsByIdAsync(int id)
        {
            var pics = from oPic in context.OrphanPictures
                       where oPic.OrphanID == id
                       select new PictureDTO
                       {
                           PictureID = oPic.Picture.PictureID,
                           PictureFileName = oPic.Picture.PictureFileName,
                           Caption = oPic.Picture.Caption,
                           OrphanID = id,
                           BaseUrl = pictureStorageService.BaseUrl                          
                       };

            return await pics.ToListAsync();
        }

    }
}

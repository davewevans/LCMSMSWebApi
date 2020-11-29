using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Services
{
    public class InAppStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;
        public string BaseUrl { get; private set; }

        public InAppStorageService(IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor
            )
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = env.WebRootPath;
        }

        public Task DeleteFile(string fileRoute, string containerName)
        {
            var fileName = Path.GetFileName(fileRoute);
            string fileDirectory = Path.Combine(BaseUrl, containerName, fileName);
            if (File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute,
            string contentType)
        {
            if (!string.IsNullOrEmpty(fileRoute))
            {
                await DeleteFile(fileRoute, containerName);
            }

            return await SaveFile(content, extension, containerName, contentType);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string containerName, string contentType, string fileName=null)
        {
            fileName ??= $"{Guid.NewGuid()}{extension}";
            
            string folder = Path.Combine(BaseUrl, containerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string savingPath = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(savingPath, content);

            var currentUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var pathForDatabase = Path.Combine(currentUrl, containerName, fileName).Replace("\\", "/");
            return pathForDatabase;
        }

        public string GetUri(string fileName, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> DownloadAsync(string fileName, string containerName)
        {
            throw new NotImplementedException();
        }
    }
}

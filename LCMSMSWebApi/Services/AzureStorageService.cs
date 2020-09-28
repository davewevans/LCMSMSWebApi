using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Services
{
    public enum StorageConnectionType
    {
        Photo,
        Document
    }

    public class AzureStorageService : IFileStorageService
    {
        private string connectionString;

        private readonly IConfiguration configuration;

        public string BaseUri { get; }

        // AzureDocumentStorage
        // AzurePhotoStorage

        public AzureStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzurePhotoStorage");
            BaseUri = configuration.GetValue<string>("BlobBaseUri");
            this.configuration = configuration;
        }

        public void SetConnectionString(StorageConnectionType storageType)
        {
            switch (storageType)
            {
                case StorageConnectionType.Photo:
                    connectionString = configuration.GetConnectionString("AzurePhotoStorage");
                    break;
                case StorageConnectionType.Document:
                    connectionString = configuration.GetConnectionString("AzureDocumentStorage");
                    break;
            }
        }

        public async Task DeleteFile(string fileRoute, string containerName)
        {
            try
            {
                if (fileRoute != null)
                {
                    var account = CloudStorageAccount.Parse(connectionString);
                    var client = account.CreateCloudBlobClient();
                    var container = client.GetContainerReference(containerName);

                    var blobName = Path.GetFileName(fileRoute);
                    var blob = container.GetBlobReference(blobName);
                    await blob.DeleteIfExistsAsync();
                }
            }
            catch (Exception ex)
            {
                // TODO log exception
            }

        }

        public async Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute, string contentType)
        {
            await DeleteFile(fileRoute, containerName);
            return await SaveFile(content, extension, containerName, contentType);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string containerName, string contentType, string fileName = null)
        {
            try
            {
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(containerName);
                await container.CreateIfNotExistsAsync();
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

                fileName ??= $"{Guid.NewGuid()}{extension}";

                var blob = container.GetBlockBlobReference(fileName);
                await blob.UploadFromByteArrayAsync(content, 0, content.Length);
                blob.Properties.ContentType = contentType;
                await blob.SetPropertiesAsync();
                return blob.Uri.ToString();
            }
            catch (Exception ex)
            {
                // TODO log exception
            }

            return null;

        }

        public string GetUri(string fileName, string containerName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName)) return null;
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(containerName);
                var blockBlob = container.GetBlockBlobReference(fileName);
                return blockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                // TODO log exception
                return string.Empty;
            }
        }

        public async Task<byte[]> DownloadAsync(string fileName, string containerName)
        {
            try
            {
                var account = CloudStorageAccount.Parse(connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(containerName);
                var blockBlob = container.GetBlockBlobReference(fileName);

                await using var memoryStream = new MemoryStream();
                await blockBlob.DownloadToStreamAsync(memoryStream);
                memoryStream.Position = 0;
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                // TODO log exception
                return new byte[] { };
            }

        }
    }
}

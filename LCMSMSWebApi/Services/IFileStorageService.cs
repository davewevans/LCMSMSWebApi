using System.Threading.Tasks;

namespace LCMSMSWebApi.Services
{
    public interface IFileStorageService
    {
        string BaseUrl { get; }

        Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute, string contentType);
        
        Task DeleteFile(string fileRoute, string containerName);

        Task<string> SaveFile(byte[] content, string extension, string containerName, string contentType, string fileName=null);

        string GetUri(string fileName, string containerName);

        Task<byte[]> DownloadAsync(string fileName, string containerName);
    }
}

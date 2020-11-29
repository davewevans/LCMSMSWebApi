using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LCMSMSWebApi.Services
{
    public class PictureStorageService : AzureStorageService, IPictureStorageService
    {
        public PictureStorageService(IConfiguration configuration, ILogger logger) : base(configuration, logger)
        {
            base.connectionString = configuration.GetConnectionString("AzurePhotoStorage");
            BaseUrl = configuration.GetValue<string>("BlobBasePhotosUrl");
        }
    }
}

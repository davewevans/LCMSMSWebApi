﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LCMSMSWebApi.Services
{
    public class DocumentStorageService : AzureStorageService, IDocumentStorageService
    {
        public DocumentStorageService(IConfiguration configuration) : base(configuration)
        {
            connectionString = configuration.GetConnectionString("AzureDocumentStorage");
            BaseUrl = configuration.GetValue<string>("BlobBaseDocumentsUrl");
        }
    }
}

using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Spark.Core.Server.Storage
{
    public class AzureStorageService : IFileStorageService
    {
        private readonly string connectionString;

        public AzureStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorageConnection");
        }

        public async Task DeleteFile(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute))
                return;

            var client = new BlobContainerClient(connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            var file = Path.GetFileName(fileRoute);
            var blob = client.GetBlobClient(file);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(Stream content, string extension, string containerName, string fileRoute)
        {
            await DeleteFile(fileRoute, containerName);
            return await SaveFile(content, extension, containerName);
        }

        public async Task<string> SaveFile(Stream content, string extension, string containerName)
        {

            var client = new BlobContainerClient(connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(fileName);
            await blob.UploadAsync(content);

            return blob.Uri.ToString();
        }
    }
}

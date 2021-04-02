using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Spark.Core.Server.Storage
{
    public class InAppStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public InAppStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task DeleteFile(string fileRoute, string containerName)
        {
            var fileName = Path.GetFileName(fileRoute);
            string fileDirectory = Path.Combine(env.WebRootPath, containerName, fileName);

            if (File.Exists(fileDirectory))
                File.Delete(fileDirectory);

            return Task.FromResult(0);
        }

        public async Task<string> EditFile(Stream content, string extension, string containerName, string fileRoute)
        {
            if (!string.IsNullOrEmpty(fileRoute))
                await DeleteFile(fileRoute, containerName);

            return await SaveFile(content, extension, containerName);
        }

        public async Task<string> SaveFile(Stream content, string extension, string containerName)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, containerName);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string savingPath = Path.Combine(folder, fileName);
            using var fileStream = new FileStream(savingPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            await content.CopyToAsync(fileStream);

            var currentUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var pathForDb = Path.Combine(currentUrl, containerName, fileName);
            return pathForDb;
        }
    }
}

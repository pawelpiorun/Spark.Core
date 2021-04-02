using System.IO;
using System.Threading.Tasks;

namespace Spark.Core.Server.Storage
{
    public interface IFileStorageService
    {
        Task<string> EditFile(Stream content, string extension, string containerName, string fileRoute);
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> SaveFile(Stream content, string extension, string containerName);
    }
}

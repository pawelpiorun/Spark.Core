using System.Threading.Tasks;

namespace Spark.Core.Client.Services
{
    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
    }
}

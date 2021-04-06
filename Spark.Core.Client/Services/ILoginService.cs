using Spark.Core.Shared.DTOs;
using System.Threading.Tasks;

namespace Spark.Core.Client.Services
{
    public interface ILoginService
    {
        Task Login(UserToken token);
        Task Logout();
        Task TryRenewToken();
    }
}

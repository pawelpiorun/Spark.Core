using Spark.Core.Shared.DTOs;
using System.Threading.Tasks;

namespace Spark.Core.Client.Repository
{
    public interface IAccountsRepository
    {
        Task<UserToken> Login(UserInfo userInfo);
        Task<UserToken> Register(UserInfo userInfo);
        Task<UserToken> RenewToken();
    }
}

using Spark.Core.Shared.DTOs;
using Spark.Core.Client.Services;
using System;
using System.Threading.Tasks;

namespace Spark.Core.Client.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/accounts";

        public AccountsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<UserToken> Register(UserInfo userInfo)
        {
            var response = await httpService.Post<UserInfo, UserToken>($"{url}/create", userInfo);
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());

            return response.Response;
        }

        public async Task<UserToken> Login(UserInfo userInfo)
        {
            var response = await httpService.Post<UserInfo, UserToken>($"{url}/login", userInfo);
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());

            return response.Response;
        }

        public async Task<UserToken> RenewToken()
        {
            var response = await httpService.Get<UserToken>($"{url}/renewtoken");
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());

            return response.Response;
        }
    }
}

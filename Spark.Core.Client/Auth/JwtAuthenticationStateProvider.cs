using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Spark.Core.Client.Repository;
using Spark.Core.Client.Services;
using Spark.Core.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spark.Core.Client.Auth
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly ILocalStorageService localStorage;
        private readonly HttpClient httpClient;
        private readonly IAccountsRepository accountsRepo;
        private readonly string TOKENKEY = "TOKENKEY";
        private readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        private readonly string AUTHENTICATIONTYPE = "jwt";
        private readonly string AUTHENTICATIONSCHEME = "bearer";

        private AuthenticationState Anonymous =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public JwtAuthenticationStateProvider(ILocalStorageService localStorage,
            HttpClient httpClient,
            IAccountsRepository accountsRepo)
        {
            this.localStorage = localStorage;
            this.httpClient = httpClient;
            this.accountsRepo = accountsRepo;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsStringAsync(TOKENKEY);

            if (string.IsNullOrEmpty(token))
                return Anonymous;

            var expirationTimeString = await localStorage.GetItemAsync<string>(EXPIRATIONTOKENKEY);
            DateTime expirationTime;
            if (DateTime.TryParse(expirationTimeString, out expirationTime))
            {
                if (IsTokenExpired(expirationTime))
                {
                    await Cleanup();
                    return Anonymous;
                }

                if (ShouldRenewToken(expirationTime))
                    token = await RenewToken(token);

            }

            return BuildAuthenticationState(token);
        }

        private async Task<string> RenewToken(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(AUTHENTICATIONSCHEME, token);
            var newToken = await accountsRepo.RenewToken();
            await localStorage.SetItemAsync(TOKENKEY, newToken.Token);
            await localStorage.SetItemAsync(EXPIRATIONTOKENKEY, newToken.Expiration.ToString());
            return newToken.Token;
        }

        private bool ShouldRenewToken(DateTime expirationtime)
        {
            return expirationtime.Subtract(DateTime.UtcNow) < UserTokenDefaults.ShouldRenewTime;
        }

        private bool IsTokenExpired(DateTime expirationTime)
        {
            return expirationTime <= DateTime.UtcNow;
        }

        public AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(AUTHENTICATIONSCHEME, token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), AUTHENTICATIONTYPE)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split(".")[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
                    foreach (var role in parsedRoles)
                        claims.Add(new Claim(ClaimTypes.Role, role));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(UserToken token)
        {
            await localStorage.SetItemAsync(TOKENKEY, token.Token);
            await localStorage.SetItemAsync(EXPIRATIONTOKENKEY, token.Expiration.ToString());
            var authState = BuildAuthenticationState(token.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Cleanup();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }

        public async Task TryRenewToken()
        {
            var expirationTimeString = await localStorage.GetItemAsync<string>(EXPIRATIONTOKENKEY);
            DateTime expirationTime;
            if (DateTime.TryParse(expirationTimeString, out expirationTime))
            {
                if (IsTokenExpired(expirationTime))
                {
                    // this shouldn't happen
                    await Logout();
                }

                if (ShouldRenewToken(expirationTime))
                {
                    var token = await localStorage.GetItemAsStringAsync(TOKENKEY);
                    var newToken = await RenewToken(token);
                    var authState = BuildAuthenticationState(newToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }

            }
        }

        private async Task Cleanup()
        {
            await localStorage.RemoveItemAsync(TOKENKEY);
            await localStorage.RemoveItemAsync(EXPIRATIONTOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Spark.Core.Client.Auth;
using Spark.Core.Client.Dialogs;
using Spark.Core.Client.Services;

namespace Spark.Core.Client.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSparkCoreClient(this IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<IDialogService, DialogService>();

            services.AddAuthorizationCore();

            // same instance of auth provider and login service
            services.AddScoped<JwtAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());

            services.AddScoped<ILoginService, JwtAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());
        }
    }
}

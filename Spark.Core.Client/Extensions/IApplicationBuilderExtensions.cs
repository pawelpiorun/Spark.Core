using Microsoft.AspNetCore.Builder;

namespace Spark.Core.Client.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseSparkAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}

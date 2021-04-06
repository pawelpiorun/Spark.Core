using System;

namespace Spark.Core.Shared.DTOs
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public static class UserTokenDefaults
    {
        public static readonly TimeSpan ExpirationTime = TimeSpan.FromDays(1);
        public static readonly TimeSpan RefreshIntervalTime = TimeSpan.FromSeconds(4);
        public static readonly TimeSpan ShouldRenewTime = TimeSpan.FromMinutes(120);
    }
}

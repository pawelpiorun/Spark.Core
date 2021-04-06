using Spark.Core.Client.Services;
using Spark.Core.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Spark.Core.Client.Auth
{
    public class TokenRenewer : IDisposable
    {
        Timer timer;
        private readonly ILoginService loginService;

        public TokenRenewer(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        public void Initialize(double refreshRateMs = 1000 * 60 * 1)
        {
            timer = new Timer();
            timer.Interval = UserTokenDefaults.RefreshIntervalTime.TotalMilliseconds;
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            loginService?.TryRenewToken();
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}

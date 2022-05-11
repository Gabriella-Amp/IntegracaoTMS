using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Services.Shared.Configurations.Http.Models;
using Services.Shared.Interfaces.Services.Logging;

namespace Services.Shared.Configurations.Http.RetryPolicies.Sap
{
    public static class SapRetryPolicy
    {
        private const int SleepDurationInSeconds = 30;

        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetryForeverAsync(ILoggerService loggerService, string appName) =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryForeverAsync(SleepDurationProvider, (delegateResult, timeSpan, context) => SaveLog(loggerService, delegateResult, appName));

        private static TimeSpan SleepDurationProvider(int retryCount, Context context)
        {
            return TimeSpan.FromSeconds(SleepDurationInSeconds);
        }

        private static async Task SaveLog(ILoggerService loggerService, DelegateResult<HttpResponseMessage> delegateResult, string appName)
        {
            if (loggerService == null) return;

            var content = new
            {
                ExceptionMessage = delegateResult.Exception?.Message,
                Body = delegateResult.Result?.Content is null ? default : await delegateResult.Result.Content.ReadAsStringAsync()
            };

            var responseWrap = new HttpResponseMessageWrapper(delegateResult.Result, content, TimeSpan.FromSeconds(SleepDurationInSeconds));

            await loggerService.ServerError(appName, JsonConvert.SerializeObject(responseWrap));
        }
    }
}
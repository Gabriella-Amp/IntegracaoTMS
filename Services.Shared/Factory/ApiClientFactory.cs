using Service.Shared.ApiClient;
using Services.Shared.Util;
using System;
using System.Threading;

namespace Services.Shared.Factory
{
    public static class ApiClientFactory
    {
        private static Uri apiUri;

        private static Lazy<ClientApi> restClient = new Lazy<ClientApi>(
          () => new ClientApi(apiUri),
          LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
            apiUri = new Uri(ApplicationSettings.WebApiUrl);
        }

        public static ClientApi Instance
        {
            get
            {
                return restClient.Value;
            }
        }
    }
}

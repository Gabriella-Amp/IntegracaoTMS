using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Services.Shared.Interfaces.Services.Logging;
using Services.Shared.Interfaces.Services.Repositories;
using Services.Shared.Repositories;
using Services.Shared.Services.Log;
using AutoMapper;
using System;

namespace Services.TMS.Ocorrencia
{
    public static class Program
    {
        public static string[] CommandLineArgs;

        public static void Main(string[] args)
        {
            CommandLineArgs = args;

            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) => services
                    .AddSingleton<ILoggerService, LoggerService>()
                    .AddSingleton<ISqlServerRepository, SqlServerRepository>()
                    .AddHostedService<OcorrenciaWorkerService>());
    }
}

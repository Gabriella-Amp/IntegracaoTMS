using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using Services.Shared.Util;
using Services.Shared.Model;
using Services.Shared.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Services.Shared.Interfaces.Services.Repositories;
using Services.Shared.Interfaces.Services.Logging;
using Services.Shared.Factory;
using Services.Shared.Controllers;
using System.Xml;

namespace Services.TMS.Ocorrencia
{
    public class OcorrenciaWorkerService : WorkerService
    {

        private readonly IConfiguration _configuration;        
        private readonly ISqlServerRepository _sqlServerRepository;        

        private readonly ILogger _logger;
        private System.Threading.Timer _timer;


        public OcorrenciaWorkerService(
            IConfiguration configuration,
            ILoggerService logger,
            ISqlServerRepository sqlServerRepository) : base(logger)
        {
            _configuration = configuration;
            _sqlServerRepository = sqlServerRepository;
            ApplicationSettings.WebApiUrl = _configuration["Configs:MySettings:WebApiBaseUrl"];
            ApplicationSettings.ConnectionString = _configuration["Configs:MySettings:connectionString"];
        }

        protected override bool ExecuçãoViaLinhaDeComando => Program.CommandLineArgs.Length > 0;

        protected override TimeSpan IntervaloDeExecução => TimeSpan.FromMilliseconds(int.Parse(_configuration["Configs:Workers:Ocorrencia:IntervaloEmMilissegundos"]));

        protected override async Task<int> Processar()
        {
            try
            {
                var listaOcorrencia = await ApiClientFactory.Instance.GetOcorrenciaPendente();
                foreach (var ocorrenciaId in listaOcorrencia.occurrenceId)
                {
                    var ocorrenciaCompleta = await ApiClientFactory.Instance.PostOcorrenciaCompleta(ocorrenciaId);

                    /*DuplicataController duplicataController = new DuplicataController();
                    var retornoProc = duplicataController.ExecutaProcInserirDuplicata(duplCompleta.empresa, duplCompleta.oid, duplCompleta.cnpjEmissor,
                                                                                      duplCompleta.observacao, duplCompleta.dataGeracao, duplCompleta.dataVencimento,
                                                                                      duplCompleta.valorLiquido, duplCompleta.numero, duplCompleta.ToString());*/


                    if (retornoProc[0].Status == 1)
                    {
                        var retornoOcor = new OcorrenciaRetornoModel();
                        var retorno = await ApiClientFactory.Instance.PostOcorrenciaRetorno(retornoOcor);
                    }


                }

            }
            catch (Exception ex)
            {
                _logger.LogError(1, ex, "Erro ao executar o Serviço");
                //throw ex;
            }

            return 1;
        }

        /*public CTEWorkerService(ILogger<CTEWorkerService> logger, IOptions<MySettingsModel> app)
        {
            _logger = logger;
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
            ApplicationSettings.ConnectionString = appSettings.Value.connectionString;
        }*/

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço Retorno Iniciado");
            _timer = new System.Threading.Timer(ExecutarAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(300));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço Retorno Encerrado");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;

        }

        private async void ExecutarAsync(object state)
        {
            
        }
    }
}

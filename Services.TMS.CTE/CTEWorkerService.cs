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

namespace Services.TMS.CTE
{
    public class CTEWorkerService : WorkerService
    {

        private readonly IConfiguration _configuration;        
        private readonly ISqlServerRepository _sqlServerRepository;        

        private readonly ILogger _logger;
        private System.Threading.Timer _timer;


        public CTEWorkerService(
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

        protected override TimeSpan IntervaloDeExecução => TimeSpan.FromMilliseconds(int.Parse(_configuration["Configs:Workers:CTE:IntervaloEmMilissegundos"]));

        protected override async Task<int> Processar()
        {
            try
            {
                var listaCTE = await ApiClientFactory.Instance.GetCTEPendente();
                foreach (var cte in listaCTE.ctes)
                {
                    var cteCompleto = await ApiClientFactory.Instance.PostCTECompleto(cte);
                    var cteDownload = await ApiClientFactory.Instance.GetCTEDownload(cteCompleto.chave);

                    if (cteDownload.Contains("Não foi encontrado"))
                    {
                        continue;
                    }

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(cteDownload);

                    var cnpjEmp = cteCompleto.cnpjUnidade;
                    var numDocumento = doc.GetElementsByTagName("nCT").Item(0).InnerText;
                    var serie = doc.GetElementsByTagName("serie").Item(0).InnerText;
                    var cnpjEmitente = doc.GetElementsByTagName("CNPJ").Item(0).InnerText;
                    var protocolo = Convert.ToInt64(doc.GetElementsByTagName("nProt").Item(0).InnerText);
                    var chaveAcesso = doc.GetElementsByTagName("chCTe").Item(0).InnerText;
                    var valorTotal = Convert.ToDecimal(doc.GetElementsByTagName("vTPrest").Item(0).InnerText);
                    var dataDocumento = Convert.ToDateTime(doc.GetElementsByTagName("dhEmi").Item(0).InnerText).ToString("yyyy-MM-dd"); ;
                    var naturazaOperacao = doc.GetElementsByTagName("natOp").Item(0).InnerText;
                    var razaoSocial = doc.GetElementsByTagName("xNome").Item(0).InnerText;
                    var arquivoXml = cteDownload;
                    
                    NotaFiscalFornecedorController notaFiscalController = new NotaFiscalFornecedorController();
                    var retornoProc = notaFiscalController.ExecutaProcInserirCTe(cnpjEmp, numDocumento, serie, cnpjEmitente, protocolo,
                                                                             chaveAcesso, valorTotal, dataDocumento, naturazaOperacao,
                                                                             razaoSocial, arquivoXml);

                    if (retornoProc[0].Status == 1)
                    {
                        var retornoCTE = new CTERetornoModel();
                        var retorno = await ApiClientFactory.Instance.PostCTERetorno(retornoCTE);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(1, ex, "Erro ao executar o Serviço");
            }

            return 1;
        }

    }
}

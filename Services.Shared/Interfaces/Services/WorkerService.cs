using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Services.Shared.Interfaces.Services.Logging;

namespace Services.Shared.Interfaces.Services
{
    public abstract class WorkerService : BackgroundService
    {
        protected abstract TimeSpan IntervaloDeExecução { get; }

        protected WorkerService(ILoggerService logger)
        {
            Logger = logger;

            AppDomain.CurrentDomain.ProcessExit += (sender, args) =>
            {
                Logger.ServerError(WorkerServiceName, "O Integrador foi parado").Wait();
            };
        }

        protected ILoggerService Logger { get; }

        protected string WorkerServiceName => GetType().FullName;

        protected virtual bool ExecuçãoViaLinhaDeComando => false;

        protected abstract Task<int> Processar();

        protected virtual Task<int> ProcessarLinhaDeComando() { throw new NotImplementedException(); }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Logger.Information(WorkerServiceName, "Inicialização do Integrador");

            var stopWatch = Stopwatch.StartNew();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    stopWatch.Restart();

                    PrintMessage("Processamento iniciado");

                    int dataQuantity;

                    if (ExecuçãoViaLinhaDeComando)
                    {
                        Logger.MensagemSufixo = ExecuçãoViaLinhaDeComando ? "[Linha de Comando]" : "";

                        dataQuantity = await ProcessarLinhaDeComando();
                    }
                    else
                    {
                        dataQuantity = await Processar();
                    }

                    if (dataQuantity > 0)
                    {
                        await Logger.Information(WorkerServiceName, $"Tempo gasto no último processamento: {stopWatch.Elapsed}. {dataQuantity} registro(s)");
                    }
                }
                /*catch (Exception exception)
                {
                    try
                    {
                        await Logger.GenericError(WorkerServiceName, $"Ocorreu um erro inesperado. Mensagem: {exception}");
                    }
                    catch
                    {
                        // ignored
                    }
                }*/
                finally
                {
                    if (ExecuçãoViaLinhaDeComando)
                    {
                        await StopAsync(cancellationToken);

                        PrintMessage("Fim da Execução via Linha de Comando");
                    }
                    else
                    {
                        PrintMessage($"Execução finalizada. Aguardando {IntervaloDeExecução.TotalSeconds} segundos para a próxima execução.");

                        await Task.Delay(IntervaloDeExecução, cancellationToken);
                    }
                }
            }
        }

        protected void PrintMessage(string message, bool error = false) => Logger.ConsoleLog(message, error);
    }
}
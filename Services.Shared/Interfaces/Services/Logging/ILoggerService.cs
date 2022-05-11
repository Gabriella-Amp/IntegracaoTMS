using System.Threading.Tasks;

namespace Services.Shared.Interfaces.Services.Logging
{
    public interface ILoggerService
    {
        public string MensagemSufixo { get; set; }

        Task GenericError(string source, string message, params ILoggerNotification[] loggerNotifications);

        Task Information(string source, string message, params ILoggerNotification[] loggerNotifications);

        Task ServerError(string source, string message, params ILoggerNotification[] loggerNotifications);

        void ConsoleLog(string message, bool error = false);
    }
}
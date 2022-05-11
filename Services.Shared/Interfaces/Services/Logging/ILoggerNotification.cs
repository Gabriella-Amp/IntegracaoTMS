using System.Threading.Tasks;

namespace Services.Shared.Interfaces.Services.Logging
{
    public interface ILoggerNotification
    {
        Task SendNotification(ILog log);
    }
}
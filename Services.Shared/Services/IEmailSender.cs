using System.Threading.Tasks;

namespace IntegracaoGNRE.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

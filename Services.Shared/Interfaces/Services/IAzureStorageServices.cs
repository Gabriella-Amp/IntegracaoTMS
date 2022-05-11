using System.Threading.Tasks;

namespace Services.Shared.Interfaces.Services
{
    public interface IAzureStorageServices
    {
        Task<string> SaveFileAsync(byte[] bytes, string extension);
    }
}
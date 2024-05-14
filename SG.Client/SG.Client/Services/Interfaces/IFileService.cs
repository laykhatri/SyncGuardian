using System.Threading.Tasks;

namespace SG.Client.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> ReadTextAsync(string filePath);
        Task WriteTextAsync(string filePath, string content);
    }
}

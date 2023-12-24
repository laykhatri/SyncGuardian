using System.Net.Http;
using System.Threading.Tasks;

namespace SyncGuardianMobile.Services.Interface
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, string content);
        Task<HttpResponseMessage> PostAsync(string url, object content);
    }
}

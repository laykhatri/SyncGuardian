using SyncGuardianMobile.Services.Interface;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SyncGuardianMobile.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, string content)
        {
            var body = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(url, body);
        }

        public Task<HttpResponseMessage> PostAsync(string url, object content)
        {
            throw new NotImplementedException();
        }
    }
}

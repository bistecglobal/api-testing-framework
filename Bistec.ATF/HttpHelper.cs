using Bistec.ATF.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Bistec.ATF
{
    internal class HttpHelper
    {
        private readonly TokenResponse tokenResponse;
        private HttpClient client;

        public HttpHelper(IConfiguration configuration, TokenResponse tokenResponse)
        {
            Configuration = configuration;
            this.tokenResponse = tokenResponse;
        }

        public IConfiguration Configuration { get; }

        public HttpClient GetClient()
        {
            var handler = new HttpClientHandler();
            client = new HttpClient(handler);
            client.BaseAddress = new System.Uri(Configuration.GetValue("baseUrl", "http://localhost:3002/"));
            return client;
        }

        public async Task<TResponse?> PostJsonAsync<TResponse, TRequest>(string url, TRequest data)
        {
            if(client == null)
            {
                client = GetClient();
            }

            client.DefaultRequestHeaders.Clear();

            if (string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", tokenResponse.AccessToken);
            }

            var response = await client.PostAsJsonAsync(url, data);
            var jsonData = await response.Content.ReadFromJsonAsync<TResponse>();

            return jsonData;
        }
    }
}

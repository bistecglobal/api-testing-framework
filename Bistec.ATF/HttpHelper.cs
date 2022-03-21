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
        private HttpResponseMessage? response;

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

        public async Task<HttpResponseMessage> PostJsonAsync<TRequest>(string url, TRequest data)
        {
            if(client == null)
            {
                client = GetClient();
            }

            client.DefaultRequestHeaders.Clear();

            if (!string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.AccessToken}");
            }

            response = await client.PostAsJsonAsync(url, data);

            return response;
        }

        public async Task<HttpResponseMessage?> GetResponseAsync(string url)
        {
            if (client == null)
            {
                client = GetClient();
            }

            client.DefaultRequestHeaders.Clear();

            if (!string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.AccessToken}");
            }

            response = await client.GetAsync(url);

            return response;
        }

        public int GetStatusCode()
        {
            if(response != null)
                return (int)response.StatusCode;

            return 0;
        }

        public HttpResponseMessage? GetResponse()
        {           
            return response;
        }
    }
}

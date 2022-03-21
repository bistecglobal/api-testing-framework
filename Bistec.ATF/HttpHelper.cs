using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Bistec.ATF
{
    internal class HttpHelper
    {
        public HttpHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public HttpClient GetClient()
        {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            client.BaseAddress = new System.Uri(Configuration.GetValue("baseUrl", "http://localhost:3002/"));
            return client;
        }
    }
}

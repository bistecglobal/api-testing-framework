using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bistec.ATF
{
    public class StatusTests
    {
        private readonly ServiceCollection services;
        private readonly IConfigurationRoot config;
        private readonly ServiceProvider app;

        public StatusTests()
        {
            services = new ServiceCollection();
            services.AddLogging();

            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddSingleton<IConfiguration>(sp => config);
            services.AddSingleton<HttpHelper>();
            app = services.BuildServiceProvider();
        }
        [Fact]
        public async void ShouldReturnOkStatus()
        {
            var httpHelper = app.GetRequiredService<HttpHelper>();
            var client = httpHelper.GetClient();
            var response = await client.GetStringAsync("api/status");
            var status = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusResponse>(response);
            status.Status.Should().BeEquivalentTo("ok");
        }
    }

    public class StatusResponse
    {
        public string Status { get; set; }
    }

}
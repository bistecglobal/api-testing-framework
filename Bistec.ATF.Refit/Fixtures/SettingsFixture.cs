using Microsoft.Extensions.Configuration;

namespace Bistec.ATF.Refit.Fixtures
{
    public class SettingsFixture : IDisposable
    {
        public readonly AppSettings AppSettings;

        public SettingsFixture()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            AppSettings = new AppSettings
            {
                BaseAddress = configuration["baseUrl"],
                MinimumResponseTime = int.Parse(configuration["minimumResponseTime"])
            };
        }

        public void Dispose()
        {
        }
    }
}
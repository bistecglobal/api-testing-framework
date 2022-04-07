using Bistec.ATF.Refit.Support;
using Refit;

namespace Bistec.ATF.Refit.Fixtures
{
    public class RefitFixture<TRefitApi> : IDisposable
    {
        public TRefitApi GetRestClient(string baseAddress, ScenarioContext context) =>
            RestService.For<TRefitApi>(new HttpClient(new AuthHeaderHandler(context))
                {
                    BaseAddress = new Uri(baseAddress)
                }
            );

        public void Dispose()
        {
        }
    }
}

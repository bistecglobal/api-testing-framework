using Refit;

namespace Bistec.ATF.Refit.Fixtures
{
    public class RefitFixture<TRefitApi> : IDisposable
    {
        public TRefitApi GetRestClient(string baseAddress) =>
            RestService.For<TRefitApi>(baseAddress);

        public void Dispose()
        {
        }
    }
}

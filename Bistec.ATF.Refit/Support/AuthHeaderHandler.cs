using Bistec.ATF.Refit.Models;
using System.Net.Http.Headers;

namespace Bistec.ATF.Refit.Support
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ScenarioContext context;

        public AuthHeaderHandler(ScenarioContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = string.Empty;

            if (context.ContainsKey(Constants.ACCESS_TOKEN_KEY))
            {
                token = context.Get<string>(Constants.ACCESS_TOKEN_KEY);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}

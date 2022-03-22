using Bistec.ATF.Models;
using FluentAssertions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Bistec.ATF.Steps
{
    [Binding]
    internal class AuthSteps
    {
        private string username;
        private string password;
        private HttpResponseMessage response;
        private readonly HttpHelper httpHelper;
        private readonly TokenResponse tokenResponse;

        public AuthSteps(HttpHelper httpHelper, TokenResponse tokenResponse)
        {
            this.httpHelper = httpHelper;
            this.tokenResponse = tokenResponse;
        }


        [Given(@"Admin username is (.*)")]
        public void GivenAdminUsername(RandomisedValue name)
        {
            this.username = name.StringValue;
        }

        [Given(@"Admin password is (.*)")]
        public void GivenAdminPasswordIsPass(string password)
        {
            if (password == "<null>")
            {
                password = string.Empty;
            }
            else
            {
                this.password = password;
            }
        }

        [When(@"Create admin api is called")]
        public async Task WhenCreateAdminApiIsCalledAsync()
        {
            response = await httpHelper.PostJsonAsync("admin", new CreateUserRequest
            {
                password = password,
                username = username,
                extra = username
            });

            var jsonData = await response.Content.ReadFromJsonAsync<TokenResponse>();
            tokenResponse.AccessToken = jsonData.AccessToken;

        }

        [Then(@"response should have (.*) status code")]
        public void ThenResponseShouldHaveStatusCodeAsync(int code)
        {
            httpHelper.GetStatusCode().Should().Be(code);
        }

        [Then(@"response should have a valid access_token")]
        public void ThenResponseShouldHaveAValidAccessTokenAsync()
        {
            tokenResponse.AccessToken.Should().NotBeNullOrEmpty();
        }

        [When(@"Login admin api is called")]
        public async Task WhenLoginAdminApiIsCalledAsync()
        {
            response = await httpHelper.PostJsonAsync("admin/login", new LoginRequest
            {
                password = password,
                username = username,
            });

            var jsonData = await response.Content.ReadFromJsonAsync<TokenResponse>();
            tokenResponse.AccessToken = jsonData.AccessToken;

        }

        [Given(@"A new admin is created with password (.*)")]
        public async Task GivenANewAdminIsCreatedWithPasswordPassAsync(string password)
        {
            username = Transformations.GenerateName(10);
            response = await httpHelper.PostJsonAsync("admin", new CreateUserRequest
            {
                password = password,
                username = username,
                extra = username
            });
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadFromJsonAsync<TokenResponse>();
            tokenResponse.AccessToken = jsonData.AccessToken;
        }
    }
}

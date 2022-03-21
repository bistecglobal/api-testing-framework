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
        private readonly HttpClient client;

        public AuthSteps(HttpHelper httpHelper)
        {
            this.client = httpHelper.GetClient();
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
            response = await client.PostAsJsonAsync("admin", new CreateUserRequest
            {
                password = password,
                username = username,
                extra = username
            });

        }

        [Then(@"response should have (.*) status code")]
        public async Task ThenResponseShouldHaveStatusCodeAsync(int code)
        {

            ((int)response.StatusCode).Should().Be(code);
        }

        [Then(@"response should have a valid access_token")]
        public async Task ThenResponseShouldHaveAValidAccessTokenAsync()
        {
            var jsonData = await response.Content.ReadFromJsonAsync<TokenResponse>();
            jsonData.AccessToken.Should().NotBeNullOrEmpty();
        }

        [When(@"Login admin api is called")]
        public async Task WhenLoginAdminApiIsCalledAsync()
        {
            response = await client.PostAsJsonAsync("admin/login", new LoginRequest
            {
                password = password,
                username = username,
            });

        }

        [Given(@"A new admin is created with password (.*)")]
        public async Task GivenANewAdminIsCreatedWithPasswordPassAsync(string password)
        {
            username = Transformations.GenerateName(10);
            response = await client.PostAsJsonAsync("admin", new CreateUserRequest
            {
                password = password,
                username = username,
                extra = username
            });
            response.EnsureSuccessStatusCode();
        }
    }
}

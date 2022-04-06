using Bistec.ATF.Refit.Apis;
using Bistec.ATF.Refit.Fixtures;
using Bistec.ATF.Refit.Models;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace Bistec.ATF.Refit.StepDefinitions
{
    [Binding]
    public class AuthStepDefinitions : RefitFixture<IAuthApi>
    {
        private readonly ScenarioContext senarioContext;
        private readonly SettingsFixture settings;

        public AuthStepDefinitions(ScenarioContext senarioContext, SettingsFixture settings)
        {
            this.senarioContext = senarioContext;
            this.settings = settings;
        }

        [Given(@"Admin username is (.*)")]
        public void GivenAdminUsername(RandomisedValue randomisedValue)
        {
            senarioContext.Add(Constants.USERNAME_KEY, randomisedValue.StringValue);
        }

        [Given(@"Admin password is (.*)")]
        public void GivenAdminPasswordIsPass(string password)
        {
            senarioContext.Add(Constants.PASSWORD_KEY, password);
        }

        [When(@"Create admin api is called")]
        public async Task WhenCreateAdminApiIsCalled()
        {
            var token = await GetRestClient(settings.AppSettings.BaseAddress)
                .CreateAdmin(new CreateUserRequest
            {
                username = senarioContext.Get<string>(Constants.USERNAME_KEY),
                password = senarioContext.Get<string>(Constants.PASSWORD_KEY),
                extra = senarioContext.Get<string>(Constants.USERNAME_KEY)
            });

            senarioContext.Add(Constants.ACCESS_TOKEN_KEY, token?.Content?.AccessToken);
            senarioContext.Add(Constants.STATUS_CODE_KEY, token?.StatusCode);
        }

        [Then(@"Status code should be (.*)")]
        public void ThenStatusCodeShouldBe(int status)
        {
            var code = (int)senarioContext.Get<HttpStatusCode>(Constants.STATUS_CODE_KEY);
            status.Should().Be(code);
        }


        [Then(@"response should have a valid access_token")]
        public void ThenResponseShouldHaveAValidAccess_Token()
        {
            var token = senarioContext.Get<string>(Constants.ACCESS_TOKEN_KEY);
            token.Should().NotBeNullOrEmpty();
        }
    }
}

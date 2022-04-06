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
        private readonly ScenarioContext scenarioContext;
        private readonly SettingsFixture settings;

        public AuthStepDefinitions(ScenarioContext scenarioContext, SettingsFixture settings)
        {
            this.scenarioContext = scenarioContext;
            this.settings = settings;
        }

        [Given(@"Admin username is (.*)")]
        public void GivenAdminUsername(RandomisedValue randomizedValue)
        {
            scenarioContext.Add(Constants.USERNAME_KEY, randomizedValue.StringValue);
        }

        [Given(@"Admin password is (.*)")]
        public void GivenAdminPasswordIsPass(string password)
        {
            scenarioContext.Add(Constants.PASSWORD_KEY, password);
        }

        [When(@"Create admin api is called")]
        public async Task WhenCreateAdminApiIsCalled()
        {
            var token = await GetRestClient(settings.AppSettings.BaseAddress)
                .CreateAdmin(new CreateUserRequest
            {
                username = scenarioContext.Get<string>(Constants.USERNAME_KEY),
                password = scenarioContext.Get<string>(Constants.PASSWORD_KEY),
                extra = scenarioContext.Get<string>(Constants.USERNAME_KEY)
            });

            scenarioContext.Add(Constants.ACCESS_TOKEN_KEY, token?.Content?.AccessToken);
            scenarioContext.Add(Constants.STATUS_CODE_KEY, token?.StatusCode);
        }

        [Then(@"Status code should be (.*)")]
        public void ThenStatusCodeShouldBe(int status)
        {
            var code = (int)scenarioContext.Get<HttpStatusCode>(Constants.STATUS_CODE_KEY);
            status.Should().Be(code);
        }


        [Then(@"response should have a valid access_token")]
        public void ThenResponseShouldHaveAValidAccess_Token()
        {
            var token = scenarioContext.Get<string>(Constants.ACCESS_TOKEN_KEY);
            token.Should().NotBeNullOrEmpty();
        }
    }
}

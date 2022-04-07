using Bistec.ATF.Refit.Apis;
using Bistec.ATF.Refit.Fixtures;
using Bistec.ATF.Refit.Models;

namespace Bistec.ATF.Refit.StepDefinitions
{
    [Binding]
    [Collection("Settings collection")]
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
            var token = await GetRestClient(settings.AppSettings.BaseAddress, scenarioContext)
                .CreateAdmin(new CreateUserRequest
            {
                username = scenarioContext.Get<string>(Constants.USERNAME_KEY),
                password = scenarioContext.Get<string>(Constants.PASSWORD_KEY),
                extra = scenarioContext.Get<string>(Constants.USERNAME_KEY)
            });

            scenarioContext.Add(Constants.ACCESS_TOKEN_KEY, token?.Content?.AccessToken);
            scenarioContext.Add(Constants.STATUS_CODE_KEY, token?.StatusCode);
        }
        
        [Given(@"A new admin is created with username (.*) and password (.*)")]
        public async Task GivenANewAdminIsCreated(RandomisedValue randomizedValue, string password)
        {
            var token = await GetRestClient(settings.AppSettings.BaseAddress, scenarioContext)
                .CreateAdmin(new CreateUserRequest
                {
                    username = randomizedValue.StringValue,
                    password = password,
                    extra = randomizedValue.StringValue
                });

            scenarioContext.Add(Constants.ACCESS_TOKEN_KEY, token?.Content?.AccessToken);
            scenarioContext.Add(Constants.STATUS_CODE_KEY, token?.StatusCode);
        }
    }
}

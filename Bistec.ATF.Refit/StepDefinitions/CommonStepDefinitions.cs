using Bistec.ATF.Refit.Models;
using System.Net;

namespace Bistec.ATF.Refit.StepDefinitions
{
    [Binding]
    [Collection("Settings collection")]
    public class CommonStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        public CommonStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
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

using FluentAssertions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Bistec.ATF.Steps
{
    [Binding]
    internal class StatusSteps
    {
        private readonly HttpHelper httpHelper;
        private readonly ScenarioContext context;
        private StatusResponse? status = null;
        private string? statusValue = string.Empty;

        public StatusSteps(HttpHelper httpHelper, ScenarioContext context)
        {
            this.httpHelper = httpHelper;
            this.context = context;
        }

        [Given("status api is called")]
        public async Task GivenStatusApiCalled()
        {
            var client = httpHelper.GetClient();
            var response = await client.GetStringAsync("api/status");
            status = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusResponse>(response);
            
        }

        [When("status is checked")]
        public void WhenStatusIsChecked()
        {
            statusValue = status?.Status;
        }

        [Then("status should be (.*)")]
        public void ShouldEqualValue(string value)
        {
            statusValue.Should().BeEquivalentTo(value);
        }
    }
}

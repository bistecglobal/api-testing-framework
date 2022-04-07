using Bistec.ATF.Refit.Apis;
using Bistec.ATF.Refit.Fixtures;
using Bistec.ATF.Refit.Models;

namespace Bistec.ATF.Refit.StepDefinitions
{
    [Binding]
    [Collection("Settings Collection")]
    public class EmployeesStepDefinitions : RefitFixture<IEmployeeApi>
    {
        private readonly ScenarioContext context;
        private readonly SettingsFixture settings;

        public EmployeesStepDefinitions(ScenarioContext context, SettingsFixture settings)
        {
            this.context = context;
            this.settings = settings;
        }

        [When(@"Get Employee list api is called")]
        public async Task WhenGetEmployeeListApiIsCalled()
        {
            var employees = await GetRestClient(settings.AppSettings.BaseAddress, context)
                                    .GetEmployees();
            context.Remove(Constants.STATUS_CODE_KEY);
            context.Add(Constants.STATUS_CODE_KEY, employees?.StatusCode);
        }
    }
}

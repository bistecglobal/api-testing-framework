using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Bistec.ATF.Steps
{
    [Binding]
    internal class EmployeesStepDefinitions
    {
        private readonly HttpHelper httpHelper;

        public EmployeesStepDefinitions(HttpHelper httpHelper)
        {
            this.httpHelper = httpHelper;
        }

        [When(@"Get Employee list api is called")]
        public async Task WhenGetEmployeeListApiIsCalledAsync()
        {
            await httpHelper.GetResponseAsync("api/protected/employe?limit=100");
        }
    }
}

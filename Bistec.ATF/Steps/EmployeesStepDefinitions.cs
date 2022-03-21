using Bistec.ATF.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Bistec.ATF.Steps
{
    [Binding]
    internal class EmployeesStepDefinitions
    {
        private readonly HttpHelper httpHelper;
        private readonly TokenResponse tokenResponse;
        private HttpResponseMessage? response;

        public EmployeesStepDefinitions(HttpHelper httpHelper, TokenResponse tokenResponse)
        {
            this.httpHelper = httpHelper;
            this.tokenResponse = tokenResponse;
        }

        [When(@"Get Employee list api is called")]
        public async Task WhenGetEmployeeListApiIsCalledAsync()
        {
            response = await httpHelper.GetResponseAsync("api/protected/employe?limit=100");
        }
    }
}

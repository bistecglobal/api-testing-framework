using Bistec.ATF.Refit.Models;
using Refit;

namespace Bistec.ATF.Refit.Apis
{
    public interface IEmployeeApi
    {
        [Get("/api/protected/employe")]
        Task<ApiResponse<List<EmployeeResponse>>> GetEmployees([Query] int limit = 5);
    }
}

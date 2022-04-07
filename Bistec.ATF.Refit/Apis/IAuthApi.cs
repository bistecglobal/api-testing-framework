using Bistec.ATF.Refit.Models;
using Refit;

namespace Bistec.ATF.Refit.Apis
{
    public interface IAuthApi
    {
        [Post("/admin")]
        Task<ApiResponse<TokenResponse>> CreateAdmin([Body] CreateUserRequest request);

        [Post("/admin")]
        Task<ApiResponse<TokenResponse>> Login([Body] LoginRequest request);
    }
}

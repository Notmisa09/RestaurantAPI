using Restaurant.Core.Application.Dto.Account;

namespace Restaurant.Core.Application.Interfaces.IServices
{
    public interface IAccountService
    {
        Task SingOutAsync();
        Task<ServiceResult> RegisterUserAsync(RegisterRequest request, string origin,  string UserRoles);
        Task<AuthenticationResponse> AuthetnticateAsync(AuthenticationRequest request);
        Task<string> ConfrimAccountAsync(string UserId, string token);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordRequest request);
    }
}

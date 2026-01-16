using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface IAuthService
{
    Task<Result<LoginResult>> LoginAsync(LoginRequest request);
    Task<BaseResult> RegisterAsync(RegisterRequest request);
    Task<BaseResult> DeleteAccountAsync(int id);
    Task<BaseResult> ChangePasswordAsync(int userId,ChangePasswordRequest request);
}
using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.IServices;

public interface IUserService
{
    Task<Result<UserReadInfo>> GetMyProfileAsync(int userId);
    Task<BaseResult> UpdateProfileAsync(int userId, UserUpdateInfo entity);
    Task<Result<UserReadInfo>> GetUserByIdAsync(int id);
    Task<Result<PagedResponse<IEnumerable<UserReadInfo>>>> GetUsersAsync(UserFilter filter);
}
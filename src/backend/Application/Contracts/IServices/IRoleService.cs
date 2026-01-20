using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface IRoleService
{
    Task<BaseResult> CreateAsync(RoleCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int roleId, RoleUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int roleId);
    Task<Result<RoleReadInfo>> GetByIdAsync(int id);
    Task<Result<PagedResponse<IEnumerable<RoleReadInfo>>>> GetAllAsync();
}
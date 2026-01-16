using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface IRoleService
{
    Task<BaseResult> CreateAsync(int sellerId, RoleCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int roleId, RoleUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int roleId);
    Task<Result<ProductReadInfo>> GetByIdAsync(int id);
    Task<Result<PagedResponse<RoleReadInfo>>> GetAllAsync();
}
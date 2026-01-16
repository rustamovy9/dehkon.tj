using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;

namespace Infrastructure.ImplementationContract.Services;

public class RoleService(IRoleRepository repository) : IRoleService
{
    public Task<BaseResult> CreateAsync(int sellerId, RoleCreateInfo createInfo)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> UpdateAsync(int roleId, RoleUpdateInfo updateInfo)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> DeleteAsync(int roleId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ProductReadInfo>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PagedResponse<RoleReadInfo>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
using System.Collections;
using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;

namespace Infrastructure.ImplementationContract.Services;

public class RoleService(IRoleRepository repository) : IRoleService
{
    public async Task<BaseResult> CreateAsync(RoleCreateInfo createInfo)
    {
        var res = await repository
            .Find(r => r.Name == createInfo.Name);

        var exists = res.Value!.Any();


        if (exists)
            return BaseResult.Failure(Error.Conflict("Role already exists"));

        Role role = createInfo.ToEntity();

        var result = await repository.AddAsync(role);
        
        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }

    public async Task<BaseResult> UpdateAsync(int roleId, RoleUpdateInfo updateInfo)
    {
        var roleResult = await repository.GetByIdAsync(roleId);
        if (!roleResult.IsSuccess)
            return BaseResult.Failure(roleResult.Error);

        Role role = roleResult.Value!;
        role.ToEntity(updateInfo);

        var result = await repository.UpdateAsync(role);
        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }

    public async Task<BaseResult> DeleteAsync(int roleId)
    {
        var roleRes = await repository.GetByIdAsync(roleId);
        if(!roleRes.IsSuccess)
            return BaseResult.Failure(roleRes.Error);


        var res = await repository.DeleteAsync(roleId);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<Result<RoleReadInfo>> GetByIdAsync(int id)
    {
        var res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess)
            return Result<RoleReadInfo>.Failure(res.Error);

        return Result<RoleReadInfo>.Success(res.Value!.ToRead());
    }

    public async Task<Result<PagedResponse<IEnumerable<RoleReadInfo>>>> GetAllAsync()
    {
        var request = await repository.Find(_=>true);

        if (!request.IsSuccess)
            return Result<PagedResponse<IEnumerable<RoleReadInfo>>>.Failure(request.Error);

        List<RoleReadInfo> roles = request.Value!
            .Select(r => r.ToRead())
            .ToList();

        var response = PagedResponse<IEnumerable<RoleReadInfo>>
            .Create(1, roles.Count, roles.Count, roles);

        return Result<PagedResponse<IEnumerable<RoleReadInfo>>>.Success(response);
    }
}
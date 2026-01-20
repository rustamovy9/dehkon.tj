using System.Linq.Expressions;
using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Extensions;

namespace Infrastructure.ImplementationContract.Services;

public class UserService(IUserRepository userRepository,IFileService fileService) : IUserService
{
    public async Task<Result<UserReadInfo>> GetMyProfileAsync(int userId)
    {
        var res = await userRepository.GetByIdWithRoleAsync(userId);
        if (!res.IsSuccess)
            return Result<UserReadInfo>.Failure(res.Error);

        return Result<UserReadInfo>.Success(res.Value!.ToRead());
    }

    public async Task<BaseResult> UpdateProfileAsync(int userId, UserUpdateInfo entity)
    {
        var userRes = await userRepository.GetByIdAsync(userId);
        if (!userRes.IsSuccess)
            return BaseResult.Failure(userRes.Error);

        bool conflict = (await userRepository.GetAllAsync()).Value!.Any(user =>
            user.UserName.ToLower().Contains(entity.UserName.ToLower())
            || user.PhoneNumber!.ToLower().Contains(entity.PhoneNumber!));

        if (conflict) return BaseResult.Failure(Error.Conflict("username or phone already exist"));
        
        User user = userRes.Value!;

        await user.ToEntity(entity, fileService);
        var res = await userRepository.UpdateAsync(user);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);


    }

    public async Task<Result<UserReadInfo>> GetUserByIdAsync(int id)
    {
        var res = await userRepository.GetByIdWithRoleAsync(id);
        if (!res.IsSuccess)
            return Result<UserReadInfo>.Failure(res.Error);

        return Result<UserReadInfo>.Success(res.Value!.ToRead());
    }

    public async Task<Result<PagedResponse<IEnumerable<UserReadInfo>>>> GetUsersAsync(UserFilter filter)
    {
        Expression<Func<User, bool>> filterExpression = u =>
            (filter.RoleId == null || u.RoleId == filter.RoleId) &&
            (string.IsNullOrEmpty(filter.UserName) || u.UserName == filter.UserName) &&
            (string.IsNullOrEmpty(filter.Email) || u.Email == filter.Email) &&
            (string.IsNullOrEmpty(filter.FullName) || u.FullName == filter.FullName) &&
            (filter.IsActive == null || u.IsDeleted != filter.IsActive);

        var request = await userRepository.FindWithRole(filterExpression);

        if (!request.IsSuccess)
            return Result<PagedResponse<IEnumerable<UserReadInfo>>>.Failure(request.Error);

        List<UserReadInfo> query = request.Value!
            .Select(u => u.ToRead())
            .ToList();

        int totalCount = query.Count;

        IEnumerable<UserReadInfo> items =
            query.Page(filter.PageNumber, filter.PageSize);

        var response = PagedResponse<IEnumerable<UserReadInfo>>
            .Create(filter.PageNumber, filter.PageSize, totalCount, items);

        return Result<PagedResponse<IEnumerable<UserReadInfo>>>.Success(response);
    }
}
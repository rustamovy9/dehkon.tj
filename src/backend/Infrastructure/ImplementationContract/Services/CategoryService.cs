using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;

namespace Infrastructure.ImplementationContract.Services;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<Result<CategoryReadInfo>> GetByIdAsync(int id)
    {
        Result<Category> res = await repository.GetByIdAsync(id);
        if (!res.IsSuccess)
            return Result<CategoryReadInfo>.Failure(res.Error);

        return Result<CategoryReadInfo>.Success(res.Value!.ToRead());
    }

    public async Task<Result<IEnumerable<CategoryReadInfo>>> GetAllAsync()
    {
        Result<IEnumerable<Category>> res = await repository.GetAllAsync();
        if (!res.IsSuccess)
            return Result<IEnumerable<CategoryReadInfo>>.Failure(res.Error);

        return Result<IEnumerable<CategoryReadInfo>>.Success(res.Value!.Select(c => c.ToRead()));
    }

    public async Task<BaseResult> CreateAsync(CategoryCreateInfo createInfo)
    {
 

        var findResult = repository.Find(c => c.Name == createInfo.Name);
        if (!findResult.IsSuccess)
            return BaseResult.Failure(findResult.Error);

        if (findResult.Value!.Any())
            return BaseResult.Failure(Error.Conflict("Category already exists"));

        Category category = createInfo.ToEntity();

        Result<int> res = await repository.AddAsync(category);
        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> UpdateAsync(int id, CategoryUpdateInfo updateInfo)
    {
        Result<Category> existing = await repository.GetByIdAsync(id);
        if (!existing.IsSuccess)
            return BaseResult.Failure(existing.Error);

        Category category = existing.Value!.ToEntity(updateInfo);

        Result<int> updateRes = await repository.UpdateAsync(category);
        return updateRes.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(updateRes.Error);
    }

    public async Task<BaseResult> DeleteAsync(int id)
    {
        var deleteRes = await repository.DeleteAsync(id);
        return deleteRes.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(deleteRes.Error);
    }
}
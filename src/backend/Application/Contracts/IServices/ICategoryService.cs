using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.IServices;

public interface ICategoryService
{
    Task<Result<CategoryReadInfo>> GetByIdAsync(int id);
    Task<Result<PagedResponse<IEnumerable<CategoryReadInfo>>>> GetAllAsync(CategoryFilter filter);
    Task<BaseResult> CreateAsync(CategoryCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int id, CategoryUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int id);
}
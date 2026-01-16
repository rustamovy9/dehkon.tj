using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface ICategoryService
{
    Task<Result<CategoryReadInfo>> GetByIdAsync(int id);
    Task<Result<IEnumerable<CategoryReadInfo>>> GetAllAsync();
    Task<BaseResult> CreateAsync(CategoryCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int id, CategoryUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int id);
}
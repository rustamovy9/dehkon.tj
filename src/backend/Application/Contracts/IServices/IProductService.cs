using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.IServices;

public interface IProductService
{
    Task<BaseResult> CreateAsync(int sellerId, ProductCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int productId,int sellerId, ProductUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int productId,int sellerId);
    
    Task<Result<ProductReadInfo>> GetByIdAsync(int id);
    Task<Result<PagedResponse<IEnumerable<ProductReadInfo>>>> GetAllAsync(ProductFilter filter);
}
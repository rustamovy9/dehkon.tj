using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Infrastructure.ImplementationContract.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public Task<BaseResult> CreateAsync(int sellerId, ProductCreateInfo createInfo)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> UpdateAsync(int productId, ProductUpdateInfo updateInfo)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> DeleteAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ProductReadInfo>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PagedResponse<ProductReadInfo>>> GetAllAsync(ProductFilter filter)
    {
        throw new NotImplementedException();
    }
}
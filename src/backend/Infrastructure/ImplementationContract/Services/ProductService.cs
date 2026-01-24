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

public class ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository,IFileService fileService) : IProductService
{
    public async Task<BaseResult> CreateAsync(int sellerId, ProductCreateInfo createInfo)
    {
        var categoryRes = await categoryRepository.GetByIdAsync(createInfo.CategoryId);
        if (!categoryRes.IsSuccess)
            return BaseResult.Failure(categoryRes.Error);

        Product product = await createInfo.ToEntity(fileService,sellerId);

        var res = await productRepository.AddAsync(product);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> UpdateAsync(int productId,int sellerId, ProductUpdateInfo updateInfo)
    {
        var productRes = await productRepository.GetByIdAsync(productId);
        if(!productRes.IsSuccess)
            return BaseResult.Failure(productRes.Error);

        Product product = productRes.Value!;

        if (product.SellerId != sellerId)
            return BaseResult.Failure(Error.Forbidden());

        await product.ToEntity(updateInfo,fileService,sellerId);

        var res = await productRepository.UpdateAsync(product);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> DeleteAsync(int productId, int sellerId)
    {
        var productResult = await productRepository.GetByIdAsync(productId);
        if (!productResult.IsSuccess)
            return BaseResult.Failure(productResult.Error);

        if (productResult.Value!.SellerId != sellerId)
            return BaseResult.Failure(Error.Forbidden());

        var result = await productRepository.DeleteAsync(productId);

        return result.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(result.Error);
    }

    public async Task<Result<ProductReadInfo>> GetByIdAsync(int id)
    {
        Result<Product> res = await productRepository.GetByIdAsync(id);
        if (!res.IsSuccess)
            return Result<ProductReadInfo>.Failure(res.Error);

        return Result<ProductReadInfo>.Success(res.Value!.ToRead());
    }
    
    public async Task<Result<PagedResponse<IEnumerable<ProductReadInfo>>>> GetAllAsync(ProductFilter filter)
    {
     
            Expression<Func<Product, bool>> filterExpression = product =>
                (filter.CategoryId == null || product.CategoryId == filter.CategoryId) &&
                (filter.SellerId == null || product.SellerId == filter.SellerId) &&
                (string.IsNullOrEmpty(filter.Name) || product.Name.ToLower().Contains(filter.Name.ToLower())) &&
                (filter.MinPrice == null || product.PricePerKg >= filter.MinPrice) &&
                (filter.MaxPrice == null || product.PricePerKg <= filter.MaxPrice) &&
                (filter.InStock == null || product.StockPerKg >= filter.InStock);

            var request = await  productRepository.Find(filterExpression);

            if (!request.IsSuccess)
                return Result<PagedResponse<IEnumerable<ProductReadInfo>>>.Failure(request.Error);

            List<ProductReadInfo> query = request.Value!
                .Select(p => p.ToRead()).ToList();

            int count = query.Count;

            IEnumerable<ProductReadInfo> products =
                query.Page(filter.PageNumber, filter.PageSize);

            PagedResponse<IEnumerable<ProductReadInfo>> response =
                PagedResponse<IEnumerable<ProductReadInfo>>
                    .Create(filter.PageNumber, filter.PageSize, count, products);

            return Result<PagedResponse<IEnumerable<ProductReadInfo>>>.Success(response);
    }
}
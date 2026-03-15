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

public class MarketService(IMarketRepository repository) : IMarketService
{
    public async Task<Result<PagedResponse<IEnumerable<MarketReadInfo>>>> GetAllAsync(MarketFilter filter)
    {
        Expression<Func<Market, bool>> expression = m =>
            (string.IsNullOrEmpty(filter.Name) || 
             m.Name.ToLower().Contains(filter.Name.ToLower()));

        var request = await repository.GetAllWithSellersAsync(expression);

        if (!request.IsSuccess)
            return Result<PagedResponse<IEnumerable<MarketReadInfo>>>
                .Failure(request.Error);

        var query = request.Value!
            .OrderBy(m => m.Name)
            .Select(m => m.ToRead())
            .ToList();

        int count = query.Count;

        var items = query.Page(filter.PageNumber, filter.PageSize);

        var response = PagedResponse<IEnumerable<MarketReadInfo>>
            .Create(filter.PageNumber, filter.PageSize, count, items);

        return Result<PagedResponse<IEnumerable<MarketReadInfo>>>
            .Success(response);
    }

    public async Task<Result<MarketReadInfo>> GetByIdAsync(int id)
    {
        Result<Market> res = await repository.GetByIdWithSellersAsync(id);
        if (!res.IsSuccess)
            return Result<MarketReadInfo>.Failure(res.Error);

        return Result<MarketReadInfo>.Success(res.Value!.ToRead());
    }

    public async Task<BaseResult> CreateAsync(MarketCreateInfo createInfo)
    {
        var exists = await repository.Find(m =>
            m.Name.ToLower() == createInfo.Name.ToLower());

        if (!exists.IsSuccess)
            return BaseResult.Failure(Error.Conflict("Market already exists"));

        Market market = createInfo.ToEntity();

        var res = await repository.AddAsync(market);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }


    public async Task<BaseResult> UpdateAsync(int id, MarketUpdateInfo updateInfo)
    {
        var marketRes = await repository.GetByIdAsync(id);
        if (!marketRes.IsSuccess)
            return BaseResult.Failure(marketRes.Error);

        var exists = await repository.Find(m =>
            m.Id != id &&
            m.Name.ToLower() == updateInfo.Name.ToLower());

        if (exists.IsSuccess)
            return BaseResult.Failure(
                Error.Conflict("Market with this name already exists"));

        Market market = marketRes.Value!;

        market.ToEntity(updateInfo);

        var res = await repository.UpdateAsync(market);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> DeleteAsync(int id)
    {
        var marketRes = await repository.GetByIdAsync(id);
        if (!marketRes.IsSuccess)
            return BaseResult.Failure(marketRes.Error);

        if (marketRes.Value!.Sellers.Any())
            return BaseResult.Failure(
                Error.BadRequest("Cannot delete market with sellers"));

        var res = await repository.DeleteAsync(id);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }
}
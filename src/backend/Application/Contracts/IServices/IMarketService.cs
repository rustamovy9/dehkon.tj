using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.IServices;

public interface IMarketService
{
    Task<Result<PagedResponse<IEnumerable<MarketReadInfo>>>> GetAllAsync(MarketFilter filter);
    Task<Result<MarketReadInfo>> GetByIdAsync(int id);
    Task<BaseResult> CreateAsync(MarketCreateInfo info);
    Task<BaseResult> UpdateAsync(int id, MarketUpdateInfo info);
    Task<BaseResult> DeleteAsync(int id);
}
using Domain.Common;

namespace Application.Filters;

public record ProductFilter(
    int? CategoryId,
    int? SellerId,
    int? MarketId,
    decimal? MinPrice,
    decimal? MaxPrice,
    decimal? InStock,
    string? Name) : BaseFilter;
using Domain.Common;

namespace Application.Filters;

public record ProductFilter(
    int? CategoryId,
    int? SellerId,
    decimal? MinPrice,
    decimal? MaxPrice,
    decimal? InStock,
    string? Name) : BaseFilter;
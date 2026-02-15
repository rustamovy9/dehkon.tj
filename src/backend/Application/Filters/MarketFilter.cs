using Domain.Common;

namespace Application.Filters;

public record MarketFilter(
    string? Name,
    string? Slug) : BaseFilter;
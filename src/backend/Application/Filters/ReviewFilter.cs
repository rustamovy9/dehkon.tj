using Domain.Common;

namespace Application.Filters;

public record ReviewFilter(
    int? ProductId,
    int? MinRating) : BaseFilter;
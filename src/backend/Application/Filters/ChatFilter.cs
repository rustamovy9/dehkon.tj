using Domain.Common;

namespace Application.Filters;

public record ChatFilter(
    int? UserId,
    bool? IsGlobal) : BaseFilter;
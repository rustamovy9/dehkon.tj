using Domain.Common;

namespace Application.Filters;

public record AnnouncementFilter(
    DateTimeOffset? CreatedFrom,
    DateTimeOffset? CreatedTo,
    int? CreatedBy) : BaseFilter;
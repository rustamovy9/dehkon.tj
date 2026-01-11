using Domain.Common;

namespace Application.Filters;

public record UserFilter(
    int? RoleId,
    string? UserName,
    string? Email,
    string? FullName,
    bool? IsActive) : BaseFilter;
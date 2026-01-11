using Domain.Common;

namespace Application.Filters;

public record CategoryFilter(
    string? Name): BaseFilter;
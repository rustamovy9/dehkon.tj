using Domain.Common;
using Domain.Enums;

namespace Application.Filters;

public record OrderFilter(
    OrderStatus? Status,
    int? BuyerId,
    int? SellerId,
    int? CourierId,
    DateTimeOffset? DateFrom,
    DateTimeOffset? DateTo) : BaseFilter;

using Domain.Enums;

namespace Application.DTO_s;

public record OrderCreateInfo(
    string? DeliveryAddress);

public record OrderDetailReadInfo(
    int Id,
    int BuyerId,
    int? CourierId,
    OrderStatus Status,
    decimal TotalPrice, 
    DateTimeOffset CreatedAt,
    IReadOnlyCollection<OrderItemReadInfo> Items);
    
public record OrderShortReadInfo(
    int Id,
    OrderStatus Status,
    decimal TotalPrice,
    DateTimeOffset CreatedAt
);

public record ChangeOrderStatusInfo(
    OrderStatus Status
);

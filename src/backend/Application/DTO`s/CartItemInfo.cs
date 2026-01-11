namespace Application.DTO_s;

public record CartItemReadInfo(
    int Id,
    decimal QuantityKg,
    decimal PriceAtMoment,
    decimal TotalPrice,
    int ProductId);

public record CartItemCreateInfo(
    int ProductId,
    decimal QuantityKg
);

public record CartItemUpdateInfo(
    int ProductId,
    decimal QuantityKg
);
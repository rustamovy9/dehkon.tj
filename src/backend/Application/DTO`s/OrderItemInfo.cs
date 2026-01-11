namespace Application.DTO_s;

public record OrderItemReadInfo(
    int Id,
    int ProductId,
    string ProductName,
    decimal QuantityKg,
    decimal PricePerKg,
    decimal TotalPrice);
namespace Application.DTO_s;

public record CartReadInfo(
    int Id,
    IReadOnlyCollection<CartItemReadInfo> Items,
    decimal TotalPrice
);
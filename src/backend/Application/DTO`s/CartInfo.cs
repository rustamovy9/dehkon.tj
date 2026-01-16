namespace Application.DTO_s;

public record CartReadInfo(
    int Id,
    ICollection<CartItemReadInfo> Items,
    decimal TotalPrice
);
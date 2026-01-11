namespace Application.DTO_s;

public interface IBaseProductInfo
{
    public string Name { get; init; }
    public string? ImageUrl { get; init; } 
    public decimal PricePerKg { get; init; }
    public decimal StockPerKg { get; init; }
}

public record ProductCreateInfo(
    string Name,
    string? ImageUrl,
    decimal PricePerKg,
    decimal StockPerKg,
    int CategoryId) : IBaseProductInfo;

public record ProductUpdateInfo(
    string Name,
    string? ImageUrl,
    decimal PricePerKg,
    decimal StockPerKg,
    int CategoryId) : IBaseProductInfo;

public record ProductReadInfo(
    int Id,
    string Name,
    string ImageUrl,
    decimal PricePerKg,
    decimal StockPerKg,
    int SellerId,
    int CategoryId) : IBaseProductInfo;
    
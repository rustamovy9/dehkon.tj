namespace Application.DTO_s;

public abstract record MarketCreateInfo(
    string Name,
    string Slug,
    string Address,
    double? Latitude,
    double? Longitude
);


public record MarketUpdateInfo(
    string Name,
    string Slug,
    string Address,
    double? Latitude,
    double? Longitude
);

public record MarketReadInfo(
    int Id,
    string Name,
    string Slug,
    string Address,
    double? Latitude,
    double? Longitude,
    int SellersCount
);
using Application.Contracts.IServices;
using Application.DTO_s;
using Domain.Constants;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class MarketMap
{
    public static MarketReadInfo ToRead(this Market entity)
        => new MarketReadInfo(
            Id: entity.Id,
            Name: entity.Name,
            Slug: entity.Slug,
            Address: entity.Address,
            Latitude: entity.Latitude,
            Longitude: entity.Longitude,
            SellersCount: entity.Sellers?.Count ?? 0);

    public static Market ToEntity(this MarketCreateInfo createInfo)
    { 
        return new Market
        {
            Name = createInfo.Name,
            Slug = GenerateSlug(createInfo.Slug),
            Address = createInfo.Address,
            Latitude = createInfo.Latitude,
            Longitude = createInfo.Longitude
        };
    }

    public static Market ToEntity(this Market entity, MarketUpdateInfo updateInfo)
    {
        entity.Name = updateInfo.Name;
        entity.Slug = GenerateSlug(updateInfo.Slug);
        entity.Address = updateInfo.Address;
        entity.Latitude = updateInfo.Latitude;
        entity.Longitude = updateInfo.Longitude;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        return entity;
    }
    
    private static string GenerateSlug(string name)
        => name.Trim().ToLower().Replace(" ", "-");

}
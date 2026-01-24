using Application.Contracts.IServices;
using Application.DTO_s;
using Domain.Constants;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class ProductMap
{
    public static ProductReadInfo ToRead(this Product entity)
        => new ProductReadInfo(
            Id: entity.Id,
            Name: entity.Name,
            ImageUrl: entity.ImageUrl,
            PricePerKg: entity.PricePerKg,
            StockPerKg: entity.StockPerKg,
            SellerId: entity.SellerId,
            CategoryId: entity.CategoryId);

    public static async Task<Product> ToEntity(this ProductCreateInfo createInfo, IFileService fileService,int sellerId)
    {
        string? imagePath = FileData.Default;
        if (createInfo.ImageUrl is not null)
            imagePath = await fileService.CreateFile(createInfo.ImageUrl, MediaFolders.Images);

        return new Product
        {
            Name = createInfo.Name,
            PricePerKg = createInfo.PricePerKg,
            ImageUrl = imagePath,
            StockPerKg = createInfo.StockPerKg,
            SellerId = sellerId,
            CategoryId = createInfo.CategoryId
        };
    }

    public static async Task<Product> ToEntity(this Product entity, ProductUpdateInfo updateInfo, IFileService fileService,
        int sellerId)
    {
        if (updateInfo.ImageUrl is not null)
        {
            fileService.DeleteFile(entity.ImageUrl, MediaFolders.Images);

            entity.ImageUrl = await fileService.CreateFile(updateInfo.ImageUrl,MediaFolders.Images);
        }

        entity.Name = updateInfo.Name;
        entity.PricePerKg = updateInfo.PricePerKg;
        entity.StockPerKg = updateInfo.StockPerKg;
        entity.CategoryId = updateInfo.CategoryId;
        entity.SellerId = sellerId;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        return entity;
    }

}
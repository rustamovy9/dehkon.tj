using Application.Contracts.IServices;
using Application.DTO_s;
using Domain.Constants;
using Domain.Entities;
using Microsoft.Extensions.FileProviders;

namespace Application.Extensions.Mappers;

public static class CategoryMap
{
    public static async Task<Category> ToEntity(this CategoryCreateInfo createInfo,IFileService fileService)
    {
        string? imagePath = FileData.Default;
        if (createInfo.ImageUrl is not null)
            imagePath = await fileService.CreateFile(createInfo.ImageUrl, MediaFolders.Images);

        return new()
        {
            Name = createInfo.Name,
            ImageUrl = imagePath
        };
    }

    public static async Task<Category> ToEntity(this Category entity, CategoryUpdateInfo updateInfo,IFileService fileService)
    {
        if (updateInfo.ImageUrl is not null)
        {
            fileService.DeleteFile(entity.ImageUrl, MediaFolders.Images);

            entity.ImageUrl = await fileService.CreateFile(updateInfo.ImageUrl,MediaFolders.Images);
        }
        entity.Name = updateInfo.Name;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        return entity;
    }

    public static CategoryReadInfo ToRead(this Category entity)
        => new(entity.Id, entity.Name);
}
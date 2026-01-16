using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class CategoryMap
{
    public static Category ToEntity(this CategoryCreateInfo createInfo)
        => new() { Name = createInfo.Name };

    public static Category ToEntity(this Category entity, CategoryUpdateInfo updateInfo)
    {
        entity.Name = updateInfo.Name;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.Now;
        return entity;
    }

    public static CategoryReadInfo ToRead(this Category entity)
        => new(entity.Id, entity.Name);
}
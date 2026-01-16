using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class ReviewMap
{
    public static Review ToEntity(this ReviewCreateInfo createInfo, int userId)
        => new()
        {
            ProductId = createInfo.ProductId,
            UserId = userId,
            Rating = createInfo.Rating,
            Comment = createInfo.Comment
        };

    public static ReviewReadInfo ToRead(this Review entity)
        => new(
            entity.Id,
            entity.ProductId,
            entity.UserId,
            entity.User.UserName,
            entity.Rating,
            entity.Comment,
            entity.CreatedAt);

}
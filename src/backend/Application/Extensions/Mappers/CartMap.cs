using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class CartMap
{
    public static CartReadInfo ToReadInfo(this Cart entity)
        => new CartReadInfo(
            entity.Id,
            entity.CartItems.Select(i => i.ToRead()).ToList(),
            entity.CartItems.Sum(i => i.TotalPrice));
}
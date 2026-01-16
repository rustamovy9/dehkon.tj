using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class CartItemMap
{
    public static CartItem ToEntity(this CartItemCreateInfo createInfo, int cartId, decimal priceAtMoment)
        => new()
        {
            CartId = cartId,
            ProductId = createInfo.ProductId,
            QuantityKg = createInfo.QuantityKg,
            PriceAtMoment = priceAtMoment
        };

    public static CartItemReadInfo ToRead(this CartItem item)
        => new CartItemReadInfo(
            item.Id,
            item.QuantityKg,
            item.PriceAtMoment,
            item.TotalPrice,
            item.ProductId);
}
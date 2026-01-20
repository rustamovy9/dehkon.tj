using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class OrderMap
{
    public static Order ToEntity(this OrderCreateInfo createInfo, int buyerId)
        => new()
        {
            BuyerId = buyerId,
            DeliveryAddress = createInfo.DeliveryAddress!
        };

    public static OrderDetailReadInfo ToReadDetail(this Order order)
        => new(
            order.Id,
            order.BuyerId,
            order.CourierId,
            order.Status,
            order.TotalPrice,
            order.CreatedAt,
            order.OrderItems.Select(i=>i.ToRead()).ToList()); 
    
    public static OrderShortReadInfo ToReadShort(this Order order)
        => new(
            order.Id,
            order.Status,
            order.TotalPrice,
            order.CreatedAt);
}
using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class OrderItemMap
{
    public static OrderItemReadInfo ToRead(this OrderItem entity)
        => new(
            entity.Id,
            entity.ProductId,
            entity.Product.Name,
            entity.QuantityKg,
            entity.PricePerKg,
            entity.TotalPrice);
    
    
}
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public int BuyerId { get; set; }
    public int CourierId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Created;
    public decimal TotalPrice { get; set; }
    public string DeliveryAddress { get; set; } = null!;
    
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public int BuyerId { get; set; }
    public int? CourierId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Created;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public DateTimeOffset PaidAt { get; set; }
    public decimal TotalPrice { get; set; }
    public string DeliveryAddress { get; set; } = null!;
    public DateTimeOffset DeliveredAt { get; set; }
    public User Buyer { get; set; } = null!;

    public User? Courier { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
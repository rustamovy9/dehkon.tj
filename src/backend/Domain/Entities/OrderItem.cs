using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal QuantityKg { get; set; }
    public decimal PricePerKg { get; set; }

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
    
    [NotMapped]
    public decimal TotalPrice => QuantityKg * PricePerKg;
}
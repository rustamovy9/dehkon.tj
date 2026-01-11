using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class CartItem : BaseEntity
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public decimal QuantityKg { get; set; }
    public decimal PriceAtMoment { get; set; }

    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
    
    [NotMapped]
    public decimal TotalPrice => QuantityKg * PriceAtMoment;
}
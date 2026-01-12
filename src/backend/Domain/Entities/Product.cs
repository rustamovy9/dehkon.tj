using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = FileData.Default;
    public decimal PricePerKg { get; set; }
    public decimal StockPerKg { get; set; }

    public int SellerId { get; set; }
    public int  CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public User Seller { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public ICollection<CartItem> CartItems { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
}
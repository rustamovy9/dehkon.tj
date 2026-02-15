using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = FileData.Default;
    
    public ICollection<Product> Products { get; set; } = [];
}
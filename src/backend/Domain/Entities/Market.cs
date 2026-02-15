using Domain.Common;

namespace Domain.Entities;

public class Market : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Address { get; set; } = null!;

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public ICollection<User> Sellers { get; set; } = [];
}
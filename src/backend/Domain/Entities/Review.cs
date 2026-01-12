using Domain.Common;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; } = null!;

    public User User { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
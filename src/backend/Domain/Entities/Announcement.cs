using Domain.Common;

namespace Domain.Entities;

public class Announcement : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    
    public int UserId { get; set; }
    public User? User { get; set; }
}
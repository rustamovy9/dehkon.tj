using Domain.Common;

namespace Domain.Entities;

public class Message : BaseEntity
{
    public int ChatId { get; set; }
    public int SenderId { get; set; }
    public string Text { get; set; } = null!;
    public bool IsRead { get; set; }

    public Chat Chat { get; set; } = null!;
    public User Sender { get; set; } = null!;
}
using Domain.Common;

namespace Domain.Entities;

public class ChatUser : BaseEntity
{
    public int ChatId { get; set; }
    public Chat? Chat { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}
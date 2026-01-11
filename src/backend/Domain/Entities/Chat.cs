using Domain.Common;

namespace Domain.Entities;

public class Chat : BaseEntity
{
    public bool IsGlobal { get; set; }

    public ICollection<ChatUser> ChatUsers { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}
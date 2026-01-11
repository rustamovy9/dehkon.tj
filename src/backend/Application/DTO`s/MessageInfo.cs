namespace Application.DTO_s;

public record MessageReadInfo(
    int Id,
    int SenderId,
    string SenderUserName,
    string Text,
    DateTimeOffset SentAt,
    bool IsRead);
    
public record MessageCreateInfo(
    int ChatId,
    string Text);
    

using Domain.Entities;

namespace Application.DTO_s;

public record ChatReadInfo(
    int Id,
    bool IsGlobal,
    IReadOnlyCollection<ChatUserReadInfo> Participants,
    IReadOnlyCollection<MessageReadInfo> Messages);
    
public record ChatShortReadInfo(
    int Id,
    bool IsGlobal,
    string Title,
    DateTimeOffset LastMessageAt
);

public record PrivateChatCreateInfo(
    int UserId);
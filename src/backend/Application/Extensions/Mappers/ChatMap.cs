using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class ChatMap
{
    public static ChatReadInfo ToRead(this Chat entity)
        => new(
            entity.Id,
            entity.IsGlobal,
            entity.IsGlobal
                ? []
                : entity.ChatUsers?.Select(cu => cu.ToRead()).ToList()!,
            entity.Messages?.OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault()
                ?.ToRead());

    public static Chat ToPrivateChat(int userId, PrivateChatCreateInfo privateChatCreateInfo)
    {
        return new Chat
        {
            IsGlobal = false,
            ChatUsers =
            [
                new ChatUser { UserId = userId },
                new ChatUser { UserId = privateChatCreateInfo.OtherUserId }
            ]
        };
    }
}
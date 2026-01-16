using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class MessageMap
{
    public static Message ToEntity(this MessageCreateInfo createInfo, int senderId)
        => new()
        {
            ChatId = createInfo.ChatId,
            SenderId = senderId,
            Text = createInfo.Text
        };

    public static MessageReadInfo ToRead(this Message entity)
        => new(
            entity.Id,
            entity.SenderId,
            entity.Sender.UserName,
            entity.Text,
            entity.CreatedAt,
            entity.IsRead);
}
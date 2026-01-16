using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class ChatUserMap
{
    public static ChatUserReadInfo ToRead(this ChatUser entity)
        => new(
            entity.UserId,
            entity.User!.UserName);
}
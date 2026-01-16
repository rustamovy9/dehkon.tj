using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class ChatMap
{
    public static ChatReadInfo ToRead(this Chat entity)
        => new(
            entity.Id,
            entity.IsGlobal,
            entity.ChatUsers.Select(cu=>cu.ToRead()).ToList(),
            entity.Messages.MaxBy(m=>m.CreatedAt)
                ?.ToRead());
}
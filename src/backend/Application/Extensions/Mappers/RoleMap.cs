using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class RoleMap
{
    public static RoleReadInfo ToRead(this Role role)
        => new RoleReadInfo
        (
            Id: role.Id,
            Name : role.Name,
            Description: role.Description
        );

    public static Role ToEntity(this RoleCreateInfo roleCreate)
        => new Role()
        {
            Name = roleCreate.Name,
            Description = roleCreate.Description,
        };

    public static Role ToEntity(this Role role, RoleUpdateInfo updateInfo)
    {
        role.Description = updateInfo.Description;
        role.Name = updateInfo.Name;
        role.Version++;
        role.UpdatedAt = DateTimeOffset.UtcNow;
        return role;
    }
}
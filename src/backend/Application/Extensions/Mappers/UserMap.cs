using Application.Contracts.IServices;
using Application.DTO_s;
using Domain.Constants;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class UserMap
{
    public static UserReadInfo ToRead(this User user)
            => new UserReadInfo(
                user.Id,
                user.UserName,
                user.Email,
                user.FullName,
                user.PhoneNumber,
                user.ProfilePhotoUrl,
                user.Role.Name);

    
    public static  async Task<User> ToEntity(this User entity, UserUpdateInfo updateInfo, IFileService fileService)
    {
        if (updateInfo.ProfilePhotoUrl is not null && entity?.ProfilePhotoUrl is not null)
        {
            fileService.DeleteFile(entity.ProfilePhotoUrl, MediaFolders.Images);

            entity.ProfilePhotoUrl = await fileService.CreateFile(updateInfo.ProfilePhotoUrl, MediaFolders.Images);
        }
        entity!.UserName = updateInfo.UserName;
        entity.FullName = updateInfo.FullName;
        entity.PhoneNumber = updateInfo.PhoneNumber!;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        return entity;
    }
}
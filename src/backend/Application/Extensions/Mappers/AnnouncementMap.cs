using Application.DTO_s;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class AnnouncementMap
{
    public static AnnouncementReadInfo ToRead(this Announcement announcement)
        => new AnnouncementReadInfo(
            Id: announcement.Id,
            Title: announcement.Title, 
            Content: announcement.Content);
    
    
    public static  Announcement ToEntity(this AnnouncementCreateInfo createInfo,int userId)
        => new Announcement()
        {
            Title = createInfo.Title,
            Content = createInfo.Content,
            UserId = userId
        };
    
    public static  Announcement ToEntity(this Announcement entity, AnnouncementUpdateInfo updateInfo)
    {
        entity.Title = updateInfo.Title;
        entity.Content = updateInfo.Content;
        entity.Version++;
        entity.UpdatedAt = DateTimeOffset.Now;
        return entity;
    }

}
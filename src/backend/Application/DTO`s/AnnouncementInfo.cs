namespace Application.DTO_s;

public record AnnouncementReadInfo(
    int Id,
    string Title,
    string Content);

public record AnnouncementCreateInfo(
    string Title,
    string Content);

public record AnnouncementUpdateInfo(
    string Title,
    string Content);

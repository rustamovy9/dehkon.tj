namespace Application.DTO_s;


public record ReviewReadInfo(
    int Id,
    int ProductId,
    string ProductName,
    int UserId,
    string UserName,
    int Rating,
    string? Comment,
    DateTimeOffset CreatedAt);

public record ReviewCreateInfo(
    int ProductId,
    int Rating,
    string? Comment);


public record ReviewUpdateInfo(
    int Rating,
    string? Comment);
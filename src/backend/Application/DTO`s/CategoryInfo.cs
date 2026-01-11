namespace Application.DTO_s;

public record CategoryReadInfo(
    int Id,
    string Name);

public record CategoryCreateInfo(
    string Name);

public record CategoryUpdateInfo(
    string Name);
    

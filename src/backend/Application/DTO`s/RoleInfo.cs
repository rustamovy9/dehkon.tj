namespace Application.DTO_s;

public interface IBaseRoleInfo
{
    public string Name { get; init; } 
    public string? Description { get; init; }
}

public record  RoleReadInfo(
    int Id,
    string Name,
    string? Description) : IBaseRoleInfo;
    
public  record  RoleCreateInfo(
    string Name,
    string? Description) : IBaseRoleInfo;

public  record  RoleUpdateInfo(
    string Name,
    string? Description) : IBaseRoleInfo;
    
    